import React from 'react';
import Link from "next/link";
import styles from '../../styles/outfitCard.module.css';

const OutfitCard = ({ outfit }) => {
  return (
    <div className={styles.card}>
      <h3>
        <Link href={`/outfit/${outfit.id}`}>
          Outfit
        </Link>
      </h3>
      <div className={styles.items}>
        {outfit.itemNames.map(item => (
          <div key={item.id} className={styles.item}>
            <img src={item.imageURL} alt={item.name} />
            <p>{item.name}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default OutfitCard;
