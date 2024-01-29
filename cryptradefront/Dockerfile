FROM node:latest as build
WORKDIR /app
COPY . .
RUN npm install nodemon -g
EXPOSE 3000
CMD ["npm", "start"]