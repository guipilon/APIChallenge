# APIChallenge

This repository contains a Clean Architecture solution built with .NET 9, implementing Domain-Driven Design (DDD) principles. It includes a WebAPI project, a WorkerService project, and a Vue.js frontend for managing and displaying job listings.

---

## Project Overview

### Architecture
- **Clean Architecture**: The solution is structured into layers: Domain, Application, Infrastructure, and Presentation.
- **Domain-Driven Design (DDD)**: Domain models encapsulate core business logic.
- **SOLID Principles**: Ensures maintainable and extendable code.

### Components
1. **WebAPIHost**: A Web API project exposing endpoints for managing job data.
2. **WorkerServiceHost**: A background service that uses Hangfire to fetch and upsert job data from an external source.
3. **Vue.js Frontend**: A TypeScript-based Vue.js application for viewing and filtering job listings.

### Features
- **EF Core**: For data persistence with an MSSQL database.
- **Hangfire**: For scheduling recurring data-fetching jobs.
- **CORS**: Configured to allow frontend communication with the Web API.
- **Filterable Grid**: A frontend feature to search and filter job listings interactively.

---

## Prerequisites

### Backend
- .NET 9 SDK
- MSSQL Database

### Frontend
- Node.js (v16 or later)
- npm or yarn

---

## Step-by-Step Execution

### 1. Clone the Repository

```bash
git clone https://github.com/guipilon/APIChallenge.git
cd APIChallenge
```

### 2. Set Up the Database

1. Update the connection string in `appsettings.json` for both **WebAPIHost** and **WorkerServiceHost** projects:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;"
   }
   ```
2. Apply the migrations:
   ```bash
   cd Infrastructure
   dotnet ef database update --startup-project ../WebAPIHost/WebAPIHost.csproj
   ```

### 3. Run the Backend Projects

#### WebAPIHost
```bash
cd WebAPIHost
 dotnet run
```
The API will be accessible at `https://localhost:7246` by default.

#### WorkerServiceHost
```bash
cd WorkerServiceHost
 dotnet run
```
This service fetches and upserts job data from an external API every hour using Hangfire.

### 4. Run the Vue.js Frontend

1. Navigate to the `webapp` directory:
   ```bash
   cd webapp
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the development server:
   ```bash
   npm run serve
   ```
4. Open the browser at `http://localhost:8080` to access the frontend.

---

## Solution Details

### WebAPIHost
- Provides CRUD operations for job entities.
- Configured with Swagger for API documentation.
- CORS enabled to allow communication with the frontend.

### WorkerServiceHost
- Hosts Hangfire jobs for periodic data fetching.
- Implements a repository pattern for database interaction.

### Vue.js Frontend
- Displays job listings in a filterable grid.
- Uses Axios to fetch data from the Web API.
- Implements Vue 3 Composition API and TypeScript for scalability.

---

## Key Endpoints

### Web API
- `GET /api/jobs`: Fetch all jobs.
- `GET /api/jobs/{id}`: Fetch a job by ID.
- `POST /api/jobs`: Add a new job.
- `PUT /api/jobs/{id}`: Update a job.
- `DELETE /api/jobs/{id}`: Delete a job.

---

## Contributions

Guilherme Cesar Pilon

