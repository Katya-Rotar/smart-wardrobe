'use client';

import React, { useEffect, useState, useRef } from 'react';
import '../../styles/publicationModal.css';
import PublicationDetail from './publicationDetail';
import PublicationCard from '../publicationCard';
import api from '../../../api/api';
import PortalModal from '../../components/portalModal';

export default function PublicationModal({ publicationId, onClose, onNext, onPrev }) {
    const [publication, setPublication] = useState(null);
    const sliderTrackRef = useRef(null);

    const [viewMode, setViewMode] = useState('details');
    const [aiPublications, setAiPublications] = useState([]);
    const [aiLoading, setAiLoading] = useState(false);
    const [selectedItemUrl, setSelectedItemUrl] = useState('');

    useEffect(() => {
        const handleEscape = (e) => e.key === 'Escape' && onClose();
        window.addEventListener('keydown', handleEscape);
        return () => window.removeEventListener('keydown', handleEscape);
    }, [onClose]);

    useEffect(() => {
        async function fetchPublication() {
            try {
                const res = await api.get(`/publication/${publicationId}`);
                setPublication(res.data);
                setViewMode('details');
            } catch (error) {
                console.error('Failed to load publication details', error);
                onClose();
            }
        }

        if (publicationId) {
            fetchPublication();
        }
    }, [publicationId, onClose]);

    const handleLocalVisualSearch = async (itemId, imageUrl) => {
        setAiLoading(true);
        // Захист: дістаємо рядок урла, навіть якщо прилетів складний об'єкт
        const cleanUrl = imageUrl && imageUrl.imageURL ? imageUrl.imageURL : imageUrl;
        setSelectedItemUrl(cleanUrl || '');
        setViewMode('ai');

        try {
            const res = await api.get(`/publication/search-by-item/${itemId}`);
            setAiPublications(res.data || []);
            if (sliderTrackRef.current) sliderTrackRef.current.scrollLeft = 0;
        } catch (err) {
            console.error('Error during ML visual search inside modal:', err);
        } finally {
            setAiLoading(false);
        }
    };

    if (!publication) return null;

    return (
        <PortalModal>
            <div className="modal-overlay" onClick={onClose}>
                <div className="modal-content" onClick={e => e.stopPropagation()}>
                    <button className="modal-close" onClick={onClose}>×</button>

                    {viewMode === 'details' ? (
                        <PublicationDetail
                            publication={publication}
                            onItemClick={handleLocalVisualSearch}
                        />
                    ) : (
                        <div className="ai-view-content">
                            <div className="ai-modal-header">
                                <div className="target-preview-container">
                                    {selectedItemUrl && (
                                        <img
                                            src={selectedItemUrl}
                                            alt="Item"
                                            className="target-preview-image"
                                        />
                                    )}
                                    <h3 className="ai-modal-title">Схожі образи</h3>
                                </div>
                                <button
                                    className="ai-back-button"
                                    onClick={() => setViewMode('details')}
                                >
                                    ← Назад до образу
                                </button>
                            </div>

                            {aiLoading ? (
                                <div style={{ textAlign: 'center', padding: '60px 0', color: '#64748b', fontSize: '15px' }}>
                                    ✨ Модель аналізує стиль та шукає збіги...
                                </div>
                            ) : aiPublications.length === 0 ? (
                                <div style={{ textAlign: 'center', padding: '60px 0', color: '#64748b' }}>
                                    Схожих комбінацій з цією річчю не знайдено.
                                </div>
                            ) : (
                                <div className="ai-slider-container">
                                    <div className="ai-carousel-track" ref={sliderTrackRef}>
                                        {aiPublications.map((pub) => {
                                            return (
                                                /* Клікабельна обгортка картки, тепер без відсотків */
                                                <div
                                                    key={pub.id}
                                                    className="ai-carousel-item"
                                                    onClick={() => {
                                                        setPublication(null); // Очищуємо старий пост
                                                        api.get(`/publication/${pub.id}`).then(res => {
                                                            setPublication(res.data);
                                                            setViewMode('details'); // Повертаємо на деталі нового образу
                                                        }).catch(err => console.error(err));
                                                    }}
                                                >
                                                    <PublicationCard
                                                        imageURL={pub.imageURL}
                                                        tags={pub.tags || []}
                                                    />
                                                </div>
                                            );
                                        })}
                                    </div>
                                </div>
                            )}
                        </div>
                    )}
                </div>
            </div>
        </PortalModal>
    );
}
