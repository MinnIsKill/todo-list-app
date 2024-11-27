# Build frontend
FROM node:18 AS build
WORKDIR /app

# Install dependencies
COPY package.json package-lock.json ./
RUN npm install

# Copy the rest of frontend files
COPY . .

# Run building process
RUN npm run build

# Serve frontend
FROM nginx:alpine

# Copy the previously built files
COPY --from=build /app/dist /usr/share/nginx/html

# Expose the port frontend will be running on (in Docker)
EXPOSE 80

# Start nginx server
CMD ["nginx", "-g", "daemon off;"]