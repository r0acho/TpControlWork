import React, { useState, useEffect } from 'react';
import EmployeeForm from './components/EmployeeForm';
import EmployeeList from './components/EmployeeList';
import StatisticsCalculator from './components/StatisticsCalculator';
import { httpClient } from './services/httpClient';

const App = () => {
  const [employees, setEmployees] = useState([]);
  const [selectedEmployee, setSelectedEmployee] = useState(null);

  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    try {
      const response = await httpClient.get('/employee');
      setEmployees(response.data);
    } catch (error) {
      console.error('Error fetching employees:', error);
    }
  };

  const handleEmployeeSelect = (employee) => {
    setSelectedEmployee(employee);
  };

  return (
    <div>
      <h1>Employee Management System</h1>
      <EmployeeList employees={employees} onSelect={handleEmployeeSelect} />
      <EmployeeForm
        selectedEmployee={selectedEmployee}
        onEmployeeChange={fetchEmployees}
      />
      <StatisticsCalculator />
    </div>
  );
};

export default App;
