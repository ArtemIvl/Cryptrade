import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import axios from 'axios';
import './ProfilePage.css'

function ProfilePage({isLoggedIn}) {
    const navigate = useNavigate()
    const [userData, setUserData] = useState({});
    const [newName, setNewName] = useState('');
    const [newEmail, setNewEmail] = useState('');

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token || !isLoggedIn) {
          navigate('/cryptocurrency');
        } else {
          axios.get('https://localhost:7036/api/user', {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          })
            .then(response => {
              setUserData(response.data);
              setNewName(response.data.name);
              setNewEmail(response.data.email);
            })
            .catch(error => {
              console.error('Error fetching user data:', error);
              navigate('/cryptocurrency');
            });
        }
      }, [isLoggedIn, navigate]);

    const handleNameChange = () => {
        const token = localStorage.getItem('token');
        if (newName === userData.name) {
            return;
        }

        axios.put('https://localhost:7036/api/user', { name: newName, email: userData.email }, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
        .then(response => {
            window.location.reload(false);
        })
        .catch(error => {
            console.error('Error changing name:', error);
        });
    };

    const handleEmailChange = () => {
        const token = localStorage.getItem('token');
        if (newEmail === userData.email) {
            return;
        }

        axios.put('https://localhost:7036/api/user', { name: userData.name, email: newEmail }, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
        .then(response => {
            window.location.reload(false);
        })
        .catch(error => {
            console.error('Error changing email:', error);
        });
    };

    const handleDeleteAccount = () => {
        const token = localStorage.getItem('token');
        
        axios.delete('https://localhost:7036/api/user', {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
        .then(response => {
            alert('Account deleted successfully!');
            localStorage.removeItem('token');
            window.location.reload(false);
        })
        .catch(error => {
            console.error('Error deleting account:', error);
        });
    };
    
  return (
    <div className='profile-container'>
      <h1>User Profile</h1>
      <hr />
      <div className='content-container'>
      <label for='name'>Name</label>
        <span>
      <input value={newName} onChange={(e) => setNewName(e.target.value)}/>
      <button className="action-button" onClick={handleNameChange}>Change Name</button>
      </span>
      <label for='email'>Email Address</label>
      <span>
      <input value={newEmail} onChange={(e) => setNewEmail(e.target.value)}/>
      <button className="action-button" onClick={handleEmailChange}>Change Email</button>
      </span>
      <button className="action-button" onClick={handleDeleteAccount}>Delete account</button>
      </div>
    </div>
  )
}

export default ProfilePage