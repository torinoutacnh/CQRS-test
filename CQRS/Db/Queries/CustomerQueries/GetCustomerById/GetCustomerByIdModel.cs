namespace CQRS.Db.Queries.CustomerQueries.Models
{
    public class GetCustomerByIdModel
    {
        public Guid Id { get; set; }

        public GetCustomerByIdModel(Guid id)
        {
            Id = id;
        }
    }
}
