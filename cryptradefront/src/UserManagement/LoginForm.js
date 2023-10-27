import React, { useState } from 'react';
import axios from 'axios';
import './RegistrationForm.css';

const RegistrationForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = async (e) => {
    e.preventDefault();

    // Make an API request to your ASP.NET Core microservice to register the user
    try {
      const response = await axios.post('https://localhost:7036/api/user/login', {
        email,
        password,
      });

      // Handle successful registration, e.g., show a success message
      console.log('Login successful', response.data);
      alert('Login successful');
    } catch (error) {
      // Handle registration error, e.g., show an error message
      console.error('Login error', error);
      alert(`Login unsuccessful ${error}`);
    }
  };

  return (
    <div className='body-register'>
    <div className="container">
      <form onSubmit={handleLogin} className='registration-form'>
        <h2>Login</h2>
        <div className='form-group'>
          <label for='email'>Email</label>
          <input 
              type='text' 
              id='email' 
              required 
              placeholder='Email' 
              value={email} 
              onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div className='form-group'>
          <label for='password'>Password</label>
          <input 
              type='password' 
              id='password' 
              required 
              placeholder='Password' 
              value={password} 
              onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
    </div>
  );
};

export default RegistrationForm;
