import React, { useState } from 'react'
import axios from 'axios'
import './Transactions.css'

function EditTransaction({transaction}) {

    const [amount, setAmount] = useState(transaction.amount);
    const [price, setPrice] = useState(transaction.price);
    const [createdAt, setCreatedAt] = useState(transaction.createdAt.date);
    const totalSpent = (amount * price);

    console.log(transaction);

    const handleEditTransaction = async () => {
        try {
          await axios.put(`https://localhost:8006/api/Transaction?transactionid=${transaction.id}`, { amount: amount, price: price, createdAt: createdAt, type: "buy", cryptoName: transaction.cryptoName, cryptoSymbol: transaction.cryptoSymbol});
          alert('Transaction edited successfully!');
          window.location.reload(false);
        } catch (error) {
          console.error('Error editing transaction:', error);
        }
      };

    return (
        <div className='overlay'>
            <div className='add-transaction-container'>
                <div className='text-container'>
                    Edit Transaction
                </div>
                <div className='quantity-price-container'>
                    <div className='quantity-container'>
                    <label>Quantity</label>
                    <input value={amount} onChange={(e) => setAmount(e.target.value)} type='number' />
                    </div>
                    <div className='price-container'>
                    <label>Price per coin</label>
                    <input type="number" value={price} onChange={(e) => setPrice(e.target.value)} />
                    </div>
                </div>
                <div className='transaction-date-container'>
                    <label>Date</label>
                    <input type="date" value={transaction.createdAt} onChange={(e) => setCreatedAt(e.target.value)}/>
                </div>
                <div className='total-spent-container'>
                    <label>Total spent</label>
                    <span>{totalSpent}</span>
                </div>
                <div className='add-transaction-button-container'>
                    <button className='add-transaction-button' onClick={handleEditTransaction}>Edit Transaction</button>
                </div>
            </div>
        </div>
       )
}

export default EditTransaction