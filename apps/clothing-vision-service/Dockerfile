FROM python:3.11-slim-bookworm

WORKDIR /app

# Встановлюємо стабільні системні залежності
RUN apt-get update && apt-get install -y \
    libgl1 \
    libglib2.0-0 \
    && rm -rf /var/lib/apt/lists/*

COPY requirements.txt .
# Додаємо тайм-аут для повільного інтернету
RUN pip install --no-cache-dir --default-timeout=100 -r requirements.txt

COPY . .

CMD ["uvicorn", "app:app", "--host", "0.0.0.0", "--port", "8000"]