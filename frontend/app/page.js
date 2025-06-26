'use client';
import { useEffect, useState, useRef, useCallback } from 'react';
import api from '../api/api';
import './publicationsPage.css';
import PublicationCard from './components/publicationCard';

export default function AllPublicationsPage() {
  const [publications, setPublications] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [loading, setLoading] = useState(false);

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
    fetchPublications(pageNumber);
  }, [pageNumber]);

  const fetchPublications = async (page) => {
    setLoading(true);
    try {
      const res = await api.get('/publication', {
        params: {
          pageNumber: page,
          pageSize: 12
        }
      });

      const data = res.data;
      const newItems = data.items || data;

      setPublications(prev => [...prev, ...newItems]);
      setHasMore(newItems.length > 0);
    } catch (err) {
      console.error('Error loading publications:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
      <div className="publication-grid">
        {publications.map((pub, index) => {
          if (index === publications.length - 1) {
            return (
                <div ref={lastPublicationRef} key={pub.id}>
                  <PublicationCard imageURL={pub.imageURL} tags={pub.tags} />
                </div>
            );
          } else {
            return (
                <PublicationCard key={pub.id} imageURL={pub.imageURL} tags={pub.tags} />
            );
          }
        })}
      </div>
  );
}
