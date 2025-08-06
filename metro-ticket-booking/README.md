# Metro Ticket Booking System

A modern, responsive React frontend application for the Metro Ticket Booking system with comprehensive features for both users and administrators.

## 🚀 Features

### User Features
- **User Registration & Authentication** - Secure user registration and login system
- **Ticket Booking** - Easy ticket booking with route selection and payment integration
- **Booking Management** - View, filter, and cancel existing bookings
- **Metro Card Application** - Apply for metro cards and track application status
- **Complaint Management** - Submit complaints and view admin responses
- **User Profile** - Manage personal information and view loyalty points

### Admin Features
- **Admin Dashboard** - Comprehensive overview of system statistics
- **Station Management** - CRUD operations for metro stations
- **Route Management** - Manage metro routes and schedules
- **Metro Management** - Control metro vehicles and capacity
- **Metro Card Approval** - Review and approve/reject metro card applications
- **Complaint Management** - View and respond to user complaints

### Technical Features
- **Responsive Design** - Fully responsive from 320px to 1920px widths
- **Modern UI/UX** - Clean, modern interface with Tailwind CSS
- **Form Validation** - Client-side validation using React Hook Form and Yup
- **Authentication** - JWT-based authentication with protected routes
- **Toast Notifications** - User-friendly notifications for all actions
- **Loading States** - Proper loading indicators for better UX
- **Error Handling** - Comprehensive error handling and user feedback

## 🛠️ Tech Stack

- **Frontend Framework**: React 18.2.0
- **Routing**: React Router v6
- **Styling**: Tailwind CSS
- **Form Management**: React Hook Form + Yup validation
- **HTTP Client**: Axios
- **Icons**: Lucide React
- **Notifications**: React Toastify
- **Animations**: Framer Motion
- **QR Code**: React QR Code
- **Date Handling**: Date-fns

## 📦 Installation

### Prerequisites
- Node.js (v16 or higher)
- npm or yarn
- Backend API running at `https://localhost:44378/api`

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd metro-ticket-booking
   ```

2. **Install dependencies**
   ```bash
   npm install --legacy-peer-deps
   ```

3. **Configure environment variables**
   Create a `.env` file in the root directory:
   ```env
   REACT_APP_API_BASE_URL=https://localhost:44378/api
   REACT_APP_APP_NAME=Metro Ticket Booking System
   REACT_APP_VERSION=1.0.0
   ```

4. **Start the development server**
   ```bash
   npm start
   ```

5. **Open your browser**
   Navigate to `http://localhost:3000`

## 🏗️ Project Structure

```
src/
├── components/
│   ├── common/           # Reusable components
│   │   ├── Navbar.jsx
│   │   ├── Footer.jsx
│   │   ├── LoadingSpinner.jsx
│   │   └── ProtectedRoute.jsx
│   ├── Auth/            # Authentication components
│   │   ├── Login.jsx
│   │   └── Register.jsx
│   ├── Admin/           # Admin-specific components
│   │   ├── AdminDashboard.jsx
│   │   ├── ManageStations.jsx
│   │   ├── ManageRoutes.jsx
│   │   ├── ManageMetros.jsx
│   │   ├── ApproveMetroCards.jsx
│   │   └── ViewComplaints.jsx
│   └── UserDashboard.jsx
├── pages/               # Page components
│   ├── HomePage.jsx
│   ├── NotFound.jsx
│   └── user/
│       ├── BookTicket.jsx
│       ├── MyBookings.jsx
│       ├── MetroCardApplication.jsx
│       └── SubmitComplaint.jsx
├── context/             # React Context
│   └── AuthContext.jsx
├── config/              # Configuration files
│   └── api.js
├── App.js
├── AppRouting.jsx
└── index.js
```

## 🔧 Configuration

### API Configuration
The application is configured to work with the backend API. Update the API endpoints in `src/config/api.js` if needed.

### Tailwind CSS
The project uses Tailwind CSS for styling. Configuration is in `tailwind.config.js`.

### Environment Variables
- `REACT_APP_API_BASE_URL`: Backend API base URL
- `REACT_APP_APP_NAME`: Application name
- `REACT_APP_VERSION`: Application version

## 🚀 Deployment

### Build for Production
```bash
npm run build
```

### Deploy to Hostinger
1. Build the application: `npm run build`
2. Upload the `build` folder contents to your Hostinger hosting
3. Configure your domain to point to the uploaded files
4. Set up environment variables in your hosting environment

### Environment Variables for Production
Make sure to update the API base URL for production:
```env
REACT_APP_API_BASE_URL=https://your-production-api.com/api
```

## 📱 Responsive Design

The application is fully responsive and optimized for:
- Mobile devices (320px+)
- Tablets (768px+)
- Desktop (1024px+)
- Large screens (1920px+)

## 🔐 Authentication

The application uses JWT-based authentication:
- Tokens are stored in localStorage
- Protected routes automatically redirect to login
- Admin routes require admin privileges
- Automatic token refresh and validation

## 🎨 UI Components

### Reusable Components
- **Navbar**: Responsive navigation with mobile menu
- **Footer**: Information and social links
- **LoadingSpinner**: Consistent loading indicators
- **ProtectedRoute**: Route protection based on authentication

### Form Components
- **Login Form**: User and admin login
- **Registration Form**: User registration with validation
- **Booking Form**: Ticket booking with real-time pricing
- **Complaint Form**: Complaint submission with categories

## 🧪 Testing

Run tests with:
```bash
npm test
```

## 📄 API Endpoints

The application expects the following API endpoints:

### Authentication
- `POST /auth/login` - User login
- `POST /auth/login/admin` - Admin login
- `POST /auth/register/user` - User registration
- `GET /auth/verify` - Verify token

### User Management
- `GET /user/profile` - Get user profile
- `PUT /user/profile/update` - Update user profile
- `GET /user/loyalty-points` - Get loyalty points

### Ticket Booking
- `POST /ticket/book` - Book ticket
- `GET /ticket/bookings` - Get user bookings
- `POST /ticket/cancel` - Cancel booking

### Metro Card
- `POST /metrocard/apply` - Apply for metro card
- `GET /metrocard/status` - Get application status
- `GET /metrocard/applications` - Get all applications (admin)

### Stations, Routes, Metros
- `GET /station/all` - Get all stations
- `GET /route/all` - Get all routes
- `GET /metro/all` - Get all metros

### Complaints
- `POST /complaint/submit` - Submit complaint
- `GET /complaint/user` - Get user complaints
- `GET /complaint/all` - Get all complaints (admin)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📝 License

This project is licensed under the MIT License.

## 🆘 Support

For support and questions:
- Create an issue in the repository
- Contact the development team
- Check the documentation

## 🔄 Updates

### Version 1.0.0
- Initial release
- Complete user and admin functionality
- Responsive design
- Modern UI/UX
- Comprehensive error handling

---

**Built with ❤️ for seamless metro travel experience**
