import React, { useState } from "react";

export default function ManageMetros() {
  const [metros, setMetros] = useState([
    { id: 1, name: "Metro Line 1", capacity: 200 }
  ]);
  const [form, setForm] = useState({ id: null, name: "", capacity: "" });
  const [isEditing, setIsEditing] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleAdd = () => {
    if (!form.name || !form.capacity) {
      alert("Please fill all fields.");
      return;
    }
    const newMetro = { ...form, id: Date.now(), capacity: Number(form.capacity) };
    setMetros([...metros, newMetro]);
    setForm({ id: null, name: "", capacity: "" });
  };

  const handleEdit = (metro) => {
    setForm(metro);
    setIsEditing(true);
  };

  const handleUpdate = () => {
    if (!form.name || !form.capacity) {
      alert("Please fill all fields.");
      return;
    }
    setMetros(metros.map(m => m.id === form.id ? { ...form, capacity: Number(form.capacity) } : m));
    setForm({ id: null, name: "", capacity: "" });
    setIsEditing(false);
  };

  const handleDelete = (id) => {
    if(window.confirm("Are you sure?")) {
      setMetros(metros.filter(m => m.id !== id));
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h3>Manage Metros</h3>
      <div style={{ marginBottom: 20 }}>
        <input
          type="text"
          name="name"
          placeholder="Metro Name"
          value={form.name}
          onChange={handleChange}
          style={{ marginRight: 10 }}
        />
        <input
          type="number"
          name="capacity"
          placeholder="Capacity"
          value={form.capacity}
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
            <th>ID</th><th>Name</th><th>Capacity</th><th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {metros.map(metro => (
            <tr key={metro.id}>
              <td>{metro.id}</td>
              <td>{metro.name}</td>
              <td>{metro.capacity}</td>
              <td>
                <button onClick={() => handleEdit(metro)} style={{ marginRight: 10 }}>Edit</button>
                <button onClick={() => handleDelete(metro.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
