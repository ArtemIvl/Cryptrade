/* eslint-disable react-hooks/exhaustive-deps */
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './PortfolioPage.css';
import EditPortfolio from './EditPortfolio';
import EditTransaction from '../Transactions/EditTransaction';

function PortfolioPage({ isLoggedIn, handleAddTransaction }) {
  const navigate = useNavigate();
  const [portfolioName, setPortfolioName] = useState('');
  const [portfolioDescription, setPortfolioDescription] = useState('');
  const [existingPortfolio, setExistingPortfolio] = useState({});
  const [portfolioData, setPortfolioData] = useState([]);
  const token = localStorage.getItem('token');
  const [assets, setAssets] = useState([]);
  const [transactions, setTransactions] = useState([]);
  const [showTransactions, setShowTransactions] = useState(false);
  const [showEditTransaction, setShowEditTransaction] = useState(false);
  const [showEditPortfolio, setShowEditPortfolio] = useState(false);
  const [selectedTransaction, setSelectedTransaction] = useState(null);

  useEffect(() => {
    if (isLoggedIn && token) {
      // Check if there is an existing portfolio
      if (!existingPortfolio || Object.keys(existingPortfolio).length === 0) {
        fetchPortfolioData();
      } else {
        getAllAssets(); // If the portfolio already exists, directly fetch assets
      }
    } else {
      navigate('/cryptocurrency');
    }
  }, [existingPortfolio, isLoggedIn, navigate, token]);

  const fetchTotalValue = async () => {
    try {
      const response = await axios.get(`http://localhost:5174/api/Portfolio/total-value?portfolioId=${localStorage.getItem('portfolioId')}`);
      setPortfolioData(response.data);
      console.log('Portfolio Data', response.data)
    } catch (error) {
      console.error('Error fetching portfolio data:', error);
    }
  };

  const fetchPortfolioData = async () => {
    try {
      const response = await axios.get(`http://localhost:5174/api/portfolio`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      const portfolioData = response.data;
      setExistingPortfolio((prev) => ({ ...prev, ...portfolioData }));
      localStorage.setItem('portfolioId', portfolioData.id);
      getAllAssets(); // Call getAllTransactions when portfolio data is available
    } catch (error) {
      console.error('Error fetching user data:', error);
    }
  };

  const handleCreatePortfolio = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        `http://localhost:5174/api/portfolio`,
        {
          name: portfolioName,
          description: portfolioDescription,
          totalValue: 0,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      console.log('Portfolio creation successful', response.data);
      alert('Creation of Portfolio successful');
      localStorage.setItem('portfolioId', response.data.id);
      window.location.reload(false);
    } catch (error) {
      console.error('Portfolio creation error', error);
      alert(`Creation unsuccessful ${error}`);
    }
  };

  const handleDeletePortfolio = async (e) => {
    e.preventDefault();
    try {
      await axios.delete(`http://localhost:5174/api/portfolio?portfolioId=${existingPortfolio.id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      await axios.delete(`http://localhost:5102/api/transaction/byportfolio?portfolioId=${existingPortfolio.id}`);
      alert('Account deleted successfully!');
      setExistingPortfolio({});
      localStorage.removeItem('portfolioId');
      // window.location.reload(false);
    } catch (error) {
      console.error('Error deleting account:', error);
    }
  };

  const handleEditPortfolio = () => {
    setShowEditPortfolio(true);
  }

  const getAllAssets = async () => {
    try {
      const response = await axios.get(`http://localhost:5102/api/Transaction/assets?portfolioId=${localStorage.getItem('portfolioId')}`);
      setAssets(response.data);
      fetchTotalValue();
  } catch (error) {
      console.error('Error fetching assets:', error);
    }
  };
    
  const handleDeleteTransaction = async (transaction) => {
    try {
      await axios.delete(`http://localhost:5102/api/transaction?transactionid=${transaction.id}`);
      alert('Transaction deleted successfully!');
      
      // Fetch updated list of transactions after deletion
    const updatedTransactions = await axios.get(`http://localhost:5102/api/transaction/trans-asset?portfolioid=${localStorage.getItem('portfolioId')}&assetname=${transaction.cryptoName}`);
    const updatedAssets = await axios.get(`http://localhost:5102/api/Transaction/assets?portfolioId=${localStorage.getItem('portfolioId')}`);
      
    setAssets(updatedAssets.data);
    setTransactions(updatedTransactions.data);
    fetchTotalValue();
    } catch (error) {
      console.error('Error deleting transaction:', error);
    }
  };

  const handleGetTransactions = async (asset) => {
    try {
      const response = await axios.get(`http://localhost:5102/api/transaction/trans-asset?portfolioid=${localStorage.getItem('portfolioId')}&assetname=${asset.cryptoName}`);
      setTransactions(response.data);
      setShowTransactions(true); // Show transactions
    } catch (error) {
      console.error('Error fetching transactions:', error);
    }
  };

  const handleEditTransactionClick = (transaction) => {
    setSelectedTransaction(transaction);
    setShowEditTransaction(true);
  }

  const handleBackToAssets = async() => {
    try {    
      setShowTransactions(false); // Hide transactions and show assets
    } catch (error) {
      console.error('Error fetching assets:', error);
    }
  };

  return (
    <div className="portfolio-container">
      {showEditTransaction && <EditTransaction transaction={selectedTransaction} onClose={() => setShowEditTransaction(false)} />}
      {showEditPortfolio && <EditPortfolio portfolio={existingPortfolio}/>}
      <h1>Portfolio</h1>
      <hr />
      {Object.keys(existingPortfolio).length === 0 ? (
        <span className="content-container">
          <input placeholder="name" onChange={(e) => setPortfolioName(e.target.value)}></input>
          <input placeholder="description" onChange={(e) => setPortfolioDescription(e.target.value)}></input>
          <button className="action-button" onClick={handleCreatePortfolio}>
            Create Portfolio
          </button>
        </span>
      ) : (
        <>
          <div className="portfolio-template">
            <span className="portfolio-name-worth">
              <label>{existingPortfolio.name}</label>
              <br />
              <label>{portfolioData.totalValue} $</label>
            </span>
            <span className="portfolio-actions">
              <button className="portfolio-add-transaction" onClick={handleAddTransaction}>
                Add transaction
              </button>
              <button className="portfolio-delete" onClick={handleDeletePortfolio}>
                Delete
              </button>
              <button onClick={() => handleEditPortfolio()} className="portfolio-edit">
                Edit
              </button>
            </span>
            <span className="portfolio-history">History <br />
            </span>
            <span className="portfolio-best-performer">Best Performer<br />{portfolioData?.bestPerformer?.cryptoSymbol}<br />{portfolioData?.bestPerformer?.profitLoss} $</span>
            <span className="portfolio-worst-performer">Worst Performer<br />{portfolioData?.worstPerformer?.cryptoSymbol}<br />{portfolioData?.worstPerformer?.profitLoss} $</span>
            <span className="portfolio-all-time-profit">All time profit/loss: {Math.round(portfolioData.profitLoss)} $</span>
          </div>
          {showTransactions ? (
            <>
              <button className="back-button" onClick={handleBackToAssets}>
                Back to Assets
              </button>
              <h3 className="assets-line">Transactions</h3>
              <hr className="assets-line" />
              <li className="transactions-labels">
                <p>#</p>
                <p>Name</p>
                <p>Buy Price</p>
                <p>amount</p>
                <p>Date</p>
                <p>Actions</p>
              </li>
              <hr className="assets-line" />
                {transactions.map((transaction) => (
                  <li key={transaction.id}>
                    <div className="transactions-data">
                    <p className="left">{transaction.id}</p>
                    <p className="left">
                      {transaction.cryptoName} <b>{transaction.cryptoSymbol}</b>
                    </p>
                    <p className="right">{transaction.price} $</p>
                    <p className="right">{transaction.amount} {transaction.cryptoSymbol}</p>
                    <p className="right">{transaction.createdAt}</p>
                    <button onClick={() => handleDeleteTransaction(transaction)}>Delete</button>
                    <button onClick={() => handleEditTransactionClick(transaction)}>Edit</button>
                    </div>
                    <hr className="assets-line thin" />
                  </li>
                ))}
            </>
          ) : (
            <>
          <h3 className="assets-line">Assets</h3>
          <hr className="assets-line" />
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
          <hr className="assets-line" />
          {assets.map((item) => (
            <li key={item.cryptoSymbol} onClick={() => handleGetTransactions(item)}>
              <div className="assets-data">
                <p className="left">{item.id}</p>
                <p className="left">
                  {item.cryptoName} <b>{item.cryptoSymbol}</b>
                </p>
                <p className="right">{item.currentPrice} $</p>
                <p className="right">{item.percentChange24h} %</p>
                <p className="right">
                  {item.amount} {item.cryptoSymbol}
                </p>
                <p className="right">{item.avgBuyPrice}</p>
                <p className="right">{Math.round(item.profitLoss)} $</p>
                <p className="right">{item.id}</p>
              </div>
              <hr className="assets-line thin" />
            </li>
          ))}
          </>
          )}
          </>
      )}
    </div>
  );
}

export default PortfolioPage;
