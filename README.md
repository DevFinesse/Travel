# Travel Booking & Reservation System

A full-stack travel booking platform built with **Angular 19** and **ASP.NET Core 10** featuring JWT authentication, tour management, booking system, and analytics dashboard.

## 🚀 Features

### Authentication & Authorization
- JWT-based authentication
- Role-based access control (Admin/Customer)
- Secure password hashing with BCrypt

### Tour Management (Admin)
- Create, edit, and delete tour packages
- Manage tour details (name, location, price, dates, availability)
- Admin dashboard with analytics

### Customer Experience
- Browse and search tours by location, price, and date
- View detailed tour information
- Book tours with availability checking
- View booking history
- Cancel bookings

### Analytics Dashboard (Admin)
- Total bookings and revenue metrics
- Monthly booking trends visualization
- Tour statistics

## 🛠️ Tech Stack

**Frontend:**
- Angular 19 (Standalone Components)
- TypeScript
- RxJS
- Angular Router & HTTP Client

**Backend:**
- ASP.NET Core 10 Web API
- Entity Framework Core
- SQL Server (LocalDB)
- JWT Authentication
- Clean Architecture

## 📋 Prerequisites

- .NET 10 SDK
- Node.js 22.x
- SQL Server LocalDB (or SQL Server Express)

## 🔧 Setup Instructions

### Backend Setup

1. Navigate to the project root:
```bash
cd c:/Users/BABATUNDE/Desktop/Travel
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Apply database migrations:
```bash
dotnet ef database update --project TravelBooking.Infrastructure --startup-project TravelBooking.API
```

4. Run the API:
```bash
dotnet run --project TravelBooking.API
```

The API will be available at `http://localhost:5000`

### Frontend Setup

1. Navigate to the client folder:
```bash
cd client
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The app will be available at `http://localhost:4200`

## 📁 Project Structure

### Backend
```
TravelBooking.sln
├── TravelBooking.API/          # Web API Controllers
├── TravelBooking.Core/         # Domain Entities, DTOs, Interfaces
└── TravelBooking.Infrastructure/ # EF Core, Services, Data Access
```

### Frontend
```
client/src/app/
├── core/                       # Services, Guards, Interceptors
├── features/
│   ├── auth/                  # Login, Register
│   ├── admin/                 # Dashboard, Tour Management
│   ├── tours/                 # Tour Catalog, Details
│   └── booking/               # User Bookings
└── shared/                    # Shared Components
```

## 🔐 Default Admin Account

To create an admin user, register normally and then manually update the database:

```sql
UPDATE Users SET Role = 'Admin' WHERE Username = 'your-username';
```

## 🌐 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user

### Tours
- `GET /api/tours` - Get all tours
- `GET /api/tours/{id}` - Get tour by ID
- `GET /api/tours/search` - Search tours
- `POST /api/tours` - Create tour (Admin)
- `PUT /api/tours/{id}` - Update tour (Admin)
- `DELETE /api/tours/{id}` - Delete tour (Admin)

### Bookings
- `POST /api/bookings` - Create booking
- `GET /api/bookings/my-bookings` - Get user bookings
- `GET /api/bookings` - Get all bookings (Admin)
- `POST /api/bookings/{id}/cancel` - Cancel booking

### Analytics
- `GET /api/analytics` - Get analytics data (Admin)

## 📝 License

This project is for demonstration purposes.
