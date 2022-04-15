using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NETCoreRestAPI.Data
{
    public class User
    {
        public int ID { get; set; }
        public string CustomerId { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string AddUserID  { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserID { get; set; }
        public DateTime EditDate { get; set; }
    }
}
