import React from 'react'
import './Header.css'

function Header() {
  return (
    <div className='header'>
      <div className='container-left'>
        <span>Cryptrade</span>
        <span>Cryptocurrencies</span>
        <span>Mock Trading</span>
      </div>
      <div className='container-right'>
        <span>Portfolio</span>
        <span>Search Bar</span>
        <span>Sign in</span>
      </div>
    </div>
  )
}

export default Header