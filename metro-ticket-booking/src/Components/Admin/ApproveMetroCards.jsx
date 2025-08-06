import React, { useEffect, useState } from "react";
import axios from "axios";

export default function ApproveMetroCards() {
  const [pendingCards, setPendingCards] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  // Fetch pending metro card applications from backend
  useEffect(() => {
    axios.get("http://localhost:5000/api/metrocards/pending")
      .then(response => {
        setPendingCards(response.data);
        setLoading(false);
      })
      .catch(err => {
        setError("Failed to load pending metro cards.");
        setLoading(false);
      });
  }, []);

  // Approve a card
  const handleApprove = (cardId) => {
    axios.post(`http://localhost:5000/api/metrocards/approve/${cardId}`)
      .then(() => {
        // Remove approved card from list
        setPendingCards(prev => prev.filter(card => card.id !== cardId));
        alert("Metro card approved successfully.");
      })
      .catch(() => {
        alert("Failed to approve metro card. Try again.");
      });
  };

  if (loading) return <p>Loading pending metro cards...</p>;

  if (error) return <p style={{ color: "red" }}>{error}</p>;

  if (!pendingCards.length) return <p>No pending metro card applications.</p>;

  return (
    <div style={{ padding: 20 }}>
      <h3>Approve Metro Cards</h3>
      <table border={1} cellPadding={5} cellSpacing={0} width="100%">
        <thead>
          <tr>
            <th>Applicant Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Application Date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {pendingCards.map(card => (
            <tr key={card.id}>
              <td>{card.name}</td>
              <td>{card.email}</td>
              <td>{card.phone}</td>
              <td>{new Date(card.applicationDate).toLocaleDateString()}</td>
              <td>
                <button
                  onClick={() => handleApprove(card.id)}
                  style={{
                    backgroundColor: "green",
                    color: "white",
                    padding: "5px 10px",
                    border: "none",
                    borderRadius: 4,
                    cursor: "pointer"
                  }}
                >
                  Approve
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
