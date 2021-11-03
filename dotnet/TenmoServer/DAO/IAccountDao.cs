using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        Account GetAccountById(int accountId);
        Account GetBalanceById(int accountId, decimal amount);
        decimal GetBalance(int id);
    }
}
