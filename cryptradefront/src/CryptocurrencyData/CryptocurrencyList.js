import React, { useEffect, useState } from 'react';
import axios from 'axios';
import "./CryptocurrencyList.css"

function CryptocurrencyList() {
  const [cryptoData, setCryptoData] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7145/api/Crypto/get-crypto-data')
      .then(response => {
        setCryptoData(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);

  return (
    <div className='cryptocurrencies-container'>
      <h1>Today's cryptocurrencies by Market Capitalizaiton</h1>
      <hr />
      <ul className="crypto-list">
        <l1 className="crypto-labels">
        <p className="crypto-property">#</p>
            <p className="crypto-property">Name</p>
            <p className="crypto-property">Price</p>
            <p className="crypto-property">24h %</p>
            <p className="crypto-property">Market Cap</p>
            <p className="crypto-property">Volume 24h</p>
            <p className="crypto-property">Circulating Supply</p>
        </l1>
        <hr />
        {cryptoData.map(item => (
          <li key={item.id} className="crypto-item">
            <p className='crypto-property crypto-rank'>{item.id}</p>
            <p className="crypto-property crypto-symbol">{item.name} {item.symbol}</p>
            <p className="crypto-property crypto-price">{item.price} $</p>
            <p className={item.percentChange24h > 0 ? " crypto-volume crypto-property" : "crypto-property crypto-price-change"}>{item.percentChange24h}%</p>
            <p className="crypto-property crypto-market-cap">{item.marketCap} $</p>
            <p className='crypto-property crypto-volume'>{item.volume24h} $</p>
            <p className='crypto-property'>{item.circulatingSupply}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default CryptocurrencyList;
