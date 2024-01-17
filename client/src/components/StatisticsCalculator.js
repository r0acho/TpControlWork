import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { httpClient } from '../services/httpClient';

const StatisticsCalculator = () => {
  const [statistics, setStatistics] = useState({});

  useEffect(() => {
    fetchStatistics();
  }, []);

  const fetchStatistics = async () => {
    try {
      const sumResponse = await httpClient.get('/statisticsCalculator/sum');
      const avgResponse = await httpClient.get('/statisticsCalculator/avg');
      const medianResponse = await httpClient.get('/statisticsCalculator/median');
      const minResponse = await httpClient.get('/statisticsCalculator/min');
      const maxResponse = await httpClient.get('/statisticsCalculator/max');

      setStatistics({
        sum: sumResponse.data,
        avg: avgResponse.data,
        median: medianResponse.data,
        min: minResponse.data,
        max: maxResponse.data,
      });
    } catch (error) {
      console.error('Error fetching statistics:', error);
    }
  };

  return (
    <div>
      <h2>Statistics Calculator</h2>
      {/* Display statistics using the data in the 'statistics' state */}
      <p>Sum: {statistics.sum}</p>
      <p>Average: {statistics.avg}</p>
      <p>Median: {statistics.median}</p>
      <p>Min: {statistics.min}</p>
      <p>Max: {statistics.max}</p>
    </div>
  );
};

export default StatisticsCalculator;
