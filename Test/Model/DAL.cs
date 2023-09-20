using System;
using Test.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Test.Model
{
    public class DAL
    {
        public Response GetAllCustomers(SqlConnection connection ) { 
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customer",connection);
            DataTable dt=new DataTable();
            List<Customer> list = new List<Customer>();

            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    Customer customer = new Customer();
                    customer.UserId = Convert.ToString(dt.Rows[i]["UserID"]);
                    customer.Username = Convert.ToString(dt.Rows[i]["Username"]);
                    customer.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    customer.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    customer.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    customer.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                    customer.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                    
                    list.Add(customer);
                }
                
            }

            if (list.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.ListCustomer= list;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data found";
                response.ListCustomer = null;
            }
            return response; 
        }

        public Response CreateCustomers(SqlConnection connection, Customer customer)
        {
            Response response = new Response();

            try
            {
                string query = "INSERT INTO Customer (UserId, Username, Email, FirstName, LastName, CreatedOn, IsActive) " +
                               "VALUES (@UserId, @Username, @Email, @FirstName, @LastName, @CreatedOn, @IsActive)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                    cmd.Parameters.AddWithValue("@Username", customer.Username);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@CreatedOn", customer.CreatedOn);
                    cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Customer added";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "No data inserted";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during database operations.
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = "Error: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }

        public Response UpdateCustomers(SqlConnection connection, Customer customer)
        {
            Response response = new Response();

            try
            {
                connection.Open();

                string sql = "UPDATE Customer SET Username=@Username, Email=@Email, FirstName=@FirstName, LastName=@LastName WHERE UserId=@UserId";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Username", customer.Username);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@UserId", customer.UserId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Customer Updated";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No data Updated";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = "An error occurred: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }

        public Response DeleteCustomers(SqlConnection connection, string UserId) {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE ID='"+UserId+"'",connection);
            connection.Open();
            int i=cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Customer deleted";
            }
            else {
                response.StatusCode = 100;
                response.StatusMessage = "Customer is not deleted";
            }

            return response;
        }

        public Response GetActiveOrders(SqlConnection connection)
        {
            Response response = new Response();
            String query = "SELECT Order.OrderId, Order.OrderName, Product.ProductName FROM Order INNER JOIN Product ON Order.ProductId = Product.ProductId INNER JOIN Customer ON Order.OrderBy = Customer.CustomerIdWHERE Order.IsActive = 1";
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            List<Order> list = new List<Order>();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Order orders = new Order();
                    orders.OrderId = Convert.ToString(dt.Rows[i]["OrderId"]);
                    orders.ProductId =  Convert.ToString(dt.Rows[i]["ProductId"]);
                    list.Add(orders);
                }

            }

            if (list.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.ListOrder = list;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data found";
                response.ListOrder = null;
            }
            return response;
        }


    }
}
