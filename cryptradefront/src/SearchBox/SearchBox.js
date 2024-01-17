import React, {useState, useEffect, useRef} from 'react'
import axios from 'axios';
import './SearchBox.css';
import { useNavigate } from 'react-router-dom';

const SearchBox = ({handleSearchClick}) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [topCoins, setTopCoins] = useState([]);
  const inputRef = useRef(null);
  const navigate = useNavigate();

  useEffect(() => {
    // Focus on the input field when the component mounts
    inputRef.current.focus();
  }, []);

  useEffect(() => {
    const fetchTopCoins = async () => {
        try {
        const response = await axios.get('https://localhost:8005/api/Crypto/get-sorted-by-price-data');
        setTopCoins(response.data);
      } catch (error) {
        console.error('Error fetching top coins:', error);
      }
    };

    if (!searchTerm) {
      fetchTopCoins();
    }
  }, [searchTerm]);

  useEffect(() => {
    // Fetch relevant cryptocurrencies based on the search term
    const fetchSearchResults = async () => {
        try {
            const response = await axios.get(`https://localhost:8005/api/Crypto/search-crypto?searchTerm=${searchTerm}`);
            setSearchResults(response.data);
          } catch (error) {
            console.error('Error fetching search results:', error);
          }
    };

    if (searchTerm !== '') {
      fetchSearchResults();
    }
  }, [searchTerm]);

  const handleInputChange = async (event) => {
    const { value } = event.target;
    setSearchTerm(value);
    };

    const handleCryptoClick = (symbol) => {
      handleSearchClick();
      navigate(`/cryptocurrency/${symbol}`);
    };
  

  return (
    <div className="search-box">
    <div className='search-fields'>
    <input className='search-bar-box'
      type="text"
      placeholder="Search for cryptocurrencies..."
      value={searchTerm}
      onChange={handleInputChange}
      ref={inputRef}
    />
    <span onClick={handleSearchClick}>X</span>
    </div>
    <div className="search-results">
      {searchTerm === '' && (
        <>
          <h3>Trending</h3>
          <div className='coin-list-search'>
            {topCoins.slice(0,5).map((coin) => (
              <li className='coin-display' onClick={() => handleCryptoClick(coin.symbol)} key={coin.id}>
                <span>{coin.name} <b>{coin.symbol}</b></span>
                <span className='coin-price'>{coin.price}$</span>
              </li>
            ))}
            </div>
        </>
      )}
      {searchTerm !== '' && (
        <>
          <h3>Search results:</h3>
          <div className='coin-list-search'>
            {searchResults.slice(0,5).map((coin) => (
              <li className='coin-display' onClick={() => handleCryptoClick(coin.symbol)} key={coin.id}>
                <span>{coin.name} <b>{coin.symbol}</b></span>
                <span className='coin-price'>{coin.price}$</span>
                </li>
            ))}
          </div>
        </>
      )}
    </div>
  </div>
);
}

export default SearchBox