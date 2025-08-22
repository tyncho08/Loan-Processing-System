# 🏦 Loan Processing System

A modern loan processing system built with **Angular** frontend and **.NET Core 8** Web API backend, using **SQLite** database for cross-platform compatibility.

## 🚀 Features

- **User Management**: Admin, Customer, and Client roles
- **Loan Management**: Multiple loan types (Personal, Home, Car, Education)
- **Application Processing**: Loan application submission and approval workflow
- **Cross-Platform**: Runs on Windows, Mac, and Linux
- **Modern Stack**: Angular 8 + .NET Core 8 + SQLite

## 🛠️ Technology Stack

### Frontend
- **Angular 8** with TypeScript
- **Bootstrap 4** for responsive design
- **Font Awesome** for icons
- **RxJS** for reactive programming

### Backend
- **.NET Core 8** Web API
- **Entity Framework Core** with SQLite
- **CORS** enabled for cross-origin requests
- **Swagger** for API documentation

### Database
- **SQLite** for cross-platform compatibility
- Pre-seeded with sample data

## 📋 Prerequisites

- **Node.js** (v14 or higher)
- **.NET Core 8 SDK**
- **Git**

## 🔧 Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/tyncho08/Loan-Processing-System.git
   cd Loan-Processing-System
   ```

2. **Run the application**
   ```bash
   ./start.sh
   ```

   The script will automatically:
   - Install Angular dependencies
   - Start the .NET Core Web API on port 5275
   - Start the Angular frontend on port 4200
   - Create and seed the SQLite database

## 🌐 Access URLs

- **Frontend**: http://localhost:4200
- **Backend API**: http://localhost:5275
- **API Documentation**: http://localhost:5275/swagger

## 📁 Project Structure

```
Loan-Processing-System/
├── UI Angular/LPSystemAngular/     # Angular frontend
│   ├── src/app/                    # Angular components & services
│   └── package.json                # Node.js dependencies
├── WebAPICore/                     # .NET Core Web API
│   ├── Controllers/                # API controllers
│   ├── Models/                     # Entity models
│   ├── Data/                       # Database context
│   └── Program.cs                  # Application entry point
├── Documents/                      # Project documentation
├── DB Screenshots/                 # Database structure images
├── start.sh                        # Startup script
└── README.md                       # This file
```

## 🎯 API Endpoints

### User Management
- `GET /api/UserTables` - Get all users
- `POST /api/UserTables` - Create user
- `PUT /api/UserTables/{id}` - Update user
- `DELETE /api/UserTables/{id}` - Delete user

### Loan Management
- `GET /api/LoanTables` - Get all loans
- `POST /api/LoanTables` - Create loan
- `PUT /api/LoanTables/{id}` - Update loan
- `DELETE /api/LoanTables/{id}` - Delete loan

### Application Management
- `GET /api/ApplTables` - Get all applications
- `POST /api/ApplTables` - Submit application
- `PUT /api/ApplTables/{appId}/{userId}/{loanId}` - Update application

## 💾 Sample Data

The system comes pre-loaded with:
- **4 Users**: Admin, 2 Customers, 1 Client
- **4 Loan Types**: Personal, Home, Car, Education loans
- **2 Sample Applications**: One pending, one approved

## 🔧 Manual Setup (Alternative)

If you prefer to run components separately:

### Backend (.NET Core Web API)
```bash
cd WebAPICore
dotnet restore
dotnet run --launch-profile https
```

### Frontend (Angular)
```bash
cd "UI Angular/LPSystemAngular"
npm install
npm start
```

## 🐛 Troubleshooting

### Common Issues

1. **Port conflicts**: The script automatically kills processes on ports 4200 and 5275
2. **Node.js compatibility**: Uses `--openssl-legacy-provider` flag for older Angular versions
3. **SSL certificates**: Configured to use HTTP for development to avoid certificate issues
4. **Connection refused**: Make sure both backend and frontend are running before accessing the app

### Node.js Version Issues
If you encounter SSL/OpenSSL errors:
```bash
export NODE_OPTIONS="--openssl-legacy-provider"
npm start
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 📄 License

This project is for educational purposes.

## 👨‍💻 Development

- **Original Framework**: .NET Framework 4.7.2 with SQL Server
- **Modernized**: Converted to .NET Core 8 with SQLite for cross-platform support
- **Database Migration**: Automated Entity Framework migrations with seed data

---

**Note**: This is a modernized version of the original Loan Processing System, converted from .NET Framework to .NET Core for better cross-platform compatibility.