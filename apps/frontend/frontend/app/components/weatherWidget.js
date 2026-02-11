"use client";

import { useState, useEffect } from 'react';
import styles from '../styles/WeatherWidget.module.css';

const WeatherWidget = () => {
  const [currentDate, setCurrentDate] = useState('');
  const [isExpanded, setIsExpanded] = useState(false);
  const [isCompact, setIsCompact] = useState(false);

  useEffect(() => {
    const today = new Date();
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    setCurrentDate(today.toLocaleDateString(undefined, options));

    const handleResize = () => {
      setIsCompact(window.innerWidth <= 768);
    };

    handleResize();
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  return (
    <div className={`${styles['weather-widget']} ${isCompact && !isExpanded ? styles.compact : ''}`}>
      <div className={styles['weather-header']}>
        {(!isCompact || isExpanded) && <h2>Weather</h2>}
        <button className={styles.iconButton} onClick={() => setIsExpanded(!isExpanded)}>
          <img src="/sunny.svg" alt="Weather Widget Icon" />
        </button>
      </div>

      {(!isCompact || isExpanded) && (
        <>
          <div className={styles['weather-main']}>
            <p className={styles.day}>Today</p>
            <p className={styles.temp}>25°</p>
            <p className={styles.condition}>Sunny</p>
            <p className={styles.location}>California, Los Angeles</p>
            <p className={styles.date}>{currentDate}</p>
          </div>

          <div className={styles['weather-forecast']}>
            <div className={styles['forecast-item']}>
              <p>Now</p>
              <p>25°</p>
            </div>
            <div className={styles['forecast-item']}>
              <p>2 AM</p>
              <p>23°</p>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default WeatherWidget;

