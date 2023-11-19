import React, {useState, useEffect} from 'react'
import { useNavigate } from 'react-router-dom'
import axios from 'axios';
import './PortfolioPage.css'
import EditPortfolio from './EditPortfolio';

function PortfolioPage(isLoggedIn) {

  const navigate = useNavigate();
  const [portfolioName, setPortfolioName] = useState('');
  const [portfolioDescription, setPortfolioDescription] = useState('');
  const [existingPortfolio, setExistingPortfolio] = useState({});
  const [newName, setNewName] = useState(portfolioName);
  const [newDescription, setNewDescription] = useState(portfolioDescription);
  const token = localStorage.getItem('token');

  useEffect(() => {
    if (isLoggedIn && token) {
      try {
      axios.get(`https://localhost:7230/api/portfolio`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
        .then(response => {
          setExistingPortfolio(response.data);
          console.log(response.data);
        })
        .catch(error => {
          console.error('Error fetching user data:', error);
        });
      } catch (error) {
        console.error(error);
      }
    }
    else {
      navigate('/cryptocurrency'); 
    } 
  }, [isLoggedIn, navigate, token]);

  const handleCreatePortfolio = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(`https://localhost:7230/api/portfolio`, 
      {
        name: portfolioName,
        description: portfolioDescription,
        totalValue: 0,
      }, 
      {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    });
      console.log('Portfolio creation successful', response.data);
      alert('Creation of Portfolio successful');
      window.location.reload(false);
    } catch (error) {
      console.error('Portfolio creation error', error);
      alert(`Creation unsuccessful ${error}`);
    }
  };

  const handleDeletePortfolio = async (e) => {
    e.preventDefault();
    try {
        await axios.delete(`https://localhost:7230/api/portfolio?portfolioId=${existingPortfolio.id}`,
        {
          headers: {
          Authorization: `Bearer ${token}`,
        }});
      alert('Account deleted successfully!');
      setExistingPortfolio({});
      // window.location.reload(false);
    } catch (error) {
      console.error('Error deleting account:', error);
    }
  };

  const handleEditPortfolio = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`https://localhost:7230/api/portfolio?portfolioId=${existingPortfolio.id}`,
      {
        name: newName,
        description: newDescription,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        }
      })
    } catch (error) {
      console.error('Error editing portfolio:', error);
    }
  };

  return (
    <div className='portfolio-container'>
      <h1>Portfolio</h1>
      <hr />
      {Object.keys(existingPortfolio).length === 0 ? 
      <span className='content-container'>
        <input placeholder='name' onChange={(e) => setPortfolioName(e.target.value)}></input>
        <input placeholder='description' onChange={(e) => setPortfolioDescription(e.target.value)}></input>
      <button className="action-button" onClick={handleCreatePortfolio}>Create Portfolio</button>
      </span> :
      <>
      <div className='portfolio-template'>
        <span className='portfolio-name-worth'>
          <label>{existingPortfolio.name}</label>
          <br />
          <label>{existingPortfolio.totalValue}</label>
        </span>
        <span className='portfolio-actions'>
        <button className='portfolio-add-transaction'>Add transaction</button>
        <button className='portfolio-delete' onClick={handleDeletePortfolio}>Delete</button>
        <button onClick={() => <EditPortfolio />} className='portfolio-edit'>Edit</button>
        </span>
        <span className='portfolio-history'>History</span>
        <span className='portfolio-best-performer'>BTC</span>
        <span className='portfolio-worst-performer'>ETH</span>
        <span className='portfolio-all-time-profit'>All time profit/loss</span>
      </div>
      <h3 className='assets-line'>Assets</h3>
      <hr className='assets-line'/>
      <li className="assets-labels">
        <p>#</p>
            <p>Name</p>
            <p>Price</p>
            <p>24h %</p>
            <p>Holdings</p>
            <p>Avg. Buy Price</p>
            <p>Profit/Loss</p>
            <p>Actions</p>
        </li>
        <hr className='assets-line'/>
        </>
        }
    </div>
  )
}

export default PortfolioPage