'use client';

import ItemCard from "../../components/wardrobe/itemCard";
import React, { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import api from '../../../api/api';
import CreatePublicationModal from '../../components/createPublicationModal';
// 👈 1. ІМПОРТУЄМО СТИЛІ ЯК МОДУЛЬ
import styles from '../../styles/outfitDetails.module.css';
// Іконки для інтерфейсу
import { FaEdit, FaTrashAlt, FaShareSquare, FaTshirt } from 'react-icons/fa';

const OutfitDetailsPage = () => {
    const { id } = useParams();
    const router = useRouter();

    const [outfit, setOutfit] = useState(null);
    const [loading, setLoading] = useState(true);
    const [deleting, setDeleting] = useState(false);
    const [showModal, setShowModal] = useState(false);

    const handleOpenModal = () => setShowModal(true);
    const handleCloseModal = () => setShowModal(false);

    const handlePublish = async (dto) => {
        try {
            const response = await api.post('/publication', dto);
            const id = response.data.id;
            router.push(`/publication/${id}`);
        } catch (error) {
            console.error('Failed to publish:', error);
            alert('Failed to publish.');
        } finally {
            handleCloseModal();
        }
    };

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
            router.push('/outfit');
        } catch (error) {
            console.error('Failed to delete outfit:', error);
            alert('Failed to delete outfit.');
        } finally {
            setDeleting(false);
        }
    };

    if (loading) return <div className={styles.centerStatus}>Завантаження деталей образу...</div>;
    if (!outfit) return <div className={styles.centerStatus}>Образ не знайдено.</div>;

    return (
        <div className={styles.container}>
            {/* Верхня частина: Заголовок та Кнопки управління */}
            <div className={styles.headerSection}>
                <h1 className={styles.title}>Outfit Details</h1>

                <div className={styles.actionButtons}>
                    <button onClick={handleEdit} className={`${styles.btn} ${styles.btnEdit}`}>
                        <FaEdit /> Edit Outfit
                    </button>
                    <button onClick={handleDelete} disabled={deleting} className={`${styles.btn} ${styles.btnDelete}`}>
                        <FaTrashAlt /> {deleting ? 'Deleting...' : 'Delete Outfit'}
                    </button>
                    <button onClick={handleOpenModal} className={`${styles.btn} ${styles.btnPublish}`}>
                        <FaShareSquare /> Create Publication
                    </button>
                </div>
            </div>

            {/* Блок метаданих: Стилі, Сезони, Температура */}
            <div className={styles.metaGrid}>
                <div className={styles.metaCard}>
                    <span className={styles.metaLabel}>🌡️ Temperature suitability</span>
                    <span className={styles.metaValue}>{outfit.temperatureSuitabilityName}</span>
                </div>

                <div className={styles.metaCard}>
                    <span className={styles.metaLabel}>✨ Styles</span>
                    <span className={styles.metaValue}>
                        {outfit.styleNames && outfit.styleNames.length > 0
                            ? outfit.styleNames.join(', ')
                            : 'Не вказано'}
                    </span>
                </div>

                <div className={styles.metaCard}>
                    <span className={styles.metaLabel}>📅 Seasons</span>
                    <span className={styles.metaValue}>
                        {outfit.seasonNames && outfit.seasonNames.length > 0
                            ? outfit.seasonNames.join(', ')
                            : 'Не вказано'}
                    </span>
                </div>

                <div className={styles.metaCard}>
                    <span className={styles.metaLabel}>👥 Groups</span>
                    <span className={styles.metaValue}>
                        {outfit.groupNames && outfit.groupNames.length > 0
                            ? outfit.groupNames.join(', ')
                            : 'Не вказано'}
                    </span>
                </div>
            </div>

            {/* Окремий блок для красивих тегів */}
            {outfit.tags && outfit.tags.length > 0 && (
                <div style={{ marginBottom: '30px' }}>
                    <span className={styles.metaLabel} style={{ display: 'block', marginBottom: '8px' }}>🏷️ Tags</span>
                    <div>
                        {outfit.tags.map((tag, idx) => (
                            <span key={idx} className={styles.tagBadge}>#{tag}</span>
                        ))}
                    </div>
                </div>
            )}

            {/* Нижня частина: Сітка елементів одягу */}
            <div className={styles.itemsSection}>
                <h2 className={styles.sectionTitle}>
                    <FaTshirt /> Items in this outfit:
                </h2>
                <div className={styles.itemsGrid}>
                    {outfit.itemNames && outfit.itemNames.map(item => (
                        <ItemCard
                            key={item.id}
                            id={item.id}
                            name={item.name}
                            image={item.imageURL}
                        />
                    ))}
                </div>
            </div>

            {/* Модалка створення публікації */}
            {showModal && (
                <CreatePublicationModal
                    outfitId={outfit.id}
                    tags={outfit.tags}
                    onClose={handleCloseModal}
                    onPublish={handlePublish}
                />
            )}
        </div>
    );
};

export default OutfitDetailsPage;