// ProfilePage.spec.js

describe('ProfilePage Component', () => {
    beforeEach(() => {
      // Log in and store the token in localStorage
      cy.visit('http://localhost:8002/');
      cy.contains('Sign in').click();
      cy.get('input[name="email"]').type('artem@example.com');
      cy.get('input[name="password"]').type('password123');
      cy.get('.login-register-btn').click();
      cy.on('window:alert', (text) => {
        expect(text).to.equal('Login successful');
      });
  
      // Visit the profile page using a link
      cy.contains('Profile').click();
    });
  
    it('should change the user name', () => {
      cy.contains('User Profile').should('be.visible');
      
      // Generate a unique name
      const newName = `NewName_${Cypress._.uniqueId()}`;
  
      // Change user name
      cy.get('input[for="name"]').clear().type(newName);
      cy.get('.action-button:contains("Change Name")').click();
  
      // Assert that the new name is present on the screen
      cy.get('input[for="name"]').invoke('val').should('contain', newName);
    });
  
    // Add more tests for other profile actions as needed
  });
  