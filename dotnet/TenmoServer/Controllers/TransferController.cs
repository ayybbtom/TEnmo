﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        public ITransferDao transferDao;

        public TransferController(ITransferDao _transferDao)
        {
            transferDao = _transferDao;
        }

        [HttpGet("{transferId}")]
        public Transfer GetTransferById(int transferId)
        {
            Transfer transfer = transferDao.GetTransferByID(transferId);
            return transfer;    
        }

        [HttpGet("{userId}/alltransfers")]
        public List<Transfer> TransferLookupUserId(int userId)
        {
            List<Transfer> transfers = transferDao.TransferLookupUserId(userId);
            return transfers;
        }

        [HttpGet]
        public List<Transfer> GetAllTransfers()
        {
            List<Transfer> transfers = transferDao.GetAllTransfers();
            return transfers;
        }
    }
}
