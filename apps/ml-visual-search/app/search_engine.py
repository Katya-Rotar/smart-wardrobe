import numpy as np
import requests
from io import BytesIO
from PIL import Image
from tensorflow.keras.preprocessing import image
from tensorflow.keras.applications.resnet50 import ResNet50, preprocess_input as preprocess_resnet
from tensorflow.keras.models import Model
from sklearn.metrics.pairwise import cosine_similarity
import os

class VisualSearchEngine:
    def __init__(self):
        print("Ініціалізація моделі комп'ютерного зору ResNet-50 (ImageNet)...")
        base_resnet = ResNet50(weights="imagenet", include_top=False, pooling="avg")
        self.model = Model(inputs=base_resnet.input, outputs=base_resnet.output)

        self.embeddings_path = "models/outfits_resnet_embeddings.npy"
        self.metadata_path = "models/outfits_metadata.npy"

        self.embeddings = None
        self.metadata = []
        self.load_matrices()

    def load_matrices(self):
        if os.path.exists(self.embeddings_path) and os.path.exists(self.metadata_path):
            try:
                self.embeddings = np.load(self.embeddings_path)
                self.metadata = np.load(self.metadata_path, allow_pickle=True).tolist()
                print(f"Успішно завантажено матриці з диска. Кількість речей в індексі: {len(self.metadata)}")
            except Exception as e:
                print(f"Помилка завантаження матриць з диска: {e}. Ініціалізуємо новий індекс.")
                self.embeddings = None
                self.metadata = []
        else:
            print("Файли індексів .npy не знайдені на диску. Очікується первинна синхронізація.")

    def _extract_from_url(self, url: str):
        try:
            response = requests.get(url, timeout=5)
            if response.status_code != 200:
                print(f"[HTTP ERROR] Не вдалося скачати картинку. Статус: {response.status_code} для URL: {url}")
                return None

            img = Image.open(BytesIO(response.content)).convert("RGB")
            img = img.resize((224, 224))
            x = image.img_to_array(img)
            x = np.expand_dims(x, axis=0)
            x = preprocess_resnet(x)

            feat = self.model.predict(x, verbose=0)
            return feat.flatten()
        except Exception as e:
            print(f"[EXTRACTION ERROR] Помилка обробки зображення за адресою {url}: {e}")
            return None

    def rebuild_index(self, items_data: list):
        if self.embeddings is None:
            self.load_matrices()

        current_embeddings = list(self.embeddings) if self.embeddings is not None else []
        current_metadata = list(self.metadata) if self.metadata else []

        print(f"\n[INDEXING] Обробка пакета речей. Поточний розмір бази ШІ: {len(current_metadata)}")
        has_changes = False

        for data in items_data:
            outfit_id = data["outfit_id"]
            item_id = data["item_id"]
            url = data["image_url"]

            existing_idx = next((i for i, meta in enumerate(current_metadata)
                                 if meta["outfit_id"] == outfit_id and meta["item_id"] == item_id), None)

            vec = self._extract_from_url(url)
            if vec is not None:
                if existing_idx is not None:
                    print(f"   Оновлення існуючої речі: ItemID {item_id} в OutfitID {outfit_id}")
                    current_embeddings[existing_idx] = vec
                else:
                    current_embeddings.append(vec)
                    current_metadata.append({"outfit_id": outfit_id, "item_id": item_id})
                has_changes = True

        if has_changes:
            self.embeddings = np.array(current_embeddings)
            self.metadata = current_metadata

            os.makedirs("models", exist_ok=True)
            np.save(self.embeddings_path, self.embeddings)
            np.save(self.metadata_path, np.array(self.metadata, dtype=object))
            print(f"[SAVED] Базу ШІ успішно оновлено! Усього об'єктів в індексі: {len(self.metadata)}")
            return True
        return False

    def remove_from_index(self, outfit_id: int, item_id: int = None):
        if self.embeddings is None or len(self.metadata) == 0:
            return False

        current_metadata = list(self.metadata)
        current_embeddings = list(self.embeddings)

        indices_to_remove = []
        for i, meta in enumerate(current_metadata):
            if item_id is not None:
                if meta["outfit_id"] == outfit_id and meta["item_id"] == item_id:
                    indices_to_remove.append(i)
            else:
                if meta["outfit_id"] == outfit_id:
                    indices_to_remove.append(i)

        if not indices_to_remove:
            return False

        for idx in sorted(indices_to_remove, reverse=True):
            current_metadata.pop(idx)
            current_embeddings.pop(idx)

        print(f"[REMOVE] Видалено {len(indices_to_remove)} елементів з індексу ШІ.")

        if current_metadata:
            self.embeddings = np.array(current_embeddings)
            self.metadata = current_metadata
            np.save(self.embeddings_path, self.embeddings)
            np.save(self.metadata_path, np.array(self.metadata, dtype=object))
        else:
            self.embeddings = None
            self.metadata = []
            if os.path.exists(self.embeddings_path): os.remove(self.embeddings_path)
            if os.path.exists(self.metadata_path): os.remove(self.metadata_path)
        return True

    def search_similar(self, query_image_url: str, top_k: int = 6):
        if self.embeddings is None or len(self.metadata) == 0:
            return []

        query_vec = self._extract_from_url(query_image_url)
        if query_vec is None:
            return []

        query_vec_norm = query_vec / np.linalg.norm(query_vec)
        matrix_norms = np.linalg.norm(self.embeddings, axis=1, keepdims=True)
        embeddings_norm = self.embeddings / matrix_norms

        sims = cosine_similarity([query_vec_norm], embeddings_norm)[0]
        top_indices = sims.argsort()[::-1][:len(self.metadata)]

        results = []
        seen_outfits = set()

        for idx in top_indices:
            outfit_id = int(self.metadata[idx]["outfit_id"])
            item_id = int(self.metadata[idx]["item_id"])
            confidence = float(sims[idx])

            if outfit_id in seen_outfits:
                continue

            seen_outfits.add(outfit_id)
            results.append({
                "outfitId": outfit_id,
                "similarItemId": item_id,
                "confidence": confidence
            })

            if len(results) == top_k:
                break
        return results