"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";  // Імпорт роутера
import api from "../../../api/api";
import "./pageAdd.css";

// Константи для Cloudinary
const CLOUD_NAME = "ddapkpo6c";
const UPLOAD_PRESET = "unsigned_preset";

async function uploadImageToCloudinary(file) {
  const url = `https://api.cloudinary.com/v1_1/${CLOUD_NAME}/upload`;
  const formData = new FormData();
  formData.append("file", file);
  formData.append("upload_preset", UPLOAD_PRESET);

  const response = await fetch(url, {
    method: "POST",
    body: formData,
  });

  if (!response.ok) {
    throw new Error("Failed to upload image to Cloudinary");
  }

  const data = await response.json();
  return data.secure_url;
}

export default function AddItems() {
  const router = useRouter();  // Ініціалізуємо роутер

  const [selectedPhoto, setSelectedPhoto] = useState(null);
  const [photoFile, setPhotoFile] = useState(null);
  const [name, setName] = useState("");
  const [color, setColor] = useState("");
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [types, setTypes] = useState([]);
  const [selectedType, setSelectedType] = useState(null);
  const [styles, setStyles] = useState([]);
  const [selectedStyle, setSelectedStyle] = useState(null);
  const [seasons, setSeasons] = useState([]);
  const [selectedSeason, setSelectedSeason] = useState(null);
  const [temps, setTemps] = useState([]);
  const [selectedTemp, setSelectedTemp] = useState(null);
  const [categories, setCategories] = useState([]);

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
        console.error("Failed to fetch reference data:", error);
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
        setSelectedType(null);
      } catch (error) {
        console.error("Failed to fetch types:", error);
        setTypes([]);
        setSelectedType(null);
      }
    }
    fetchTypes();
  }, [selectedCategory]);

  const handlePhotoUpload = (event) => {
    const file = event.target.files[0];
    if (file) {
      setPhotoFile(file);
      setSelectedPhoto(URL.createObjectURL(file));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!name.trim()) return alert("Please enter a name");
    if (!color) return alert("Please select a color");
    if (!selectedCategory) return alert("Please select a category");
    if (!selectedType) return alert("Please select a type");
    if (!selectedStyle) return alert("Please select a style");
    if (!selectedSeason) return alert("Please select a season");
    if (!selectedTemp) return alert("Please select a temperature suitability");

    try {
      let imageURL = null;

      if (photoFile) {
        imageURL = await uploadImageToCloudinary(photoFile);
      }

      const createDto = {
        userID: 1,
        name: name.trim(),
        color,
        categoryID: selectedCategory.id,
        typeID: selectedType.id,
        styleID: selectedStyle.id,
        seasonID: selectedSeason.id,
        temperatureSuitabilityID: selectedTemp.id,
        imageURL,
        lastWornDate: null,
      };

      await api.post("/clothingitem", createDto);

      alert("Item added successfully!");

      // Перенаправлення після успішного створення
      router.push("/wardrobe");

      // Очистка форми (необов’язково, бо ми редіректимо)
      setName("");
      setColor("");
      setSelectedCategory(null);
      setTypes([]);
      setSelectedType(null);
      setSelectedStyle(null);
      setSelectedSeason(null);
      setSelectedTemp(null);
      setSelectedPhoto(null);
      setPhotoFile(null);
    } catch (error) {
      console.error("Failed to add item:", error);
      alert("Failed to add item");
    }
  };

  const renderButtons = (items, selectedItem, onSelect, labelKey) =>
      items.map((item) => (
          <button
              type="button"
              key={item.id}
              className={`white-btn ${selectedItem?.id === item.id ? "active" : ""}`}
              onClick={() => onSelect(item)}
          >
            {item[labelKey]}
          </button>
      ));

  return (
    <div className="container">
      <h1 className="title">Add Items</h1>
      <form className="content" onSubmit={handleSubmit}>
        <div className="photo-section">
          <div className="photo-box">
            {selectedPhoto ? (
              <img src={selectedPhoto} alt="Selected" className="uploaded-photo" />
            ) : (
              <label className="add-photo-btn">
                <span className="icon">⬆</span> Add a photo
                <input type="file" accept="image/*" onChange={handlePhotoUpload} hidden />
              </label>
            )}
          </div>
          <button type="submit" className="black-btn">
            Add
          </button>
        </div>

        <div className="form-section">
          <div className="input-group">
            <label>Name</label>
            <input
              type="text"
              placeholder="Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </div>

          <div className="input-group">
            <label>Color</label>
            <select value={color} onChange={(e) => setColor(e.target.value)}>
              <option value="">Select a color</option>
              <option value="Red">Red</option>
              <option value="Blue">Blue</option>
              <option value="Green">Green</option>
              <option value="Black">Black</option>
              <option value="White">White</option>
            </select>
          </div>

          <div className="input-group">
            <label>Category</label>
            <div className="btn-group">
              {renderButtons(categories, selectedCategory, setSelectedCategory, "categoryName")}
            </div>
          </div>

          <div className="input-group">
            <label>Type</label>
            <div className="btn-group">
              {types.length > 0 ? (
                renderButtons(types, selectedType, setSelectedType, "typeName")
              ) : (
                <p>Please select a category first</p>
              )}
            </div>
          </div>

          <div className="input-group">
            <label>Style</label>
            <div className="btn-group">{renderButtons(styles, selectedStyle, setSelectedStyle, "styleName")}</div>
          </div>

          <div className="input-group">
            <label>Season</label>
            <div className="btn-group">{renderButtons(seasons, selectedSeason, setSelectedSeason, "seasonName")}</div>
          </div>

          <div className="input-group">
            <label>Temperature Suitability</label>
            <div className="btn-group btn-group-large">
              {renderButtons(temps, selectedTemp, setSelectedTemp, "temperatureSuitabilityName")}
            </div>
          </div>
        </div>
      </form>
    </div>
  );
}
