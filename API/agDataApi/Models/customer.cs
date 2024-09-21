namespace AgDataAPI.Models
{
    public class customer
    {
        public int CustomerId { get; set; }  // unique identifier for the customer
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // concatenate for full name
        public string FullName => $"{FirstName} {LastName}";  
    }
}