using CQRS.Db.Models;

namespace CQRS.Db.Commands.CustomerCommands.CreateCustomer
{
    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public CreateCustomerModel(string name,string email,string address)
        {
            Name = name;
            Email = email;
            Address = address;
        }

        public static Customer CreateCustomer(CreateCustomerModel model)
        {
            return new Customer(model.Name, model.Email, model.Address);
        }
    }
}
