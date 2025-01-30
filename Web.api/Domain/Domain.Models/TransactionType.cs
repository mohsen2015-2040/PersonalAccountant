using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Models
{
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } = [];
        public virtual ICollection<Category> Categories { get; set; } = [];
    }
}
