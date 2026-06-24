FROM python:3.10-slim

WORKDIR /app

# Встановлюємо системні залежності
RUN apt-get update && apt-get install -y --no-install-recommends \
    build-essential \
    libgl1 \
    libglib2.0-0 \
    && rm -rf /var/lib/apt/lists/*

# Копіюємо requirements
COPY requirements.txt .

RUN pip install --no-cache-dir --default-timeout=1000 -r requirements.txt

# Копіюємо весь код додатка
COPY . .

EXPOSE 8003

# Запускаємо сервер uvicorn на порту 8003
CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8003"]