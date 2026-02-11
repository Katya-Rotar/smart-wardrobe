'use client';
import { useState, useEffect, useRef } from 'react';
import styles from "../styles/userProfile.module.css";
import LoginModal from '../components/login/loginModal';
import EventCard from './eventCard';
import api from '../../api/api';
import { useRouter } from 'next/navigation';

export default function UserProfile() {
    const [showNotifications, setShowNotifications] = useState(false);
    const [showProfile, setShowProfile] = useState(false);
    const [isCompact, setIsCompact] = useState(false);
    const [user, setUser] = useState(null);
    const [showLoginModal, setShowLoginModal] = useState(false);
    const dropdownRef = useRef(null);
    const router = useRouter();

    const fetchUser = async () => {
        const token = localStorage.getItem('token');
        if (!token) {
            setUser(null);
            return;
        }

        try {
            const response = await api.get('/User/user-info', {
                headers: { Authorization: `Bearer ${token}` }
            });
            setUser(response.data);
        } catch (error) {
            setUser(null);
        }
    };

    useEffect(() => {
        fetchUser();
    }, []);

    const handleLoginSuccess = () => {
        setShowLoginModal(false);
        fetchUser();
    };

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setShowNotifications(false);
                setShowProfile(false);
            }
        };

        const handleResize = () => {
            setIsCompact(window.innerWidth <= 768);
        };

        handleResize();

        window.addEventListener("resize", handleResize);
        document.addEventListener("mousedown", handleClickOutside);

        return () => {
            window.removeEventListener("resize", handleResize);
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, []);

    if (!user) {
        return (
            <div className={styles['user-profile']}>
                <button
                    className={styles['login-button']}
                    onClick={() => setShowLoginModal(true)}
                >
                    Увійти
                </button>

                {showLoginModal && (
                    <LoginModal
                        onClose={() => setShowLoginModal(false)}
                        onLoginSuccess={handleLoginSuccess}
                    />
                )}
            </div>
        );
    }

    return (
        <div className={`${styles['user-profile']} ${isCompact ? styles.compact : ''}`} ref={dropdownRef}>
            <button
                className={styles['icon-button']}
                onClick={() => {
                    setShowNotifications(!showNotifications);
                    setShowProfile(false);
                }}
            >
                <img src="/notifications.svg" alt="Notifications" className={styles.icon} />
            </button>

            <div className={styles['user-info']}>
                <img
                    src={user.profileImage ? user.profileImage : "/Image.png"}
                    alt={user.username}
                    className={styles['avatar']}
                />
                {!isCompact && (
                    <div className={styles['text-info']}>
                        <p className={styles['name']}>{user.username}</p>
                        <p className={styles['email']}>{user.email}</p>
                    </div>
                )}
            </div>

            <button
                className={styles['icon-button']}
                onClick={() => {
                    setShowProfile(!showProfile);
                    setShowNotifications(false);
                }}
            >
                <img src="/arrow_down.svg" alt="Arrow Down" className={styles.icon} />
            </button>

            {showNotifications && (
                <div className={`${styles['dropdown']} ${styles['notifications']}`}>
                    <EventCard />
                    <EventCard />
                </div>
            )}

            {showProfile && (
                <div className={`${styles['dropdown']} ${styles['profile']}`}>
                    <ul>
                        <li onClick={() => router.push('/settings')} className={styles['menu-item']}>
                            <img src="/settings.svg" alt="Settings" className={styles['menu-icon']} /> Settings
                        </li>
                        <li
                            className={styles['logout-item']}
                            onClick={() => {
                                localStorage.removeItem('token');
                                window.location.reload();
                            }}
                            style={{ cursor: 'pointer', display: 'flex', alignItems: 'center' }}
                        >
                            <img
                                src="/logout.svg"
                                alt="Sign out"
                                className={styles['menu-icon']}
                                style={{ marginRight: '8px' }}
                            />
                            Sign out
                        </li>
                    </ul>
                </div>
            )}
        </div>
    );
}
