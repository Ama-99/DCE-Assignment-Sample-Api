using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Test.Model;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetActiveOrders")]
        public Response GetActiveOrders()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("TestConn").ToString());
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.GetActiveOrders(connection);
            return response;
        }

    }
}
