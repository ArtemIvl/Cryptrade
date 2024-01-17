import React, { useState, useEffect} from 'react'
import axios from 'axios';
import './TradingPage.css'

function TradingPage({isLoggedIn}) {

    const [selectedCrypto, setSelectedCrypto] = useState(null);
    const [cryptoList, setCryptoList] = useState([]);
    const [pricePerCoin, setPricePerCoin] = useState();
    const [useMarketPrice, setUseMarketPrice] = useState(false);
    const [openPrice, setOpenPrice] = useState('');
    const [tradeAmount, setTradeAmount] = useState();

    useEffect(() => {
        axios.get('https://localhost:8005/api/Crypto')
          .then(response => {
            setCryptoList(response.data);
          })
          .catch(error => {
            console.error('Error fetching data:', error);
          });
    }, []);

    const handleCryptoSelect = (crypto) => {
        // Fetch the price of the selected cryptocurrency
        axios.get(`https://localhost:8005/api/Crypto/${crypto}`)
          .then(response => {
            setSelectedCrypto(response.data);
            setPricePerCoin(response.data.price); // Assuming the API response has a 'price' field
          })
          .catch(error => {
            console.error('Error fetching cryptocurrency price:', error);
          });
      };

    const handleToggleChange = () => {
        setUseMarketPrice((prevValue) => !prevValue);
        if (useMarketPrice) {
          setOpenPrice(pricePerCoin);
        }
      };

    const handleLongTrade = () => {
        // Add your logic for handling long trade
        console.log('Long Trade');
      };
    
    const handleShortTrade = () => {
        // Add your logic for handling short trade
        console.log('Short Trade');
      };
    
  return (
    <div className='trading-container'>
      <h1>Mock Trading</h1>
      <hr />
      <div className='new-order-container'>
      <div className='select-crypto'>
      <label for='select-crypto'>Select cryptocurrency you want to trade:</label>
      <div className='list-of-cryptos'>
            <select onChange={(e) => handleCryptoSelect(e.target.value)}>
            <option value="" disabled>Select a cryptocurrency</option>
            {cryptoList.map(crypto => (
            <option key={crypto.id} value={crypto.symbol}>{crypto.name} ({crypto.symbol})</option>
                ))}
            </select>
        </div>
        </div>
        <div className='toggle-container'>
        <label for='use-market-price'>Use market price:</label>
        <input
          type='checkbox'
          checked={useMarketPrice}
          onChange={handleToggleChange}
        />
      </div>
      {!useMarketPrice && (
        <div className='price-input-container'>
          <label>Enter price:</label>
          <input
            type='number'
            value={openPrice}
            onChange={(e) => setOpenPrice(e.target.value)}
          />$USD
        </div>
      )}
      <div className='amount-input-container'>
        <label>Trading amount:</label>
        <input 
          type='number' 
          value={tradeAmount}
          onChange={(e) => setTradeAmount(e.target.value)}
          />{selectedCrypto ? selectedCrypto.symbol : ''}
      </div>
      <div className='trade-buttons'>
        <button className='long-button' onClick={handleLongTrade}>Long</button>
        <button className='short-button' onClick={handleShortTrade}>Short</button>
      </div>
    </div>
    <hr />
    <div className='open-orders-container'>
        <div className='orders-label'>
        <h2>Your Orders</h2>
        <button>View History</button>
        </div>
        <hr />
        <li className="labels-open-orders">
            <p>#</p>
            <p>Name</p>
            <p>Entry price</p>
            <p>Amount</p>
            <p>Current price</p>
            <p>Profit/Loss</p>
            <p>Actions</p>
          </li>
        </div>
    </div>
  )
}

export default TradingPage