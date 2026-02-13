'use client';
import { useEffect, useState, useRef, useCallback } from 'react';
import api from '../api/api';
import './publicationsPage.css';
import PublicationCard from './components/publicationCard';
import PublicationModal from './components/publication/publicationModal';

export default function AllPublicationsPage() {
  const [publications, setPublications] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [loading, setLoading] = useState(false);
  const [selectedPublicationId, setSelectedPublicationId] = useState(null);
  const [filter, setFilter] = useState('all');
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const observer = useRef();
  
  const getValidToken = () => {
    const token = localStorage.getItem('token');
    return token && token !== 'undefined' && token.trim() !== '' ? token : null;
  };
  
  useEffect(() => {
    const token = getValidToken();
    setIsAuthenticated(!!token);
  }, []);
  
  useEffect(() => {
    setPageNumber(1);
    setPublications([]);
  }, [filter]);

  useEffect(() => {
    fetchPublications(pageNumber, filter);
  }, [pageNumber, filter]);
  
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

  const fetchPublications = async (page, filterType) => {
    setLoading(true);
    try {
      const token = getValidToken();
      let endpoint = '/publication';
      const headers = {};

      if (filterType === 'subscriptions' && token) {
        endpoint = '/publication/followings';
        headers.Authorization = `Bearer ${token}`;
      }

      const res = await api.get(endpoint, {
        params: { pageNumber: page, pageSize: 12 },
        headers
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
      console.error('Error loading publications:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
      <>
        {isAuthenticated && (
            <div className="publication-filter-buttons">
              <button
                  onClick={() => setFilter('all')}
                  className={filter === 'all' ? 'active-filter' : ''}
              >
                All
              </button>
              <button
                  onClick={() => setFilter('subscriptions')}
                  className={filter === 'subscriptions' ? 'active-filter' : ''}
              >
                Subscriptions
              </button>
            </div>
        )}

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
