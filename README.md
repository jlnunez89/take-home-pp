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

## Features

### 🔐 Authentication

- User registration with firm name
- JWT-based secure login/logout
- Protected routes and API endpoints

### 👥 Customer Management

- Create, view, update, and delete customers
- Phone number validation with real-time formatting
- Search and data validation

### 📋 Matter Management

- Create and view legal matters
- Associate matters with customers
- Status tracking and management

### 🎨 Modern UI/UX

- Professional, responsive design
- Real-time form validation with error feedback
- Loading states and smooth interactions
- Mobile-friendly interface

## Technology Stack

### Backend (.NET 8 API)

- **Framework**: ASP.NET Core 8
- **Database**: Entity Framework Core with PostgreSQL
- **Authentication**: JWT tokens with secure validation
- **Documentation**: Swagger/OpenAPI with interactive UI
- **Validation**: Custom phone number validation, data annotations

### Frontend (React)

- **Framework**: React 19 with TypeScript
- **Styling**: TailwindCSS + ShadcnUI components
- **State Management**: React Context for authentication
- **HTTP Client**: Fetch API with JWT token handling
- **Form Validation**: Real-time validation with error display

### Database

- **Engine**: PostgreSQL 15
- **Migrations**: Automatic Entity Framework migrations
- **Schema**: Users, Customers, and Matters with relationships
- **Health Checks**: Container health monitoring

### DevOps

- **Containerization**: Multi-stage Docker builds
- **Orchestration**: Docker Compose with service dependencies
- **Proxy**: Nginx reverse proxy for API requests
- **Environment**: Separate configs for development/production

## Development

### Local Development (Without Docker)

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

### Container Development (Recommended)

```bash
# Build and run everything
docker-compose up --build

# View logs
docker-compose logs -f [service-name]

# Rebuild specific service
docker-compose build [api|webapp|postgres]
```

For detailed containerization information, see [DOCKER_README.md](DOCKER_README.md).

## API Endpoints

### Authentication

- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login

### Customers

- `GET /api/customers` - List customers
- `POST /api/customers` - Create customer
- `GET /api/customers/{id}` - Get customer details
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

### Matters

- `GET /api/matters` - List matters
- `POST /api/matters` - Create matter
- `GET /api/matters/{id}` - Get matter details

## Database Schema

- **Users**: `Id`, `Email`, `PasswordHash`, `FirmName`, `CreatedAt`
- **Customers**: `Id`, `Name`, `Phone`, `CreatedAt`, `UpdatedAt`
- **Matters**: `Id`, `CustomerId`, `Title`, `Description`, `Status`, `CreatedAt`

## Security Features

- JWT token-based authentication
- Password hashing with secure algorithms
- Input validation and sanitization
- CORS protection
- SQL injection prevention via Entity Framework
- Phone number format validation (backend + frontend)

## Cross-Platform Compatibility

✅ **Windows**: Docker Desktop + PowerShell/CMD  
✅ **macOS**: Docker Desktop + Terminal  
✅ **Linux**: Docker Engine + Bash

The entire application runs in containers, ensuring consistent behavior across all platforms.

## Troubleshooting

**Port conflicts**: Ensure ports 3000, 8080, 5432 are available  
**Container issues**: Run `docker-compose down -v && docker-compose up -d`  
**Database connection**: Wait for PostgreSQL health check to pass

For detailed troubleshooting, see [DOCKER_README.md](DOCKER_README.md).
