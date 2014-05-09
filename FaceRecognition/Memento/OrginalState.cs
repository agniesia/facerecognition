using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.Memento
{
     class Originator
    {



        public image_as_tab State { get; set; }

        
        public Memento  SaveMemento()
        {

            return (new Memento(State));

        }


        public void RestoreMemento(Memento memento)
        {

            State = memento.State.copy();

        }
    }
 
}
