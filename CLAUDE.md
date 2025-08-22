# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a modernized Loan Processing System with an Angular 8 frontend and .NET Core 8 Web API backend using SQLite database. The system was converted from .NET Framework 4.7.2 with SQL Server to .NET Core for cross-platform compatibility.

## Development Commands

### Quick Start
```bash
./start.sh
```
This script automatically starts both frontend and backend services with proper configuration.

### Backend (.NET Core Web API)
```bash
cd WebAPICore
dotnet build                     # Build the project
dotnet run --urls="http://localhost:5275"  # Run on port 5275
dotnet ef migrations add <name>  # Add new migration
dotnet ef database update        # Apply migrations
```

### Frontend (Angular)
```bash
cd "UI Angular/LPSystemAngular"
npm install                      # Install dependencies
npm start                        # Start dev server (port 4200)
npm run build                    # Build for production
npm run test                     # Run unit tests with Karma
npm run lint                     # Run TSLint
npm run e2e                      # Run end-to-end tests with Protractor
```

### Running Individual Tests
```bash
# Frontend tests
cd "UI Angular/LPSystemAngular"
ng test --watch=false --browsers=ChromeHeadless  # Single test run

# Backend tests (if unit test project exists)
cd "Test Cases/LPSystemUnitTest"
dotnet test                      # Run all tests
```

## Architecture Overview

### Backend Structure (.NET Core Web API)
- **WebAPICore/Program.cs**: Main entry point with service configuration, CORS policy for Angular app, and SQLite database setup
- **WebAPICore/Data/LPSystemContext.cs**: Entity Framework context with composite key configuration for ApplTable and pre-seeded data
- **WebAPICore/Controllers/**: API controllers for UserTables, LoanTables, ApplTables, and LoginAuth
- **WebAPICore/Models/**: Entity models (UserTable, LoanTable, ApplTable) representing database structure

### Frontend Structure (Angular 8)
- **src/app/app.module.ts**: Main module with component declarations and HTTP interceptor configuration
- **src/app/app-routing.module.ts**: Route configuration for role-based navigation
- **Services**: 
  - `authen.service.ts`: Authentication and user session management
  - `user.service.ts`: User CRUD operations
  - `loan.service.ts`: Loan management
  - `application.service.ts`: Loan application processing
- **Components**: Role-based components (admin, customer, client views)
- **auth.guard.ts**: Route protection based on user roles
- **auth.interceptor.ts**: HTTP request/response intercepting

### Database Architecture
- **SQLite database** with three main entities:
  - **UserTable**: User management with roles (Admin, Customer, Client)
  - **LoanTable**: Loan types with amounts, ROI, and periods
  - **ApplTable**: Applications with composite primary key (AppId, UserId, LoanId)
- **Relationships**: Applications reference Users and Loans with restrict delete behavior
- **Seed Data**: Pre-populated with 4 users, 4 loan types, and 2 sample applications

### Key Integration Points
- **CORS Configuration**: Backend allows Angular app on localhost:4200 with credentials
- **Authentication Flow**: Login through `/api/LoginAuth`, session managed via services
- **API Base URL**: Backend runs on `http://localhost:5275`, frontend proxies requests
- **Role-Based Access**: Different UI components and API endpoints based on user roles

### Legacy Compatibility
- **Node.js Compatibility**: Uses `--openssl-legacy-provider` flag for Angular 8 with newer Node versions
- **Database Migration**: Converted from SQL Server to SQLite with Entity Framework Core
- **Port Configuration**: Uses HTTP instead of HTTPS for development to avoid certificate issues

### Testing Infrastructure
- **Frontend**: Karma + Jasmine for unit tests, Protractor for e2e tests
- **Backend**: Separate test project in `Test Cases/LPSystemUnitTest` with browser automation tests