'use client';
import { useState } from 'react';
import styles from '../../styles/registerModal.module.css';
import api from "../../../api/api";
import AvatarCropModal from './avatarCropModal';

// Cloudinary constants
const CLOUD_NAME = "ddapkpo6c";
const UPLOAD_PRESET = "unsigned_preset";

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
        throw new Error("Не вдалося завантажити фото на Cloudinary");
    }

    const data = await response.json();
    return data.secure_url;
}

export default function RegisterModal({ onClose }) {
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        passwordHash: '',
        profileImage: ''
    });

    const [error, setError] = useState('');
    const [success, setSuccess] = useState(false);
    const [photoFile, setPhotoFile] = useState(null);
    const [photoPreview, setPhotoPreview] = useState(null);
    const [showCrop, setShowCrop] = useState(false);
    const [tempImage, setTempImage] = useState(null);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handlePhotoUpload = (e) => {
        const file = e.target.files[0];
        if (file) {
            const imageUrl = URL.createObjectURL(file);
            setTempImage(imageUrl);
            setShowCrop(true);
        }
    };

    const handleCropDone = (croppedFile) => {
        setPhotoFile(croppedFile);
        setPhotoPreview(URL.createObjectURL(croppedFile));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        try {
            let imageURL = '';
            if (photoFile) {
                imageURL = await uploadImageToCloudinary(photoFile);
            }

            const submitData = {
                ...formData,
                profileImage: imageURL
            };

            const response = await api.post('/User', submitData);

            if (response.status === 201) {
                setSuccess(true);
                setTimeout(onClose, 1500);
            } else {
                setError('Щось пішло не так при реєстрації');
            }
        } catch (error) {
            if (error.response && error.response.data) {
                setError(error.response.data.message || 'Помилка при реєстрації');
            } else {
                setError('Сервер недоступний');
            }
        }
    };

    return (
        <>
            {/* Crop Modal */}
            {showCrop && (
                <AvatarCropModal
                    image={tempImage}
                    onClose={() => setShowCrop(false)}
                    onCropDone={handleCropDone}
                />
            )}

            <div className={styles.overlay}>
                <div className={styles.modal}>
                    <button onClick={onClose} className={styles.closeBtn}>✕</button>

                    <h2 className={styles.title}>Створити акаунт</h2>

                    <form onSubmit={handleSubmit} className={styles.form}>

                        <input
                            type="text"
                            name="username"
                            placeholder="Ім'я користувача"
                            value={formData.username}
                            onChange={handleChange}
                            required
                        />

                        <input
                            type="email"
                            name="email"
                            placeholder="Email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                        />

                        <input
                            type="password"
                            name="passwordHash"
                            placeholder="Пароль"
                            value={formData.passwordHash}
                            onChange={handleChange}
                            required
                        />

                        <label className={styles.photoLabel}>
                            Фото профілю
                            <input
                                type="file"
                                accept="image/*"
                                onChange={handlePhotoUpload}
                            />
                        </label>

                        {photoPreview && (
                            <img
                                src={photoPreview}
                                alt="Preview"
                                className={styles.photoPreview}
                            />
                        )}

                        <button type="submit" className={styles.primaryBtn}>
                            Зареєструватися
                        </button>

                        {error && <p className={styles.error}>{error}</p>}
                        {success && <p className={styles.success}>Успішно створено!</p>}

                    </form>
                </div>
            </div>
        </>
    );
}
