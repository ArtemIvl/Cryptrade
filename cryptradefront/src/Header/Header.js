import React, {useState} from 'react'
import { Link } from 'react-router-dom'
import './Header.css'

function Header({ handleLoginClick, isLoggedIn, handleLogoutClick }) {

  return (
    <>
    <div className='header'>
      <div className='container-left'>
        <span>Cryptrade</span>
        <Link to='/cryptocurrency'>
        <span>Cryptocurrencies</span>
        </Link>
        <span>Mock Trading</span>
      </div>
      <div className='container-right'>
        <span>Portfolio</span>
        <input className='search-bar' placeholder='Search'></input>
        {isLoggedIn ? <Link to='/profile'><span>Profile</span></Link> : null}
        {isLoggedIn ? <span onClick={handleLogoutClick}>Sing Out</span> : <span onClick={handleLoginClick}>Sign in</span>}
      </div>
    </div>
    </>
  )
}

export default Header