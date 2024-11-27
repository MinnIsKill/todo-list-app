# Todo List Application

This is a simple Todo List application built with **Vue.js** (frontend), **.NET 9/ASP.NET** (backend), and **SQLite** for data storage. It allows users to create and manage TODO tasks.

---

## Features

- Adding TODO tasks
- Deleting TODO tasks
- changing State of TODO tasks (Open, In Progress, Finished)
- View tasks in a simple card structure

---

## Technologies Used

- **Frontend**: Vue.js, Vite
- **Backend**: .NET 9 / ASP.NET, SQLite
- **Database**: SQLite
- **Development Environment**: Docker (optional)

---

## Prerequisites

Make sure you have the following installed before running the application:

- [Node.js](https://nodejs.org/) (for the frontend)
- [Docker](https://www.docker.com/get-started) (optional, for containerized development)
- [SQLite](https://www.sqlite.org/) (installed by the backend for local development)

---

## .env variables

It is possible that you may encounter an issue with downloading the required .env variables, as Git tries to exclude them by default. Check if you have them in your **/backend** directory, and if not, create them:

1. **.env.local (for local development)**

   Create a .env.local file in the /backend directory of your project with the following contents:

   ```bash
   VITE_API_URL=http://localhost:5029
   ```

2. **.env.docker (for Docker development)**

   Create a .env.docker file in the /backend directory of your project with the following contents:

   ```bash
   VITE_API_URL=http://backend:8080
   ```

## Running with Docker (Recommended)

1.  **Build and start the application using Docker Compose**
    Make sure you are in the root of the project (where docker-compose.yml is located), then run:

    ```bash
    docker-compose up
    ```

    The frontend should now be available at: http://localhost:5173/  
    The backend API should be available at: http://localhost:8080/api/todo

## Running Locally (Without Docker)

1.  **Install frontend dependencies**:
    ```bash
    cd frontend
    npm install
    ```
2.  **Install backend dependencies**:
    ```bash
    cd backend
    dotnet restore
    ```
3.  **Within separate terminal windows, start the servers**:

    ```bash
    cd backend
    dotnet run

    cd frontend
    npm run dev
    ```

    The frontend should now be available at: http://localhost:5173/  
    The backend API should be available at: http://localhost:5029/api/todo

---

## Folder Structure

**frontend:** Contains the Vue.js application  
**backend:** Contains the ASP.NET backend project  
**backend/database:** Contains SQLite database and data files.

---

## Troubleshooting

- **CORS Issues:** If you're running into CORS issues, make sure the backend is properly configured to accept requests from the frontend. You can verify by checking the CORS settings in the backend code.
- **Database Issues:** If the SQLite database doesn't exist, the backend will attempt to create it when the application starts. Ensure that the backend has proper permissions to write to the database folder.
