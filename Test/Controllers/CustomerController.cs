using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Microsoft.Extensions.Configuration;
using Test.Model;
using System.Data.SqlClient;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
       private readonly IConfiguration _configuration;
       public CustomerController(IConfiguration configuration)
        {
            _configuration= configuration;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public Response GetAllCustomers()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("TestConn").ToString());
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.GetAllCustomers(connection);
            return response;
        }

        [HttpPost]
        [Route("CreateCustomers")]
        public Response CreateCustomers(Customer customer)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("TestConn").ToString());
            Response response = new Response();
            DAL dal = new DAL();
            response=dal.CreateCustomers(connection, customer);
            return response;
        }

        [HttpPut]
        [Route("UpdateCustomers")]
        public Response UpdateCustomers(Customer customer)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("TestConn").ToString());
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.UpdateCustomers(connection, customer);
            return response;
        }

        [HttpDelete]
        [Route("DeleteCustomers/(id)")]
        public Response DeleteCustomers(string id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("TestConn").ToString());
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.DeleteCustomers(connection, id);
            return response;
        }
    }
}
