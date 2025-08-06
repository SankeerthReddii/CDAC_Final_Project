import React from "react";
import { Routes, Route } from "react-router-dom";

import Login from "./Auth/Login";
import Register from "./Auth/Register";

import UserDashboard from "./UserDashboard";
import UserProfile from "./UserProfile";

import AdminDashboard from "./Admin/AdminDashboard";
// import AdminProfile from "./Profile/AdminProfile"; // If exists under Components/Profile

import PaymentSuccess from "./PaymentSuccess";

import ManageRoutes from "./Admin/ManageRoutes";
import ManageMetros from "./Admin/ManageMetros";
import ManageStations from "./Admin/ManageStations";
import ViewComplaints from "./Admin/ViewComplaints";
import ApproveMetroCards from "./Admin/ApproveMetroCards";

export default function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/user/dashboard" element={<UserDashboard />} />
      <Route path="/user/profile" element={<UserProfile />} />
      <Route path="/admin/dashboard" element={<AdminDashboard />} />
      {/* <Route path="/admin/profile" element={<AdminProfile />} /> */}
      <Route path="/payment-success" element={<PaymentSuccess />} />
      <Route path="/admin/routes" element={<ManageRoutes />} />
      <Route path="/admin/metros" element={<ManageMetros />} />
      <Route path="/admin/stations" element={<ManageStations />} />
      <Route path="/admin/complaints" element={<ViewComplaints />} />
      <Route path="/admin/approve-metrocards" element={<ApproveMetroCards />} />
    </Routes>
  );
}
