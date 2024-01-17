import React from 'react';

const EmployeeList = ({ employees, onClick }) => {
  return (
    <div id='employee-list'>
      <h2>Employee List</h2>
      <ul>
        {employees.map((employee) => (
          <li key={employee.id} onClick={() => onClick(employee)}>
            {employee.name}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default EmployeeList;
