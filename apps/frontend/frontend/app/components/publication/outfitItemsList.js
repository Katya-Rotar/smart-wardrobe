import React from 'react';

export default function OutfitItemsList({ items }) {
    return (
        <div className="outfit-items">
            {items.map((item, i) => (
                <img key={i} src={item.imageURL} alt="Item" className="outfit-item-image" />
            ))}
        </div>
    );
}
