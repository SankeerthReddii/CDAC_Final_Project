import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { toast } from 'react-toastify';
import { 
  Train, 
  MapPin, 
  Calendar, 
  Users, 
  CreditCard, 
  ArrowRight,
  Clock,
  DollarSign
} from 'lucide-react';
import axios from 'axios';
import API_CONFIG from '../../config/api';

const validationSchema = Yup.object({
  routeId: Yup.string().required('Please select a route'),
  fromStation: Yup.string().required('Please select departure station'),
  toStation: Yup.string().required('Please select destination station'),
  travelDate: Yup.date().min(new Date(), 'Travel date must be in the future').required('Please select travel date'),
  ticketCount: Yup.number().min(1, 'At least 1 ticket required').max(10, 'Maximum 10 tickets per booking').required('Please enter number of tickets'),
  passengerClass: Yup.string().required('Please select passenger class'),
});

const BookTicket = () => {
  const navigate = useNavigate();
  const [routes, setRoutes] = useState([]);
  const [stations, setStations] = useState([]);
  const [metros, setMetros] = useState([]);
  const [selectedRoute, setSelectedRoute] = useState(null);
  const [loading, setLoading] = useState(true);
  const [bookingLoading, setBookingLoading] = useState(false);

  const {
    register,
    handleSubmit,
    watch,
    setValue,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(validationSchema),
  });

  const watchedRouteId = watch('routeId');
  const watchedTicketCount = watch('ticketCount') || 1;
  const watchedPassengerClass = watch('passengerClass');

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    if (watchedRouteId) {
      const route = routes.find(r => r.id === parseInt(watchedRouteId));
      setSelectedRoute(route);
      if (route) {
        setValue('fromStation', route.fromStation);
        setValue('toStation', route.toStation);
      }
    }
  }, [watchedRouteId, routes, setValue]);

  const fetchData = async () => {
    try {
      const [routesRes, stationsRes, metrosRes] = await Promise.all([
        axios.get(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.ROUTE.ALL}`),
        axios.get(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.STATION.ALL}`),
        axios.get(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.METRO.ALL}`)
      ]);

      setRoutes(routesRes.data);
      setStations(stationsRes.data);
      setMetros(metrosRes.data);
    } catch (error) {
      console.error('Error fetching data:', error);
      // Set mock data for demo
      setRoutes([
        { id: 1, name: 'Blue Line', fromStation: 'Central Station', toStation: 'North Terminal', duration: '25 min', price: 15 },
        { id: 2, name: 'Red Line', fromStation: 'South Hub', toStation: 'East Gateway', duration: '30 min', price: 20 },
        { id: 3, name: 'Green Line', fromStation: 'West Junction', toStation: 'Airport Metro', duration: '45 min', price: 25 },
        { id: 4, name: 'Yellow Line', fromStation: 'Central Station', toStation: 'South Hub', duration: '20 min', price: 12 },
      ]);
      setStations([
        { id: 1, name: 'Central Station', location: 'City Center' },
        { id: 2, name: 'North Terminal', location: 'North District' },
        { id: 3, name: 'South Hub', location: 'South District' },
        { id: 4, name: 'East Gateway', location: 'East District' },
        { id: 5, name: 'West Junction', location: 'West District' },
        { id: 6, name: 'Airport Metro', location: 'Airport Area' },
      ]);
      setMetros([
        { id: 1, name: 'Metro Express 1', capacity: 200, status: 'active' },
        { id: 2, name: 'Metro Express 2', capacity: 200, status: 'active' },
        { id: 3, name: 'Metro Express 3', capacity: 200, status: 'active' },
      ]);
    } finally {
      setLoading(false);
    }
  };

  const calculateTotalPrice = () => {
    if (!selectedRoute || !watchedPassengerClass) return 0;
    
    const basePrice = selectedRoute.price || 15;
    const classMultiplier = {
      'economy': 1,
      'business': 1.5,
      'first': 2
    };
    
    return (basePrice * classMultiplier[watchedPassengerClass] * watchedTicketCount).toFixed(2);
  };

  const onSubmit = async (data) => {
    setBookingLoading(true);
    try {
      const bookingData = {
        ...data,
        totalAmount: calculateTotalPrice(),
        routeId: parseInt(data.routeId),
        ticketCount: parseInt(data.ticketCount),
      };

      const response = await axios.post(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.TICKET.BOOK}`, bookingData);
      
      toast.success('Ticket booked successfully!');
      navigate('/payment-success', { 
        state: { 
          bookingId: response.data.bookingId,
          amount: calculateTotalPrice()
        } 
      });
    } catch (error) {
      const message = error.response?.data?.message || 'Booking failed. Please try again.';
      toast.error(message);
    } finally {
      setBookingLoading(false);
    }
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
      <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8">
        {/* Header */}
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">Book Your Ticket</h1>
          <p className="text-gray-600">Select your route and travel details</p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Booking Form */}
          <div className="lg:col-span-2">
            <div className="card">
              <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
                {/* Route Selection */}
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Select Route
                  </label>
                  <select
                    {...register('routeId')}
                    className="input-field"
                  >
                    <option value="">Choose a route</option>
                    {routes.map((route) => (
                      <option key={route.id} value={route.id}>
                        {route.name} - {route.fromStation} to {route.toStation}
                      </option>
                    ))}
                  </select>
                  {errors.routeId && (
                    <p className="mt-1 text-sm text-red-600">{errors.routeId.message}</p>
                  )}
                </div>

                {/* Station Details */}
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      From Station
                    </label>
                    <div className="relative">
                      <MapPin className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                      <input
                        {...register('fromStation')}
                        type="text"
                        className="input-field pl-10"
                        placeholder="Departure station"
                        readOnly
                      />
                    </div>
                    {errors.fromStation && (
                      <p className="mt-1 text-sm text-red-600">{errors.fromStation.message}</p>
                    )}
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      To Station
                    </label>
                    <div className="relative">
                      <MapPin className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                      <input
                        {...register('toStation')}
                        type="text"
                        className="input-field pl-10"
                        placeholder="Destination station"
                        readOnly
                      />
                    </div>
                    {errors.toStation && (
                      <p className="mt-1 text-sm text-red-600">{errors.toStation.message}</p>
                    )}
                  </div>
                </div>

                {/* Travel Details */}
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      Travel Date
                    </label>
                    <div className="relative">
                      <Calendar className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                      <input
                        {...register('travelDate')}
                        type="date"
                        className="input-field pl-10"
                        min={new Date().toISOString().split('T')[0]}
                      />
                    </div>
                    {errors.travelDate && (
                      <p className="mt-1 text-sm text-red-600">{errors.travelDate.message}</p>
                    )}
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      Number of Tickets
                    </label>
                    <div className="relative">
                      <Users className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                      <input
                        {...register('ticketCount')}
                        type="number"
                        min="1"
                        max="10"
                        className="input-field pl-10"
                        placeholder="1"
                      />
                    </div>
                    {errors.ticketCount && (
                      <p className="mt-1 text-sm text-red-600">{errors.ticketCount.message}</p>
                    )}
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      Passenger Class
                    </label>
                    <select
                      {...register('passengerClass')}
                      className="input-field"
                    >
                      <option value="">Select class</option>
                      <option value="economy">Economy</option>
                      <option value="business">Business</option>
                      <option value="first">First Class</option>
                    </select>
                    {errors.passengerClass && (
                      <p className="mt-1 text-sm text-red-600">{errors.passengerClass.message}</p>
                    )}
                  </div>
                </div>

                {/* Submit Button */}
                <button
                  type="submit"
                  disabled={bookingLoading}
                  className="w-full btn-primary py-3 text-base font-medium disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  {bookingLoading ? (
                    <div className="flex items-center justify-center">
                      <div className="spinner w-5 h-5 mr-2"></div>
                      Processing Booking...
                    </div>
                  ) : (
                    <div className="flex items-center justify-center">
                      <CreditCard className="w-5 h-5 mr-2" />
                      Proceed to Payment
                    </div>
                  )}
                </button>
              </form>
            </div>
          </div>

          {/* Booking Summary */}
          <div className="lg:col-span-1">
            <div className="card sticky top-8">
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Booking Summary</h3>
              
              {selectedRoute ? (
                <div className="space-y-4">
                  <div className="flex items-center space-x-3">
                    <Train className="w-5 h-5 text-blue-600" />
                    <div>
                      <p className="font-medium text-gray-900">{selectedRoute.name}</p>
                      <p className="text-sm text-gray-600">{selectedRoute.fromStation} → {selectedRoute.toStation}</p>
                    </div>
                  </div>

                  <div className="flex items-center space-x-3">
                    <Clock className="w-5 h-5 text-gray-400" />
                    <span className="text-sm text-gray-600">Duration: {selectedRoute.duration}</span>
                  </div>

                  <div className="border-t border-gray-200 pt-4 space-y-2">
                    <div className="flex justify-between text-sm">
                      <span className="text-gray-600">Base Price:</span>
                      <span className="font-medium">${selectedRoute.price || 15}</span>
                    </div>
                    
                    {watchedPassengerClass && (
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-600">Class ({watchedPassengerClass}):</span>
                        <span className="font-medium">
                          {watchedPassengerClass === 'economy' ? '1x' : 
                           watchedPassengerClass === 'business' ? '1.5x' : '2x'}
                        </span>
                      </div>
                    )}
                    
                    <div className="flex justify-between text-sm">
                      <span className="text-gray-600">Tickets:</span>
                      <span className="font-medium">{watchedTicketCount}</span>
                    </div>
                    
                    <div className="border-t border-gray-200 pt-2">
                      <div className="flex justify-between font-semibold text-lg">
                        <span>Total:</span>
                        <span className="text-blue-600">${calculateTotalPrice()}</span>
                      </div>
                    </div>
                  </div>
                </div>
              ) : (
                <div className="text-center py-8">
                  <DollarSign className="w-12 h-12 text-gray-300 mx-auto mb-4" />
                  <p className="text-gray-500">Select a route to see pricing</p>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BookTicket; 