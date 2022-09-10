namespace CQRS.Db.Models
{
    public class Customer:Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; }

        private Customer() { }

        public Customer(string name, string email, string address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }
}