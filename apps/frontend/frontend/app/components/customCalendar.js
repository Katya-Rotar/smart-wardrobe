"use client";

import React, { useState, useEffect } from "react";
import Calendar from "react-calendar";
import '../styles/Calendar.css';
import styles from "../styles/CustomCalendar.module.css";

const CustomCalendar = () => {
  const [date, setDate] = useState(null);
  const [isCompact, setIsCompact] = useState(false);
  const [isExpanded, setIsExpanded] = useState(false);
  
  useEffect(() => {
    const handleResize = () => {
      setIsCompact(window.innerWidth <= 768);
    };

    setDate(new Date());

    handleResize();
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  if (!date) return null;

  return (
    <div className={`${styles.calendarContainer} ${isCompact && !isExpanded ? styles.compact : ''}`}>

      <header className={styles.header}>
        {(!isCompact || isExpanded) && <h2>Calendar</h2>}
        <button className={styles.iconButton} onClick={() => setIsExpanded(!isExpanded)}>
          <img src="/calendar.svg" alt="Calendar Icon" />
        </button>
      </header>

      {(!isCompact || isExpanded) && (
        <>
          <div className={styles.controls}>
            <button className={styles.dateButton}>
              <span>{date.toLocaleString("default", { day:"numeric", month: "short", year: "numeric" })}</span>
            </button>
            <a href="#" className={styles.viewLink}>
              View
            </a>
          </div>

          <Calendar
            onChange={setDate}
            value={date}
            className={styles.customCalendar}
          />
        </>
      )}
    </div>
  );
};

export default CustomCalendar;
