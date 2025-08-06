import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { toast } from 'react-toastify';
import { 
  CreditCard, 
  User, 
  Calendar, 
  MapPin, 
  Phone, 
  Mail,
  CheckCircle,
  Clock,
  XCircle,
  AlertCircle,
  FileText,
  Upload
} from 'lucide-react';
import axios from 'axios';
import API_CONFIG from '../../config/api';

const validationSchema = Yup.object({
  cardType: Yup.string().required('Please select a card type'),
  purpose: Yup.string().required('Please describe the purpose'),
  documents: Yup.string().required('Please upload required documents'),
});

const MetroCardApplication = () => {
  const [application, setApplication] = useState(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(validationSchema),
  });

  useEffect(() => {
    fetchApplicationStatus();
  }, []);

  const fetchApplicationStatus = async () => {
    try {
      const response = await axios.get(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.METRO_CARD.STATUS}`);
      setApplication(response.data);
    } catch (error) {
      console.error('Error fetching application status:', error);
      // Set mock data for demo
      setApplication({
        id: 1,
        cardType: 'student',
        purpose: 'Daily commute to university',
        status: 'pending',
        appliedDate: '2024-01-10',
        documents: 'student_id.pdf, address_proof.pdf',
        remarks: 'Application under review',
        estimatedProcessingTime: '5-7 business days'
      });
    } finally {
      setLoading(false);
    }
  };

  const onSubmit = async (data) => {
    setSubmitting(true);
    try {
      const response = await axios.post(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.METRO_CARD.APPLY}`, data);
      toast.success('Metro card application submitted successfully!');
      fetchApplicationStatus();
    } catch (error) {
      const message = error.response?.data?.message || 'Failed to submit application';
      toast.error(message);
    } finally {
      setSubmitting(false);
    }
  };

  const getStatusColor = (status) => {
    switch (status) {
      case 'approved':
        return 'bg-green-100 text-green-800';
      case 'pending':
        return 'bg-yellow-100 text-yellow-800';
      case 'rejected':
        return 'bg-red-100 text-red-800';
      case 'under_review':
        return 'bg-blue-100 text-blue-800';
      default:
        return 'bg-gray-100 text-gray-800';
    }
  };

  const getStatusIcon = (status) => {
    switch (status) {
      case 'approved':
        return <CheckCircle className="w-5 h-5" />;
      case 'pending':
        return <Clock className="w-5 h-5" />;
      case 'rejected':
        return <XCircle className="w-5 h-5" />;
      case 'under_review':
        return <AlertCircle className="w-5 h-5" />;
      default:
        return <Clock className="w-5 h-5" />;
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
          <h1 className="text-3xl font-bold text-gray-900 mb-2">Metro Card Application</h1>
          <p className="text-gray-600">Apply for a metro card or check your application status</p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Application Status */}
          <div className="card">
            <h2 className="text-xl font-semibold text-gray-900 mb-4">Application Status</h2>
            
            {application ? (
              <div className="space-y-4">
                <div className="flex items-center justify-between">
                  <span className="text-sm font-medium text-gray-700">Status</span>
                  <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${getStatusColor(application.status)}`}>
                    {getStatusIcon(application.status)}
                    <span className="ml-1 capitalize">{application.status.replace('_', ' ')}</span>
                  </span>
                </div>

                <div className="space-y-3">
                  <div>
                    <span className="text-sm font-medium text-gray-700">Card Type:</span>
                    <p className="text-sm text-gray-900 capitalize">{application.cardType}</p>
                  </div>
                  
                  <div>
                    <span className="text-sm font-medium text-gray-700">Purpose:</span>
                    <p className="text-sm text-gray-900">{application.purpose}</p>
                  </div>
                  
                  <div>
                    <span className="text-sm font-medium text-gray-700">Applied Date:</span>
                    <p className="text-sm text-gray-900">
                      {new Date(application.appliedDate).toLocaleDateString()}
                    </p>
                  </div>
                  
                  {application.estimatedProcessingTime && (
                    <div>
                      <span className="text-sm font-medium text-gray-700">Processing Time:</span>
                      <p className="text-sm text-gray-900">{application.estimatedProcessingTime}</p>
                    </div>
                  )}
                  
                  {application.remarks && (
                    <div>
                      <span className="text-sm font-medium text-gray-700">Remarks:</span>
                      <p className="text-sm text-gray-900">{application.remarks}</p>
                    </div>
                  )}
                </div>

                {application.status === 'approved' && (
                  <div className="bg-green-50 border border-green-200 rounded-lg p-4">
                    <div className="flex items-center">
                      <CheckCircle className="w-5 h-5 text-green-600 mr-2" />
                      <div>
                        <h3 className="text-sm font-medium text-green-800">Application Approved!</h3>
                        <p className="text-sm text-green-700">Your metro card will be delivered within 3-5 business days.</p>
                      </div>
                    </div>
                  </div>
                )}

                {application.status === 'rejected' && (
                  <div className="bg-red-50 border border-red-200 rounded-lg p-4">
                    <div className="flex items-center">
                      <XCircle className="w-5 h-5 text-red-600 mr-2" />
                      <div>
                        <h3 className="text-sm font-medium text-red-800">Application Rejected</h3>
                        <p className="text-sm text-red-700">Please review the remarks and submit a new application if needed.</p>
                      </div>
                    </div>
                  </div>
                )}
              </div>
            ) : (
              <div className="text-center py-8">
                <CreditCard className="w-16 h-16 text-gray-300 mx-auto mb-4" />
                <h3 className="text-lg font-medium text-gray-900 mb-2">No Application Found</h3>
                <p className="text-gray-600">You haven't applied for a metro card yet.</p>
              </div>
            )}
          </div>

          {/* New Application Form */}
          <div className="card">
            <h2 className="text-xl font-semibold text-gray-900 mb-4">Apply for Metro Card</h2>
            
            {application && application.status === 'pending' ? (
              <div className="text-center py-8">
                <Clock className="w-16 h-16 text-yellow-300 mx-auto mb-4" />
                <h3 className="text-lg font-medium text-gray-900 mb-2">Application in Progress</h3>
                <p className="text-gray-600">You already have a pending application. Please wait for the review process.</p>
              </div>
            ) : (
              <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
                {/* Card Type */}
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Card Type
                  </label>
                  <select
                    {...register('cardType')}
                    className="input-field"
                  >
                    <option value="">Select card type</option>
                    <option value="student">Student Card</option>
                    <option value="senior">Senior Citizen Card</option>
                    <option value="regular">Regular Card</option>
                    <option value="premium">Premium Card</option>
                  </select>
                  {errors.cardType && (
                    <p className="mt-1 text-sm text-red-600">{errors.cardType.message}</p>
                  )}
                </div>

                {/* Purpose */}
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Purpose of Application
                  </label>
                  <textarea
                    {...register('purpose')}
                    rows={3}
                    className="input-field"
                    placeholder="Describe why you need a metro card..."
                  />
                  {errors.purpose && (
                    <p className="mt-1 text-sm text-red-600">{errors.purpose.message}</p>
                  )}
                </div>

                {/* Documents Upload */}
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Required Documents
                  </label>
                  <div className="border-2 border-dashed border-gray-300 rounded-lg p-6 text-center">
                    <Upload className="w-8 h-8 text-gray-400 mx-auto mb-2" />
                    <p className="text-sm text-gray-600 mb-2">
                      Upload your ID proof, address proof, and other required documents
                    </p>
                    <input
                      {...register('documents')}
                      type="file"
                      multiple
                      className="hidden"
                      id="documents"
                    />
                    <label
                      htmlFor="documents"
                      className="btn-secondary cursor-pointer"
                    >
                      Choose Files
                    </label>
                  </div>
                  {errors.documents && (
                    <p className="mt-1 text-sm text-red-600">{errors.documents.message}</p>
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
                      Submitting Application...
                    </div>
                  ) : (
                    <div className="flex items-center justify-center">
                      <FileText className="w-5 h-5 mr-2" />
                      Submit Application
                    </div>
                  )}
                </button>
              </form>
            )}
          </div>
        </div>

        {/* Information Section */}
        <div className="mt-8 card">
          <h2 className="text-xl font-semibold text-gray-900 mb-4">Metro Card Information</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div>
              <h3 className="font-medium text-gray-900 mb-2">Card Types</h3>
              <ul className="space-y-2 text-sm text-gray-600">
                <li><strong>Student Card:</strong> 50% discount on all rides</li>
                <li><strong>Senior Citizen Card:</strong> 75% discount on all rides</li>
                <li><strong>Regular Card:</strong> Standard pricing with convenience</li>
                <li><strong>Premium Card:</strong> Priority boarding and premium services</li>
              </ul>
            </div>
            <div>
              <h3 className="font-medium text-gray-900 mb-2">Required Documents</h3>
              <ul className="space-y-2 text-sm text-gray-600">
                <li>• Government-issued ID proof</li>
                <li>• Address proof (utility bill, rental agreement)</li>
                <li>• Passport-size photograph</li>
                <li>• Student ID (for student cards)</li>
                <li>• Age proof (for senior citizen cards)</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MetroCardApplication; 