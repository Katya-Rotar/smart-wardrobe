'use client';

import { useEffect, useState, useRef, useCallback } from 'react';
import { useParams } from 'next/navigation';
import api from '../../../api/api';
import PublicationCard from '@/app/components/publicationCard';
import PublicationModal from '@/app/components/publication/publicationModal';
import '../../publicationsPage.css';
import '../../styles/profile.css';

function getValidToken() {
    const token = localStorage.getItem('token');
    return token && token !== 'undefined' && token.trim() !== '' ? token : null;
}

export default function UserProfilePage() {
    const { userId } = useParams();
    const [user, setUser] = useState(null);
    const [followersCount, setFollowersCount] = useState(0);
    const [followingsCount, setFollowingsCount] = useState(0);
    const [isFollowing, setIsFollowing] = useState(false);
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isOwnProfile, setIsOwnProfile] = useState(false);
    const [publications, setPublications] = useState([]);
    const [selectedPublicationId, setSelectedPublicationId] = useState(null);
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
        const token = getValidToken();
        setIsAuthenticated(!!token);

        if (token && userId) {
            try {
                const decoded = JSON.parse(atob(token.split('.')[1]));
                setIsOwnProfile(parseInt(decoded.userId) === parseInt(userId));
            } catch {
                setIsOwnProfile(false);
            }
        }
    }, [userId]);

    useEffect(() => {
        if (userId) {
            setPublications([]);
            setPageNumber(1);
            fetchUserInfo();
            fetchUserPublications(1);
        }
    }, [userId]);

    useEffect(() => {
        if (pageNumber > 1 && userId) {
            fetchUserPublications(pageNumber);
        }
    }, [pageNumber]);

    const fetchUserInfo = async () => {
        try {
            const token = getValidToken();
            const userRes = await api.get(`/user/${userId}`);
            setUser(userRes.data);
            
            try {
                const followersRes = await api.get(`/followers/${userId}/followers-count`);
                setFollowersCount(followersRes.data);
            } catch {
                setFollowersCount(0);
            }

            try {
                const followingsRes = await api.get(`/followers/${userId}/following-count`);
                setFollowingsCount(followingsRes.data);
            } catch {
                setFollowingsCount(0);
            }
            
            if (token) {
                try {
                    const isFollowingRes = await api.get(`/followers/is-following`, {
                        params: { followingId: parseInt(userId) },
                        headers: { Authorization: `Bearer ${token}` }
                    });
                    setIsFollowing(isFollowingRes.data);
                } catch {
                    setIsFollowing(false);
                }
            }

        } catch (err) {
            console.error("User not found", err);
            setUser({ username: "User not found" }); // щоб не зависало
        }
    };

    const fetchUserPublications = async (page) => {
        setLoading(true);
        try {
            const res = await api.get(`/publication/user/${userId}`, {
                params: {
                    pageNumber: page,
                    pageSize: 12
                }
            });
            const data = res.data.items || res.data;

            setPublications(prev => {
                const ids = new Set(prev.map(pub => pub.id));
                const filtered = data.filter(pub => !ids.has(pub.id));
                return [...prev, ...filtered];
            });
            setHasMore(data.length > 0);
        } catch (err) {
            console.error('Failed to load publications', err);
        } finally {
            setLoading(false);
        }
    };

    const handleFollowToggle = async () => {
        const token = getValidToken();
        if (!token) return;

        try {
            const headers = {
                Authorization: `Bearer ${token}`
            };

            if (isFollowing) {
                // Відписка
                await api.delete('/followers', {
                    data: {
                        FollowingID: parseInt(userId) // ВАЖЛИВО: назва поля з великої літери
                    },
                    headers
                });
                setIsFollowing(false);
                setFollowersCount(prev => prev - 1);
            } else {
                // Підписка
                await api.post('/followers', {
                    FollowingID: parseInt(userId) // ВАЖЛИВО: назва поля з великої літери
                }, {
                    headers
                });
                setIsFollowing(true);
                setFollowersCount(prev => prev + 1);
            }
        } catch (err) {
            console.error('Failed to follow/unfollow', err);
        }
    };


    if (!user) return <div>Loading...</div>;

    return (
        <div className="profile-page">
            <div className="profile-header">
                <img src={user.profileImage || '/default-avatar.png'} alt="avatar" className="profile-avatar" />
                <div className="profile-info">
                    <h2>{user.username}</h2>
                    <div className="counts">
                        <span>{followersCount} followers </span>
                        <span>{followingsCount} follow </span>
                    </div>

                    {/* Кнопка показується тільки якщо авторизований і це не свій профіль */}
                    {isAuthenticated && !isOwnProfile && (
                        <button
                            className={`follow-btn ${isFollowing ? 'following' : 'not-following'}`}
                            onClick={handleFollowToggle}
                        >
                            {isFollowing ? 'Subscribed' : 'Subscribe'}
                        </button>
                    )}
                </div>
            </div>

            <div className="publication-grid">
                {publications.map((pub, index) => {
                    const card = (
                        <div className="publication-card" key={pub.id}>
                            <PublicationCard
                                imageURL={pub.imageURL}
                                tags={pub.tags}
                                onClick={() => setSelectedPublicationId(pub.id)}
                            />
                        </div>
                    );

                    return index === publications.length - 1
                        ? <div ref={lastPublicationRef} key={`last-${pub.id}`}>{card}</div>
                        : card;
                })}
            </div>

            {selectedPublicationId && (
                <PublicationModal
                    publicationId={selectedPublicationId}
                    onClose={() => setSelectedPublicationId(null)}
                />
            )}
        </div>
    );
}
