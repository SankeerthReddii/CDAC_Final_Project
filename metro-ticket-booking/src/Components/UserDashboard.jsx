import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

export default function UserDashboard() {
  // States for stations list, selections, tickets, price, and errors
  const [stations, setStations] = useState([]);
  const [fromStation, setFromStation] = useState("");
  const [toStation, setToStation] = useState("");
  const [ticketCount, setTicketCount] = useState(1);
  const [ticketPrice, setTicketPrice] = useState(0);
  const [error, setError] = useState("");
  const navigate = useNavigate();

  // Fixed price per ticket (could be dynamic based on distance)
  const pricePerTicket = 50;

  // Load stations from backend on mount
  useEffect(() => {
    axios
      .get("http://localhost:5000/api/stations")
      .then((res) => setStations(res.data))
      .catch(() =>
        setError("Unable to fetch station list. Please try again later.")
      );
  }, []);

  // Update price when stations or ticket count change
  useEffect(() => {
    if (fromStation && toStation && ticketCount > 0) {
      setTicketPrice(ticketCount * pricePerTicket);
    } else {
      setTicketPrice(0);
    }
  }, [fromStation, toStation, ticketCount]);

  // Increment/decrement ticket count with limits (1 to 10)
  const incrementTickets = () => {
    if (ticketCount < 10) setTicketCount(ticketCount + 1);
  };
  const decrementTickets = () => {
    if (ticketCount > 1) setTicketCount(ticketCount - 1);
  };

  // Load Razorpay checkout script dynamically
  function loadRazorpayScript(src) {
    return new Promise((resolve) => {
      const script = document.createElement("script");
      script.src = src;
      script.onload = () => resolve(true);
      script.onerror = () => resolve(false);
      document.body.appendChild(script);
    });
  }

  // Handle payment on Proceed button click
  const handleProceed = async () => {
    if (!fromStation || !toStation) {
      alert("Please select both From and To stations.");
      return;
    }
    if (fromStation === toStation) {
      alert("From and To stations cannot be the same.");
      return;
    }

    const res = await loadRazorpayScript(
      "https://checkout.razorpay.com/v1/checkout.js"
    );
    if (!res) {
      alert("Razorpay SDK failed to load. Are you online?");
      return;
    }

    try {
      // Create order on backend with amount in paise
      const orderResponse = await axios.post(
        "http://localhost:5000/payment/orders",
        {
          amount: ticketPrice * 100,
          currency: "INR",
          // You can also send user info here if required
        }
      );

      const { id: order_id, amount, currency } = orderResponse.data;

      const options = {
        key: "rzp_test_ATb9AKrFWqKJpj", // Replace with your Razorpay Key ID
        amount: amount.toString(),
        currency: currency,
        name: "Metro Ticket Booking",
        description: `Tickets from station ID ${fromStation} to ${toStation}`,
        order_id: order_id,
        handler: async function (response) {
          // Send payment details to backend to verify and finalize booking
          await axios.post("http://localhost:5000/payment/success", {
            razorpayOrderId: response.razorpay_order_id,
            razorpayPaymentId: response.razorpay_payment_id,
            razorpaySignature: response.razorpay_signature,
            fromStation,
            toStation,
            ticketCount,
            ticketPrice,
          });
          alert("Payment successful! Your tickets have been booked.");
          navigate("/payment-success"); // Redirect to success page
          // Optionally reset state or redirect user after booking
        },
        prefill: {
          // Optionally provide user details here to autofill checkout
          name: "User Name",
          email: "user@example.com",
          contact: "9999999999",
        },
        theme: {
          color: "#1976d2",
        },
      };

      const paymentObject = new window.Razorpay(options);
      paymentObject.open();
    } catch (err) {
      alert("Payment processing failed. Please try again.");
    }
  };

  return (
    <div style={{ maxWidth: 600, margin: "auto", padding: 20, position: 'relative' }}>
      {/* Circular profile button on top right */}
      <button
        onClick={() => navigate("/user/profile")}
        style={{
          position: "absolute",
          top: 0,
          right: 0,
          width: 40,
          height: 40,
          borderRadius: "50%",
          border: "none",
          backgroundColor: "#1976d2",
          color: "white",
          fontWeight: "bold",
          cursor: "pointer",
          fontSize: 18,
          lineHeight: "40px",
          textAlign: "center",
          padding: 0,
        }}
        title="User Profile"
      >
        U
      </button>

      <h2>User Dashboard - Book Metro Ticket</h2>
      {error && <div style={{ color: "red", marginBottom: 10 }}>{error}</div>}

      <div style={{ marginBottom: 15 }}>
        <label>From Station:</label>
        <select
          onChange={(e) => setFromStation(e.target.value)}
          value={fromStation}
          style={{ marginLeft: 10, padding: 5 }}
        >
          <option value="">Select Station</option>
          {stations.map((station) => (
            <option key={station.id} value={station.id}>
              {station.name}
            </option>
          ))}
        </select>
      </div>

      <div style={{ marginBottom: 15 }}>
        <label>To Station:</label>
        <select
          onChange={(e) => setToStation(e.target.value)}
          value={toStation}
          style={{ marginLeft: 38, padding: 5 }}
        >
          <option value="">Select Station</option>
          {stations.map((station) => (
            <option key={station.id} value={station.id}>
              {station.name}
            </option>
          ))}
        </select>
      </div>

      <div style={{ marginBottom: 15 }}>
        <label>No. of Tickets:</label>
        <button
          onClick={decrementTickets}
          style={{ marginLeft: 10, padding: "2px 10px", cursor: "pointer" }}
        >
          -
        </button>
        <span style={{ margin: "0 15px" }}>{ticketCount}</span>
        <button
          onClick={incrementTickets}
          style={{ padding: "2px 10px", cursor: "pointer" }}
        >
          +
        </button>
      </div>

      <div style={{ marginBottom: 15 }}>
        <strong>Price: </strong>₹{ticketPrice}
      </div>

      <button
        onClick={handleProceed}
        style={{
          padding: "10px 20px",
          backgroundColor: "#1976d2",
          color: "white",
          border: "none",
          borderRadius: 4,
          cursor: "pointer",
        }}
      >
        Proceed to Pay
      </button>

      <hr style={{ margin: "30px 0" }} />

      <nav style={{ display: "flex", justifyContent: "space-around" }}>
        <button style={{ padding: 10, cursor: "pointer" }}>Apply MetroCard</button>
        <button style={{ padding: 10, cursor: "pointer" }}>Recharge Card</button>
        <button style={{ padding: 10, cursor: "pointer" }}>Raise Complaint</button>
      </nav>
    </div>
  );
}
