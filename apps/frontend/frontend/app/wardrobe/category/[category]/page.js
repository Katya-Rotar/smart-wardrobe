"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import api from "../../../../api/api";
import CategoryDisplay from "../../../../app/components/wardrobe/categoryDisplay";

export default function Category() {
  const params = useParams();
  const { category } = params; // category — це typeID

  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [categoryName, setCategoryName] = useState("");

  useEffect(() => {
    if (!category) return;

    const fetchItemsAndTypeName = async () => {
      setLoading(true);
      setError(null);

      try {
        const userId = 1;
        const itemsResponse = await api.get(`/ClothingItem/type/${category}?userId=${userId}`);
        setItems(itemsResponse.data);

        const typeResponse = await api.get(`/Type/${category}`);
        setCategoryName(typeResponse.data.typeName);
      } catch (err) {
        setError("Помилка при завантаженні даних");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchItemsAndTypeName();
  }, [category]);

  if (loading) return <p>Завантаження...</p>;
  if (error) return <p>{error}</p>;
  if (!items.length) return <p>Немає елементів у цій категорії</p>;

  return (
    <div>
      <CategoryDisplay categoryName={categoryName} items={items} />
    </div>
  );
}
