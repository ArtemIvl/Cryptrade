import './App.css';
import CryptocurrencyList from './CryptocurrencyData/CryptocurrencyList';
import LoginForm from './UserManagement/LoginForm';

function App() {
  return (
    <div className="App">
      <header className='header'>
        Cryptrade
      </header>
      <CryptocurrencyList />
    </div>
  );
}

export default App;
