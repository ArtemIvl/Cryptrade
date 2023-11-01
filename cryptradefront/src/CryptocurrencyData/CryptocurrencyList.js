import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import axios from 'axios';
import "./CryptocurrencyList.css"

function CryptocurrencyList() {
  const [cryptoData, setCryptoData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    setIsLoading(true);
    axios.get('https://localhost:7145/api/Crypto/get-crypto-data')
      .then(response => {
        setCryptoData(response.data);
        setIsLoading(false);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
        setIsLoading(false);
      });
  }, []);

  const handleSortByPriceChange = () => {
    setIsLoading(true);
    axios.get('https://localhost:7145/api/Crypto/get-sorted-by-price-data')
      .then(response => {
        setCryptoData(response.data);
        setIsLoading(false);
      })
      .catch(error => {
        console.error('Error sorting by price change:', error);
        setIsLoading(false);
      });
  };

  const handleSortByVolume24h = () => {
    setIsLoading(true);
    axios.get('https://localhost:7145/api/Crypto/get-sorted-by-volume-data')
      .then(response => {
        setCryptoData(response.data);
        setIsLoading(false);
      })
      .catch(error => {
        console.error('Error sorting by volume 24h:', error);
        setIsLoading(false);
      });
  };

  const handleSearch = async () => {
    try {
      const response = await axios.get(`https://localhost:7145/api/Crypto/search-crypto?searchTerm=${searchTerm}`);
      setCryptoData(response.data);
    } catch (error) {
      console.error('Error searching data:', error);
    }
  };

  const handleCryptoClick = (symbol) => {
    navigate(`/cryptocurrency/${symbol}`);
  };

  return (
    <div className='cryptocurrencies-container'>
      <h1>Today's cryptocurrencies by Market Capitalizaiton</h1>
      <hr />
      <div className="filters">
        <button onClick={handleSortByPriceChange}>Sort by Price Change</button>
        <button onClick={handleSortByVolume24h}>Sort by Volume 24h</button>
        <input
        type="text"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        placeholder="Search by name or symbol"
      />
      <button onClick={handleSearch}>Search</button>
      </div>
      {isLoading ? (
        <p>Loading...</p>
      ) : (
      <ul className="crypto-list">
        <li className="crypto-labels">
        <p className="crypto-property">#</p>
            <p className="crypto-property">Name</p>
            <p className="crypto-property">Price</p>
            <p className="crypto-property">24h %</p>
            <p className="crypto-property">Market Cap</p>
            <p className="crypto-property">Volume 24h</p>
            <p className="crypto-property">Circulating Supply</p>
        </li>
        <hr />
        {cryptoData.map(item => (
          <li key={item.id} className="crypto-item" onClick={() => handleCryptoClick(item.symbol)}>
            <p className='crypto-property'>{item.id}</p>
            <p className="crypto-property">{item.name} {item.symbol}</p>
            <p className="crypto-property">{item.price} $</p>
            <p className={item.percentChange24h > 0 ? " crypto-volume crypto-property" : "crypto-property crypto-price-change"}>{item.percentChange24h}%</p>
            <p className="crypto-property">{item.marketCap} $</p>
            <p className='crypto-property'>{item.volume24h} $</p>
            <p className='crypto-property'>{item.circulatingSupply} {item.symbol}</p>
          </li>
        ))}
      </ul>
      )}
    </div>
  );
}

export default CryptocurrencyList;
