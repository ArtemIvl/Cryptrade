import React, { useState } from 'react';
import axios from 'axios';
// import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
// import { Visibility } from '@mui/icons-material';
import './RegistrationLoginForm.css';

/* eslint-disable jsx-a11y/anchor-is-valid */

const RegistrationLoginForm = ({handleLoginClick, setIsLoggedIn}) => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [loginEmail, setLoginEmail] = useState('');
    const [loginPassword, setLoginPassword] = useState('');
    const [formType, setFormType] = useState('login');

    const handleRegistration = async (e) => {
    e.preventDefault();

    // Make an API request to your ASP.NET Core microservice to register the user
    try {
      const response = await axios.post('https://localhost:8003/api/user/register', {
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

  const handleLogin = async (e) => {
    e.preventDefault();

    // Make an API request to your ASP.NET Core microservice to register the user
    try {
      const response = await axios.post('https://localhost:8003/api/user/login', {
        email: loginEmail,
        password: loginPassword,
      });

      // Handle successful registration, e.g., show a success message
      console.log('Login successful', response.data);
      const token = response.data.jwtToken; // Ensure this matches the actual key the token is sent with
      localStorage.setItem('token', token);
      setIsLoggedIn(true);
      handleLoginClick();
      alert('Login successful');
    } catch (error) {
      // Handle registration error, e.g., show an error message
      console.error('Login error', error.response.data);
      console.log(loginEmail, loginPassword)
      alert(`Login unsuccessful ${error}`);
    }
  };

    // // show hide password
    // const [passwordShown, setPasswordShown] = useState(false);
    // const togglePassword = () => {
    //   setPasswordShown(!passwordShown);
    // }

return (
  <div className='overlay'>
    <div className='login-container'>
        <div className='text-container'>
          <div className='method-container'>
          <label className={formType === 'login' ? 'login-text active' : 'login-text'} onClick={() => setFormType('login')}>Log In</label>
          <label className={formType === 'register' ? 'login-text active' : 'login-text'} onClick={() => setFormType('register')}>Sign Up</label>
          </div>
          <span className="visibility-login" onClick={handleLoginClick}>X</span>
        </div>
        {formType === 'register' && (
          <>
        <div className='name-container'>
          <label for='name'>Name</label>
          <input
            type='text'
            name='name'
            placeholder='Enter your name...'
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          {/* <p className='name-error'>{formErrors.name}</p> */}
        </div>
        <div className='email-container'>
          <label for='email'>Email Address</label>
          <input
            type='email'
            name='email'
            placeholder='Enter your email address...'
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          {/* <p className='email-error'>{formErrors.email}</p> */}
        </div>
        <div className='password-container'>
          <div className='password-labels'>
            <label for='password'>Password</label>
          </div>
          <input
            type='password'
            name='password'
            placeholder='Enter your password...'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          {/* <p className='password-error'>{formErrors.password}</p> */}
        </div>
        <div className='password-container'>
          <div className='password-labels'>
            <label for='conpass'>Repeat your password</label>
          </div>
          <input
            type='password'
            name='confirmpassword'
            placeholder='Repeat your password...'
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
          {/* <p className='confirmpassword-error'>{formErrors.confirmpassword}</p> */}
        </div>
        <button className='login-register-btn' onClick={handleRegistration}>
          Register
        </button>
        </>
        )}
        {formType === 'login' && (
          <>
          <div className='email-container'>
            <label for='email'>Email Address</label>
            <input type="email" name='email' placeholder='Enter your email address...'
            onChange={e => setLoginEmail(e.target.value)}></input>
          </div>
          <div className='password-container'>
            <div className='password-labels'>
              <label for='password'>Password</label>
              <a href="#" className='forgot-pass'>Forgot password?</a>
            </div>
          <input type='password' name='password' placeholder='Enter your password...'
          onChange={e => setLoginPassword(e.target.value)}></input>
          </div>
          <button className='login-register-btn'
          onClick={handleLogin}>
          Log In
          </button>
        </>
        )}
        </div>
        </div>
    );
        };
          

export default RegistrationLoginForm;
