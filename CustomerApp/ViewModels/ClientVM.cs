using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerApp.ViewModels
{
    public class ClientVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }
        public string UID { get; set; }
    }
}