using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
	[TestFixture()]
	public class TransactionTest
	{
        [TestFixture]
        public class TransactionServiceTests
        {
            [Test]
            public void AddTransaction_ShouldAddTransactionToContext()
            {
                // Arrange
                var dbContext = new Mock<TransactionDbContext>();
                var cryptoService = new Mock<CryptocurrencyService>();
                var rabbitMQPublisher = new Mock<RabbitMQPublisher>();
                var transactionService = new TransactionService(dbContext.Object, cryptoService.Object, rabbitMQPublisher.Object);
                var transaction = new Transaction();

                // Act
                transactionService.AddTransaction(transaction);

                // Assert
                dbContext.Verify(c => c.Transactions.Add(transaction), Times.Once);
                dbContext.Verify(c => c.SaveChanges(), Times.Once);
            }

            [Test]
            public async Task GetTransactionsByPortfolioId_ShouldReturnListOfTransactions()
            {
                // Arrange
                var dbContext = new Mock<TransactionDbContext>();
                var cryptoService = new Mock<CryptocurrencyService>();
                var rabbitMQPublisher = new Mock<RabbitMQPublisher>();
                var transactionService = new TransactionService(dbContext.Object, cryptoService.Object, rabbitMQPublisher.Object);
                var portfolioId = 1;

                // Act
                var result = await transactionService.GetTransactionsByPortfolioId(portfolioId);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<List<Transaction>>(result);
            }

            // Similar tests can be added for other methods...

            [Test]
            public void DeleteTransaction_ExistingTransaction_ShouldDeleteTransaction()
            {
                // Arrange
                var dbContext = new Mock<TransactionDbContext>();
                var cryptoService = new Mock<CryptocurrencyService>();
                var rabbitMQPublisher = new Mock<RabbitMQPublisher>();
                var transactionService = new TransactionService(dbContext.Object, cryptoService.Object, rabbitMQPublisher.Object);
                var transactionId = 1;

                // Act
                transactionService.DeleteTransaction(transactionId);

                // Assert
                dbContext.Verify(c => c.Transactions.Remove(It.IsAny<Transaction>()), Times.Once);
                dbContext.Verify(c => c.SaveChanges(), Times.Once);
            }

            [Test]
            public void DeleteTransactionsByPortfolioId_ExistingTransactions_ShouldDeleteTransactions()
            {
                // Arrange
                var dbContext = new Mock<TransactionDbContext>();
                var cryptoService = new Mock<CryptocurrencyService>();
                var rabbitMQPublisher = new Mock<RabbitMQPublisher>();
                var transactionService = new TransactionService(dbContext.Object, cryptoService.Object, rabbitMQPublisher.Object);
                var portfolioId = 1;

                // Act
                transactionService.DeleteTransactionsByPortfolioId(portfolioId);

                // Assert
                dbContext.Verify(c => c.Transactions.RemoveRange(It.IsAny<IEnumerable<Transaction>>()), Times.Once);
                dbContext.Verify(c => c.SaveChanges(), Times.Once);
            }
        }
    }
