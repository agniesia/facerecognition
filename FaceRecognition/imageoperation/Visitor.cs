using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    
    interface Visitor
    {
        
        void rob(image_as_tab image);
        void Visit(image_Gray Grey);
        void Visit(image_RGB RGB);

    }
}
