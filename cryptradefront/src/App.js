import './App.css';
import CryptocurrencyList from './CryptocurrencyData/CryptocurrencyList';
import Header from './Header/Header';
import RegistrationLoginForm from './UserManagement/RegistrationLoginForm';
import { useState } from 'react';

function App() {

  const [formType, setFormType] = useState('login'); // Initial form type

  const changeFormType = (type) => {
    setFormType(type);
  };

  
  return (
    <div className="App">
      <Header />
      <RegistrationLoginForm formType={formType} onChangeFormType={changeFormType}/>
    </div>
  );
}

export default App;
