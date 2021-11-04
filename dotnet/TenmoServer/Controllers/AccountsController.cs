using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("/")]
    [ApiController]
    [Authorize]
    public class AccountsController : Controller
    {
        public IAccountDao accountDao;
        private IUserDao userDao;
        public AccountsController(IAccountDao _accountDao, IUserDao _userDao)
        {
            accountDao = _accountDao;
            userDao = _userDao;
        }
        [HttpGet]
        public List<Account> GetAccounts() //homepage data, 100% totally safe to put
        {
            List<Account> accounts = accountDao.GetAccounts();
            return accounts;
        }

        [HttpPost("accounts/{userId}")]
        public Account GetAccount(int userId)
        {
            Account account = accountDao.GetAccount(userId);
            return account;
        }
    }
}
