import React, { useState, useEffect } from 'react';
import EmployeeList from './components/EmployeeList';
import { EmployeeInfo } from './components/EmployeeInfo';
import StatisticsCalculator from './components/StatisticsCalculator';
import HistogramChart from './components/HistogramChart';
import { httpClient } from './services/httpClient';

const App = () => {
  const [employees, setEmployees] = useState([]);
  const [employeeSelected, setEmployeeSelected] = useState(null);

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

  const onClick = (employee) => {
    setEmployeeSelected(employee);
  };

  return (
    <div>
      <h1>Employee Management System</h1>
      <EmployeeList employees={employees} onSelect={handleEmployeeSelect} onClick={onClick}/>
      <EmployeeInfo employee={employeeSelected}/>
      <StatisticsCalculator />
      <HistogramChart />
    </div>
  );
};

export default App;
