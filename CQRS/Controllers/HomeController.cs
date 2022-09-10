using CQRS.Db.Commands.CustomerCommands.CreateCustomer;
using CQRS.Db.Models;
using CQRS.Db.Queries.CustomerQueries;
using CQRS.Db.Queries.CustomerQueries.GetCustomerByEmail;
using CQRS.Db.Queries.CustomerQueries.Models;
using CQRS.Infras.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace CQRS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public HomeController(ILogger<HomeController> logger, IQueryDispatcher queryDispatcher,ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        [Route("get-user-by-id")]
        public Task<Customer> GetUserById([FromBody] GetCustomerByIdModel model)
        {
            var item = _queryDispatcher.Dispatch<GetCustomerByIdModel, Customer>(model, new CancellationToken());
            return item;
        }

        [HttpPost]
        [Route("get-user-by-email")]
        public Task<Customer> GetUserByEmail([FromBody] GetCustomerByEmailModel model)
        {
            var item = _queryDispatcher.Dispatch<GetCustomerByEmailModel, Customer>(model, new CancellationToken());
            return item;
        }

        [HttpPost]
        [Route("create-user")]
        public Task<Customer> CreateUser([FromBody] CreateCustomerModel model)
        {
            var item = _commandDispatcher.Dispatch<CreateCustomerModel, Customer>(model, new CancellationToken());
            return item;
        }
    }
}