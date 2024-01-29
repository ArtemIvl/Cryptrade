FROM node:latest as build
WORKDIR /app
COPY . .
RUN npm install nodemon -g
RUN npm run build
EXPOSE 3000
CMD ["npm", "start"]