using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SchedAero.Core.Interfaces;
using SchedAero.Core.Services;
using SchedAreo.Core.Model;


namespace SchedAero.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2) { throw new Exception("Missing input values: Please provide input xml menu file path followed by page url"); }

            //created for reaability
            string filePath = args[0];
            string activePage = args[1];

            if (File.Exists(filePath))
            {
                IMenuParser menuParser = new MenuParser();

                var menu = menuParser.ParseMenuFile(filePath, activePage);

                Console.Write(GetActivePage(menu));

                Console.ReadLine();
            }
            else
            {
                throw new FileNotFoundException(string.Format("Could not locate {0}. Verify file name and location.", filePath));

            }



        }

        private static string GetActivePage(Menu menu)
        {
            StringBuilder output = new StringBuilder();

            //loop through menu items to set output format
            foreach (var item in menu.MenuItems)
            {
                string tabs = string.Empty;
                //create indention based on item level
                for (int i = 0; i < item.Level; i++) { tabs += '\t'; };

                output.Append(tabs);
                output.Append(string.Format("{0}, {1} {2}", item.DisplayName, item.Path, (item.IsActive) ? "ACTIVE" : string.Empty).Trim());
                output.Append("\r\n");
                
            }

            return output.ToString();
        }
    }
}
