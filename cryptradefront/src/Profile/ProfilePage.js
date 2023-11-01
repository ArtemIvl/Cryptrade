import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import axios from 'axios';
import './ProfilePage.css'

function ProfilePage({isLoggedIn}) {
    const navigate = useNavigate()
    const [userData, setUserData] = useState({});

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token || !isLoggedIn) {
          navigate('/cryptocurrency');
        } else {
          axios.get('https://localhost:7036/api/user/profile', {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          })
            .then(response => {
              setUserData(response.data);
            })
            .catch(error => {
              console.error('Error fetching user data:', error);
            });
        }
      }, [isLoggedIn, navigate]);

  return (
    <div className='profile-container'>
      <h1>User Profile</h1>
      <hr />
      <p>Name: {userData.name}</p>
      <p>Email: {userData.email}</p>
    </div>
  )
}

export default ProfilePage