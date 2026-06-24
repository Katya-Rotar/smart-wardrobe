"use client";

import React, { useEffect, useState } from "react";
import OutfitCard from "../../app/components/outfit/outfitCard";
import Link from "next/link";
import api from "../../api/api";
import styles from "./outfits.module.css";
import { useRouter } from "next/navigation";

const OutfitsPage = () => {
  const [outfits, setOutfits] = useState([]);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  const [stylesList, setStyles] = useState([]);
  const [seasons, setSeasons] = useState([]);
  const [temps, setTemps] = useState([]);

  const [selectedStyle, setSelectedStyle] = useState("");
  const [selectedSeason, setSelectedSeason] = useState("");
  const [selectedTemp, setSelectedTemp] = useState("");

  useEffect(() => {
    async function fetchReferenceData() {
      try {
        const [styleRes, seasonRes, tempRes] = await Promise.all([
          api.get("/style"),
          api.get("/season"),
          api.get("/temperatureSuitability"),
        ]);
        setStyles(styleRes.data);
        setSeasons(seasonRes.data);
        setTemps(tempRes.data);
      } catch (error) {
        console.error(error);
      }
    }
    fetchReferenceData();
  }, []);

  useEffect(() => {
    async function fetchOutfits() {
      setLoading(true);
      try {
        const params = {
          ...(selectedStyle && { StyleName: selectedStyle }),
          ...(selectedSeason && { SeasonName: selectedSeason }),
          ...(selectedTemp && { TemperatureSuitabilityName: selectedTemp }),
        };
        const response = await api.get("/outfit", { params });
        setOutfits(response.data || []);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    }
    fetchOutfits();
  }, [selectedStyle, selectedSeason, selectedTemp]);

  return (
      <div className={styles.page}>
        <h1 className={styles.title}>My Outfits</h1>

        {/* БАНЕР-КНОПКА ДЛЯ ПЕРЕХОДУ НА СТОРІНКУ AI ГЕНЕРАЦІЇ */}
        <div className={styles.aiBanner}>
          <div className={styles.aiBannerText}>
            <h3>🤖 Розумний підбір гардероба</h3>
            <p>Дозвольте штучному інтелекту згрупувати ваші речі в ідеальні капсули за допомогою алгоритму K-Means.</p>
          </div>
          <button
              className={styles.generateNavButton}
              onClick={() => router.push("/outfit/generate")}
          >
            ✨ Згенерувати капсулу
          </button>
        </div>

        {/* ТВОЇ ФІЛЬТРИ */}
        <div className={styles.filters}>
          <div className={styles.filter}>
            <label>Style</label>
            <select value={selectedStyle} onChange={(e) => setSelectedStyle(e.target.value)}>
              <option value="">All</option>
              {stylesList.map((style) => (
                  <option key={style.id} value={style.styleName}>{style.styleName}</option>
              ))}
            </select>
          </div>

          <div className={styles.filter}>
            <label>Season</label>
            <select value={selectedSeason} onChange={(e) => setSelectedSeason(e.target.value)}>
              <option value="">All</option>
              {seasons.map((season) => (
                  <option key={season.id} value={season.seasonName}>{season.seasonName}</option>
              ))}
            </select>
          </div>

          <div className={styles.filter}>
            <label>Temperature</label>
            <select value={selectedTemp} onChange={(e) => setSelectedTemp(e.target.value)}>
              <option value="">All</option>
              {temps.map((temp) => (
                  <option key={temp.id} value={temp.temperatureSuitabilityName}>{temp.temperatureSuitabilityName}</option>
              ))}
            </select>
          </div>
        </div>

        <Link href="/outfit/add" className={styles.addButton}>+</Link>

        {loading ? (
            <p className={styles.state}>Loading...</p>
        ) : outfits.length === 0 ? (
            <p className={styles.state}>No outfits found</p>
        ) : (
            <div className={styles.grid}>
              {outfits.map((outfit) => (
                  <OutfitCard key={outfit.id} outfit={outfit} />
              ))}
            </div>
        )}
      </div>
  );
};

export default OutfitsPage;