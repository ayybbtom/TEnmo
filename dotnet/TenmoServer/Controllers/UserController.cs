using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IAccountDao accountDao;
        private readonly ITransferDao transferDao;

        public UserController(IUserDao _userDao, IAccountDao _accountDao, ITransferDao _transferDao)
        {
            userDao = _userDao;
            accountDao = _accountDao;
            transferDao = _transferDao;
        }
        
        [HttpGet("account")]
        public ActionResult<decimal> GetBalance(int id)
        {
            string name = User.Identity.Name;
            decimal balance = accountDao.GetBalance(id);
            if (balance != null)
            {
                return Ok(balance);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
