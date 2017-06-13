using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedAreo.Core.Model
{
    public class MenuItem
    {
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public string ParentName { get; set; }
        public bool IsActive { get; set; }
    }
}
