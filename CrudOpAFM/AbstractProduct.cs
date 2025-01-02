using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOpAFM
{
    public interface ICrudHandlers
    {
        void create();
        void read();
        void update();
        void delete();
        
    }
}
