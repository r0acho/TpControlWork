import React, { useState, useEffect } from 'react';
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
      const employees = await httpClient.get('/employee');

      const enumTypeToString = {
        1: "Full Time",
        2: "Part Time",
        3: "Contractor"
      }

      // Группируем данные по employeeType
      const groupedData = employees.data.reduce((acc, item) => {
        const key = item.employeeType;
        if (!acc[key]) {
          acc[key] = [];
        }
        acc[key].push(item.salary);
        return acc;
      }, {});

      // Создаем массив для labels и datasets
      const labels = Object.keys(groupedData);
      const datasets = Object.values(groupedData).map((salaries, index) => ({
        label: enumTypeToString[labels[index]],
        data: salaries.reduce((sum, salary) => sum + salary, 0),
      }));

      setChartData(datasets)
      console.log(datasets)

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
            dataKey="label"
            series={[{ name: "data", color: 'blue' }]}
          />
        </Card>}
    </div>
  );
};

export default HistogramChart;
