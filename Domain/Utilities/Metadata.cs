using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilities
{
    public class Metadata
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Next { get; set; }
        public int Previous { get; set; }
    }
}
