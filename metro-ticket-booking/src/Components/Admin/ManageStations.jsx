import React, { useState } from "react";

export default function ManageStations() {
  // List of stations
  const [stations, setStations] = useState([
    { id: 1, name: "Central Station", address: "123 Main St" }
  ]);
  // Form state
  const [form, setForm] = useState({ id: null, name: "", address: "" });
  const [isEditing, setIsEditing] = useState(false);

  // Handle form input change
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  // Add new station
  const handleAdd = () => {
    if (!form.name || !form.address) {
      alert("Please fill all fields.");
      return;
    }
    const newStation = { ...form, id: Date.now() };
    setStations([...stations, newStation]);
    setForm({ id: null, name: "", address: "" });
  };

  // Prepare to edit
  const handleEdit = (station) => {
    setForm(station);
    setIsEditing(true);
  };

  // Update edited station
  const handleUpdate = () => {
    if (!form.name || !form.address) {
      alert("Please fill all fields.");
      return;
    }
    setStations(stations.map(s => s.id === form.id ? form : s));
    setForm({ id: null, name: "", address: "" });
    setIsEditing(false);
  };

  // Delete station
  const handleDelete = (id) => {
    if(window.confirm("Are you sure?")){
      setStations(stations.filter(s => s.id !== id));
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h3>Manage Stations</h3>
      <div style={{ marginBottom: 20 }}>
        <input
          type="text"
          name="name"
          placeholder="Station Name"
          value={form.name}
          onChange={handleChange}
          style={{ marginRight: 10 }}
        />
        <input
          type="text"
          name="address"
          placeholder="Station Address"
          value={form.address}
          onChange={handleChange}
          style={{ marginRight: 10 }}
        />
        {isEditing ? (
          <button onClick={handleUpdate}>Update</button>
        ) : (
          <button onClick={handleAdd}>Add</button>
        )}
      </div>
      <table border={1} cellPadding={5} cellSpacing={0} width="100%">
        <thead>
          <tr>
            <th>ID</th><th>Name</th><th>Address</th><th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {stations.map(station => (
            <tr key={station.id}>
              <td>{station.id}</td>
              <td>{station.name}</td>
              <td>{station.address}</td>
              <td>
                <button onClick={() => handleEdit(station)} style={{ marginRight: 10 }}>Edit</button>
                <button onClick={() => handleDelete(station.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
