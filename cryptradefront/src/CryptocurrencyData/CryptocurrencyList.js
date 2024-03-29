import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Loading from '../Loading/Loading';

import axios from 'axios';
import "./CryptocurrencyList.css"

function CryptocurrencyList() {
  const [cryptoData, setCryptoData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    setIsLoading(true);
    handleSortByMarketCap();
  }, []);

  const handleSortByMarketCap = () => {
    setIsLoading(true);
    axios.get('https://localhost:8005/api/Crypto')
    .then(response => {
      setCryptoData(response.data);
      setIsLoading(false);
    })
    .catch(error => {
      console.error('Error fetching data:', error);
      setIsLoading(false);
    });
  };

  const handleSortByPriceChange = () => {
    setIsLoading(true);
    axios.get('https://localhost:8005/api/Crypto/get-sorted-by-price-data')
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
    axios.get('https://localhost:8005/api/Crypto/get-sorted-by-volume-data')
      .then(response => {
        setCryptoData(response.data);
        setIsLoading(false);
      })
      .catch(error => {
        console.error('Error sorting by volume 24h:', error);
        setIsLoading(false);
      });
  };

  const handleRefreshData = () => {
    setIsLoading(true);
    axios.get('https://localhost:8005/api/Crypto/refresh-data')
    .then(response => {
      setCryptoData(response.data);
      setIsLoading(false);
    })
    .catch(error => {
      console.error('Error fetching data:', error);
      setIsLoading(false);
    });
  };

  const handleCryptoClick = (symbol) => {
    navigate(`/cryptocurrency/${symbol}`);
  };

  return (
    <div className='cryptocurrencies-container'>
      <div className='header-container'>
      <h1 className='title'>Today's cryptocurrencies by Market Capitalization</h1>
      <button onClick={handleRefreshData}>Refresh crypto data</button>
      </div>
      <hr />
      {isLoading ? (
        <Loading />
      ) : (
      <ul className="crypto-list">
        <li className="crypto-labels">
            <p className="left">#</p>
            <p className="left">Name</p>
            <p className="right">Price</p>
            <p className="right hoverable" onClick={handleSortByPriceChange}>24h %</p>
            <p className="right hoverable" onClick={handleSortByMarketCap}>Market Cap</p>
            <p className="right hoverable" onClick={handleSortByVolume24h}>Volume 24h</p>
            <p className="right">Circulating Supply</p>
        </li>
        <hr className='thin-line'/>
        {cryptoData.map(item => (
          <li key={item.id} onClick={() => handleCryptoClick(item.symbol)}>
            <div className="crypto-labels-data">
            <p className='left'>{item.id}</p>
            <p className="left">{item.name} <b>{item.symbol}</b></p>
            <p className="right">{item.price} $</p>
            <p className={item.percentChange24h > 0 ? " crypto-volume right" : "right crypto-price-change"}>{item.percentChange24h}%</p>
            <p className="right">{item.marketCap} $</p>
            <p className='right'>{item.volume24h} $</p>
            <p className='right'>{item.circulatingSupply} {item.symbol}</p>
            </div>
            <hr className='thin-line'/>
          </li>
        ))}
      </ul>
      )}
    </div>
  );
}

export default CryptocurrencyList;
