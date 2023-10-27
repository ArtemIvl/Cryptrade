import React, { useState } from 'react';
import axios from 'axios';
import './RegistrationForm.css';

const RegistrationForm = () => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const handleRegistration = async (e) => {
    e.preventDefault();

    // Make an API request to your ASP.NET Core microservice to register the user
    try {
      const response = await axios.post('https://localhost:7036/api/user/register', {
        name,
        email,
        password,
        confirmPassword
      });

      // Handle successful registration, e.g., show a success message
      console.log('Registration successful', response.data);
      alert('Registration successful');
    } catch (error) {
      // Handle registration error, e.g., show an error message
      console.error('Registration error', error);
      alert(`Registration unsuccessful ${error}`);
    }
  };

  return (
    <div className='body-register'>
    <div className="container">
      <form onSubmit={handleRegistration} className='registration-form'>
        <h2>Register</h2>
        <div className='form-group'>
          <label for='name'>Name</label>
          <input 
              type='text' 
              id='name' 
              required 
              placeholder='Name' 
              value={name} 
              onChange={(e) => setName(e.target.value)}
          />
        </div>
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
        <div className='form-group'>
          <label for='confirm-password'>Confirm Password</label>
          <input 
              type='password' 
              id='confirm-password' 
              required 
              placeholder='Confirm password' 
              value={confirmPassword} 
              onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </div>
        <div class="form-group">
          <label for="terms">
              <input type="checkbox" id="terms" required /> I have read and agree to the Terms and Conditions
          </label>
        </div>
        <button type="submit">Register</button>
      </form>
    </div>
    </div>
  );
};

export default RegistrationForm;
