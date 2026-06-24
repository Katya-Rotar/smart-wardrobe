"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import api from "../../../api/api";
import "./pageAdd.css";

const CLOUD_NAME = "ddapkpo6c";
const UPLOAD_PRESET = "unsigned_preset";

async function uploadImageToCloudinary(file) {
    const url = `https://api.cloudinary.com/v1_1/${CLOUD_NAME}/upload`;
    const formData = new FormData();
    formData.append("file", file);
    formData.append("upload_preset", UPLOAD_PRESET);

    const response = await fetch(url, { method: "POST", body: formData });
    if (!response.ok) throw new Error("Failed to upload image to Cloudinary");
    const data = await response.json();
    return data.secure_url;
}

export default function AddItems() {
    const router = useRouter();

    const [selectedPhoto, setSelectedPhoto] = useState(null);
    const [photoFile, setPhotoFile] = useState(null);
    const [name, setName] = useState("");
    const [color, setColor] = useState("#ffffff"); // Значення за замовчуванням
    const [selectedCategory, setSelectedCategory] = useState(null);
    const [selectedType, setSelectedType] = useState(null);
    const [selectedStyles, setSelectedStyles] = useState([]);
    const [selectedSeasons, setSelectedSeasons] = useState([]);
    const [selectedTemp, setSelectedTemp] = useState(null);

    const [categories, setCategories] = useState([]);
    const [types, setTypes] = useState([]);
    const [styles, setStyles] = useState([]);
    const [seasons, setSeasons] = useState([]);
    const [temps, setTemps] = useState([]);

    const [isPredicting, setIsPredicting] = useState(false);

    useEffect(() => {
        async function fetchData() {
            try {
                const [catRes, styleRes, seasonRes, tempRes] = await Promise.all([
                    api.get("/category"),
                    api.get("/style"),
                    api.get("/season"),
                    api.get("/temperatureSuitability"),
                ]);
                setCategories(catRes.data);
                setStyles(styleRes.data);
                setSeasons(seasonRes.data);
                setTemps(tempRes.data);
            } catch (error) {
                console.error("Error loading dictionaries:", error);
            }
        }
        fetchData();
    }, []);

    useEffect(() => {
        async function fetchTypes() {
            if (!selectedCategory) {
                setTypes([]);
                setSelectedType(null);
                return;
            }
            try {
                const res = await api.get(`/type/by-category/${selectedCategory.id}`);
                setTypes(res.data);
            } catch (error) {
                setTypes([]);
            }
        }
        fetchTypes();
    }, [selectedCategory]);

    const findInList = (list, val, key) => {
        if (!val || !list.length) return null;
        return list.find(item => item[key].toLowerCase().includes(val.toLowerCase())) || null;
    };

    const toggleSelection = (item, selectedList, setSelectedList) => {
        if (selectedList.some(i => i.id === item.id)) {
            setSelectedList(selectedList.filter(i => i.id !== item.id));
        } else {
            setSelectedList([...selectedList, item]);
        }
    };

    const handlePhotoUpload = async (event) => {
        const file = event.target.files[0];
        if (!file) return;

        setPhotoFile(file);
        setSelectedPhoto(URL.createObjectURL(file));
        setIsPredicting(true);

        const formData = new FormData();
        formData.append("file", file);

        try {
            const response = await api.post("/clothingitem/predict", formData);
            const prediction = response.data;

            if (prediction.baseColour) {
                // Перевіряємо чи колір у форматі HEX, якщо ні — ставимо білий
                const colorHex = prediction.baseColour.startsWith('#') ? prediction.baseColour : "#ffffff";
                setColor(colorHex);
            }
            if (prediction.articleType) setName(prediction.articleType);

            const matchedCat = findInList(categories, prediction.subCategory, "categoryName");
            if (matchedCat) {
                setSelectedCategory(matchedCat);
                const typeRes = await api.get(`/type/by-category/${matchedCat.id}`);
                const matchedType = findInList(typeRes.data, prediction.articleType, "typeName");
                if (matchedType) setSelectedType(matchedType);
            }

            const matchedStyle = findInList(styles, prediction.usage, "styleName");
            if (matchedStyle) setSelectedStyles([matchedStyle]);

            const matchedSeason = findInList(seasons, prediction.season, "seasonName");
            if (matchedSeason) setSelectedSeasons([matchedSeason]);

        } catch (error) {
            console.error("ML Prediction error:", error);
        } finally {
            setIsPredicting(false);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!name.trim() || !color || !selectedCategory || !selectedType || selectedStyles.length === 0 || selectedSeasons.length === 0 || !selectedTemp) {
            alert("Please fill in all fields!");
            return;
        }

        try {
            let imageURL = null;
            if (photoFile) imageURL = await uploadImageToCloudinary(photoFile);

            const createDto = {
                name: name.trim(),
                color,
                categoryID: selectedCategory.id,
                typeID: selectedType.id,
                styleIds: selectedStyles.map(s => s.id),
                seasonIds: selectedSeasons.map(s => s.id),
                temperatureSuitabilityID: selectedTemp.id,
                imageURL
            };

            await api.post("/clothingitem", createDto);
            alert("Item successfully added!");
            router.push("/wardrobe");
        } catch (error) {
            alert("Error saving the item");
        }
    };

    return (
        <div className="container">
            <h1 className="title">Add New Item</h1>
            <form className="content" onSubmit={handleSubmit}>
                <div className="photo-section">
                    <div className="photo-box">
                        {selectedPhoto ? (
                            <>
                                <img src={selectedPhoto} className="uploaded-photo" alt="Item" />
                                <label className="change-photo">
                                    Change Photo
                                    <input type="file" hidden onChange={handlePhotoUpload} />
                                </label>
                            </>
                        ) : (
                            <label className="add-photo-btn">
                                <span className="icon">⬆</span>
                                Upload Photo
                                <input type="file" hidden onChange={handlePhotoUpload} />
                            </label>
                        )}
                    </div>
                    <button type="submit" className="black-btn" disabled={isPredicting}>
                        {isPredicting ? "Analyzing..." : "Save Item"}
                    </button>
                </div>

                <div className="form-section">
                    <div className="input-group">
                        <label>Item Name</label>
                        <input type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder="e.g. White T-shirt" />
                    </div>

                    <div className="input-group">
                        <label>Color</label>
                        <div className="color-row">
                            <div className="color-picker-wrapper">
                                <input
                                    type="color"
                                    value={color}
                                    onChange={(e) => setColor(e.target.value)}
                                    className="color-input"
                                />
                                <div className="color-preview" style={{ backgroundColor: color }}>
                                    <span className="eyedropper-icon"></span>
                                </div>
                            </div>
                            <div className="color-hex-badge">
                                <span className="color-hex-text">{color.toUpperCase()}</span>
                            </div>
                        </div>
                    </div>

                    <div className="input-group">
                        <label>Category</label>
                        <div className="btn-group">
                            {categories.map(c => (
                                <button type="button" key={c.id} className={`white-btn ${selectedCategory?.id === c.id ? "active" : ""}`} onClick={() => setSelectedCategory(c)}>
                                    {c.categoryName}
                                </button>
                            ))}
                        </div>
                    </div>

                    <div className="input-group">
                        <label>Type</label>
                        <div className="btn-group">
                            {types.length > 0 ? types.map(t => (
                                <button type="button" key={t.id} className={`white-btn ${selectedType?.id === t.id ? "active" : ""}`} onClick={() => setSelectedType(t)}>
                                    {t.typeName}
                                </button>
                            )) : <p className="small-info">Please select a category first</p>}
                        </div>
                    </div>

                    <div className="input-group">
                        <label>Styles (Multi-select)</label>
                        <div className="btn-group">
                            {styles.map(s => (
                                <button type="button" key={s.id} className={`white-btn ${selectedStyles.some(i => i.id === s.id) ? "active" : ""}`} onClick={() => toggleSelection(s, selectedStyles, setSelectedStyles)}>
                                    {s.styleName}
                                </button>
                            ))}
                        </div>
                    </div>

                    <div className="input-group">
                        <label>Seasons (Multi-select)</label>
                        <div className="btn-group">
                            {seasons.map(s => (
                                <button type="button" key={s.id} className={`white-btn ${selectedSeasons.some(i => i.id === s.id) ? "active" : ""}`} onClick={() => toggleSelection(s, selectedSeasons, setSelectedSeasons)}>
                                    {s.seasonName}
                                </button>
                            ))}
                        </div>
                    </div>

                    <div className="input-group">
                        <label>Temperature Range</label>
                        <div className="btn-group btn-group-large">
                            {temps.map(t => (
                                <button type="button" key={t.id} className={`white-btn ${selectedTemp?.id === t.id ? "active" : ""}`} onClick={() => setSelectedTemp(t)}>
                                    {t.temperatureSuitabilityName}
                                </button>
                            ))}
                        </div>
                    </div>
                </div>
            </form>
        </div>
    );
}
