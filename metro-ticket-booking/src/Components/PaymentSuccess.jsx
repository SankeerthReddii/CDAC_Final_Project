import React from "react";

// You can use a free QR like below or generate your own
const STATIC_QR_URL = "https://api.qrserver.com/v1/create-qr-code/?data=ThankYouForUsingMetroApp&size=200x200";

export default function PaymentSuccess() {
  return (
    <div style={{ maxWidth: 400, margin: "auto", padding: 20, textAlign: "center" }}>
      <h2>Payment Successful!</h2>
      <p>Your ticket has been successfully booked.</p>
      <div style={{ margin: "30px 0" }}>
        <img src={STATIC_QR_URL} alt="QR Code" style={{ width: 200, height: 200 }} />
      </div>
      <p>Use this QR code for metro entry.</p>
    </div>
  );
}
