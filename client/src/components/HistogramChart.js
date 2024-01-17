import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { httpClient } from '../services/httpClient';
import { BarChart } from '@mantine/charts';
import { Card } from '@mantine/core';

const HistogramChart = () => {
  const [chartData, setChartData] = useState({});

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await httpClient.get('/statisticsCalculator/sum');
      const employees = await httpClient.get('/employee');

      setChartData(employees.data)
      const sumData = response.data;

      console.log(employees.data)

      // // Assuming your sumData structure is an array of objects with 'employeeType' and 'sum' properties
      // const labels = sumData.map((item) => item.employeeType);
      // const data = sumData.map((item) => item.sum);

      // setChartData({
      //   labels: labels,
      //   datasets: [
      //     {
      //       label: 'Sum of Salaries',
      //       data: data,
      //       backgroundColor: 'rgba(75,192,192,0.6)',
      //     },
      //   ],
      // });
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  return (
    <div>
      <h2>Histogram of Sums Grouped by Employee Type</h2>
      {chartData.length > 0 &&
        <Card>
          <BarChart
            h={300}
            data={chartData}
            dataKey="employeeType"
            series={[{ name: 'salary', color: 'blue' }]}
          />
        </Card>}
    </div>
  );
};

export default HistogramChart;
