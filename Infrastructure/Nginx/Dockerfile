# Этап 1: Сборка фронтенда (Node.js)
FROM node:20-alpine AS frontend-build
WORKDIR /app

COPY marketplace-frontend/package.json marketplace-frontend/package-lock.json* ./
RUN npm install

COPY marketplace-frontend/ ./
RUN npm run build


# Этап 2: Финальный образ веб-сервера (Nginx)
FROM nginx:alpine
WORKDIR /usr/share/nginx/static

COPY Infrastructure/Nginx/nginx.conf /etc/nginx/nginx.conf
COPY Infrastructure/Nginx/default.conf /etc/nginx/conf.d/default.conf

COPY --from=frontend-build /app/dist ./web

EXPOSE 80