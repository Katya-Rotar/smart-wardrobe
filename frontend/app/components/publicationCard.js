import React from 'react';
import '../styles/publicationCard.css';

export default function PublicationCard({ imageURL, tags }) {
    if (!imageURL || imageURL.trim() === '') return null;

    return (
        <div className="publication-card">
            <img src={imageURL} alt="Publication" />
            <p className="tags">
                {tags?.map((tag) => `#${tag}`).join(' ')}
            </p>
        </div>
    );
}
