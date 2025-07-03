# Legal SaaS Application

Customer & Matter Management System

## Overview

A complete Legal SaaS application featuring customer and matter management with modern web technologies. Built with .NET 8 backend API, React frontend, and PostgreSQL database, fully containerized with Docker.

## Architecture

- **Backend API**: .NET 8 with Entity Framework Core, JWT authentication
- **Frontend**: React + TypeScript + TailwindCSS + ShadcnUI
- **Database**: PostgreSQL 15
- **Containerization**: Docker & Docker Compose

## Project Structure

```
├── api/
│   └── src/
│       └── legal-saas-api/      # .NET 8 Web API with JWT auth
├── web/
│   └── src/                     # React + TypeScript frontend
└── compose/
    ├── docker-compose.yml       # Full stack orchestration
    └── docker-compose.override.yml
```

## Prerequisites

- **Docker & Docker Compose** (cross-platform: Windows, Mac, Linux)
- **Git** (to clone the repository)

_No need to install .NET, Node.js, or PostgreSQL - everything runs in containers!_

## Quick Start

1. **Clone and run**

   ```bash
   git clone <repository-url>
   cd take-home-pp/compose
   docker-compose up -d
   ```

2. **Access the application**

   - **Frontend**: http://localhost:3000 (React app)
   - **API**: http://localhost:8080 (REST API)
   - **API Docs**: http://localhost:8080/swagger (Interactive docs)
   - **Database**: localhost:5432 (postgres/postgres)

3. **Stop the application**
   ```bash
   docker-compose down
   ```

## Development

### Container Development (Recommended)

```bash
# Build and run everything
docker-compose up --build

# View logs
docker-compose logs -f [service-name]

# Rebuild specific service
docker-compose build [api|webapp|postgres]
```

### Local Development (Mostly without Docker)

> The entire application runs in containers, but if we need to run locally to debug or something...

```bash
# Start PostgreSQL only
cd compose && docker-compose up postgres -d

# Run API locally
cd api/src/legal-saas-api
dotnet restore && dotnet run

# Run frontend locally
cd web
npm install && npm start
```

## Troubleshooting

**Port conflicts**: Ensure ports 3000 (webapp), 8080 (api), 5432 (database) are available.
**Container issues**: Run `docker-compose down -v && docker-compose up -d`
**Database connection**: Wait for PostgreSQL health check to pass
