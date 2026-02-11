import ItemCard from "./itemCard";
import Link from "next/link";
import styles from "@/app/styles/wardrobe/categoryDisplay.module.css";

export default function CategoryDisplay({ categoryName, items }) {
  // Припускаємо, що всі items мають однаковий typeID
  const typeID = items.length > 0 ? items[0].typeID : null;

  return (
    <div className={styles["category-container"]}>
      <div className={styles["category-title"]}>{categoryName}</div>
      
      <div className={styles["content-wrapper"]}>
        <div className={styles["item-grid"]}>
          {items.map((item) => (
            <ItemCard
              key={item.id}
              id={item.id}
              name={item.name}
              image={item.imageURL}
            />
          ))}
        </div>

        {typeID && (
          <Link href={`/wardrobe/category/${typeID}`} className={styles["next-arrow"]}>
            ➜
          </Link>
        )}
      </div>
    </div>
  );
}
