import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './CryptocurrencyList.css';

function SingleCrypto() {
  const { symbol } = useParams();
  const [crypto, setCrypto] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`https://localhost:8005/api/Crypto/${symbol}`)
      .then(response => {
        setCrypto(response.data);
      })
      .catch(error => {
        console.error('Error fetching single cryptocurrency:', error);
      });
  }, [symbol]);

  const handleBackClick = () => {
    navigate(`/cryptocurrency`);
  };

  return (
    <div className="single-crypto-container">
      {crypto ? (
        <div>
          <h2>Name: {crypto.name} {crypto.symbol}</h2>
          <p>Current Price: {crypto.price}</p>
          <p>Volume last 24h: {crypto.volume24h}</p>
          <p>Price change last 24h: {crypto.percentChange24h}</p>
          <p>Market capitalization: {crypto.marketCap}</p>
          <p>Current circulating supply: {crypto.circulatingSupply}</p>
          <p>Total supply: </p>
          <p>Max supply or infinite supply: </p>
        </div>
      ) : (
        <p>Loading...</p>
      )}
      <button className='back-button' onClick={handleBackClick}>Back to the list</button>
    </div>
  );
}

export default SingleCrypto;
