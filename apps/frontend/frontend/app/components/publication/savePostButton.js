'use client';
import React, { useEffect, useState } from 'react';
import { FaBookmark, FaRegBookmark } from 'react-icons/fa';
import api from '../../../api/api';

function getValidToken() {
    const token = localStorage.getItem('token');
    return token && token !== 'undefined' && token.trim() !== '' ? token : null;
}

export default function SavePostButton({ publicationId }) {
    const [saved, setSaved] = useState(false);

    useEffect(() => {
        const token = getValidToken();
        if (!token) return;

        api.get(`/savedposts/is-saved?publicationId=${publicationId}`, {
            headers: { Authorization: `Bearer ${token}` }
        })
            .then(res => setSaved(res.data))
            .catch(err => console.warn('Cannot check saved state', err));
    }, [publicationId]);

    const toggleSave = async () => {
        const token = getValidToken();
        if (!token) return alert("Увійдіть, щоб зберегти пост.");

        try {
            if (saved) {
                await api.delete('/savedposts', {
                    data: { publicationID: publicationId },
                    headers: { Authorization: `Bearer ${token}` }
                });
                setSaved(false);
            } else {
                await api.post('/savedposts', { publicationID: publicationId }, {
                    headers: { Authorization: `Bearer ${token}` }
                });
                setSaved(true);
            }
        } catch (err) {
            console.error("Error toggling save", err);
        }
    };

    return (
        <button
            className="save-button-icon"
            onClick={toggleSave}
            title={saved ? "Прибрати зі збережених" : "Зберегти пост"}
        >
            {saved ? <FaBookmark /> : <FaRegBookmark />}
        </button>
    );
}
