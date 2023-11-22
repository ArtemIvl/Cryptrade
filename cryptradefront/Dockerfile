# FROM node:18-alpine AS build
# WORKDIR /app
# COPY package*.json ./
# RUN npm install
# COPY . .
# RUN npm run build

# FROM node:18-alpine as release
# WORKDIR /
# COPY --from=build /app/build /app/build
# EXPOSE 80
# CMD ["npm", "start"]
FROM node:14-alpine as build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build
EXPOSE 3000
CMD ["npm", "start"]