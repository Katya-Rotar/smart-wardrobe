'use client';
import React, { useEffect, useState } from 'react';
import api from '../../../api/api';
import { FaHeart, FaRegHeart, FaPaperPlane } from 'react-icons/fa';

function getValidToken() {
    const token = localStorage.getItem('token');
    return token && token !== 'undefined' && token.trim() !== '' ? token : null;
}

export default function CommentSection({ publicationId }) {
    const [comments, setComments] = useState([]);
    const [likes, setLikes] = useState({});
    const [likeCounts, setLikeCounts] = useState({});
    const [text, setText] = useState('');
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const token = getValidToken();
        setIsAuthenticated(!!token);
        fetchComments(token);
    }, [publicationId]);

    const fetchComments = async (token) => {
        try {
            const res = await api.get(`/comments/publication/${publicationId}`);
            setComments(res.data);

            const likesData = {};
            const countsData = {};

            await Promise.all(res.data.map(async (c) => {
                try {
                    const countRes = await api.get(`/commentlikes/${c.id}/count`);
                    countsData[c.id] = countRes.data;
                } catch (e) {
                    countsData[c.id] = 0;
                }

                if (token) {
                    try {
                        const likeRes = await api.get(`/commentlikes/is-liked?commentId=${c.id}`, {
                            headers: { Authorization: `Bearer ${token}` }
                        });
                        likesData[c.id] = likeRes.data;
                    } catch (e) {
                        likesData[c.id] = false;
                    }
                }
            }));

            setLikes(likesData);
            setLikeCounts(countsData);
        } catch (err) {
            console.error('Error loading comments:', err);
        }
    };

    const toggleLike = async (commentId) => {
        const token = getValidToken();
        if (!token) return alert("Увійдіть, щоб ставити лайки");

        const isLiked = likes[commentId];
        try {
            if (isLiked) {
                await api.delete('/commentlikes', {
                    data: { commentID: commentId },
                    headers: { Authorization: `Bearer ${token}` }
                });
                setLikeCounts((prev) => ({ ...prev, [commentId]: (prev[commentId] || 1) - 1 }));
            } else {
                await api.post('/commentlikes', { commentID: commentId }, {
                    headers: { Authorization: `Bearer ${token}` }
                });
                setLikeCounts((prev) => ({ ...prev, [commentId]: (prev[commentId] || 0) + 1 }));
            }
            setLikes({ ...likes, [commentId]: !isLiked });
        } catch (err) {
            console.error('Error toggling like:', err);
        }
    };

    const addComment = async () => {
        const token = getValidToken();
        if (!token) return alert("Авторизуйтесь, щоб додати коментар");

        try {
            await api.post('/comments', { publicationID: publicationId, content: text }, {
                headers: { Authorization: `Bearer ${token}` }
            });
            setText('');
            fetchComments(token);
        } catch (err) {
            console.error("Error adding comment:", err);
        }
    };

    return (
        <div className="comment-section">
            <div className="comment-header">
                💬 {comments.length}
            </div>

            <div className="comment-list">
                {comments.map((c) => (
                    <div key={c.id} className="comment">
                        <img className="comment-avatar" src={c.profileImage || '/default-avatar.png'} alt="user" />
                        <div className="comment-body">
                            <div className="comment-header-row">
                                <span className="comment-username">{c.username}</span>
                                <span className="comment-date">{new Date(c.createdAt).toLocaleDateString()}</span>
                            </div>
                            <div className="comment-content">{c.content}</div>
                            <button
                                className={`comment-like ${likes[c.id] ? 'liked' : ''}`}
                                onClick={() => toggleLike(c.id)}
                            >
                                {likes[c.id] ? <FaHeart /> : <FaRegHeart />} {likeCounts[c.id] || 0}
                            </button>
                        </div>
                    </div>
                ))}
            </div>

            <div className="comment-input-container">
                <input
                    value={text}
                    onChange={(e) => setText(e.target.value)}
                    placeholder="Add a comment..."
                />
                <button onClick={addComment}>
                    <FaPaperPlane />
                </button>
            </div>
        </div>
    );
}
