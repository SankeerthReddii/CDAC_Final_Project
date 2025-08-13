//namespace Metro_Ticket_Booking.Services
//{
//    public class PaymentService
//    {
//    }
//}

using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Metro_Ticket_Booking.Services
{
    public class PaymentService
    {
        private readonly RazorpayClient _razorpayClient;
        private readonly string _keySecret;

        public PaymentService(IConfiguration configuration)
        {
            var keyId = configuration["Razorpay:KeyId"];
            _keySecret = configuration["Razorpay:KeySecret"];

            if (string.IsNullOrEmpty(keyId) || string.IsNullOrEmpty(_keySecret))
            {
                throw new Exception("Razorpay keys are not configured properly in appsettings.json or environment variables.");
            }

            _razorpayClient = new RazorpayClient(keyId, _keySecret);
        }


        /// <summary>
        /// Creates a Razorpay order and returns it.
        /// </summary>
        public Order CreateOrder(int amount, string currency = "INR")
        {
            try
            {
                var options = new Dictionary<string, object>
                {
                    { "amount", amount }, // amount in paise
                    { "currency", currency },
                    { "receipt", Guid.NewGuid().ToString() },
                    { "payment_capture", 1 } // auto capture
                };

                return _razorpayClient.Order.Create(options);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating Razorpay order: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Verifies a Razorpay payment signature.
        /// </summary>
        public bool VerifySignature(string orderId, string paymentId, string signature)
        {
            if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(paymentId) || string.IsNullOrEmpty(signature))
            {
                return false; // invalid input
            }

            var payload = $"{orderId}|{paymentId}";
            var secretBytes = Encoding.UTF8.GetBytes(_keySecret);

            using var hmac = new HMACSHA256(secretBytes);
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var generatedSignature = BytesToLowercaseHex(hashBytes);

            return generatedSignature == signature.ToLower();
        }

        private static string BytesToLowercaseHex(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2")); // always lowercase hex
            }
            return sb.ToString();
        }
    }
}