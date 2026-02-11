"use client";

import { useEffect, useState } from "react";
import CategoryDisplay from "../components/wardrobe/categoryDisplay";
import Link from "next/link";
import "./page.css";

export default function CategoryPage() {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchCategories = async () => {
      const token = localStorage.getItem("token");
      if (!token) {
        console.error("Токен не знайдено в localStorage");
        return;
      }

      try {
        const response = await fetch("http://localhost:5000/api/ClothingItem/grouped", {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!response.ok) {
          throw new Error("Не вдалося завантажити категорії");
        }

        const data = await response.json();
        const categoriesArray = Object.entries(data).map(([name, items]) => ({
          name,
          items,
        }));

        console.log("Отримано:", categoriesArray);
        setCategories(categoriesArray);
      } catch (error) {
        console.error("Помилка при завантаженні категорій:", error);
      }
    };

    fetchCategories();
  }, []);

  return (
      <div>
        <Link href={`/wardrobe/add`} className="add-button">+</Link>

        {categories.map((category, index) => (
            <CategoryDisplay
                key={index}
                categoryName={category.name}
                items={category.items}
            />
        ))}
      </div>
  );
}
