import React, { useState, useEffect } from 'react'
import './Transactions.css'
import axios from 'axios';

const AddTransaction = ({handleAddTransaction}) => {

  const [formType, setFormType] = useState('buy');
  const [cryptoList, setCryptoList] = useState([]);
  const [selectedCrypto, setSelectedCrypto] = useState(null);
  const [quantity, setQuantity] = useState();
  const [pricePerCoin, setPricePerCoin] = useState();
  const [totalSpent, setTotalSpent] = useState();
  const [date, setDate] = useState();

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
            // Reset total spent when selecting a new cryptocurrency
            setQuantity(0);
            setTotalSpent(0);
          })
          .catch(error => {
            console.error('Error fetching cryptocurrency price:', error);
          });
      };
    
    const handleQuantityChange = (e) => {
        setQuantity(e.target.value);
        // Update total spent based on the new quantity and price per coin
        setTotalSpent(e.target.value * pricePerCoin);
      };

    const handlePricePerCoinChange = (e) => {
        setPricePerCoin(e.target.value);
        setTotalSpent(e.target.value * quantity);
    };

      const addTransaction = async (e) => {
        e.preventDefault();
    
        try {
          const response = await axios.post(`https://localhost:8006/api/transaction`, 
          {
            createdAt: date,
            cryptoName: selectedCrypto.name,
            cryptoSymbol: selectedCrypto.symbol,
            type: formType,
            price: pricePerCoin,
            amount: quantity,
            portfolioId: localStorage.getItem('portfolioId'),
          }
        );
          console.log('Transaction added successfully', response.data);
          alert('Transaction was added successfully');
          window.location.reload(false);
        } catch (error) {
          console.error('Transaction creation error', error);
          alert(`Creation unsuccessful ${error}`);
        }
      };
  return (
    <div className='overlay'>
        <div className='add-transaction-container'>
            <div className='text-container'>
                Add Transaction
            </div>
            <div className='type-container'>
            <label className={formType === 'buy' ? 'login-text active' : 'login-text'} onClick={() => setFormType('buy')}>Buy</label>
            <label className={formType === 'sell' ? 'login-text active' : 'login-text'} onClick={() => setFormType('sell')}>Sell</label>
            </div>
            <div className='select-coin-container'>
            <label>Select a cryptocurrency</label>
            <div className='custom-select'>
                <select onChange={(e) => handleCryptoSelect(e.target.value)}>
                <option value="" disabled>Select a cryptocurrency</option>
                {cryptoList.map(crypto => (
                <option key={crypto.id} value={crypto.symbol}>{crypto.name} ({crypto.symbol})</option>
                    ))}
                 </select>
                 </div>
            </div>
            <div className='quantity-price-container'>
                <div className='quantity-container'>
                <label>Quantity</label>
                <input placeholder='0.00' onChange={handleQuantityChange} type='number' />
                </div>
                <div className='price-container'>
                <label>Price per coin</label>
                <input type="number" value={pricePerCoin} onChange={handlePricePerCoinChange} />
                </div>
            </div>
            <div className='transaction-date-container'>
                <label>Date</label>
                <input type="date" onChange={(e) => setDate(e.target.value)}/>
            </div>
            <div className='total-spent-container'>
                <label>Total spent</label>
                <span>{totalSpent}</span>
            </div>
            <div className='add-transaction-button-container'>
                <button className='add-transaction-button' onClick={addTransaction}>Add Transaction</button>
            </div>
        </div>
    </div>
   )
}

export default AddTransaction