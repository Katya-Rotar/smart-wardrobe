import React, { useEffect, useState } from 'react';
import '../../styles/publicationModal.css';
import PublicationDetail from './PublicationDetail';
import api from '../../../api/api';
import PortalModal from '../../components/portalModal';

export default function PublicationModal({ publicationId, onClose, onNext, onPrev }) {
    const [publication, setPublication] = useState(null);

    useEffect(() => {
        const handleEscape = (e) => e.key === 'Escape' && onClose();
        window.addEventListener('keydown', handleEscape);
        return () => window.removeEventListener('keydown', handleEscape);
    }, [onClose]);

    useEffect(() => {
        async function fetchPublication() {
            try {
                const res = await api.get(`/publication/${publicationId}`);
                setPublication(res.data);
            } catch (error) {
                console.error('Failed to load publication details', error);
                onClose();
            }
        }

        if (publicationId) {
            fetchPublication();
        }
    }, [publicationId]);

    if (!publication) return null;

    return (
        <PortalModal>
            <div className="modal-overlay" onClick={onClose}>
                <div className="modal-content" onClick={e => e.stopPropagation()}>
                    <button className="modal-close" onClick={onClose}>×</button>
                    <PublicationDetail publication={publication} />
                </div>
            </div>
        </PortalModal>
    );
}
