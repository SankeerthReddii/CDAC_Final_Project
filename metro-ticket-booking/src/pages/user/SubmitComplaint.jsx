import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { toast } from 'react-toastify';
import { 
  MessageSquare, 
  AlertTriangle, 
  Clock, 
  CheckCircle, 
  XCircle,
  Send,
  FileText,
  Calendar,
  User
} from 'lucide-react';
import axios from 'axios';
import API_CONFIG from '../../config/api';

const validationSchema = Yup.object({
  subject: Yup.string().required('Please enter a subject'),
  description: Yup.string().required('Please describe your complaint'),
  category: Yup.string().required('Please select a category'),
  priority: Yup.string().required('Please select priority level'),
});

const SubmitComplaint = () => {
  const [complaints, setComplaints] = useState([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(validationSchema),
  });

  useEffect(() => {
    fetchComplaints();
  }, []);

  const fetchComplaints = async () => {
    try {
      const response = await axios.get(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.COMPLAINT.USER_COMPLAINTS}`);
      setComplaints(response.data);
    } catch (error) {
      console.error('Error fetching complaints:', error);
      // Set mock data for demo
      setComplaints([
        {
          id: 1,
          subject: 'Delayed Metro Service',
          description: 'The metro was delayed by 15 minutes this morning on the Blue Line.',
          category: 'service_delay',
          priority: 'medium',
          status: 'resolved',
          submittedDate: '2024-01-10',
          resolvedDate: '2024-01-12',
          adminReply: 'We apologize for the inconvenience. The delay was due to technical issues which have been resolved.',
          ticketNumber: 'CMP001'
        },
        {
          id: 2,
          subject: 'Cleanliness Issue',
          description: 'The station platform was not clean and had litter.',
          category: 'cleanliness',
          priority: 'low',
          status: 'in_progress',
          submittedDate: '2024-01-15',
          adminReply: 'Thank you for bringing this to our attention. We have assigned cleaning staff to address this issue.',
          ticketNumber: 'CMP002'
        }
      ]);
    } finally {
      setLoading(false);
    }
  };

  const onSubmit = async (data) => {
    setSubmitting(true);
    try {
      await axios.post(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.COMPLAINT.SUBMIT}`, data);
      toast.success('Complaint submitted successfully!');
      reset();
      fetchComplaints();
    } catch (error) {
      const message = error.response?.data?.message || 'Failed to submit complaint';
      toast.error(message);
    } finally {
      setSubmitting(false);
    }
  };

  const getStatusColor = (status) => {
    switch (status) {
      case 'resolved':
        return 'bg-green-100 text-green-800';
      case 'in_progress':
        return 'bg-blue-100 text-blue-800';
      case 'pending':
        return 'bg-yellow-100 text-yellow-800';
      case 'closed':
        return 'bg-gray-100 text-gray-800';
      default:
        return 'bg-gray-100 text-gray-800';
    }
  };

  const getStatusIcon = (status) => {
    switch (status) {
      case 'resolved':
        return <CheckCircle className="w-4 h-4" />;
      case 'in_progress':
        return <Clock className="w-4 h-4" />;
      case 'pending':
        return <Clock className="w-4 h-4" />;
      case 'closed':
        return <XCircle className="w-4 h-4" />;
      default:
        return <Clock className="w-4 h-4" />;
    }
  };

  const getPriorityColor = (priority) => {
    switch (priority) {
      case 'high':
        return 'bg-red-100 text-red-800';
      case 'medium':
        return 'bg-yellow-100 text-yellow-800';
      case 'low':
        return 'bg-green-100 text-green-800';
      default:
        return 'bg-gray-100 text-gray-800';
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
      <div className="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8">
        {/* Header */}
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">Submit Complaint</h1>
          <p className="text-gray-600">Report issues and get support from our team</p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Submit Complaint Form */}
          <div className="card">
            <h2 className="text-xl font-semibold text-gray-900 mb-4">Submit New Complaint</h2>
            
            <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
              {/* Subject */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Subject
                </label>
                <input
                  {...register('subject')}
                  type="text"
                  className="input-field"
                  placeholder="Brief description of your complaint"
                />
                {errors.subject && (
                  <p className="mt-1 text-sm text-red-600">{errors.subject.message}</p>
                )}
              </div>

              {/* Category */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Category
                </label>
                <select
                  {...register('category')}
                  className="input-field"
                >
                  <option value="">Select category</option>
                  <option value="service_delay">Service Delay</option>
                  <option value="cleanliness">Cleanliness</option>
                  <option value="safety">Safety Issue</option>
                  <option value="payment">Payment Problem</option>
                  <option value="staff_behavior">Staff Behavior</option>
                  <option value="facility">Facility Issue</option>
                  <option value="other">Other</option>
                </select>
                {errors.category && (
                  <p className="mt-1 text-sm text-red-600">{errors.category.message}</p>
                )}
              </div>

              {/* Priority */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Priority Level
                </label>
                <select
                  {...register('priority')}
                  className="input-field"
                >
                  <option value="">Select priority</option>
                  <option value="low">Low</option>
                  <option value="medium">Medium</option>
                  <option value="high">High</option>
                </select>
                {errors.priority && (
                  <p className="mt-1 text-sm text-red-600">{errors.priority.message}</p>
                )}
              </div>

              {/* Description */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Description
                </label>
                <textarea
                  {...register('description')}
                  rows={4}
                  className="input-field"
                  placeholder="Please provide detailed description of your complaint..."
                />
                {errors.description && (
                  <p className="mt-1 text-sm text-red-600">{errors.description.message}</p>
                )}
              </div>

              {/* Submit Button */}
              <button
                type="submit"
                disabled={submitting}
                className="w-full btn-primary py-3 text-base font-medium disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {submitting ? (
                  <div className="flex items-center justify-center">
                    <div className="spinner w-5 h-5 mr-2"></div>
                    Submitting Complaint...
                  </div>
                ) : (
                  <div className="flex items-center justify-center">
                    <Send className="w-5 h-5 mr-2" />
                    Submit Complaint
                  </div>
                )}
              </button>
            </form>
          </div>

          {/* Complaint History */}
          <div className="card">
            <h2 className="text-xl font-semibold text-gray-900 mb-4">Complaint History</h2>
            
            {complaints.length === 0 ? (
              <div className="text-center py-8">
                <MessageSquare className="w-16 h-16 text-gray-300 mx-auto mb-4" />
                <h3 className="text-lg font-medium text-gray-900 mb-2">No Complaints</h3>
                <p className="text-gray-600">You haven't submitted any complaints yet.</p>
              </div>
            ) : (
              <div className="space-y-4">
                {complaints.map((complaint) => (
                  <div key={complaint.id} className="border border-gray-200 rounded-lg p-4">
                    <div className="flex items-start justify-between mb-3">
                      <div>
                        <h3 className="font-medium text-gray-900">{complaint.subject}</h3>
                        <p className="text-sm text-gray-600">Ticket: {complaint.ticketNumber}</p>
                      </div>
                      <div className="flex items-center space-x-2">
                        <span className={`inline-flex items-center px-2 py-1 rounded-full text-xs font-medium ${getStatusColor(complaint.status)}`}>
                          {getStatusIcon(complaint.status)}
                          <span className="ml-1 capitalize">{complaint.status.replace('_', ' ')}</span>
                        </span>
                        <span className={`inline-flex items-center px-2 py-1 rounded-full text-xs font-medium ${getPriorityColor(complaint.priority)}`}>
                          {complaint.priority}
                        </span>
                      </div>
                    </div>

                    <p className="text-sm text-gray-700 mb-3">{complaint.description}</p>

                    <div className="flex items-center justify-between text-xs text-gray-500 mb-3">
                      <span>Submitted: {new Date(complaint.submittedDate).toLocaleDateString()}</span>
                      {complaint.resolvedDate && (
                        <span>Resolved: {new Date(complaint.resolvedDate).toLocaleDateString()}</span>
                      )}
                    </div>

                    {complaint.adminReply && (
                      <div className="bg-blue-50 border border-blue-200 rounded-lg p-3">
                        <div className="flex items-start">
                          <User className="w-4 h-4 text-blue-600 mt-0.5 mr-2" />
                          <div>
                            <p className="text-xs font-medium text-blue-900 mb-1">Admin Response:</p>
                            <p className="text-sm text-blue-800">{complaint.adminReply}</p>
                          </div>
                        </div>
                      </div>
                    )}
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>

        {/* Information Section */}
        <div className="mt-8 card">
          <h2 className="text-xl font-semibold text-gray-900 mb-4">Complaint Guidelines</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div>
              <h3 className="font-medium text-gray-900 mb-2">How to Submit a Complaint</h3>
              <ul className="space-y-2 text-sm text-gray-600">
                <li>• Provide a clear and concise subject</li>
                <li>• Select the appropriate category</li>
                <li>• Choose the correct priority level</li>
                <li>• Include all relevant details in the description</li>
                <li>• Attach any supporting documents if needed</li>
              </ul>
            </div>
            <div>
              <h3 className="font-medium text-gray-900 mb-2">Response Times</h3>
              <ul className="space-y-2 text-sm text-gray-600">
                <li><strong>High Priority:</strong> Within 24 hours</li>
                <li><strong>Medium Priority:</strong> Within 48 hours</li>
                <li><strong>Low Priority:</strong> Within 72 hours</li>
                <li>• You'll receive updates via email</li>
                <li>• Check your complaint history for responses</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default SubmitComplaint; 