import React, { useState, useEffect } from 'react';
import api from '../../api/api';
import '../styles/createPublicationModal.css';
import PortalModal from '../components/portalModal';

const CLOUD_NAME = "ddapkpo6c";
const UPLOAD_PRESET = "unsigned_preset";

async function uploadToCloudinary(file) {
    const formData = new FormData();
    formData.append("file", file);
    formData.append("upload_preset", UPLOAD_PRESET);

    const res = await fetch(`https://api.cloudinary.com/v1_1/${CLOUD_NAME}/upload`, {
        method: 'POST',
        body: formData,
    });

    const data = await res.json();
    return data.secure_url;
}

async function searchTags(query) {
    if (!query) return [];
    const token = localStorage.getItem('token');
    try {
        const response = await api.get(`/tag/search?query=${encodeURIComponent(query)}`, {
            headers: { Authorization: `Bearer ${token}` },
        });
        return response.data;
    } catch (err) {
        console.error("Error searching tags", err);
        return [];
    }
}

export default function CreatePublicationModal({ onClose, outfitId }) {
    const [image, setImage] = useState(null);
    const [imagePreview, setImagePreview] = useState(null);
    const [tags, setTags] = useState([]);
    const [newTag, setNewTag] = useState('');
    const [suggestions, setSuggestions] = useState([]);
    const [commentingOptions, setCommentingOptions] = useState(true);
    const [loading, setLoading] = useState(false);

    function handleImageChange(e) {
        const file = e.target.files?.[0];
        if (file) {
            setImage(file);
            setImagePreview(URL.createObjectURL(file));
        }
    }
    
    useEffect(() => {
        const delayDebounceFn = setTimeout(() => {
            if (newTag.trim().length > 0) {
                searchTags(newTag.trim()).then(setSuggestions);
            } else {
                setSuggestions([]);
            }
        }, 300);

        return () => clearTimeout(delayDebounceFn);
    }, [newTag]);

    function handleAddTag() {
        const trimmed = newTag.trim();
        if (trimmed && !tags.includes(trimmed)) {
            setTags([...tags, trimmed]);
            setNewTag('');
            setSuggestions([]);
        }
    }

    function handleSelectSuggestion(tagName) {
        if (!tags.includes(tagName)) {
            setTags([...tags, tagName]);
        }
        setNewTag('');
        setSuggestions([]);
    }

    async function handleSubmit() {
        if (!image) return alert("Please upload an image");
        setLoading(true);
        try {
            const imageURL = await uploadToCloudinary(image);
            const token = localStorage.getItem('token');

            await api.post('/publication', {
                outfitID: outfitId,
                imageURL,
                tags,
                commentingOptions
            }, {
                headers: { Authorization: `Bearer ${token}` }
            });

            alert("Publication created!");
            onClose();
        } catch (err) {
            console.error("Error creating publication:", err);
            alert("Failed to create publication.");
        } finally {
            setLoading(false);
        }
    }

    return (
        <PortalModal>
            <div className="overlay">
                <div className="modal">
                    <button onClick={onClose} className="close">&times;</button>
                    <h2>Create Publication</h2>

                    <div className="image-upload">
                        {imagePreview ? (
                            <img src={imagePreview} alt="Preview" className="image-preview" />
                        ) : (
                            <label className="upload-label">
                                + Upload Image
                                <input type="file" accept="image/*" onChange={handleImageChange} hidden />
                            </label>
                        )}
                    </div>

                    <div className="tags" style={{ position: 'relative' }}>
                        <input
                            type="text"
                            value={newTag}
                            onChange={(e) => setNewTag(e.target.value)}
                            placeholder="Add tag"
                            autoComplete="off"
                        />
                        <button type="button" onClick={handleAddTag}>Add Tag</button>
                        
                        {suggestions.length > 0 && (
                            <ul className="suggestions-list">
                                {suggestions.map(tag => (
                                    <li key={tag.id ?? tag.tagName} onClick={() => handleSelectSuggestion(tag.tagName)}>
                                        #{tag.tagName}
                                    </li>
                                ))}
                            </ul>
                        )}

                        <div className="tag-list">
                            {tags.map((tag, index) => (
                                <span key={`${tag}-${index}`} className="tag"> #{tag}
                                    <button
                                        type="button"
                                        className="remove-tag-btn"
                                        onClick={() => {
                                            setTags(tags.filter(t => t !== tag));
                                        }}
                                    >
                                        ×
                                    </button>
                                </span>
                            ))}
                        </div>
                    </div>

                    <div className="options">
                        <label>Commenting:</label>
                        <select
                            value={commentingOptions ? 'enabled' : 'disabled'}
                            onChange={(e) => setCommentingOptions(e.target.value === 'enabled')}
                        >
                            <option value="enabled">Allow comments</option>
                            <option value="disabled">Disable comments</option>
                        </select>
                    </div>

                    <button onClick={handleSubmit} className="publish-btn" disabled={loading}>
                        {loading ? 'Publishing...' : 'Publish'}
                    </button>
                </div>
            </div>
        </PortalModal>
    );
}
