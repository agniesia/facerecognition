using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.Memento
{
    class Memento
    {

        

        public image_as_tab State { get; private set; }

        
        public Memento(image_as_tab state)
        {

            State = state.copy();

        }

        

    }
}
