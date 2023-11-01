import './App.css';
import React, {useState, useEffect} from 'react';
import Header from './Header/Header';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import RegistrationLoginForm from './UserManagement/RegistrationLoginForm';
import CryptocurrencyList from './CryptocurrencyData/CryptocurrencyList';
import SingleCrypto from './CryptocurrencyData/SingleCrypto';
import ProfilePage from './Profile/ProfilePage';

function App() {
  const token = localStorage.getItem('token');
  const [isLoggedIn, setIsLoggedIn] = useState(token ? true : false);
  const [showLogin, setShowLogin] = useState(false);

  const handleLoginClick = () => {
    setShowLogin(!showLogin);
  };

  const handleLogoutClick = () => {
    localStorage.removeItem('token');
    setIsLoggedIn(false);
  };

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);
  
  return (
    <div className="App">
      <Router>
      <Header handleLoginClick={handleLoginClick} isLoggedIn={isLoggedIn} handleLogoutClick={handleLogoutClick}/>
      {showLogin && <RegistrationLoginForm handleLoginClick={handleLoginClick} setIsLoggedIn={setIsLoggedIn}/>}
      <Routes>
        <Route path="/cryptocurrency" element={<CryptocurrencyList />} />
        <Route path="/cryptocurrency/:symbol" element={<SingleCrypto />} />
        <Route path="/profile" element={<ProfilePage isLoggedIn={isLoggedIn}/>} />
      </Routes>
    </Router>
    </div>
  );
}

export default App;
