FROM nginx:alpine

WORKDIR /usr/share/nginx/static

COPY ./nginx.conf /etc/nginx/nginx.conf
COPY ./default.conf /etc/nginx/conf.d/default.conf

EXPOSE 80