import React from "react";
import { useNavigate } from "react-router-dom";

export default function AdminDashboard() {
  const navigate = useNavigate();

  return (
    <div style={{ maxWidth: 800, margin: "auto", padding: 20 }}>
      <div style={{ position: "relative", marginBottom: 20 }}>
        <button
          onClick={() => navigate("/login")}
          style={{
            position: "absolute",
            right: 0,
            top: 0,
            backgroundColor: "red",
            color: "white",
            border: "none",
            padding: "8px 16px",
            borderRadius: 4,
            cursor: "pointer",
          }}
        >
          Logout
        </button>
        <h2>Admin Dashboard</h2>
      </div>

      {/* Navbar menu for admin options */}
      <nav
        style={{
          display: "flex",
          justifyContent: "space-around",
          marginBottom: 30,
        }}
      >
        <button
          onClick={() => navigate("/admin/routes")}
          style={{ cursor: "pointer" }}
        >
          Manage Routes
        </button>
        <button
          onClick={() => navigate("/admin/metros")}
          style={{ cursor: "pointer" }}
        >
          Manage Metros
        </button>
        <button
          onClick={() => navigate("/admin/stations")}
          style={{ cursor: "pointer" }}
        >
          Manage Stations
        </button>
        <button
          onClick={() => navigate("/admin/complaints")}
          style={{ cursor: "pointer" }}
        >
          Check Complaints
        </button>
        <button
          onClick={() => navigate("/admin/approve-metrocards")}
          style={{ cursor: "pointer" }}
        >
          Approve Metro Cards
        </button>
      </nav>
    </div>
  );
}
