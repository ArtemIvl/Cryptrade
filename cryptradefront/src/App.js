import './App.css';
import React, {useState, useEffect} from 'react';
import Header from './Header/Header';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import RegistrationLoginForm from './UserManagement/RegistrationLoginForm';
import SearchBox from './SearchBox/SearchBox';
import CryptocurrencyList from './CryptocurrencyData/CryptocurrencyList';
import SingleCrypto from './CryptocurrencyData/SingleCrypto';
import ProfilePage from './Profile/ProfilePage';
import PortfolioPage from './Portfolio/PortfolioPage';
import AddTransaction from './Transactions/AddTransaction';
import TradingPage from './Trading/TradingPage';

function App() {
  const token = localStorage.getItem('token');
  const [isLoggedIn, setIsLoggedIn] = useState(token != null ? true : false);
  const [showLogin, setShowLogin] = useState(false);
  const [showSearchBox, setShowSearchBox] = useState(false);
  const [showAddTransaction, setShowAddTransaction] = useState(false);

  const handleLoginClick = () => {
    setShowLogin(!showLogin);
  };

  const handleSearchClick = () => {
    setShowSearchBox(!showSearchBox);
  };

  const handleLogoutClick = () => {
    localStorage.removeItem('token');
    setIsLoggedIn(false);
  };

  const handleAddTransaction = () => {
    setShowAddTransaction(!showAddTransaction);
  }

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, [token]);
  
  return (
    <div className="App">
      <Router>
      <Header handleLoginClick={handleLoginClick} isLoggedIn={isLoggedIn} handleLogoutClick={handleLogoutClick} handleSearchClick={handleSearchClick}/>
      {showLogin && <RegistrationLoginForm handleLoginClick={handleLoginClick} setIsLoggedIn={setIsLoggedIn}/>}
      {showSearchBox && <SearchBox handleSearchClick={handleSearchClick}/>}
      {showAddTransaction && <AddTransaction handleAddTransaction={handleAddTransaction}/>}
      <Routes>
        <Route path="/cryptocurrency" element={<CryptocurrencyList />} />
        <Route path="/cryptocurrency/:symbol" element={<SingleCrypto />} />
        <Route path="/profile" element={<ProfilePage isLoggedIn={isLoggedIn}/>} />
        <Route path="/portfolio" element={<PortfolioPage isLoggedIn={isLoggedIn} handleAddTransaction={handleAddTransaction}/>} />
        <Route path="/trading" element={<TradingPage isLoggedIn={isLoggedIn}/>} />
      </Routes>
    </Router>
    </div>
  );
}

export default App;
