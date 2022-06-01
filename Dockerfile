FROM nginxinc/nginx-unprivileged:alpine
EXPOSE 8080
COPY . /usr/share/nginx/html
