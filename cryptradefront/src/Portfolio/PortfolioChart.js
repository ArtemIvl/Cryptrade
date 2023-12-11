import React from 'react';
import { Line } from 'react-chartjs-2';

function PortfolioChart({ chartData }) {
  return <Line data={chartData} />;
}

export default PortfolioChart;