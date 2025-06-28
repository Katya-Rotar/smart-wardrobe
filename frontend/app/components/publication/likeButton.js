import React, { useEffect, useState } from 'react';
import api from '../../../api/api';
import '../../styles/likeButton.css';

function getValidToken() {
    const token = localStorage.getItem('token');
    if (!token || token === 'undefined' || token.trim() === '') {
        return null;
    }
    return token;
}

export default function LikeButton({ publicationId }) {
    const [liked, setLiked] = useState(false);
    const [count, setCount] = useState(0);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const token = getValidToken();
        setIsAuthenticated(!!token);

        async function fetch() {
            try {
                const resCount = await api.get(`/postlikes/${publicationId}/count`);
                setCount(resCount.data);
            } catch (err) {
                console.error("Error fetching like count", err);
            }

            if (token) {
                try {
                    const resLiked = await api.get(`/postlikes/is-liked?publicationId=${publicationId}`, {
                        headers: { Authorization: `Bearer ${token}` },
                    });
                    setLiked(resLiked.data);
                } catch (err) {
                    console.warn("Error checking if liked", err);
                }
            } else {
                setLiked(false);
            }
        }

        fetch();
    }, [publicationId]);

    const toggleLike = async () => {
        const token = getValidToken();
        if (!token) {
            alert("Увійдіть, щоб поставити лайк.");
            return;
        }

        try {
            if (liked) {
                await api.delete(`/postlikes`, {
                    data: { publicationID: publicationId },
                    headers: { Authorization: `Bearer ${token}` },
                });
                setCount((c) => c - 1);
            } else {
                await api.post(
                    `/postlikes`,
                    { publicationID: publicationId },
                    {
                        headers: { Authorization: `Bearer ${token}` },
                    }
                );
                setCount((c) => c + 1);
            }
            setLiked(!liked);
        } catch (e) {
            if (e.response?.status === 401) {
                alert("Ви повинні бути авторизовані, щоб ставити лайки.");
            } else {
                console.error("Like toggle error", e);
                alert("Сталася помилка, спробуйте пізніше.");
            }
        }
    };

    return (
        <button
            className={`like-button-icon ${liked ? 'liked' : ''}`}
            onClick={toggleLike}
            title={isAuthenticated ? (liked ? 'Прибрати лайк' : 'Поставити лайк') : 'Авторизуйтеся'}
        >
            <span className="heart">{liked ? '❤️' : '🤍'}</span> {count}
        </button>
    );
}
