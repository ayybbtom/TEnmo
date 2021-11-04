using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using System.Data.SqlClient;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {
        private readonly string connectionString;
        public TransferSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Transfer CreateTransferTypeObject(TransferTypes transferToCreate)
        {
            Transfer createdTransfer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("", conn);

                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        SqlCommand command = new SqlCommand("SELECT @@IDENTITY", conn);
                        int newTransferId = Convert.ToInt32(command.ExecuteScalar());
                        createdTransfer = GetTransferByID(newTransferId);

                    }
                }
                return createdTransfer;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Transfer GetTransferByID(int transferId)
        {
            Transfer transfer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("", conn);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return transfer;
        }

        public List<Transfer> TransferLookupUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public Transfer TransferStatus(int transferID, TransferStatuses newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
