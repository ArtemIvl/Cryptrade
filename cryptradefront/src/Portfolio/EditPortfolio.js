import React, { useState } from 'react'
import axios from 'axios'
import './PortfolioPage.css'

function EditPortfolio({portfolio}) {

  const [name, setName] = useState(portfolio.name);
  const [description, setDescription] = useState(portfolio.description);
  const token = localStorage.getItem('token');

  const handleEditPortfolio = async () => {
      try {
        await axios.put(`http://localhost:5174/api/portfolio?portfolioId=${portfolio.id}`, { name: name, description: description}, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        alert('Portfolio edited successfully!');
        window.location.reload(false);
      } catch (error) {
        console.error('Error editing transaction:', error);
      }
  };

  return (
      <div className='overlay'>
          <div className='add-transaction-container'>
              <div className='text-container'>
                  Edit Portfolio
              </div>
              <div className='quantity-price-container'>
                  <div className='quantity-container'>
                  <label>Name</label>
                  <input value={name} onChange={(e) => setName(e.target.value)} />
                  </div>
                  <div className='price-container'>
                  <label>description</label>
                    <input value={description} onChange={(e) => setDescription(e.target.value)} />
                  </div>
              </div>
              <div className='add-transaction-button-container'>
                  <button className='add-transaction-button' onClick={handleEditPortfolio}>Edit portfolio data</button>
              </div>
          </div>
      </div>
     )
}

export default EditPortfolio