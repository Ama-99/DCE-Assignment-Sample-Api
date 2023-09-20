using System;
using Test.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace Test.Model
{
    public class Customer
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}