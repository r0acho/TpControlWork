import React, { useState, useEffect } from 'react';
import axios from 'axios';

const StatisticsCalculator = () => {
  const [statistics, setStatistics] = useState({});

  useEffect(() => {
    fetchStatistics();
  }, []);

  const fetchStatistics = async () => {
    try {
      const response = await axios.get('/api/statisticsCalculator');
      setStatistics(response.data);
    } catch (error) {
      console.error('Error fetching statistics:', error);
    }
  };

  return (
    <div>
      <h2>Statistics Calculator</h2>
      {/* Display statistics using the data in the 'statistics' state */}
    </div>
  );
};

export default StatisticsCalculator;
