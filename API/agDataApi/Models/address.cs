namespace AgDataAPI.Models
{
    public class address
    {
        public int AddressId { get; set; } // unique identifier for the address
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        // linking back to customer
        public int CustomerId { get; set; }    
    }
}