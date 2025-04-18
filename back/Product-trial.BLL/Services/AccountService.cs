using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product_trial.DAL.Context;
using Product_trial.DAL.Models;
using Product_trial.DAL.Utils;

namespace Product_trial.BLL.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Creates a new account in the database.
        /// </summary>
        /// <param name="account">The account to create.</param>
        /// <returns></returns>
        Task CreateAsync(Account account);

        Task<Account?> GetAsync(string email);
    }
    public class AccountService : IAccountService
    {
        private readonly ProductContext _context;

        /// <summary>
        /// Constructor for AccountService.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AccountService(ProductContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task CreateAsync(Account account)
        {
            account.Password = HashHelper.Hash(account.Password);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> GetAsync(string email)
        {
            return await _context.Accounts.FindAsync(email);
        }
    }
}
