using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UID { get; set; }
    }
}