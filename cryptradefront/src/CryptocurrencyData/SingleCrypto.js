import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';

function SingleCrypto() {
  const { symbol } = useParams();
  const [crypto, setCrypto] = useState(null);

  useEffect(() => {
    axios.get(`https://localhost:7145/api/Crypto/${symbol}`)
      .then(response => {
        setCrypto(response.data);
      })
      .catch(error => {
        console.error('Error fetching single cryptocurrency:', error);
      });
  }, [symbol]);

  return (
    <div className="single-crypto-container">
      {crypto ? (
        <div>
          <h2>{crypto.name} {crypto.symbol}</h2>
          <p>{crypto.price}</p>
          <p>{crypto.volume24h}</p>
          <p>{crypto.percentChange24h}</p>
          <p>{crypto.marketCap}</p>
          <p>{crypto.circulatingSupply}</p>
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}

export default SingleCrypto;
