FROM node:19-alpine
WORKDIR /app
COPY package.json .
# ENV http_proxy=http://10.37.97.36:8080
# ENV https_proxy=http://10.37.97.36:8080
RUN npm install --omit=dev
COPY . .
EXPOSE 3000
CMD ["npm", "start"]
