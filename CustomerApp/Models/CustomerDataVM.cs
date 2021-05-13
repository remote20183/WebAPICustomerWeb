using CustomerApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.Models
{
    public class CustomerDataVM
    {
        public string CustomerId { get; set; }
         
        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public HttpPostedFileBase[] CustomerFilesArray { get; set; }

        public List<CustomerFile> CustomerFiles { get; set; }
    }

    public class CustomerRetrieveVM
    {
        public string CustomerName { get; set; }
    }
    public class DownloadRetrieveVM
    {

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }
       
    }
}