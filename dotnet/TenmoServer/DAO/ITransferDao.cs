using System;
using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        void WriteTransferToDB(Transfer transfer);
        void UpdateBalanceForTransaction(Transfer transfer);
        Transfer GetTransferByID(int transferId);
        List<Transfer> TransfersForUser(int userID);
        //List<Transfer> GetAllTransfers(); would be nice to show all, but don't think needed or capstone, and is done by user in above 
    }
}
