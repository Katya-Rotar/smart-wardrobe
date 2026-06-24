from fastapi import FastAPI, Body
from typing import List, Dict, Optional
import uvicorn

app = FastAPI(title="SmartWardrobe ML Service")

CATEGORIES = {
    "LIGHT_TOPS": [2, 3, 5, 10, 22, 25],
    "SWEATERS_HOODIES": [7, 8, 15],
    "OUTERWEAR": [9, 11, 17, 18, 23, 24, 26],
    "SUMMER_BOTTOMS": [4, 6],
    "WINTER_BOTTOMS": [14, 16, 19],
    "UNIVERSAL_BOTTOMS": [12, 20, 21],
    "FULL_BODY": [1, 13],
    "SHOES_WARM": [31, 29, 30],
    "SHOES_COLD": [28],
    "SHOES_UNIVERSAL": [27, 32]
}

W_SEASON = 0.10
W_WEATHER = 0.70
W_STYLE = 0.20

# Додатковий бонус (не входив у Grid Search, але потрібен для бізнес-логіки)
W_RAIN_BONUS = 0.15

def get_weather_config(temp: float):
    """Визначає цільові та небажані категорії залежно від температури"""
    if temp >= 25:
        return {
            "target": ["SUMMER_BOTTOMS", "LIGHT_TOPS", "SHOES_WARM"],
            "avoid": ["OUTERWEAR", "SWEATERS_HOODIES", "WINTER_BOTTOMS", "SHOES_COLD"]
        }
    elif temp >= 15:
        return {
            "target": ["LIGHT_TOPS", "UNIVERSAL_BOTTOMS", "SHOES_UNIVERSAL"],
            "avoid": ["OUTERWEAR", "SHOES_COLD"]
        }
    elif temp >= 5:
        return {
            "target": ["SWEATERS_HOODIES", "UNIVERSAL_BOTTOMS", "SHOES_UNIVERSAL", "OUTERWEAR"],
            "avoid": ["SUMMER_BOTTOMS", "SHOES_WARM"]
        }
    else:
        return {
            "target": ["OUTERWEAR", "WINTER_BOTTOMS", "SHOES_COLD"],
            "avoid": ["SUMMER_BOTTOMS", "SHOES_WARM", "LIGHT_TOPS"]
        }

def calculate_item_score(item: Dict, context: Dict, cfg: Dict) -> float:
    score = 0.0
    type_id = item.get('typeID')

    # 1. СЕЗОН (Оновлена вага 0.10)
    if context['season_id'] in item.get('seasonIds', []):
        score += W_SEASON

    # 2. ТЕМПЕРАТУРА ТА КАТЕГОРІЇ (Оновлена вага 0.70)
    for cat, ids in CATEGORIES.items():
        if type_id in ids:
            # Якщо річ ідеально підходить під поточну погоду -> додаємо вагу погоди
            if cat in cfg['target']:
                score += W_WEATHER

            # М'який штраф: якщо річ не підходить, віднімаємо повну вагу погоди, 
            # щоб вона гарантовано опустилася вниз списку
            if cat in cfg['avoid']:
                score -= W_WEATHER

    # 3. СТИЛЬ (Оновлена вага 0.20)
    if context['style_id'] in item.get('styleIds', []):
        score += W_STYLE

    # 4. ДОДАТКОВІ БОНУСИ (Дощ)
    if context['is_raining'] and type_id in CATEGORIES["OUTERWEAR"]:
        score += W_RAIN_BONUS

    return round(score, 3)

@app.post("/recommend")
async def recommend(payload: Dict = Body(...)):
    items = payload.get("items", [])
    temp = payload.get("temperature", 20.0)
    weather_code = payload.get("weatherCode", 0)

    cfg = get_weather_config(temp)

    context = {
        "is_raining": weather_code >= 51,
        "season_id": payload.get("seasonId") or 1,
        "style_id": payload.get("styleId", 1)
    }

    scored_items = []
    for item in items:
        final_score = calculate_item_score(item, context, cfg)
        scored_items.append({
            "id": item.get("id"),
            "score": final_score
        })

    # Сортуємо: від найкращих до гірших
    return sorted(scored_items, key=lambda x: x['score'], reverse=True)

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8002)