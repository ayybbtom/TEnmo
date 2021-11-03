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

                    SqlCommand cmd = new SqlCommand("INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)" +
                        "VALUES ((SELECT transfer_type_id FROM transfer_types WHERE transfer_type_desc = @transferType),(SELECT transfer_status_id FROM transfer_statuses WHERE transfer_status_desc = @transferStatus), @accountFromId, @accountToId, @amount);", conn);

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


                    //to fDOOOOOOOOOOOOOO
                    SqlCommand cmd = new SqlCommand("SELECT t.account_from, t.account_to, tt.transfer_type_desc, ts.transfer_status_desc, t.amount from transfers as t join transfer_types as tt on t.transfer_type_id = tt.transfer_type_id join transfer_statuses as ts on t.transfer_status_id = ts.transfer_status_id join (SELECT a.account_id, u.username as sender from accounts as a join users as u on a.user_id = u.user_id) as af on af.account_id = t.account_from join (SELECT a.account_id, u.username as receiver from accounts as a join users as u on a.user_id = u.user_id) as at on at.account_id = t.account_to where transfer_id = @transferId;", conn);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Transfer> TransferLookupUserID(int userID)
        {
            p
        }

        public Transfer TransferStatus(int transferID, TransferStatuses newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
