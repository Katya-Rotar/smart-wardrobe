"use client";

import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import api from "../../../api/api";
import styles from "../../styles/generate.module.css";

const GenerateCapsulePage = () => {
    const router = useRouter();
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [capsuleData, setCapsuleData] = useState(null);

    useEffect(() => {
        async function startGeneration() {
            try {
                const response = await api.post("/outfit/generate-capsule");
                setCapsuleData(response.data);
            } catch (error) {
                console.error("Генерація провалена:", error);
                alert("Помилка генерації. Перевірте свій гардероб.");
                router.push("/outfit");
            } finally {
                setLoading(false);
            }
        }
        startGeneration();
    }, [router]);

    const handleSaveAll = async () => {
        if (!capsuleData || !capsuleData.outfits || !capsuleData.outfits.length) return;
        setSaving(true);
        try {
            await api.post("/outfit/save-multiple", { outfits: capsuleData.outfits });
            alert("🎉 Смарт-капсулу та всі її образи успішно збережено!");
            router.push("/outfit");
        } catch (error) {
            console.error(error);
            alert("Не вдалося зберегти образи.");
        } finally {
            setSaving(false);
        }
    };

    const findGarmentById = (id) => {
        if (!capsuleData || !capsuleData.categorizedGarments) return null;
        for (const category in capsuleData.categorizedGarments) {
            const item = capsuleData.categorizedGarments[category].find(g => g.id === id);
            if (item) return item;
        }
        return null;
    };

    // Отримуємо плаский масив усіх базових речей для виведення в секції гардероба
    const allGarments = capsuleData?.categorizedGarments
        ? Object.values(capsuleData.categorizedGarments).flat()
        : [];

    if (loading) {
        return (
            <div className={styles.loaderContainer}>
                <div className={styles.spinner}></div>
                <h2>🤖 AI формує ваш капсульний гардероб...</h2>
                <p>Поєднуємо математичні кластери K-Means та аналізуємо індекси універсальності речей.</p>
            </div>
        );
    }

    return (
        <div className={styles.page}>
            <div className={styles.headerArea}>
                <button className={styles.backButton} onClick={() => router.push("/outfit")}>← До шафи</button>
                <div className={styles.titleWrapper}>
                    <h1>✨ Ваша Персональна Smart-Капсула</h1>
                    <p>Згенеровано {capsuleData?.outfits?.length || 0} унікальних образів із {allGarments.length} базових елементів</p>
                </div>
                <button className={styles.saveButton} onClick={handleSaveAll} disabled={saving || !capsuleData?.outfits?.length}>
                    {saving ? "Збереження..." : "Зберегти всі образи в шафу"}
                </button>
            </div>

            {/* СЕКЦІЯ ОБРАЗІВ */}
            <section className={styles.section}>
                <div className={styles.sectionHeader}>
                    <h2>Спільні комбінації гардероба</h2>
                    <span className={styles.countBadge}>{capsuleData?.outfits?.length || 0} образів</span>
                </div>

                <div className={styles.outfitsGrid}>
                    {capsuleData?.outfits?.map((outfit, index) => (
                        <div key={index} className={styles.generatedOutfitCard}>
                            <div className={styles.outfitCardHeader}>
                                <h3>Образ #{index + 1}</h3>
                                <span className={styles.outfitStyleBadge}>Синергія кластерів</span>
                            </div>
                            <div className={styles.outfitItemsPreview}>
                                {outfit.clothingItemIDs.map((itemId) => {
                                    const item = findGarmentById(itemId);
                                    if (!item) return null;
                                    return (
                                        <div key={itemId} className={styles.miniItemCard}>
                                            <div className={styles.imageWrapper}>
                                                <img src={item.imageURL || "/placeholder.png"} alt={item.typeName} className={item.categoryName === "Shoes" ? styles.shoeImg : styles.garmentImg} />
                                                <div className={styles.miniColorDot} style={{ backgroundColor: item.color }} />
                                            </div>
                                            <div className={styles.miniItemDetails}>
                                                <span className={styles.miniTypeName}>{item.typeName}</span>
                                                <span className={styles.miniCategoryName}>{item.categoryName}</span>
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    ))}
                </div>
            </section>

            <hr className={styles.divider} />

            {/* СЕКЦІЯ РЕЧЕЙ */}
            <section className={styles.section}>
                <div className={styles.sectionHeader}>
                    <h2>Базовий фундамент капсули</h2>
                    <span className={styles.countBadge}>{allGarments.length} речей</span>
                </div>
                <div className={styles.garmentsGrid}>
                    {allGarments.map((garment) => (
                        <div key={garment.id} className={styles.garmentCard}>
                            <div className={styles.cardImageSection}>
                                <img src={garment.imageURL || "/placeholder.png"} alt={garment.typeName} className={styles.cardMainImg} />
                                <div className={styles.colorIndicatorBadge} style={{ backgroundColor: garment.color }} />
                            </div>
                            <div className={styles.garmentInfo}>
                                <div className={styles.cardTitleRow}>
                                    <h4>{garment.typeName}</h4>
                                    <span className={styles.categoryLabel}>{garment.categoryName}</span>
                                </div>
                                <div className={styles.badgeRow}>
                                    <span className={styles.clusterBadge}>Кластер {garment.clusterId}</span>
                                    <span className={styles.indexBadge}>🎯 Базовість: {Math.round(garment.universalityIndex * 100)}%</span>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </section>
        </div>
    );
};

export default GenerateCapsulePage;