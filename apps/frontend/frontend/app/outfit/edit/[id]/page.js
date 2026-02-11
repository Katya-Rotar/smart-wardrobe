'use client';

import { useParams } from 'next/navigation';
import { useEffect, useState } from 'react';
import CreateOutfitPage from "@/app/outfit/add/page";
import api from "@/api/api";

export default function EditOutfitPage() {
    const { id } = useParams();
    const [userId, setUserId] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUserId = async () => {
            try {
                const response = await api.get('/User/user-id-from-token');
                setUserId(response.data); // або `response.data.userId`, якщо бек повертає об'єкт
            } catch (error) {
                console.error('Failed to fetch userId from token', error);
            } finally {
                setLoading(false);
            }
        };

        fetchUserId();
    }, []);

    if (loading) {
        return <div>Завантаження...</div>;
    }

    if (!userId) {
        return <div>Користувач не авторизований</div>;
    }

    return <CreateOutfitPage outfitId={id} userId={userId} />;
}
