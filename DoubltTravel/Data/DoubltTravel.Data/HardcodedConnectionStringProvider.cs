using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoubltTravel.Data
{
    public class HardcodedConnectionStringProvider : IConnectionStringProvider
    {
        public string Value
        {
            get
            {
                return "Data Source=.\\sqlexpress;Initial Catalog=DoubltTravel;Integrated Security=SSPI;";
            }
        }
    }
}
