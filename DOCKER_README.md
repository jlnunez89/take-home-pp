# Legal SaaS Application - Dockerized Setup

This project contains a complete Legal SaaS application with a .NET API backend and React frontend, fully dockerized with PostgreSQL database.

## Architecture

- **Backend API**: .NET 8 with Entity Framework, JWT authentication
- **Frontend**: React + TypeScript + TailwindCSS + ShadcnUI
- **Database**: PostgreSQL 15
- **Containerization**: Docker & Docker Compose

## Quick Start

1. **Prerequisites**

   - Docker and Docker Compose installed
   - Git (to clone the repository)

2. **Run the application**

   ```bash
   cd compose
   docker-compose up -d
   ```

3. **Access the application**

   - **Frontend**: http://localhost:3000
   - **API**: http://localhost:8080
   - **API Documentation**: http://localhost:8080/swagger
   - **Database**: localhost:5432 (user: postgres, password: postgres)

4. **Stop the application**
   ```bash
   docker-compose down
   ```

## Services

### Frontend (webapp)

- **Port**: 3000
- **Technology**: React 19, TypeScript, TailwindCSS, ShadcnUI
- **Features**:
  - User authentication (login/signup)
  - Customer management with phone validation
  - Matter management
  - Modern, responsive UI
- **Proxy Setup**: Nginx proxies `/api/*` requests to the backend API container
- **Local Testing**: Access at `http://localhost:3000` - API calls are automatically proxied

### Backend API (api)

- **Port**: 8080
- **Technology**: .NET 8, Entity Framework Core, JWT
- **Features**:
  - RESTful API
  - JWT-based authentication
  - CRUD operations for customers and matters
  - Phone number validation
  - Swagger documentation

### Database (postgres)

- **Port**: 5432
- **Technology**: PostgreSQL 15
- **Features**:
  - Automatic migrations on startup
  - Data seeding
  - Health checks

## Development

### Environment Variables

The webapp uses environment variables for configuration:

- **Development**: Uses `REACT_APP_API_URL=http://localhost:8080/api` (from .env)
- **Production/Docker**: Uses `REACT_APP_API_URL=/api` (from .env.production)

### API Proxy Configuration

The dockerized webapp uses nginx as a reverse proxy to handle API requests:

- **Frontend URL**: `http://localhost:3000` (accessible from your browser)
- **API Requests**: `/api/*` are automatically proxied to the backend container
- **Benefit**: No CORS issues, works seamlessly from your local browser
- **How it works**: nginx in the webapp container forwards `/api/*` to `http://api:8080/api/*`

### CORS Configuration

The API is configured with CORS to allow requests from:

- `http://localhost:3000` (development)
- `http://webapp:80` (Docker internal)

### Building Individual Services

**Build API only:**

```bash
docker-compose build api
```

**Build webapp only:**

```bash
docker-compose build webapp
```

**View logs:**

```bash
docker-compose logs -f [service-name]
```

## Features

### Authentication

- User registration with firm name
- JWT-based login
- Protected routes

### Customer Management

- Create, view, and update customers
- Phone number validation and formatting
- Search and pagination

### Matter Management

- Create and view legal matters
- Associate matters with customers
- Status tracking

### UI/UX

- Modern, professional design
- Responsive layout
- Form validation with real-time feedback
- Loading states and error handling

## Technical Details

### Phone Number Validation

- **Frontend**: Real-time formatting and validation
- **Backend**: Regex validation with custom attributes
- **Format**: (XXX) XXX-XXXX

### Database Schema

- **Users**: Authentication and user profiles
- **Customers**: Client information with validated phone numbers
- **Matters**: Legal matters linked to customers

### Security

- JWT tokens for authentication
- HTTPS ready (nginx configuration)
- Input validation and sanitization
- CORS protection

## Troubleshooting

### Common Issues

1. **Port conflicts**: Ensure ports 3000, 8080, and 5432 are available
2. **Database connection**: Wait for PostgreSQL health check to pass
3. **API connectivity**: Verify CORS settings if frontend can't reach API

### Reset Everything

```bash
docker-compose down -v
docker-compose up -d
```

This will remove all data and start fresh.
