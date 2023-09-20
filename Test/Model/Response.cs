using System;
using System.Collections.Generic;

namespace Test.Model
{
    public class Response
    {
        public int StatusCode {  get; set; }

        public string StatusMessage { get; set; }

        public Customer Customer { get; set; }

        public List<Customer> ListCustomer { get; set; }

        public List<Order> ListOrder { get; set; }



    }
}
