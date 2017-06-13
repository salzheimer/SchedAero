using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SchedAreo.Core.Model;


namespace SchedAero.Core.Services
{
    public class MenuParser : Interfaces.IMenuParser
    {
        /// <summary>
        /// Get XDocument from file path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public XDocument GetMenuFile(string path)
        {
            //ensure file has an xml extension
            var ext = path.Substring(path.LastIndexOf('.'));
            if (ext != ".xml") { throw new FileFormatException("File must be have .xml extenstion"); }

            var xmlReader = XmlReader.Create(path);

            var xDoc = XDocument.Load(path);

            return xDoc;
        }
        /// <summary>
        /// Send xml menu file path and active page from menu
        /// </summary>
        /// <param name="path"></param>
        /// <param name="activePagePath"></param>
        /// <returns>Menu object which is a list of menu items</returns>
        public Menu ParseMenuFile(string path, string activePagePath)
        {
            var xDoc = GetMenuFile(path);

            var menu = new Menu();

            var menuItems = from nodes in xDoc.Elements("menu").Elements("item")
                            select nodes;

            foreach (var item in menuItems)
            {
                var menuItem = new MenuItem();
                menuItem.DisplayName = item.Element("displayName").Value;
                menuItem.Path = item.Element("path").FirstAttribute.Value;
                menuItem.IsActive = (item.Element("path").FirstAttribute.Value == activePagePath || item.LastNode.ToString().Contains(activePagePath)) ? true : false;

                menu.MenuItems.Add(menuItem);

                //get children items based on submenu tag
                if (item.LastNode.ToString().Contains("subMenu"))
                {
                    ParserSubMenu(item.Elements("subMenu").Elements("item"), activePagePath.Trim(), menu, item.Element("displayName").Value);
                }
            }


            return menu;
        }

        private void ParserSubMenu(IEnumerable<XElement> items, string activePagePath, Menu menu, string parentName)
        {
            foreach (var item in items)
            {
                var menuItem = new MenuItem();
                menuItem.ParentName = parentName;
                menuItem.DisplayName = item.Element("displayName").Value;
                menuItem.Path = item.Element("path").FirstAttribute.Value;
                menuItem.IsActive = (item.Element("path").FirstAttribute.Value == activePagePath || item.LastNode.ToString().Contains(activePagePath)) ? true : false;

                menu.MenuItems.Add(menuItem);
                //get children items based on submenu tag
                if (item.LastNode.ToString().Contains("subMenu"))
                {
                    ParserSubMenu(item.Elements("subMenu").Elements("item"), activePagePath.Trim(), menu, item.Element("displayName").Value);
                }
            }
        }
    }
}
