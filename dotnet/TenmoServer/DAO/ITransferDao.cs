using System;
using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        Transfer CreateTransferTypeObject(TransferTypes transferToCreate);
        Transfer GetTransferByID(int transferId);
        List<Transfer> TransferLookupUserID(int userID);
        Transfer TransferStatus(int transferID, TransferStatuses newStatus);
    }
}
