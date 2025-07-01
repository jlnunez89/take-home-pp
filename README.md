# take-home-pp
Legal SaaS Customer & Matter Management

## Overview

This is a Legal SaaS application for customer and matter management, built with .NET 8 and PostgreSQL.

## Project Structure

```
├── api/
│   └── src/
│       └── legal-saas-api/      # .NET 8 Web API
├── web/
│   └── src/                     # Frontend (TBD)
├── compose/
│   ├── docker-compose.yml       # Docker services configuration
│   └── docker-compose.override.yml
└── README.md
```

## Features

### Customer Management API
- **GET** `/api/customers` - Retrieve a list of customers
- **POST** `/api/customers` - Create a new customer (name, phone)
- **GET** `/api/customers/{customer_id}` - Retrieve details of a customer
- **PUT** `/api/customers/{customer_id}` - Update a customer
- **DELETE** `/api/customers/{customer_id}` - Delete a customer

## Technology Stack

- **.NET 8** - Web API framework
- **PostgreSQL 15** - Database
- **Entity Framework Core** - ORM
- **Docker & Docker Compose** - Containerization
- **Swagger/OpenAPI** - API documentation

## Prerequisites

- Docker and Docker Compose
- .NET 8 SDK (for local development)

## Quick Start

### Using Docker Compose (Recommended)

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd take-home-pp
   ```

2. **Start the services**
   
   On Linux/Mac:
   ```bash
   ./run.sh
   ```
   
   On Windows (PowerShell):
   ```powershell
   .\run.ps1
   ```
   
   Or manually:
   ```bash
   cd compose
   docker-compose up --build -d
   ```

3. **Access the API**
   - API: http://localhost:8080
   - Swagger UI: http://localhost:8080/swagger

### Local Development

1. **Start PostgreSQL**
   ```bash
   cd compose
   docker-compose up postgres -d
   ```

2. **Run the API locally**
   ```bash
   cd api/src/legal-saas-api
   dotnet restore
   dotnet run
   ```

## Database

The application uses PostgreSQL with Entity Framework Core. The connection strings are configured in:
- `appsettings.json` - For local development with localhost PostgreSQL
- `appsettings.Development.json` - For Docker development with PostgreSQL container

## API Documentation

Once the application is running, you can access:
- **Swagger UI**: http://localhost:8080/swagger
- **OpenAPI Spec**: http://localhost:8080/swagger/v1/swagger.json

## Docker Services

- **postgres**: PostgreSQL 15 database
  - Port: 5432
  - Database: `legal_saas_db`
  - Username: `postgres`
  - Password: `postgres`

- **api**: .NET 8 Web API
  - Port: 8080
  - Environment: Development

## Development Commands

```bash
# View logs
docker-compose -f compose/docker-compose.yml logs -f

# Stop services
docker-compose -f compose/docker-compose.yml down

# Rebuild and restart
docker-compose -f compose/docker-compose.yml up --build

# Run only database
docker-compose -f compose/docker-compose.yml up postgres -d
```
