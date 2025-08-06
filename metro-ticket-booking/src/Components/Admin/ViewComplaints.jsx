import React, { useEffect, useState } from "react";
import axios from "axios";

export default function ViewComplaints() {
  const [complaints, setComplaints] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [replyForm, setReplyForm] = useState({});

  // Fetch complaints from backend
  useEffect(() => {
    axios
      .get("http://localhost:5000/api/complaints")
      .then(res => {
        setComplaints(res.data);
        setLoading(false);
      })
      .catch(() => {
        setError("Failed to load complaints.");
        setLoading(false);
      });
  }, []);

  // Handle reply input change
  const handleReplyChange = (id, value) => {
    setReplyForm(prev => ({ ...prev, [id]: value }));
  };

  // Submit reply
  const handleReplySubmit = (id) => {
    const reply = replyForm[id];
    if (!reply || reply.trim() === "") {
      alert("Reply cannot be empty.");
      return;
    }

    axios.post(`http://localhost:5000/api/complaints/reply/${id}`, { reply })
      .then(() => {
        alert("Reply sent successfully.");
        setReplyForm(prev => ({ ...prev, [id]: "" }));
      })
      .catch(() => {
        alert("Failed to send reply. Try again.");
      });
  };

  if (loading) return <p>Loading complaints...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;
  if (!complaints.length) return <p>No complaints found.</p>;

  return (
    <div style={{ padding: 20 }}>
      <h3>User Complaints</h3>
      <table border={1} cellPadding={5} cellSpacing={0} width="100%">
        <thead>
          <tr>
            <th>User</th>
            <th>Complaint</th>
            <th>Reply</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {complaints.map(c => (
            <tr key={c.id}>
              <td>{c.userName}</td>
              <td>{c.message}</td>
              <td>{c.reply || "(No reply yet)"}</td>
              <td>
                <input
                  type="text"
                  placeholder="Type reply..."
                  value={replyForm[c.id] || ""}
                  onChange={e => handleReplyChange(c.id, e.target.value)}
                  style={{ marginRight: 10, width: "80%" }}
                />
                <button onClick={() => handleReplySubmit(c.id)}>Send</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
