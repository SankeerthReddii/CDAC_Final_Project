import React, { useState, useEffect } from 'react';
import { toast } from 'react-toastify';
import { 
  Train, 
  Calendar, 
  MapPin, 
  Clock, 
  Search, 
  Filter,
  Download,
  Eye,
  X,
  CheckCircle,
  XCircle,
  Clock as ClockIcon
} from 'lucide-react';
import axios from 'axios';
import API_CONFIG from '../../config/api';

const MyBookings = () => {
  const [bookings, setBookings] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState('all');
  const [dateFilter, setDateFilter] = useState('all');

  useEffect(() => {
    fetchBookings();
  }, []);

  const fetchBookings = async () => {
    try {
      const response = await axios.get(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.TICKET.BOOKINGS}`);
      setBookings(response.data);
    } catch (error) {
      console.error('Error fetching bookings:', error);
      // Set mock data for demo
      setBookings([
        {
          id: 1,
          bookingId: 'BK001',
          routeName: 'Blue Line',
          fromStation: 'Central Station',
          toStation: 'North Terminal',
          travelDate: '2024-01-15',
          ticketCount: 2,
          totalAmount: 30.00,
          status: 'confirmed',
          passengerClass: 'economy',
          bookingDate: '2024-01-10',
          departureTime: '09:00 AM'
        },
        {
          id: 2,
          bookingId: 'BK002',
          routeName: 'Red Line',
          fromStation: 'South Hub',
          toStation: 'East Gateway',
          travelDate: '2024-01-20',
          ticketCount: 1,
          totalAmount: 20.00,
          status: 'pending',
          passengerClass: 'business',
          bookingDate: '2024-01-12',
          departureTime: '02:30 PM'
        },
        {
          id: 3,
          bookingId: 'BK003',
          routeName: 'Green Line',
          fromStation: 'West Junction',
          toStation: 'Airport Metro',
          travelDate: '2024-01-08',
          ticketCount: 3,
          totalAmount: 75.00,
          status: 'completed',
          passengerClass: 'first',
          bookingDate: '2024-01-05',
          departureTime: '11:15 AM'
        }
      ]);
    } finally {
      setLoading(false);
    }
  };

  const getStatusColor = (status) => {
    switch (status) {
      case 'confirmed':
        return 'bg-green-100 text-green-800';
      case 'pending':
        return 'bg-yellow-100 text-yellow-800';
      case 'cancelled':
        return 'bg-red-100 text-red-800';
      case 'completed':
        return 'bg-blue-100 text-blue-800';
      default:
        return 'bg-gray-100 text-gray-800';
    }
  };

  const getStatusIcon = (status) => {
    switch (status) {
      case 'confirmed':
        return <CheckCircle className="w-4 h-4" />;
      case 'pending':
        return <ClockIcon className="w-4 h-4" />;
      case 'cancelled':
        return <XCircle className="w-4 h-4" />;
      case 'completed':
        return <CheckCircle className="w-4 h-4" />;
      default:
        return <ClockIcon className="w-4 h-4" />;
    }
  };

  const filteredBookings = bookings.filter(booking => {
    const matchesSearch = 
      booking.bookingId.toLowerCase().includes(searchTerm.toLowerCase()) ||
      booking.routeName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      booking.fromStation.toLowerCase().includes(searchTerm.toLowerCase()) ||
      booking.toStation.toLowerCase().includes(searchTerm.toLowerCase());
    
    const matchesStatus = statusFilter === 'all' || booking.status === statusFilter;
    
    const matchesDate = dateFilter === 'all' || 
      (dateFilter === 'upcoming' && new Date(booking.travelDate) > new Date()) ||
      (dateFilter === 'past' && new Date(booking.travelDate) <= new Date());
    
    return matchesSearch && matchesStatus && matchesDate;
  });

  const cancelBooking = async (bookingId) => {
    try {
      await axios.post(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.TICKET.CANCEL}`, { bookingId });
      toast.success('Booking cancelled successfully');
      fetchBookings();
    } catch (error) {
      const message = error.response?.data?.message || 'Failed to cancel booking';
      toast.error(message);
    }
  };

  const downloadTicket = (booking) => {
    // Mock ticket download functionality
    toast.info('Ticket download feature coming soon!');
  };

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 py-8">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">My Bookings</h1>
          <p className="text-gray-600">View and manage your metro ticket bookings</p>
        </div>

        {/* Filters and Search */}
        <div className="card mb-6">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            {/* Search */}
            <div className="md:col-span-2">
              <div className="relative">
                <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                <input
                  type="text"
                  placeholder="Search bookings..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  className="input-field pl-10"
                />
              </div>
            </div>

            {/* Status Filter */}
            <div>
              <select
                value={statusFilter}
                onChange={(e) => setStatusFilter(e.target.value)}
                className="input-field"
              >
                <option value="all">All Status</option>
                <option value="confirmed">Confirmed</option>
                <option value="pending">Pending</option>
                <option value="cancelled">Cancelled</option>
                <option value="completed">Completed</option>
              </select>
            </div>

            {/* Date Filter */}
            <div>
              <select
                value={dateFilter}
                onChange={(e) => setDateFilter(e.target.value)}
                className="input-field"
              >
                <option value="all">All Dates</option>
                <option value="upcoming">Upcoming</option>
                <option value="past">Past</option>
              </select>
            </div>
          </div>
        </div>

        {/* Bookings List */}
        <div className="space-y-4">
          {filteredBookings.length === 0 ? (
            <div className="card text-center py-12">
              <Train className="w-16 h-16 text-gray-300 mx-auto mb-4" />
              <h3 className="text-lg font-medium text-gray-900 mb-2">No bookings found</h3>
              <p className="text-gray-600">
                {searchTerm || statusFilter !== 'all' || dateFilter !== 'all' 
                  ? 'Try adjusting your filters or search terms'
                  : 'You haven\'t made any bookings yet'}
              </p>
            </div>
          ) : (
            filteredBookings.map((booking) => (
              <div key={booking.id} className="card">
                <div className="flex flex-col lg:flex-row lg:items-center lg:justify-between">
                  {/* Booking Details */}
                  <div className="flex-1">
                    <div className="flex items-start justify-between mb-4">
                      <div>
                        <h3 className="text-lg font-semibold text-gray-900">
                          {booking.routeName}
                        </h3>
                        <p className="text-sm text-gray-600">Booking ID: {booking.bookingId}</p>
                      </div>
                      <div className="flex items-center space-x-2">
                        <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${getStatusColor(booking.status)}`}>
                          {getStatusIcon(booking.status)}
                          <span className="ml-1 capitalize">{booking.status}</span>
                        </span>
                      </div>
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
                      <div className="flex items-center space-x-2">
                        <MapPin className="w-4 h-4 text-gray-400" />
                        <div>
                          <p className="text-sm font-medium text-gray-900">{booking.fromStation}</p>
                          <p className="text-xs text-gray-600">From</p>
                        </div>
                      </div>

                      <div className="flex items-center space-x-2">
                        <MapPin className="w-4 h-4 text-gray-400" />
                        <div>
                          <p className="text-sm font-medium text-gray-900">{booking.toStation}</p>
                          <p className="text-xs text-gray-600">To</p>
                        </div>
                      </div>

                      <div className="flex items-center space-x-2">
                        <Calendar className="w-4 h-4 text-gray-400" />
                        <div>
                          <p className="text-sm font-medium text-gray-900">
                            {new Date(booking.travelDate).toLocaleDateString()}
                          </p>
                          <p className="text-xs text-gray-600">Travel Date</p>
                        </div>
                      </div>

                      <div className="flex items-center space-x-2">
                        <Clock className="w-4 h-4 text-gray-400" />
                        <div>
                          <p className="text-sm font-medium text-gray-900">{booking.departureTime}</p>
                          <p className="text-xs text-gray-600">Departure</p>
                        </div>
                      </div>
                    </div>

                    <div className="mt-4 flex items-center justify-between">
                      <div className="flex items-center space-x-6 text-sm text-gray-600">
                        <span>Tickets: {booking.ticketCount}</span>
                        <span>Class: {booking.passengerClass}</span>
                        <span className="font-semibold text-gray-900">
                          Total: ${booking.totalAmount}
                        </span>
                      </div>
                    </div>
                  </div>

                  {/* Actions */}
                  <div className="mt-4 lg:mt-0 lg:ml-6 flex items-center space-x-2">
                    <button
                      onClick={() => downloadTicket(booking)}
                      className="inline-flex items-center px-3 py-2 border border-gray-300 shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
                    >
                      <Download className="w-4 h-4 mr-1" />
                      Download
                    </button>

                    {booking.status === 'confirmed' && new Date(booking.travelDate) > new Date() && (
                      <button
                        onClick={() => cancelBooking(booking.bookingId)}
                        className="inline-flex items-center px-3 py-2 border border-red-300 shadow-sm text-sm leading-4 font-medium rounded-md text-red-700 bg-white hover:bg-red-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
                      >
                        <X className="w-4 h-4 mr-1" />
                        Cancel
                      </button>
                    )}
                  </div>
                </div>
              </div>
            ))
          )}
        </div>

        {/* Summary Stats */}
        {filteredBookings.length > 0 && (
          <div className="mt-8 grid grid-cols-1 md:grid-cols-4 gap-4">
            <div className="card text-center">
              <div className="text-2xl font-bold text-blue-600">{filteredBookings.length}</div>
              <div className="text-sm text-gray-600">Total Bookings</div>
            </div>
            <div className="card text-center">
              <div className="text-2xl font-bold text-green-600">
                {filteredBookings.filter(b => b.status === 'confirmed').length}
              </div>
              <div className="text-sm text-gray-600">Confirmed</div>
            </div>
            <div className="card text-center">
              <div className="text-2xl font-bold text-yellow-600">
                {filteredBookings.filter(b => b.status === 'pending').length}
              </div>
              <div className="text-sm text-gray-600">Pending</div>
            </div>
            <div className="card text-center">
              <div className="text-2xl font-bold text-gray-600">
                ${filteredBookings.reduce((sum, b) => sum + b.totalAmount, 0).toFixed(2)}
              </div>
              <div className="text-sm text-gray-600">Total Spent</div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default MyBookings; 