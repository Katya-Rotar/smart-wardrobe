'use client';

import React, { useEffect, useState } from 'react';
import OutfitCard from '../../app/components/outfit/outfitCard';
import Link from "next/link";
import api from '../../api/api';

const OutfitsPage = () => {
  const [outfits, setOutfits] = useState([]);
  const [loading, setLoading] = useState(true);

  const [styles, setStyles] = useState([]);
  const [seasons, setSeasons] = useState([]);
  const [temps, setTemps] = useState([]);

  const [selectedStyle, setSelectedStyle] = useState('');
  const [selectedSeason, setSelectedSeason] = useState('');
  const [selectedTemp, setSelectedTemp] = useState('');

  // Завантаження довідкових даних
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
        console.error("Failed to fetch reference data:", error);
      }
    }

    fetchReferenceData();
  }, []);

  // Завантаження образів (фільтровано)
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
        console.error("Failed to load outfits:", error);
      } finally {
        setLoading(false);
      }
    }

    fetchOutfits();
  }, [selectedStyle, selectedSeason, selectedTemp]);

  return (
      <div style={{ padding: '20px' }}>
        <h1>Outfits</h1>

        <div style={{ display: 'flex', gap: '20px', marginBottom: '20px' }}>
          <div>
            <label>Style:</label><br />
            <select value={selectedStyle} onChange={e => setSelectedStyle(e.target.value)}>
              <option value="">All</option>
              {styles.map(style => (
                  <option key={style.id} value={style.styleName}>{style.styleName}</option>
              ))}
            </select>
          </div>

          <div>
            <label>Season:</label><br />
            <select value={selectedSeason} onChange={e => setSelectedSeason(e.target.value)}>
              <option value="">All</option>
              {seasons.map(season => (
                  <option key={season.id} value={season.seasonName}>{season.seasonName}</option>
              ))}
            </select>
          </div>

          <div>
            <label>Temperature:</label><br />
            <select value={selectedTemp} onChange={e => setSelectedTemp(e.target.value)}>
              <option value="">All</option>
              {temps.map(temp => (
                  <option key={temp.id} value={temp.temperatureSuitabilityName}>
                    {temp.temperatureSuitabilityName}
                  </option>
              ))}
            </select>
          </div>
        </div>

        <Link href={`/outfit/add`} className="add-button">+</Link>

        {loading ? (
            <p>Loading...</p>
        ) : outfits.length === 0 ? (
            <p>No outfits found.</p>
        ) : (
            <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px' }}>
              {outfits.map(outfit => (
                  <OutfitCard key={outfit.id} outfit={outfit} />
              ))}
            </div>
        )}
      </div>
  );
};

export default OutfitsPage;
