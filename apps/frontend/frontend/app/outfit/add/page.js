'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import api from "@/api/api"; // Якщо це окремий інстанс axios - класно
import "./CreateOutfitPage.css"
import "@/app/styles/wardrobe/itemCard.css";

export default function CreateOutfitPage({ userId, outfitId  }) {
    const router = useRouter();
    
    const [formData, setFormData] = useState({
        userID: userId, // id користувача з пропсів
        temperatureSuitabilityID: 1,
        tagIDs: [],
        styleIDs: [],
        seasonIDs: [],
        clothingItemIDs: [],
    });

    const [styles, setStyles] = useState([]);
    const [seasons, setSeasons] = useState([]);
    const [temps, setTemps] = useState([]);
    const [tags, setTags] = useState([]);
    const [clothingItems, setClothingItems] = useState([]);
    const [filteredItems, setFilteredItems] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                // Завантажуємо всі потрібні дані одночасно
                const [tagRes, styleRes, seasonRes, tempRes, clothingRes] = await Promise.all([
                    api.get('/Tag'),
                    api.get('/Style'),
                    api.get('/Season'),
                    api.get('/TemperatureSuitability'),
                    api.get(`/ClothingItem`, { params: { UserId: userId } })
                ]);

                setTags(tagRes.data);
                setStyles(styleRes.data);
                setSeasons(seasonRes.data);
                setTemps(tempRes.data);
                setClothingItems(clothingRes.data);
                setFilteredItems(clothingRes.data);
                if (outfitId) {
                    const outfitRes = await api.get(`/Outfit/${outfitId}`);
                    const outfit = outfitRes.data;

                    console.log("Outfit data from backend:", outfit);

                    // Підставляємо отримані дані в форму
                    setFormData({
                        userID: userId,
                        temperatureSuitabilityID: outfit.temperatureSuitabilityID,
                        tagIDs: outfit.tagIDs || [],
                        styleIDs: outfit.styleIDs || [],
                        seasonIDs: outfit.seasonIDs || [],
                        clothingItemIDs: outfit.clothingItemIDs || [],
                    });
                }
            } catch (err) {
                console.error('Failed request:', err);
            }
        }
        fetchData();
    }, [userId, outfitId]);

    // Функція для додавання/видалення id з масиву у formData
    const toggleId = (field, id) => {
        setFormData(prev => ({
            ...prev,
            [field]: prev[field].includes(id)
                ? prev[field].filter(i => i !== id)
                : [...prev[field], id],
        }));
    };

    // Видалення обраного елементу
    const removeClothingItem = (id) => {
        setFormData(prev => ({
            ...prev,
            clothingItemIDs: prev.clothingItemIDs.filter(itemId => itemId !== id)
        }));
    };

    // Відправка форми на бекенд
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (outfitId) {
                // Редагування: PUT-запит
                await api.put(`/Outfit/${outfitId}`, formData);
                alert('Аутфіт оновлено!');
            } else {
                // Створення: POST-запит
                await api.post('/Outfit', formData);
                alert('Аутфіт створено!');
            }
            router.push('/outfit');
        } catch (err) {
            console.error(err);
            alert('Помилка при збереженні аутфіту.');
        }
    };

    return (
        <form className="create-outfit-layout" onSubmit={handleSubmit}>
            {/* Ліва частина - фільтри і вибір даних про образ */}
            <div className="sidebar-filters">
                <h3>Style</h3>
                {styles.map(s => (
                    <button
                        key={s.id}
                        type="button"
                        className={`filter-btn ${formData.styleIDs.includes(s.id) ? 'active' : ''}`}
                        onClick={() => toggleId('styleIDs', s.id)}
                    >
                        {s.styleName}
                    </button>
                ))}

                <h3>Season</h3>
                {seasons.map(s => (
                    <button
                        key={s.id}
                        type="button"
                        className={`filter-btn ${formData.seasonIDs.includes(s.id) ? 'active' : ''}`}
                        onClick={() => toggleId('seasonIDs', s.id)}
                    >
                        {s.seasonName}
                    </button>
                ))}

                <h3>Temperature</h3>
                {temps.map(t => (
                    <button
                        key={t.id}
                        type="button"
                        className={`filter-btn ${formData.temperatureSuitabilityID === t.id ? 'active' : ''}`}
                        onClick={() => setFormData(prev => ({ ...prev, temperatureSuitabilityID: t.id }))}
                    >
                        {t.temperatureSuitabilityName}
                    </button>
                ))}

                <h3>Tags</h3>
                {tags.map(tag => (
                    <button
                        key={tag.id}
                        type="button"
                        className={`filter-btn ${formData.tagIDs.includes(tag.id) ? 'active' : ''}`}
                        onClick={() => toggleId('tagIDs', tag.id)}
                    >
                        {tag.tagName}
                    </button>
                ))}
            </div>

            {/* Центр: Вибрані елементи одягу */}
            <div className="selected-items">
                <h3>Selected Items</h3>
                <div className="scroll-box">
                    {formData.clothingItemIDs.length === 0 && <p>Немає вибраних речей</p>}
                    {formData.clothingItemIDs.map(id => {
                        const item = clothingItems.find(ci => ci.id === id);
                        return item ? (
                            <div key={id} className="selected-item item-card">
                                <div className="item-content">
                                    <img src={item.imageURL} alt={item.name} className="item-image" />
                                    <div className="item-name">{item.name}</div>
                                </div>
                                <button type="button" onClick={() => removeClothingItem(id)}>🗑</button>
                            </div>
                        ) : null;
                    })}
                </div>
                <button type="submit" className="submit-btn">
                    {outfitId ? 'Оновити аутфіт' : 'Створити аутфіт'}
                </button>
            </div>

            {/* Права частина: Всі речі користувача */}
            <div className="clothing-list">
                <h3>All Clothing Items</h3>
                <div className="grid-box">
                    {filteredItems.map(item => (
                        <div
                            key={item.id}
                            className={`clothing-item item-card ${formData.clothingItemIDs.includes(item.id) ? 'selected' : ''}`}
                            onClick={() => toggleId('clothingItemIDs', item.id)}
                            style={{ cursor: 'pointer' }}
                        >
                            <div className="item-content">
                                <img src={item.imageURL} alt={item.name} className="item-image" />
                                <div className="item-name">{item.name}</div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </form>
    );
}
