import React from 'react';
import LikeButton from './LikeButton';
import CommentSection from './CommentSection';
import OutfitItemsList from './OutfitItemsList';
import SavePostButton from './SavePostButton';
import Link from 'next/link';

export default function PublicationDetail({ publication }) {
    return (
        <div className="publication-detail">
            <img src={publication.imageURL} alt="Outfit" className="main-image" />

            <OutfitItemsList items={publication.outfitItemImages} />

            <div className="publication-info">
                <div style={{ display: 'flex', gap: '10px' }}>
                    <LikeButton publicationId={publication.id} />
                    <SavePostButton publicationId={publication.id} />
                </div>
                <p className="tags">
                    {publication.tags.map(tag => `#${tag}`).join(' ')}
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
