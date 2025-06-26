import { createPortal } from 'react-dom';
import { useEffect, useState } from 'react';

export default function PortalModal({ children }) {
    const [mounted, setMounted] = useState(false);

    useEffect(() => {
        setMounted(true);
        return () => setMounted(false);
    }, []);

    if (!mounted) return null;

    const modalRoot = document.getElementById('modal-root');
    return createPortal(children, modalRoot);
}
