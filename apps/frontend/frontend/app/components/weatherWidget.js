"use client";

import { useState, useEffect } from "react";
import styles from "../styles/weatherWidget.module.css";

import {
    Sun,
    Cloud,
    CloudRain,
    Snowflake,
    CloudLightning,
    Search,
    Pencil
} from "lucide-react";

const WeatherWidget = () => {
    const [weather, setWeather] = useState(null);
    const [forecast, setForecast] = useState([]);
    const [location, setLocation] = useState("");
    const [searchCity, setSearchCity] = useState("");
    const [suggestions, setSuggestions] = useState([]);
    const [currentDate, setCurrentDate] = useState("");
    const [isExpanded, setIsExpanded] = useState(false);
    const [isCompact, setIsCompact] = useState(false);
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {
        const today = new Date();
        setCurrentDate(
            today.toLocaleDateString(undefined, {
                year: "numeric",
                month: "short",
                day: "numeric",
            })
        );

        const handleResize = () => {
            setIsCompact(window.innerWidth <= 768);
        };

        handleResize();
        window.addEventListener("resize", handleResize);

        // 🔥 перевірка localStorage
        const saved = localStorage.getItem("selectedCity");

        if (saved) {
            const city = JSON.parse(saved);
            setLocation(city.name);
            fetchWeather(city.lat, city.lon);
        } else {
            fetchWeatherByIP();
        }

        return () => window.removeEventListener("resize", handleResize);
    }, []);

    // 📍 Поточна локація (fallback)
    const fetchWeatherByIP = async () => {
        const res = await fetch("https://ipapi.co/json/");
        const data = await res.json();
        setLocation(`${data.city}, ${data.country_name}`);
        fetchWeather(data.latitude, data.longitude);
    };

    // 🌦 Погода
    const fetchWeather = async (lat, lon) => {
        const res = await fetch(
            `https://api.open-meteo.com/v1/forecast?latitude=${lat}&longitude=${lon}&current_weather=true&daily=temperature_2m_max,temperature_2m_min,weathercode&timezone=auto`
        );
        const data = await res.json();

        setWeather(data.current_weather);

        localStorage.setItem("lastWeatherData", JSON.stringify({
            temp: data.current_weather.temperature,
            code: data.current_weather.weathercode
        }));

        const days = data.daily.time.map((day, i) => ({
            date: day,
            max: data.daily.temperature_2m_max[i],
            min: data.daily.temperature_2m_min[i],
            code: data.daily.weathercode[i],
        }));

        const weatherInfo = {
            temp: data.current_weather.temperature,
            code: data.current_weather.weathercode
        };

        localStorage.setItem("lastWeatherData", JSON.stringify(weatherInfo));
        
        window.dispatchEvent(new Event("weatherUpdated"));

        setForecast(days.slice(0, 5));
    };

    // 🔍 Пошук міста
    const handleSearch = async (value) => {
        setSearchCity(value);

        if (value.length < 2) {
            setSuggestions([]);
            return;
        }

        const res = await fetch(
            `https://nominatim.openstreetmap.org/search?format=json&addressdetails=1&limit=5&q=${value}`
        );
        const data = await res.json();
        setSuggestions(data);
    };

    // 🧠 Формат: місто, район, країна
    const formatCityName = (city) => {
        const name =
            city.address?.city ||
            city.address?.town ||
            city.address?.village ||
            city.address?.hamlet;

        const district =
            city.address?.county ||
            city.address?.state_district ||
            city.address?.region;

        const country = city.address?.country;

        return [name, district, country]
            .filter(Boolean)
            .join(", ");
    };

    // 📌 Вибір міста + збереження
    const selectCity = (city) => {
        const formatted = formatCityName(city);

        const savedCity = {
            name: formatted,
            lat: city.lat,
            lon: city.lon,
        };

        localStorage.setItem("selectedCity", JSON.stringify(savedCity));

        setLocation(formatted);
        fetchWeather(city.lat, city.lon);

        setIsEditing(false);
        setSearchCity("");
        setSuggestions([]);
    };

    // ☀️ Іконки
    const getWeatherIcon = (code) => {
        if (code === 0) return <Sun size={18} />;
        if (code === 1 || code === 2) return <Cloud size={18} />;
        if (code === 3) return <Cloud size={18} />;
        if (code >= 51 && code <= 65) return <CloudRain size={18} />;
        if (code >= 71) return <Snowflake size={18} />;
        if (code >= 95) return <CloudLightning size={18} />;
        return <Cloud size={18} />;
    };

    return (
        <div
            className={`${styles["weather-widget"]} ${
                isCompact && !isExpanded ? styles.compact : ""
            }`}
        >
            {/* HEADER */}
            <div className={styles["weather-header"]}>
                {(!isCompact || isExpanded) && <h2>Weather</h2>}

                <button
                    className={styles.iconButton}
                    onClick={() => setIsExpanded(!isExpanded)}
                >
                    <Sun size={16} />
                </button>
            </div>

            {/* MAIN */}
            {weather && (!isCompact || isExpanded) && (
                <>
                    <div className={styles["weather-main"]}>
                        <p className={styles.day}>Today</p>

                        <p className={styles.temp}>
                            {Math.round(weather.temperature)}°
                        </p>

                        <p className={styles.condition}>
                            Wind: {weather.windspeed} km/h
                        </p>

                        <p className={styles.location}>
                            {location}

                            <button
                                className={styles.editButton}
                                onClick={() => setIsEditing(!isEditing)}
                            >
                                <Pencil size={12} />
                            </button>
                        </p>

                        <p className={styles.date}>{currentDate}</p>

                        {/* SEARCH */}
                        {isEditing && (
                            <div className={styles.searchContainer}>
                                <div className={styles.searchBox}>
                                    <Search size={14} />

                                    <input
                                        type="text"
                                        placeholder="Enter city..."
                                        value={searchCity}
                                        onChange={(e) => handleSearch(e.target.value)}
                                        className={styles.cityInput}
                                    />
                                </div>

                                {suggestions.length > 0 && (
                                    <div className={styles.suggestions}>
                                        {suggestions.map((city, i) => (
                                            <div
                                                key={i}
                                                className={styles.suggestionItem}
                                                onClick={() => selectCity(city)}
                                                title={formatCityName(city)} // 👈 підказка при hover
                                            >
                                                {formatCityName(city)}
                                            </div>
                                        ))}
                                    </div>
                                )}
                            </div>
                        )}
                    </div>

                    {/* FORECAST */}
                    <div className={styles["weather-forecast"]}>
                        {forecast.map((day, i) => (
                            <div key={i} className={styles["forecast-item"]}>
                                <p>
                                    {new Date(day.date).toLocaleDateString(undefined, {
                                        weekday: "short",
                                    })}
                                </p>

                                <p>{getWeatherIcon(day.code)}</p>

                                <p>{Math.round(day.max)}°</p>

                                <p className={styles.minTemp}>
                                    {Math.round(day.min)}°
                                </p>
                            </div>
                        ))}
                    </div>
                </>
            )}
        </div>
    );
};

export default WeatherWidget;

