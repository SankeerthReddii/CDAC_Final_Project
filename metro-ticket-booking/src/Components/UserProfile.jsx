import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

export default function UserProfile() {
  const navigate = useNavigate();
  
  // User data state
  const [user, setUser] = useState({
    name: "",
    phone: "",
    dob: "",
    address: "",
    gender: "",
    email: "",
    loyaltyPoints: 0,
  });
  const [isEditing, setIsEditing] = useState(false);
  const [form, setForm] = useState({ ...user });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  // Fetch user data on component mount
  useEffect(() => {
    axios.get("http://localhost:5000/api/user/profile")
      .then(response => {
        setUser(response.data);
        setForm(response.data);
        setLoading(false);
      })
      .catch(() => {
        setError("Failed to load user profile.");
        setLoading(false);
      });
  }, []);

  // Handle form input change
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm(prevForm => ({ ...prevForm, [name]: value }));
  };

  // Save edited profile
  const handleSave = () => {
    // Basic validation (add more as needed)
    if (!form.name || !form.email) {
      alert("Name and Email are required.");
      return;
    }
    axios.put("http://localhost:5000/api/user/profile", form)
      .then(response => {
        setUser(response.data);
        setIsEditing(false);
        alert("Profile updated successfully.");
      })
      .catch(() => alert("Failed to update profile. Please try again."));
  };

  // Logout function
  const handleLogout = () => {
    // Clear session/cookies if necessary via backend
    navigate("/login");
  };

  if (loading) return <p>Loading profile...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;

  return (
    <div style={{ maxWidth: 600, margin: "auto", padding: 20 }}>
      <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <h2>User Profile</h2>
        <button
          onClick={handleLogout}
          style={{
            backgroundColor: "red",
            color: "white",
            border: "none",
            padding: "8px 16px",
            borderRadius: 4,
            cursor: "pointer"
          }}
        >
          Logout
        </button>
      </div>

      <div style={{ marginTop: 20 }}>
        <label>Name:</label><br />
        <input
          type="text"
          name="name"
          value={isEditing ? form.name : user.name}
          onChange={handleChange}
          disabled={!isEditing}
          style={{ width: "100%", padding: 8, marginBottom: 10 }}
        />

        <label>Phone:</label><br />
        <input
          type="text"
          name="phone"
          value={isEditing ? form.phone : user.phone}
          onChange={handleChange}
          disabled={!isEditing}
          style={{ width: "100%", padding: 8, marginBottom: 10 }}
        />

        <label>Date of Birth:</label><br />
        <input
          type="date"
          name="dob"
          value={isEditing ? form.dob.split("T")[0] : user.dob.split("T")[0]}
          onChange={handleChange}
          disabled={!isEditing}
          style={{ width: "100%", padding: 8, marginBottom: 10 }}
        />

        <label>Address:</label><br />
        <input
          type="text"
          name="address"
          value={isEditing ? form.address : user.address}
          onChange={handleChange}
          disabled={!isEditing}
          style={{ width: "100%", padding: 8, marginBottom: 10 }}
        />

        <label>Gender:</label><br />
        <select
          name="gender"
          value={isEditing ? form.gender : user.gender}
          onChange={handleChange}
          disabled={!isEditing}
          style={{ width: "100%", padding: 8, marginBottom: 10 }}
        >
          <option value="">Select Gender</option>
          <option value="male">Male</option>
          <option value="female">Female</option>
          <option value="other">Other</option>
        </select>

        <label>Email:</label><br />
        <input
          type="email"
          name="email"
          value={isEditing ? form.email : user.email}
          onChange={handleChange}
          disabled={!isEditing}
          style={{ width: "100%", padding: 8, marginBottom: 10 }}
        />

        <div style={{ marginTop: 20 }}>
          <strong>Loyalty Points: </strong>{user.loyaltyPoints}
        </div>

        <div style={{ marginTop: 20 }}>
          {isEditing ? (
            <>
              <button
                onClick={handleSave}
                style={{
                  padding: "10px 20px",
                  backgroundColor: "#1976d2",
                  color: "white",
                  border: "none",
                  borderRadius: 4,
                  cursor: "pointer",
                  marginRight: 10,
                }}
              >
                Save
              </button>
              <button
                onClick={() => {
                  setForm(user);
                  setIsEditing(false);
                }}
                style={{
                  padding: "10px 20px",
                  backgroundColor: "#999",
                  color: "white",
                  border: "none",
                  borderRadius: 4,
                  cursor: "pointer"
                }}
              >
                Cancel
              </button>
            </>
          ) : (
            <button
              onClick={() => setIsEditing(true)}
              style={{
                padding: "10px 20px",
                backgroundColor: "#1976d2",
                color: "white",
                border: "none",
                borderRadius: 4,
                cursor: "pointer",
              }}
            >
              Edit Profile
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
