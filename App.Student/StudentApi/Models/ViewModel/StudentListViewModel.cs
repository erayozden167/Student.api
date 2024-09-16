using System.Collections.Generic;
using System.Net;
using System.Numerics;

namespace StudentApi.Models.ViewModel
{
    public class StudentListViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int Age { get; set; }
      
    }
}
