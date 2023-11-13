import React from 'react'
import './PortfolioPage.css'

function PortfolioPage() {
  return (
    <div className='portfolio-container'>
      <h1>Portfolio</h1>
      <hr />
      <span className='content-container'>
      <button className="action-button">Create Portfolio</button>
      </span>
      <div className='portfolio-template'>
        <span className='portfolio-name-worth'>
          <label>Porfolio name</label>
          <br />
          <label>Porfolio worth</label>
        </span>
        <button className='portfolio-add-transaction'>Add transaction</button>
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
    </div>
  )
}

export default PortfolioPage