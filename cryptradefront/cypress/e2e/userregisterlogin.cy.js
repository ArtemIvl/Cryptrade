// RegistrationLoginForm.spec.js

describe('RegistrationLoginForm Component', () => {
  beforeEach(() => {
    // Visit your React app or the page containing the RegistrationLoginForm component
    cy.visit('http://localhost:8002/');
  });

  const randomEmail = `user_${Cypress._.uniqueId()}@example.com`;

  it('should register a new user', () => {
    // Switch to the registration form
    cy.contains('Sign in').click();
    cy.contains('Sign Up').click();

    // Generate a random email address

    // Fill in the registration form
    cy.get('input[name="name"]').type('Artem');
    cy.get('input[name="email"]').type(randomEmail);
    cy.get('input[name="password"]').type('password123');
    cy.get('input[name="confirmpassword"]').type('password123');

    // Click the Register button
    cy.get('.login-register-btn').click();

    // Assert that the registration was successful
    cy.on('window:alert', (text) => {
      expect(text).to.equal('Registration successful');
    });
  });

  it('should login an existing user', () => {
    // Generate a random email address for the existing user

    cy.contains('Sign in').click();
    cy.contains('Log In').click();

    // Fill in the login form with the generated email
    cy.get('input[name="email"]').type(randomEmail);
    cy.get('input[name="password"]').type('password123');

    // Click the Log In button
    cy.get('.login-register-btn').click();

    // Assert that the login was successful
    cy.on('window:alert', (text) => {
      expect(text).to.equal('Login successful');
    });
  });
});
