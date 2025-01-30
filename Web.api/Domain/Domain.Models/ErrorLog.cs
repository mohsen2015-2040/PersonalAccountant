using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Models
{
    public class ErrorLog
    {
        public int ErrorLogId { get; set; }

        public string? Address { get; set; }

        public DateTime CreationDate { get; set; }

        public string Message { get; set; }
    }
}
