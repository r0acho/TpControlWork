import React from 'react';

const employeeTypeToString = {
  1: "Full time",
  2: "Part time",
  3: "Contractor"
}

export const EmployeeInfo = ({ employee }) => {
  if (employee) {
    return (
      <>
      <div>        
          <label style={{marginRight: "16px", fontWeight: "600"}}>id</label>
          <span>{employee.id}</span>
        </div>
        <div>  
          <label style={{marginRight: "16px", fontWeight: "600"}}>name</label>
          <span>{employee.name}</span>
        </div>  
        <div>  
          <label style={{marginRight: "16px", fontWeight: "600"}}>employeeType</label>
          <span>{employeeTypeToString[employee.employeeType]}</span>
        </div>  
        <div>  
          <label style={{marginRight: "16px", fontWeight: "600"}}>salary</label>
          <span>{employee.salary}</span>
        </div>  
      </>
    );
  }

  return null
};
