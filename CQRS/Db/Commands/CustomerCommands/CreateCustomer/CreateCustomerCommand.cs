using CQRS.Db.Models;
using CQRS.Infras.Application;
using CQRS.Infras.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace CQRS.Db.Commands.CustomerCommands.CreateCustomer
{
    public class CreateCustomerCommand : ICommandHandler<CreateCustomerModel, Customer>
    {
        public IConfiguration _config { get; init; }
        public const string Query = $@"insert into customers([Id],[Name],[Email],[Address]) values(@Id,@Name,@Email,@Address)";

        public CreateCustomerCommand(IConfiguration config)
        {
            _config = config;
        }

        public Task<Customer> Handle(CreateCustomerModel query, CancellationToken cancellation)
        {
            var constr = _config.GetConnectionString("DefaultConnection");
            if (constr == null) throw new AppException(System.Net.HttpStatusCode.InternalServerError, "Connection not found!");
            using var conn = new SqlConnection(constr);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                using var tran = conn.BeginTransaction();
                try
                {
                    var prms = new DynamicParameters();
                    var customer = CreateCustomerModel.CreateCustomer(query);

                    prms.Add("Id", customer.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
                    prms.Add("Name", customer.Name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    prms.Add("Email", customer.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    prms.Add("Address", customer.Address, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                    var item = conn.Execute(Query, prms, tran);
                    tran.Commit();
                    return Task.FromResult(customer);
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
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
