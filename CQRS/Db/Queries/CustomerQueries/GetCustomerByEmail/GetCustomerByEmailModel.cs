namespace CQRS.Db.Queries.CustomerQueries.GetCustomerByEmail
{
    public class GetCustomerByEmailModel
    {
        public string Email { get; set; }

        public GetCustomerByEmailModel(string email)
        {
            Email = email;
        }
    }
}
