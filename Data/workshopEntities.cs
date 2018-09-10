using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public partial class workshopEntities
    {
        public workshopEntities(string name="workshop"):base(string.Format("name={0}",name))
        {
            
        }
    }
}
