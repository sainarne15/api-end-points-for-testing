using Microsoft.AspNetCore.Mvc;
using reward_calculator_backend.Models;

namespace reward_calculator_backend.Controllers
{
        [ApiController]
        [Route("api/transactions")]
        public class TransactionController : ControllerBase
        {
            private readonly List<Transaction> _transactions = new List<Transaction>();

            // POST: api/transactions
            [HttpPost]
            public IActionResult AddTransaction(Transaction transaction)
            {
                // Assuming you validate the transaction data here

                // Calculate reward points
                int rewardPoints = CalculateRewardPoints(transaction.TransactionAmount);
                transaction.RewardPoints = rewardPoints;

                // Assign a transaction ID (for demonstration purposes, you can use a simple counter)
                transaction.TransactionId = _transactions.Count + 1;

                // Add the transaction to the list
                _transactions.Add(transaction);

                // Return a 201 Created response with the newly added transaction
                return CreatedAtAction(nameof(GetTransaction), new { id = transaction.TransactionId }, transaction);
            }

            // Helper method to calculate reward points
            private int CalculateRewardPoints(decimal transactionAmount)
            {
                int rewardPoints = 0;

                if (transactionAmount > 100)
                {
                    rewardPoints += (int)((transactionAmount - 100) * 2);
                }

                if (transactionAmount > 50)
                {
                    rewardPoints += (int)((transactionAmount - 50) * 1);
                }

                return rewardPoints;
            }

            // GET: api/transactions
            [HttpGet]
            public IActionResult GetTransactions()
            {
                // Return the list of transactions
                return Ok(_transactions);
            }

            // GET: api/transactions/{id}
            [HttpGet("{id}")]
            public IActionResult GetTransaction(int customerId)
            {
            // Find the transaction by ID
            // Filter transactions by customer ID
            var customerTransactions = _transactions
                .Where(t => t.CustomerId == customerId)
                .ToList();

            return Ok(customerTransactions);
        }
        /*[HttpGet]
        public IActionResult GetTransactions(int customerId)
        {
            // Filter transactions by customer ID
            var customerTransactions = _transactions
                .Where(t => t.CustomerId == customerId)
                .ToList();

            return Ok(customerTransactions);
        }*/
    }

    }
