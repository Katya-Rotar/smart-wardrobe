'use client';

import ItemCard from "../../components/wardrobe/itemCard";
import React, { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import api from '../../../api/api';

const OutfitDetailsPage = () => {
    const { id } = useParams();
    const router = useRouter();

    const [outfit, setOutfit] = useState(null);
    const [loading, setLoading] = useState(true);
    const [deleting, setDeleting] = useState(false);

    useEffect(() => {
        async function fetchOutfit() {
            setLoading(true);
            try {
                const response = await api.get(`/outfit/detail/${id}`);
                setOutfit(response.data);
            } catch (error) {
                console.error('Failed to load outfit details:', error);
            } finally {
                setLoading(false);
            }
        }

        if (id) {
            fetchOutfit();
        }
    }, [id]);

    const handleEdit = () => {
        router.push(`/outfit/edit/${id}`);
    };

    const handleDelete = async () => {
        const confirmed = window.confirm('Are you sure you want to delete this outfit?');
        if (!confirmed) return;

        setDeleting(true);
        try {
            await api.delete(`/outfit/${id}`);
            router.push('/outfit'); // ← або твоя сторінка зі списком образів
        } catch (error) {
            console.error('Failed to delete outfit:', error);
            alert('Failed to delete outfit.');
        } finally {
            setDeleting(false);
        }
    };

    if (loading) return <p>Loading...</p>;
    if (!outfit) return <p>Outfit not found.</p>;

    return (
        <div style={{ padding: '20px' }}>
            <h1>Outfit Details</h1>

            <div style={{ marginBottom: '20px' }}>
                <button onClick={handleEdit} style={{ marginRight: '10px' }}>Edit Outfit</button>
                <button onClick={handleDelete} disabled={deleting}>
                    {deleting ? 'Deleting...' : 'Delete Outfit'}
                </button>
            </div>

            <p><strong>Temperature suitability:</strong> {outfit.temperatureSuitabilityName}</p>
            <p><strong>Styles:</strong> {outfit.styleNames.join(', ')}</p>
            <p><strong>Seasons:</strong> {outfit.seasonNames.join(', ')}</p>
            <p><strong>Tags:</strong> {outfit.tags.join(', ')}</p>
            <p><strong>Groups:</strong> {outfit.groupNames.join(', ')}</p>

            <h2>Items:</h2>
            <div style={{ display: 'flex', gap: '20px', flexWrap: 'wrap' }}>
                {outfit.itemNames.map(item => (
                    <ItemCard
                        key={item.id}
                        id={item.id}
                        name={item.name}
                        image={item.imageURL}
                    />
                ))}
            </div>
        </div>
    );
};

export default OutfitDetailsPage;
