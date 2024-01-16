import React from 'react';

const EmployeeList = ({ employees, onSelect }) => {
  return (
    <div>
      <h2>Employee List</h2>
      <ul>
        {employees.map((employee) => (
          <li key={employee.id} onClick={() => onSelect(employee)}>
            {employee.name}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default EmployeeList;
