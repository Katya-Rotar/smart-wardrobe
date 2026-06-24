from fastapi import FastAPI, UploadFile, File
import tensorflow as tf
import numpy as np
import pickle
import cv2
from PIL import Image
import io
from sklearn.cluster import KMeans

app = FastAPI()

# --- 1. Model Loader ---
model = tf.keras.models.load_model("smart_wardrobe_model.h5")

with open("wardrobe_encoders.pkl", "rb") as f:
    encoders = pickle.load(f)

IMG_SIZE = 224

# --- 2. Season Mapping Logic ---
# Словник для автоматичного визначення сезону на основі підкатегорії
SEASON_MAPPING = {
    # Літо
    'Shorts': 'Summer', 'Tank': 'Summer', 'Skirt': 'Summer',
    'Tee': 'Summer', 'Top': 'Summer', 'Blouse': 'Summer',

    # Зима
    'Coat': 'Winter', 'Parka': 'Winter', 'Peacoat': 'Winter',
    'Anorak': 'Winter', 'Turtleneck': 'Winter',

    # Демісезон (Весна/Осінь)
    'Jacket': 'Demi-season', 'Cardigan': 'Demi-season', 'Sweater': 'Demi-season',
    'Hoodie': 'Demi-season', 'Blazer': 'Demi-season', 'Bomber': 'Demi-season',
    'Flannel': 'Demi-season',

    # Всесезонні речі
    'Jeans': 'All-season', 'Jeggings': 'All-season', 'Leggings': 'All-season',
    'Jumpsuit': 'All-season', 'Dress': 'All-season', 'Sweatpants': 'All-season',
    'Chinos': 'All-season', 'Culottes': 'All-season'
}

def get_dominant_color(image_bytes, k=3):
    nparr = np.frombuffer(image_bytes, np.uint8)
    img = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    img = cv2.resize(img, (100, 100))
    pixels = img.reshape((-1, 3))
    clt = KMeans(n_clusters=k, n_init=10)
    clt.fit(pixels)
    labels, counts = np.unique(clt.labels_, return_counts=True)
    dominant_color = clt.cluster_centers_[np.argmax(counts)]
    return '#{:02x}{:02x}{:02x}'.format(int(dominant_color[0]), int(dominant_color[1]), int(dominant_color[2]))

@app.post("/classify")
async def classify_clothing(file: UploadFile = File(...)):
    contents = await file.read()

    # --- Preprocessing ---
    image = Image.open(io.BytesIO(contents)).convert('RGB')
    image_input = image.resize((IMG_SIZE, IMG_SIZE))
    img_array = np.array(image_input) / 255.0
    img_array = np.expand_dims(img_array, axis=0)

    # --- Inference ---
    predictions = model.predict(img_array)
    main_idx = np.argmax(predictions[0])
    sub_idx = np.argmax(predictions[1])

    # Декодування
    main_name = encoders["Main_Group"].inverse_transform([main_idx])[0]
    sub_name = encoders["Sub_Category"].inverse_transform([sub_idx])[0]

    # Логіка визначення сезону
    detected_season = SEASON_MAPPING.get(sub_name, 'All-season')

    # Логіка визначення стилю (usage) на основі категорії
    # Додамо просту мапу для стилю, щоб фронтенд міг його підтягнути
    STYLE_MAPPING = {
        'Tee': 'Casual', 'Jeans': 'Casual', 'Hoodie': 'Casual',
        'Blazer': 'Formal', 'Dress': 'Formal', 'Suit': 'Formal',
        'Sweatpants': 'Sports', 'Shorts': 'Sports'
    }
    detected_usage = STYLE_MAPPING.get(sub_name, 'Casual')

    # Визначення кольору
    hex_color = get_dominant_color(contents)

    # --- Response (Форматуємо під React-фронтенд) ---
    return {
        "subCategory": main_name,      # Буде замаплено на Category в React
        "articleType": sub_name,       # Буде замаплено на Type та Name в React
        "season": detected_season,     # Буде замаплено на Season
        "baseColour": hex_color,       # Буде замаплено на Color
        "usage": detected_usage,       # Буде замаплено на Style
        "confidence": {
            "main": round(float(np.max(predictions[0])), 4),
            "sub": round(float(np.max(predictions[1])), 4)
        }
    }