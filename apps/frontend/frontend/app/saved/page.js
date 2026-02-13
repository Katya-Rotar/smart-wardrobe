'use client';

import { useEffect, useState, useRef, useCallback } from 'react';
import api from '../../api/api';
import '../publicationsPage.css';
import PublicationCard from '../components/publicationCard';
import PublicationModal from '../components/publication/publicationModal';

export default function SavedPublicationsPage() {
    const [publications, setPublications] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const [loading, setLoading] = useState(false);
    const [selectedPublicationId, setSelectedPublicationId] = useState(null);

    const observer = useRef();

    const lastPublicationRef = useCallback((node) => {
        if (loading) return;

        if (observer.current) observer.current.disconnect();

        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && hasMore) {
                setPageNumber(prev => prev + 1);
            }
        });

        if (node) observer.current.observe(node);
    }, [loading, hasMore]);

    useEffect(() => {
        fetchSavedPublications(pageNumber);
    }, [pageNumber]);

    const fetchSavedPublications = async (page) => {
        setLoading(true);
        try {
            const token = localStorage.getItem('token');
            const res = await api.get('/publication/saved', {
                params: { pageNumber: page, pageSize: 12 },
                headers: { Authorization: `Bearer ${token}` }
            });

            const data = res.data;
            const newItems = data.items || data;

            setPublications(prev => {
                const ids = new Set(prev.map(pub => pub.id));
                const filteredNewItems = newItems.filter(pub => !ids.has(pub.id));
                return [...prev, ...filteredNewItems];
            });

            setHasMore(newItems.length > 0);
        } catch (err) {
            console.error('Error loading saved publications:', err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <div className="publication-grid">
                {publications.map((pub, index) => {
                    const card = (
                        <PublicationCard
                            key={pub.id}
                            imageURL={pub.imageURL}
                            tags={pub.tags}
                            onClick={() => setSelectedPublicationId(pub.id)}
                        />
                    );

                    return index === publications.length - 1
                        ? <div ref={lastPublicationRef} key={pub.id}>{card}</div>
                        : card;
                })}
            </div>

            {selectedPublicationId && (
                <PublicationModal
                    publicationId={selectedPublicationId}
                    onClose={() => setSelectedPublicationId(null)}
                />
            )}
        </>
    );
}
