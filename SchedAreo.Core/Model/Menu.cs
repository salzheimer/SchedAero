using System.Collections.Generic;

namespace SchedAreo.Core.Model
{
    public class Menu
    {
        public Menu()
        {
            MenuItems = new List<MenuItem>();
        }

        public List<MenuItem> MenuItems { get; set; }
    }
}