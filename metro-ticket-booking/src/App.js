import React from "react";
import { BrowserRouter } from "react-router-dom";
// import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
// import { AuthProvider } from "./context/AuthContext";
// import AppRoutes from "./Components/AppRouting";
// import Navbar from "./components/common/Navbar";
// import Footer from "./components/common/Footer";
// import ProtectedRoute from "./components/common/ProtectedRoute";
import AppRouting from "./Components/AppRouting";

function App() {
  return (

      <BrowserRouter>
        <AppRouting></AppRouting>
      </BrowserRouter>
  );
}

export default App;
