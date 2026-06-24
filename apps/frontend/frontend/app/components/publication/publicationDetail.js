'use client';
import React from 'react';
import LikeButton from './likeButton';
import CommentSection from './commentSection';
import OutfitItemsList from './outfitItemsList';
import SavePostButton from './savePostButton';
import Link from 'next/link';

export default function PublicationDetail({ publication, onItemClick }) {
    return (
        <div className="publication-detail">
            <img src={publication.imageURL} alt="Outfit" className="main-image" />

            {/* Передаємо його сюди: */}
            <OutfitItemsList items={publication.outfitItemImages} onItemClick={onItemClick} />

            <div className="publication-info">
                <div style={{ display: 'flex', gap: '10px' }}>
                    <LikeButton publicationId={publication.id} />
                    <SavePostButton publicationId={publication.id} />
                </div>
                <p className="tags">
                    {publication.tags ? publication.tags.map(tag => `#${tag}`).join(' ') : ''}
                </p>
                <div className="user-info">
                    <img src={publication.userImage || '/default-avatar.png'} alt="user" />
                    <Link href={`/profile/${publication.userID}`}>
                        <span style={{ cursor: 'pointer', textDecoration: 'underline' }}>{publication.username}</span>
                    </Link>
                </div>

                {publication.commentingOptions && (
                    <CommentSection publicationId={publication.id} />
                )}
            </div>
        </div>
    );
}
