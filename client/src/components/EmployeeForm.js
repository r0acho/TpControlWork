import React, { useState, useEffect } from 'react';
import axios from 'axios';

const EmployeeForm = ({ selectedEmployee, onEmployeeChange }) => {
  const [formData, setFormData] = useState({
    name: '',
    employeeType: 'FullTime',
    paymentType: { paymentType: 'Hourly' },
    earnings: [],
  });

  useEffect(() => {
    if (selectedEmployee) {
      setFormData({
        name: selectedEmployee.name,
        employeeType: selectedEmployee.employeeType,
        paymentType: selectedEmployee.paymentType,
        earnings: selectedEmployee.earnings,
      });
    }
  }, [selectedEmployee]);

  const handleInputChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();
    try {
      if (selectedEmployee) {
        await axios.put('/api/employee', formData);
      } else {
        await axios.post('/api/employee', formData);
      }
      setFormData({
        name: '',
        employeeType: 'FullTime',
        paymentType: { paymentType: 'Hourly' },
        earnings: [],
      });
      onEmployeeChange();
    } catch (error) {
      console.error('Error submitting employee form:', error);
    }
  };

  return (
    <div>
      <h2>Employee Form</h2>
      <form onSubmit={handleFormSubmit}>
        {/* Form fields go here */}
        <button type="submit">{selectedEmployee ? 'Update' : 'Create'}</button>
      </form>
    </div>
  );
};

export default EmployeeForm;
