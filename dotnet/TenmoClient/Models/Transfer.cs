namespace TenmoClient.Models
{
    public class Transfer
    {
        //public Transfer()
        //{
        //    //for type paramater
        //}

        public int TransferId { get; set; }
        public int TransferTypeId { get; set; }
        public string TransferTypeDesc { get; set; }
        public int TransferStatusId { get; set; }
        public string TransferStatusDesc { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }

        //public enum TransferStatusDescription
        //{
        //    Pending = 1,
        //    Approved = 2,
        //    Rejected = 3
        //}
        //public enum TransferTypeDescription
        //{
        //    Request = 1,
        //    Send = 2
        //}
        //public TransferStatusDescription TransferStatusDesc { get; set; }
        //public TransferTypeDescription TransferTypeDesc { get; set; }
    }
}
