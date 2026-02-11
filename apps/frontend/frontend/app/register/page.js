import { useState } from 'react';

export default function Register() {
    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        const response = await fetch('/api/User', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Username: name,
                Email: email,
                PasswordHash: password,
                ProfileImage: null
            })
        });

        if (response.ok) {
            window.location.href = '/login';
        } else {
            setError('Помилка реєстрації');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Реєстрація</h2>
            <input type="text" placeholder="Ім'я" value={name} onChange={e => setName(e.target.value)} required />
            <input type="email" placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} required />
            <input type="password" placeholder="Пароль" value={password} onChange={e => setPassword(e.target.value)} required />
            <button type="submit">Зареєструватись</button>
            {error && <p style={{color:'red'}}>{error}</p>}
        </form>
    );
}
