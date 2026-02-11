'use client';

import { useState } from 'react';
import api from "../../../api/api"
import RegisterModal from "@/app/components/login/registerModal";

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
        } catch (error) {
            setError('Невірний email або пароль');
        }
    };

    return (
        <>
            {showRegisterModal && (
                <RegisterModal onClose={() => setShowRegisterModal(false)} />
            )}

            <div style={{
                position: 'fixed',
                top: 0, left: 0, right: 0, bottom: 0,
                backgroundColor: 'rgba(0,0,0,0.5)',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                zIndex: 500
            }}>
                <div style={{
                    backgroundColor: 'white',
                    padding: '2rem',
                    borderRadius: '8px',
                    width: '300px',
                    position: 'relative'
                }}>
                    <button
                        onClick={onClose}
                        style={{
                            position: 'absolute',
                            top: '10px',
                            right: '10px',
                            background: 'transparent',
                            border: 'none',
                            fontSize: '1.5rem',
                            cursor: 'pointer',
                        }}
                        aria-label="Закрити"
                    >
                        &times;
                    </button>
                    <form onSubmit={handleSubmit}>
                        <h2>Увійти</h2>
                        <input
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={e => setEmail(e.target.value)}
                            required
                            style={{ width: '100%', marginBottom: '1rem', padding: '0.5rem' }}
                        />
                        <input
                            type="password"
                            placeholder="Пароль"
                            value={password}
                            onChange={e => setPassword(e.target.value)}
                            required
                            style={{ width: '100%', marginBottom: '1rem', padding: '0.5rem' }}
                        />
                        <button type="submit" style={{ width: '100%', padding: '0.5rem' }}>
                            Увійти
                        </button>
                        {error && <p style={{ color: 'red', marginTop: '1rem' }}>{error}</p>}
                        <button
                            type="button"
                            onClick={() => setShowRegisterModal(true)}
                            style={{
                                marginTop: '1rem',
                                width: '100%',
                                padding: '0.5rem',
                                backgroundColor: '#f0f0f0',
                                border: '1px solid #ccc',
                                cursor: 'pointer'
                            }}
                        >
                            Створити акаунт
                        </button>
                    </form>
                </div>
            </div>
        </>
    );
}
