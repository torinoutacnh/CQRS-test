using CQRS.Db.Models;
using CQRS.Infras.Application;
using CQRS.Infras.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CQRS.Db.Queries.CustomerQueries.GetCustomerByEmail
{
    public class GetCustomerByEmailQuery: IQueryHandler<GetCustomerByEmailModel, Customer>
    {
        public IConfiguration _config { get; init; }
        private const string Query = $@"select * from customers where email=@email";

        public GetCustomerByEmailQuery(IConfiguration config)
        {
            _config = config;
        }

        public Task<Customer> Handle(GetCustomerByEmailModel email, CancellationToken cancellation)
        {
            var constr = _config.GetConnectionString("DefaultConnection");
            if (constr == null) throw new AppException(System.Net.HttpStatusCode.InternalServerError, "Connection not found!");
            using var conn = new SqlConnection(constr);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                var prms = new DynamicParameters();
                prms.Add("email", email.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                var item = conn.QuerySingleOrDefault<Customer>(Query, prms);
                return Task.FromResult(item);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            }
        }
    }
}
