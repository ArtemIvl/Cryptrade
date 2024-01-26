import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './TradingPage.css';

function TradingPage({ isLoggedIn }) {
  const [selectedCrypto, setSelectedCrypto] = useState('');
  const [ordersList, setOrdersList] = useState([]);
  const [cryptoList, setCryptoList] = useState([]);
  const [pricePerCoin, setPricePerCoin] = useState();
  const [useMarketPrice, setUseMarketPrice] = useState(false);
  const [openPrice, setOpenPrice] = useState('');
  const [closePrice, setClosePrice] = useState('');
  const [tradeAmount, setTradeAmount] = useState();

  useEffect(() => {
    axios.get('http://localhost:5111/api/trading')
      .then(response => {
        setOrdersList(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);

  useEffect(() => {
    axios.get('http://localhost:5024/api/Crypto')
      .then(response => {
        setCryptoList(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);

  useEffect(() => {
    if (selectedCrypto) {
      axios.get(`http://localhost:5024/api/Crypto/${selectedCrypto.symbol}`)
        .then(response => {
          setPricePerCoin(response.data.price);
          console.log(response.data.price);
        })
        .catch(error => {
          console.error('Error fetching cryptocurrency price:', error);
        });
    }
  }, [selectedCrypto]);

  // useEffect(() => {
  //   axios.get('http://localhost:5024/api/Trading/history')
  //     .then(response => {
  //       setHistoryList(response.data);
  //     })
  //     .catch(error => {
  //       console.error('Error fetching data:', error);
  //     });
  // }, []);

  const handleCryptoSelect = (cryptoSymbol) => {
    const selectedCrypto = cryptoList.find(crypto => crypto.symbol === cryptoSymbol);
    setSelectedCrypto(selectedCrypto);
    console.log(selectedCrypto);
    setOpenPrice(selectedCrypto.price);
  };

  const handleToggleChange = () => {
    setUseMarketPrice((prevValue) => !prevValue);
    if (useMarketPrice) {
      setOpenPrice(pricePerCoin);
    }
  };

  const handleLongTrade = async (e) => {
    e.preventDefault();

    if (!validateLongTrade()) {
      return;
    }

    try {
      const response = await axios.post('http://localhost:5111/api/trading', {
        cryptoName: selectedCrypto.name,
        cryptoSymbol: selectedCrypto.symbol,
        openPrice: openPrice,
        closePrice: closePrice,
        amount: tradeAmount,
        type: 'Long',
        isOpen: useMarketPrice,
        finished: false,
      });

      console.log('Trade was added successfully', response.data);
      alert('Trade was added successfully');
      window.location.reload(false);
    } catch (error) {
      console.error('Trade creation error', error);
      alert(`Creation unsuccessful ${error}`);
    }
  };

  const handleShortTrade = async (e) => {
    e.preventDefault();

    if (!validateShortTrade()) {
      return;
    }

    try {
      const response = await axios.post('http://localhost:5111/api/trading', {
        cryptoName: selectedCrypto.name,
        cryptoSymbol: selectedCrypto.symbol,
        openPrice: openPrice,
        closePrice: closePrice,
        amount: tradeAmount,
        type: 'Short',
        isOpen: useMarketPrice,
        finished: false,
      });

      console.log('Trade was added successfully', response.data);
      alert('Trade was added successfully');
      window.location.reload(false);
    } catch (error) {
      console.error('Trade creation error', error);
      alert(`Creation unsuccessful ${error}`);
    }
  };

  // const closeOrder = async (order) => {

  //   try {
  //     await axios.put('http://localhost:5111/api/trading/close', {
  //       cryptoName: order.name,
  //       cryptoSymbol: order.symbol,
  //       openPrice: order.openPrice,
  //       closePrice: order,
  //       amount: order.amount,
  //       type: order.type,
  //       isOpen: order.isOpen,
  //       finished: true,
  //     });
  //   } catch (error) {
  //     console.error('Trade closing error', error);
  //     alert(`Closing unsuccessful ${error}`);
  //   }
  // }

  const validateLongTrade = () => {
    if (closePrice <= openPrice) {
      alert('Close price must be higher than open price for a Long position.');
      return false;
    }

    if (!useMarketPrice && openPrice > pricePerCoin) {
      alert('Custom open price cannot be higher than the market price for Long positions.');
      return false;
    }

    return true;
  };

  const validateShortTrade = () => {
    if (closePrice >= openPrice) {
      alert('Close price must be lower than open price for a Short position.');
      return false;
    }

    if (!useMarketPrice && openPrice < pricePerCoin) {
      alert('Custom open price cannot be lower than the market price for Short positions.');
      return false;
    }

    return true;
  };

  return (
    <div className='trading-container'>
      <h1>Mock Trading</h1>
      <hr />
      <div className='new-order-container'>
        <div className='select-crypto'>
          <label htmlFor='select-crypto'>Select cryptocurrency you want to trade:</label>
          <div className='list-of-cryptos'>
            <select onChange={(e) => handleCryptoSelect(e.target.value)}>
              <option value="" disabled>Select a cryptocurrency</option>
              {cryptoList.map(crypto => (
                <option key={crypto.id} value={crypto.symbol}>
                  {crypto.name} ({crypto.symbol})
                </option>
              ))}
            </select>
          </div>
        </div>
        <div className='toggle-container'>
          <label htmlFor='use-market-price'>Use market price:</label>
          <input
            type='checkbox'
            checked={useMarketPrice}
            onChange={handleToggleChange}
          />
        </div>
        {!useMarketPrice && (
          <div className='price-input-container'>
            <label htmlFor='open-price'>Enter open price:</label>
            <input
              type='number'
              value={openPrice}
              onChange={(e) => setOpenPrice(e.target.value)}
            />$USD
          </div>
        )}
        <div className='price-input-container'>
          <label htmlFor='close-price'>Enter close price:</label>
          <input
            type='number'
            value={closePrice}
            onChange={(e) => setClosePrice(e.target.value)}
          />$USD
        </div>
        <div className='amount-input-container'>
          <label htmlFor='trade-amount'>Trading amount:</label>
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
        <ul>
          <li className="labels-open-orders">
            <p>#</p>
            <p>Type</p>
            <p>Name</p>
            <p>Entry price</p>
            <p>Amount</p>
            <p>Current price</p>
            <p>Profit/Loss</p>
            <p>Status</p>
            <p>Actions</p>
          </li>
        </ul>
        <hr />
        {ordersList.map((order) => (
          <ul key={order.id}>
            <li className="values-orders">
              <p>{order.id}</p>
              <p>{order.type}</p>
              <p>
                {order.cryptoName} <b>{order.cryptoSymbol}</b>
              </p>
              <p>{order.openPrice} $</p>
              <p>{order.amount} {order.cryptoSymbol}</p>
              <p>curr price</p>
              <p>profit loss</p>
              <p>{order.isOpen ? "Open" : "Waiting"}</p>
              <button>close order</button>
            </li>
            <hr className="thin-line" />
          </ul>
        ))}
      </div>
    </div>
  );
}

export default TradingPage;
