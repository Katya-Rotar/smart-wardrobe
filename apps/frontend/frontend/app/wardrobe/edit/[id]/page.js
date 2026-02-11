"use client";

import { useEffect, useState } from "react";
import { useRouter, useParams } from "next/navigation";
import api from "../../../../api/api";
import "../../add/pageAdd.css";

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

  if (!response.ok) throw new Error("Failed to upload image to Cloudinary");

  const data = await response.json();
  return data.secure_url;
}

export default function EditItemPage() {
  const router = useRouter();
  const params = useParams();
  const itemId = params.id;

  const [item, setItem] = useState(null);
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

  const [selectedPhoto, setSelectedPhoto] = useState(null);

  useEffect(() => {
    async function fetchInitialData() {
      try {
        const [catRes, styleRes, seasonRes, tempRes, itemRes] = await Promise.all([
          api.get("/category"),
          api.get("/style"),
          api.get("/season"),
          api.get("/temperatureSuitability"),
          api.get(`/clothingitem/${itemId}`),
        ]);

        const fetchedItem = itemRes.data;

        setCategories(catRes.data);
        setStyles(styleRes.data);
        setSeasons(seasonRes.data);
        setTemps(tempRes.data);

        setItem(fetchedItem);
        setName(fetchedItem.name);
        setColor(fetchedItem.color);
        setSelectedCategory(catRes.data.find((c) => c.id === fetchedItem.categoryID));
        setSelectedStyle(styleRes.data.find((s) => s.id === fetchedItem.styleID));
        setSelectedSeason(seasonRes.data.find((s) => s.id === fetchedItem.seasonID));
        setSelectedTemp(tempRes.data.find((t) => t.id === fetchedItem.temperatureSuitabilityID));
        setSelectedPhoto(fetchedItem.imageURL);

        const typeRes = await api.get(`/type/by-category/${fetchedItem.categoryID}`);
        setTypes(typeRes.data);
        setSelectedType(typeRes.data.find((t) => t.id === fetchedItem.typeID));
      } catch (err) {
        console.error("Failed to load data", err);
      }
    }

    fetchInitialData();
  }, [itemId]);

  const handlePhotoUpload = (event) => {
    const file = event.target.files[0];
    if (file) {
      setPhotoFile(file);
      setSelectedPhoto(URL.createObjectURL(file));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!name.trim()) return alert("Enter a name");
    if (!color || !selectedCategory || !selectedType || !selectedStyle || !selectedSeason || !selectedTemp)
      return alert("Please fill all fields");

    try {
      let imageURL = selectedPhoto;
      if (photoFile) {
        imageURL = await uploadImageToCloudinary(photoFile);
      }

      const updateDto = {
        id: Number(itemId),
        userID: item.userID,
        name: name.trim(),
        color,
        categoryID: selectedCategory.id,
        typeID: selectedType.id,
        styleID: selectedStyle.id,
        seasonID: selectedSeason.id,
        temperatureSuitabilityID: selectedTemp.id,
        imageURL,
        lastWornDate: item.lastWornDate,
      };

      await api.put(`/clothingitem/${itemId}`, updateDto);
      alert("Item updated!");
      router.push("/wardrobe");
    } catch (err) {
      console.error("Update failed", err);
      alert("Failed to update item");
    }
  };

  const renderButtons = (items, selectedItem, onSelect, labelKey) =>
      items.map((item) => (
          <button
              key={item.id}
              type="button"
              className={`white-btn ${selectedItem?.id === item.id ? "active" : ""}`}
              onClick={() => onSelect(item)}
          >
            {item[labelKey]}
          </button>
      ));

  if (!item) return <div>Loading...</div>;

  return (
      <div className="container">
        <h1 className="title">Edit Item</h1>
        <form className="content" onSubmit={handleSubmit}>
          <div className="photo-section">
            <div className="photo-box">
              {selectedPhoto ? (
                  <img src={selectedPhoto} alt="Selected" className="uploaded-photo" />
              ) : (
                  <label className="add-photo-btn">
                    <span className="icon">⬆</span> Upload new photo
                    <input type="file" accept="image/*" onChange={handlePhotoUpload} hidden />
                  </label>
              )}
            </div>
            <button type="submit" className="black-btn">Save</button>
          </div>

          <div className="form-section">
            <div className="input-group">
              <label>Name</label>
              <input type="text" value={name} onChange={(e) => setName(e.target.value)} />
            </div>

            <div className="input-group">
              <label>Color</label>
              <select value={color} onChange={(e) => setColor(e.target.value)}>
                <option value="">Select color</option>
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
                {renderButtons(types, selectedType, setSelectedType, "typeName")}
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
