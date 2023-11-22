import React, {useState, useEffect, useRef} from 'react'
import axios from 'axios';
import './SearchBox.css';

const SearchBox = ({handleSearchClick}) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [topCoins, setTopCoins] = useState([]);
  const inputRef = useRef(null);

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
          <p>Trending</p>
          <ul>
            {topCoins.slice(0,5).map((coin) => (
              <li key={coin.id}>{coin.name}</li>
            ))}
          </ul>
        </>
      )}
      {searchTerm !== '' && (
        <>
          <p>Search results:</p>
          <ul>
            {searchResults.slice(0,5).map((coin) => (
              <li key={coin.id}>{coin.name}</li>
            ))}
          </ul>
        </>
      )}
    </div>
  </div>
);
}

export default SearchBox