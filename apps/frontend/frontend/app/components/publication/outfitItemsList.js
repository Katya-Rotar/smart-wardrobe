import React from 'react';


export default function OutfitItemsList({ items, onItemClick }) {
    if (!items || !Array.isArray(items)) return null;

    return (
        <div className="outfit-items" style={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
            {items.map((item, i) => {
                // Визначаємо ID та URL картинки залежно від структури об'єкта
                const itemId = item.clothingItemID || item.id || i;
                const imageSrc = item.imageURL || item;

                return (
                    <img
                        key={itemId}
                        src={imageSrc}
                        alt="Item"
                        className="outfit-item-image"
                        style={{ cursor: 'pointer', transition: 'transform 0.2s' }}
                        onClick={() => {
                            if (onItemClick) {
                                onItemClick(itemId, imageSrc);
                            }
                        }}
                        // Ефект збільшення при наведенні мишки для інтерактивності
                        onMouseOver={(e) => e.currentTarget.style.transform = 'scale(1.08)'}
                        onMouseOut={(e) => e.currentTarget.style.transform = 'scale(1)'}
                        title="Знайти схожі образи з цією річчю 🤖"
                    />
                );
            })}
        </div>
    );
}
