from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import List
from app.search_engine import VisualSearchEngine

app = FastAPI(title="Smart Wardrobe Visual Search Service", version="1.0.0")
engine = VisualSearchEngine()

class IndexItem(BaseModel):
    outfit_id: int
    item_id: int
    image_url: str

class SearchRequest(BaseModel):
    image_url: str
    top_k: int = 6

@app.post("/index-outfits")
async def index_outfits(items: List[IndexItem]):
    """Ендпоінт для формування/оновлення матриці схожості речей з аутфітів"""
    print("\n[ML AUDIT] Отримано запит на індексацію речей від C# API")
    print(f"Кількість елементів в отриманому пакеті: {len(items)}")

    input_data = [item.model_dump() for item in items]

    # Виведемо перші 3 елементи для перевірки структури та ID
    print("Приклад перших елементів для перевірки відповідності ідентифікаторів:")
    for i, item in enumerate(input_data[:3]):
        print(f"   [{i+1}] Outfit ID: {item['outfit_id']} | Item ID: {item['item_id']} | URL: {item['image_url'][:60]}...")

    success = engine.rebuild_index(input_data)
    if not success:
        print("[ML ERROR] Помилка формування індексу. Можливо, жодне зображення не завантажилось.")
        raise HTTPException(status_code=400, detail="Failed to process image data for indexing")

    return {"status": "success", "message": f"Successfully indexed {len(input_data)} items"}

@app.post("/search-similar-outfits")
async def search_similar_outfits(request: SearchRequest):
    """Ендпоінт, що приймає URL речі та повертає список схожих образів"""
    print(f"\n[ML SEARCH REQUEST] Пошук схожих до образу за URL: {request.image_url}")

    if engine.embeddings is None:
        print("[ML WARNING] Спроба пошуку при порожньому індексі матриці (.npy файли відсутні)")
        raise HTTPException(status_code=400, detail="Search index is empty. Please run indexation first.")

    matches = engine.search_similar(request.image_url, top_k=request.top_k)
    print(f"[ML RESPONSE] Знайдено та відправлено назад в C# усього збігів: {len(matches)}")
    return {"status": "success", "data": matches}

@app.delete("/delete-outfit/{outfit_id}")
async def delete_outfit_from_index(outfit_id: int):
    """Ендпоінт для видалення аутфіту з індексу ШІ при видаленні публікації в C#"""
    success = engine.remove_from_index(outfit_id=outfit_id)
    if not success:
        return {"status": "ignored", "message": f"Outfit {outfit_id} was not in index or already removed"}
    return {"status": "success", "message": f"Successfully removed outfit {outfit_id} from visual index"}