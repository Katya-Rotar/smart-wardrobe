'use client';
import { useState, useEffect } from 'react';
import api from '../../api/api';
import styles from '../../app/settings/settingsPage.module.css';

const CLOUD_NAME = 'ddapkpo6c';
const UPLOAD_PRESET = 'unsigned_preset';

async function uploadImageToCloudinary(file) {
    const url = `https://api.cloudinary.com/v1_1/${CLOUD_NAME}/upload`;
    const formData = new FormData();
    formData.append("file", file);
    formData.append("upload_preset", UPLOAD_PRESET);

    const response = await fetch(url, {
        method: "POST",
        body: formData,
    });

    if (!response.ok) {
        throw new Error("Не вдалося завантажити фото");
    }

    const data = await response.json();
    return data.secure_url;
}

export default function SettingsPage() {
    const [formData, setFormData] = useState({
        id: 0,
        username: '',
        email: '',
        profileImage: '',
    });
    const [password, setPassword] = useState(''); // окремо пароль, щоб не показувати хеш
    const [photoPreview, setPhotoPreview] = useState(null);
    const [photoFile, setPhotoFile] = useState(null);
    const [success, setSuccess] = useState(false);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const response = await api.get('/User/user-info');
                const data = response.data;
                setFormData({
                    id: data.id,
                    username: data.username,
                    email: data.email,
                    profileImage: data.profileImage,
                });
                setPhotoPreview(data.profileImage);
                setPassword(''); // пароль не показуємо
            } catch (error) {
                setError("Не вдалося завантажити дані користувача");
            }
        };
        fetchUserData();
    }, []);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handlePasswordChange = (e) => {
        setPassword(e.target.value);
    };

    const handlePhotoChange = (e) => {
        const file = e.target.files?.[0];
        if (file) {
            setPhotoFile(file);
            setPhotoPreview(URL.createObjectURL(file));
        }
    };

    const handleSave = async (e) => {
        e.preventDefault();
        setError('');
        try {
            let imageURL = formData.profileImage;
            if (photoFile) {
                imageURL = await uploadImageToCloudinary(photoFile);
            }

            // Формуємо об'єкт оновлення без поля passwordHash, якщо пароль не введено
            const updatedUser = {
                ...formData,
                profileImage: imageURL,
            };
            if (password.trim() !== '') {
                updatedUser.passwordHash = password.trim(); // передаємо новий пароль (бекенд його захешує)
            }

            await api.put(`/User/${formData.id}`, updatedUser);
            setSuccess(true);
            setTimeout(() => {
                window.location.href = '/';
            }, 2000);
        } catch (error) {
            setError('Помилка оновлення');
        }
    };

    const handleDeleteAccount = async () => {
        const confirmDelete = window.confirm("Ви впевнені, що хочете видалити акаунт?");
        if (!confirmDelete) return;

        try {
            await api.delete(`/User/${formData.id}`);
            localStorage.removeItem('token');
            window.location.href = '/';
        } catch (error) {
            setError('Помилка при видаленні акаунта');
        }
    };

    return (
        <div className={styles.settingsContainer}>
            <h2>Налаштування профілю</h2>
            <form onSubmit={handleSave} className={styles.form}>
                <label>Ім’я користувача</label>
                <input
                    type="text"
                    name="username"
                    value={formData.username}
                    onChange={handleChange}
                />
                <label>Email</label>
                <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                />
                <label>Пароль (залиште порожнім, щоб не змінювати)</label>
                <input
                    type="password"
                    name="password"
                    value={password}
                    onChange={handlePasswordChange}
                    placeholder="Новий пароль"
                />
                <label>Фото профілю</label>
                <input
                    type="file"
                    accept="image/*"
                    onChange={handlePhotoChange}
                />
                {photoPreview && (
                    <img
                        src={photoPreview}
                        alt="Фото"
                        className={styles.photoPreview}
                    />
                )}

                <button type="submit">Зберегти зміни</button>
                <button type="button" className={styles.deleteBtn} onClick={handleDeleteAccount}>
                    Видалити акаунт
                </button>
                {success && <p className={styles.success}>Дані збережено</p>}
                {error && <p className={styles.error}>{error}</p>}
            </form>
        </div>
    );
}
