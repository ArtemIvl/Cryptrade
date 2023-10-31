import './App.css';
import CryptocurrencyList from './CryptocurrencyData/CryptocurrencyList';
import Header from './Header/Header';
import RegistrationLoginForm from './UserManagement/RegistrationLoginForm';

function App() {
  
  return (
    <div className="App">
      <Header />
      <CryptocurrencyList />
      {/* <RegistrationLoginForm /> */}
    </div>
  );
}

export default App;
