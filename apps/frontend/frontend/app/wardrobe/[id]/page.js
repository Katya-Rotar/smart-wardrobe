"use client";

import React, { useState, useEffect } from "react";
import { notFound, useRouter } from "next/navigation";
import Image from "next/image";
import api from "../../../api/api";
import styles from "./pageID.module.css";

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

  if (loading) return <p className={styles.loading}>Завантаження...</p>;
  if (error) return <p className={styles.error}>Сталася помилка</p>;
  if (!item) return null;

  return (
      <div className={styles.page}>
        <div className={styles.card}>

          {/* IMAGE */}
          <div className={styles.imageBox}>
            <Image
                src={item.imageURL}
                alt={item.name}
                width={400}
                height={400}
                className={styles.image}
            />
          </div>

          {/* INFO */}
          <div className={styles.info}>

            <h1 className={styles.title}>{item.name}</h1>

            {/* COLOR */}
            <div className={styles.row}>
              <span className={styles.label}>Color</span>

              <span
                  className={styles.colorDot}
                  style={{ background: item.color }}
              />

              <span className={styles.text}>{item.color}</span>
            </div>

            {/* BASIC INFO */}
            <div className={styles.chips}>
              <span className={styles.chip}>{item.categoryName}</span>
              <span className={styles.chip}>{item.typeName}</span>
              <span className={styles.chip}>{item.temperatureSuitabilityName}</span>
            </div>

            {/* STYLE */}
            <div className={styles.chips}>
              {item.styleNames?.map((s, i) => (
                  <span key={i} className={styles.chipSoft}>
                {s}
              </span>
              ))}
            </div>

            {/* SEASON */}
            <div className={styles.chips}>
              {item.seasonNames?.map((s, i) => (
                  <span key={i} className={styles.chipSoft}>
                {s}
              </span>
              ))}
            </div>

            {/* ACTIONS */}
            <div className={styles.actions}>
              <button
                  onClick={() => router.push(`/wardrobe/edit/${item.id}`)}
                  className={styles.editBtn}
              >
                Edit
              </button>

              <button
                  onClick={handleDelete}
                  className={styles.deleteBtn}
              >
                Delete
              </button>
            </div>

          </div>
        </div>
      </div>
  );
}