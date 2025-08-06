import React, { useState } from "react";

export default function ManageRoutes() {
  const [routes, setRoutes] = useState([
    { id: 1, name: "Route A", start: "Station1", end: "Station5" }
  ]);
  const [form, setForm] = useState({ id: null, name: "", start: "", end: "" });
  const [isEditing, setIsEditing] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleAdd = () => {
    if (!form.name || !form.start || !form.end) {
      alert("Please fill all fields.");
      return;
    }
    const newRoute = { ...form, id: Date.now() };
    setRoutes([...routes, newRoute]);
    setForm({ id: null, name: "", start: "", end: "" });
  };

  const handleEdit = (route) => {
    setForm(route);
    setIsEditing(true);
  };

  const handleUpdate = () => {
    if (!form.name || !form.start || !form.end) {
      alert("Please fill all fields.");
      return;
    }
    setRoutes(routes.map(r => r.id === form.id ? form : r));
    setForm({ id: null, name: "", start: "", end: "" });
    setIsEditing(false);
  };

  const handleDelete = (id) => {
    if(window.confirm("Are you sure?")) {
      setRoutes(routes.filter(r => r.id !== id));
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h3>Manage Routes</h3>
      <div style={{ marginBottom: 20 }}>
        <input
          type="text"
          name="name"
          placeholder="Route Name"
          value={form.name}
          onChange={handleChange}
          style={{ marginRight: 10 }}
        />
        <input
          type="text"
          name="start"
          placeholder="Start Station"
          value={form.start}
          onChange={handleChange}
          style={{ marginRight: 10 }}
        />
        <input
          type="text"
          name="end"
          placeholder="End Station"
          value={form.end}
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
            <th>ID</th><th>Name</th><th>Start</th><th>End</th><th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {routes.map(route => (
            <tr key={route.id}>
              <td>{route.id}</td>
              <td>{route.name}</td>
              <td>{route.start}</td>
              <td>{route.end}</td>
              <td>
                <button onClick={() => handleEdit(route)} style={{ marginRight: 10 }}>Edit</button>
                <button onClick={() => handleDelete(route.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
