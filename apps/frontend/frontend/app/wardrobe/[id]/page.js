"use client";

import React, { useState, useEffect } from "react";
import { notFound, useRouter } from "next/navigation";
import Image from "next/image";
import api from "../../../api/api"
export default function ItemDetailPage({ params }) {
  const router = useRouter();
  const resolvedParams = React.use(params);
  const { id } = resolvedParams;

  const [item, setItem] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    async function fetchItem() {
      setLoading(true);
      setError(false);

      try {
        const response = await api.get(`/ClothingItem/details/${id}`);
        setItem(response.data);
      } catch (error) {
        if (error.response && error.response.status === 404) {
          notFound();
        } else {
          console.error("Помилка при завантаженні елемента:", error);
          setError(true);
        }
      } finally {
        setLoading(false);
      }
    }

    fetchItem();
  }, [id]);

  const handleDelete = async () => {
    const confirmDelete = confirm("Ви впевнені, що хочете видалити цей елемент?");
    if (!confirmDelete) return;

    try {
      await api.delete(`/ClothingItem/${id}`);
      alert("Елемент успішно видалено");
      router.push("/wardrobe");
    } catch (error) {
      alert("Сталася помилка при видаленні елемента");
      console.error(error);
    }
  };

  if (loading) return <p>Завантаження...</p>;
  if (error) return <p>Сталася помилка при завантаженні елемента</p>;
  if (!item) return null;

  return (
      <div style={{ padding: "2rem" }}>
        <h1>{item.name}</h1>
        <Image src={item.imageURL} alt={item.name} width={300} height={300} />
        <ul>
          <li><strong>Color:</strong> {item.color}</li>
          <li><strong>Category:</strong> {item.categoryName}</li>
          <li><strong>Type:</strong> {item.typeName}</li>
          <li><strong>Temperature Suitability:</strong> {item.temperatureSuitabilityName}</li>
          <li><strong>Styles:</strong> {item.styleNames.join(", ")}</li>
          <li><strong>Seasons:</strong> {item.seasonNames.join(", ")}</li>
          <li><strong>Last Worn Date:</strong> {item.lastWornDate ? new Date(item.lastWornDate).toLocaleDateString() : "N/A"}</li>
        </ul>

        <div style={{ marginTop: "1rem" }}>
          <button onClick={() => router.push(`/wardrobe/edit/${item.id}`)} style={{ marginRight: "1rem" }}>
            Edit
          </button>

          <button onClick={handleDelete} style={{ backgroundColor: "red", color: "white" }}>
            Delete
          </button>
        </div>
      </div>
  );
}
