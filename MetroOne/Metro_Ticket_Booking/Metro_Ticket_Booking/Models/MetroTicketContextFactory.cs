using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySqlConnector;  // Make sure this namespace is available
using Metro_Ticket_Booking.Models;  // Adjust if your namespace differs

namespace Metro_Ticket_Booking
{
    public class MetroTicketContextFactory : IDesignTimeDbContextFactory<MetroTicketContext>
    {
        public MetroTicketContext CreateDbContext(string[] args)
        {
            string hostname = "database-1.cv2qomuemf6s.eu-north-1.rds.amazonaws.com";

            // Resolve IPv4 address to avoid IPv6 connection issues
            var ipv4Address = Dns.GetHostAddresses(hostname)
                .First(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                .ToString();

            var csb = new MySqlConnectionStringBuilder
            {
                Server = ipv4Address,
                Database = "MetroOne",
                UserID = "admin",
                Password = "nimitesh123",
                SslMode = MySqlSslMode.Preferred
            };

            var optionsBuilder = new DbContextOptionsBuilder<MetroTicketContext>();
            optionsBuilder.UseMySql(csb.ConnectionString, ServerVersion.AutoDetect(csb.ConnectionString));

            return new MetroTicketContext(optionsBuilder.Options);
        }
    }
}
