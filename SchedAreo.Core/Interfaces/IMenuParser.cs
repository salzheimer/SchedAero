using SchedAreo.Core.Model;
using System.Xml;
using System.Xml.Linq;

namespace SchedAero.Core.Interfaces
{
    public interface IMenuParser
    {
      
        Menu ParseMenuFile(string path, string activePagePath);
    }
}