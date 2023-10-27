import React, { useEffect, useState } from 'react';
import axios from 'axios';
import "./CryptocurrencyList.css"

function CryptocurrencyList() {
  const [cryptoData, setCryptoData] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7145/api/Crypto/get-cached-crypto-data')
      .then(response => {
        setCryptoData(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);

  return (
    <div>
      <h1>Cryptocurrency Data</h1>
      <ul className="crypto-list">
        {cryptoData.map(item => (
          <li key={item.id} className="crypto-item">
            <p className="crypto-property crypto-name">Name: {item.name}</p>
            <p className="crypto-property crypto-symbol">Symbol: {item.symbol}</p>
            <p className="crypto-property crypto-price">Price: ${item.price}</p>
            <p className="crypto-property crypto-volume">24h Volume: ${item.volume24h}</p>
            <p className="crypto-property crypto-market-cap">Market Cap: ${item.marketCap}</p>
            <p className={item.percentChange24h > 0 ? " crypto-volume crypto-property" : "crypto-property crypto-price-change"}>24h Price Change: {item.percentChange24h}%</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default CryptocurrencyList;
