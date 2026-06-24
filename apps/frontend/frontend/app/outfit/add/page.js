"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import api from "@/api/api";
import "./CreateOutfitPage.css";

export default function CreateOutfitPage({ userId, outfitId }) {
    const router = useRouter();

    const [formData, setFormData] = useState({
        userID: userId,
        temperatureSuitabilityID: 1,
        tagIDs: [],
        styleIDs: [],
        seasonIDs: [],
        clothingItemIDs: [],
    });

    // --- AI ТА РЕКОМЕНДАЦІЇ ---
    const [recommendedIds, setRecommendedIds] = useState([]);
    const [isMlLoading, setIsMlLoading] = useState(false);
    // Окремий фільтр стилю спеціально для AI (за замовчуванням 1 - Casual)
    const [aiStyleFilter, setAiStyleFilter] = useState(1);

    // Списки для вибору
    const [stylesList, setStyles] = useState([]);
    const [seasons, setSeasons] = useState([]);
    const [temps, setTemps] = useState([]);
    const [tags, setTags] = useState([]);
    const [clothingItems, setClothingItems] = useState([]);
    const [groupedData, setGroupedData] = useState({});

    // --- ФУНКЦІЯ ЗАПИТУ ДО ML ЧЕРЕЗ БЕКЕНД ---
    const getSmartRecommendations = async () => {
        setIsMlLoading(true);
        try {
            const savedWeather = localStorage.getItem("lastWeatherData");
            const weather = savedWeather
                ? JSON.parse(savedWeather)
                : { temp: 20, code: 0 };

            const res = await api.get("/ClothingItem/generate", {
                params: {
                    temp: weather.temp,
                    weatherCode: weather.code,
                    styleId: aiStyleFilter // Використовуємо наш AI фільтр
                }
            });

            const ids = Array.isArray(res.data)
                ? res.data.map(item => Number(item.id || item))
                : [];

            setRecommendedIds(ids);
        } catch (err) {
            console.error("ML Recommendations Error:", err);
        } finally {
            setIsMlLoading(false);
        }
    };

    // --- ПОЧАТКОВЕ ЗАВАНТАЖЕННЯ ДАНИХ ---
    useEffect(() => {
        async function fetchData() {
            try {
                const [tagRes, styleRes, seasonRes, tempRes, groupedRes] =
                    await Promise.all([
                        api.get("/Tag"),
                        api.get("/Style"),
                        api.get("/Season"),
                        api.get("/TemperatureSuitability"),
                        api.get("/ClothingItem/grouped"), // Твій ендпоінт
                    ]);

                setTags(tagRes.data);
                setStyles(styleRes.data);
                setSeasons(seasonRes.data);
                setTemps(tempRes.data);

                // 1. Зберігаємо для рендеру підкатегорій (те, що викликало помилку)
                setGroupedData(groupedRes.data || {});

                // 2. Робимо "плоский" масив для роботи AI та вибраних речей
                const allItems = Object.values(groupedRes.data || {})
                    .flat()
                    .map(i => ({ ...i, id: Number(i.id) }));
                setClothingItems(allItems);

                if (outfitId) {
                    const outfitRes = await api.get(`/Outfit/${outfitId}`);
                    const outfit = outfitRes.data;
                    setFormData({
                        userID: userId,
                        temperatureSuitabilityID: outfit.temperatureSuitabilityID,
                        tagIDs: (outfit.tagIDs || []).map(id => Number(id)),
                        styleIDs: (outfit.styleIDs || []).map(id => Number(id)),
                        seasonIDs: (outfit.seasonIDs || []).map(id => Number(id)),
                        clothingItemIDs: (outfit.clothingItemIDs || []).map(id => Number(id)),
                    });
                }
            } catch (err) {
                console.error("Error loading data:", err);
            }
        }
        fetchData();
    }, [userId, outfitId]);

    // Оновлення рекомендацій при зміні AI-фільтра або завантаженні речей
    useEffect(() => {
        if (clothingItems.length > 0) {
            getSmartRecommendations();
        }
    }, [aiStyleFilter, clothingItems.length]);

    // Слухач події оновлення погоди з віджета
    useEffect(() => {
        const handleWeatherUpdate = () => {
            getSmartRecommendations();
        };

        window.addEventListener("weatherUpdated", handleWeatherUpdate);
        return () => window.removeEventListener("weatherUpdated", handleWeatherUpdate);
    }, [aiStyleFilter]);

    // --- ОБРОБНИКИ ПОДІЙ ---
    const toggleId = (field, id) => {
        const numId = Number(id);
        setFormData((prev) => ({
            ...prev,
            [field]: prev[field].includes(numId)
                ? prev[field].filter((i) => i !== numId)
                : [...prev[field], numId],
        }));
    };

    const removeClothingItem = (id) => {
        const numId = Number(id);
        setFormData((prev) => ({
            ...prev,
            clothingItemIDs: prev.clothingItemIDs.filter((i) => i !== numId),
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (outfitId) {
                await api.put(`/Outfit/${outfitId}`, formData);
            } else {
                await api.post("/Outfit", formData);
            }
            router.push("/outfit");
        } catch (err) {
            console.error(err);
            alert("Помилка при збереженні образу");
        }
    };

    // --- ПІДГОТОВКА СПИСКІВ ---
    const smartItems = clothingItems
        .filter(item => recommendedIds.includes(item.id))
        .sort((a, b) => recommendedIds.indexOf(a.id) - recommendedIds.indexOf(b.id))
        .slice(0, 10);

    const groupedItems = clothingItems.reduce((acc, item) => {
        const key = item.categoryName || "Other";
        if (!acc[key]) acc[key] = [];
        acc[key].push(item);
        return acc;
    }, {});

    return (
        <form className="create-outfit-layout" onSubmit={handleSubmit}>
            {/* ЛІВА ЧАСТИНА: СПИСОК ОДЯГУ */}
            <div className="clothing-list">
                <h2>My Wardrobe</h2>

                <div className="clothing-scroll">
                    {/* ✨ БЛОК РОЗУМНИХ РЕКОМЕНДАЦІЙ */}
                    <div className="category-block smart-section">
                        <div className="smart-header">
                            <h4>✨ Smart Suggestions</h4>
                        </div>

                        {isMlLoading ? (
                            <p className="loading-text">Analyzing your wardrobe...</p>
                        ) : (
                            <div className="grid-box">
                                {smartItems.map((item) => (
                                    <div
                                        key={`smart-${item.id}`}
                                        className={`clothing-item smart-card ${
                                            formData.clothingItemIDs.includes(item.id) ? "selected" : ""
                                        }`}
                                        onClick={() => toggleId("clothingItemIDs", item.id)}
                                    >
                                        <div className="recommendation-badge">AI Pick</div>
                                        {item.imageURL && <img src={item.imageURL} className="item-image" alt={item.name} />}
                                        <span className="item-name">{item.name}</span>
                                    </div>
                                ))}
                            </div>
                        )}
                    </div>

                    <hr className="section-divider" />

                    {/* КАТЕГОРІЇ (Дані з /grouped) */}
                    {Object.entries(groupedData).map(([categoryName, items]) => (
                        <div key={categoryName} className="category-block">
                            <h4>{categoryName}</h4>
                            <div className="scroll-row"> {/* Наш клас для горизонтального скролу */}
                                {items.map((item) => (
                                    <div
                                        key={item.id}
                                        className={`clothing-item ${
                                            formData.clothingItemIDs.includes(Number(item.id)) ? "selected" : ""
                                        }`}
                                        onClick={() => toggleId("clothingItemIDs", item.id)}
                                    >
                                        {item.imageURL && <img src={item.imageURL} className="item-image" alt={item.name} />}
                                        <span className="item-name">{item.name}</span>
                                    </div>
                                ))}
                            </div>
                        </div>
                    ))}
                </div>
            </div>

            {/* ЦЕНТРАЛЬНА ЧАСТИНА: ПЕРЕГЛЯД */}
            <div className="selected-items">
                <h2>New Outfit</h2>
                <div className="scroll-box">
                    {formData.clothingItemIDs.length === 0 && <p className="empty">Select items</p>}
                    {formData.clothingItemIDs.map((id) => {
                        const item = clothingItems.find((i) => i.id === id);
                        return item ? (
                            <div key={id} className="selected-item">
                                <img src={item.imageURL} alt={item.name} />
                                <span>{item.name}</span>
                                <button type="button" onClick={() => removeClothingItem(id)}>×</button>
                            </div>
                        ) : null;
                    })}
                </div>
                <button type="submit" className="submit-btn" disabled={formData.clothingItemIDs.length === 0}>
                    {outfitId ? "Update Outfit" : "Save Outfit"}
                </button>
            </div>

            {/* ПРАВА ЧАСТИНА: ПАРАМЕТРИ ОБРАЗУ */}
            <div className="sidebar-filters">
                <div className="filter-section">
                    <h3>Outfit Style</h3>
                    <div className="filter-group">
                        {stylesList.map((s) => (
                            <button
                                key={s.id}
                                type="button"
                                className={`filter-btn ${formData.styleIDs.includes(Number(s.id)) ? "active" : ""}`}
                                onClick={() => toggleId("styleIDs", s.id)}
                            >
                                {s.styleName}
                            </button>
                        ))}
                    </div>
                </div>

                <div className="filter-section">
                    <h3>Seasons</h3>
                    <div className="filter-group">
                        {seasons.map((s) => (
                            <button
                                key={s.id}
                                type="button"
                                className={`filter-btn ${formData.seasonIDs.includes(Number(s.id)) ? "active" : ""}`}
                                onClick={() => toggleId("seasonIDs", s.id)}
                            >
                                {s.seasonName}
                            </button>
                        ))}
                    </div>
                </div>

                <div className="filter-section">
                    <h3>Target Temp</h3>
                    <div className="filter-group">
                        {temps.map((t) => (
                            <button
                                key={t.id}
                                type="button"
                                className={`filter-btn ${formData.temperatureSuitabilityID === Number(t.id) ? "active" : ""}`}
                                onClick={() => setFormData(p => ({ ...p, temperatureSuitabilityID: Number(t.id) }))}
                            >
                                {t.temperatureSuitabilityName}
                            </button>
                        ))}
                    </div>
                </div>
            </div>
        </form>
    );
}
