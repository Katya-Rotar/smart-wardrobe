'use client';

import { useState } from 'react';
import api from "../../../api/api";
import RegisterModal from "@/app/components/login/registerModal";
import styles from "../../styles/authModal.module.css";

export default function LoginModal({ onClose, onLoginSuccess }) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [showRegisterModal, setShowRegisterModal] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        try {
            const response = await api.post('/User/login', { email, password });

            localStorage.setItem('token', response.data.token);
            onLoginSuccess(response.data);
            onClose();
            window.location.reload();
        } catch {
            setError('Невірний email або пароль');
        }
    };

    return (
        <>
            {showRegisterModal ? (
                <RegisterModal onClose={() => setShowRegisterModal(false)} />
            ) : (
                <div className={styles.overlay}>
                    <div className={styles.modal}>
                        <button onClick={onClose} className={styles.closeBtn}>
                            ✕
                        </button>

                        <h2 className={styles.title}>Ласкаво просимо</h2>
                        <p className={styles.subtitle}>Увійдіть у свій акаунт</p>

                        <form onSubmit={handleSubmit} className={styles.form}>
                            <input
                                type="email"
                                placeholder="Email"
                                value={email}
                                onChange={e => setEmail(e.target.value)}
                                required
                            />

                            <input
                                type="password"
                                placeholder="Пароль"
                                value={password}
                                onChange={e => setPassword(e.target.value)}
                                required
                            />

                            <button type="submit" className={styles.primaryBtn}>
                                Увійти
                            </button>

                            {error && <p className={styles.error}>{error}</p>}

                            <button
                                type="button"
                                onClick={() => setShowRegisterModal(true)}
                                className={styles.secondaryBtn}
                            >
                                Створити акаунт
                            </button>
                        </form>
                    </div>
                </div>
            )}
        </>
    );
}