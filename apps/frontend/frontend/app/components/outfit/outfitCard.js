import React from "react";
import Link from "next/link";
import styles from "../../styles/outfitCard.module.css";

const OutfitCard = ({ outfit }) => {
    return (
        <Link href={`/outfit/${outfit.id}`} className={styles.card}>

            <div className={styles.header}>
                <h3>Outfit</h3>
                <span className={styles.count}>{outfit.itemNames.length} items</span>
            </div>

            <div className={styles.grid}>
                {outfit.itemNames.slice(0, 4).map((item) => (
                    <div key={item.id} className={styles.imageWrap}>
                        <img src={item.imageURL} alt={item.name} />
                    </div>
                ))}
            </div>

        </Link>
    );
};

export default OutfitCard;