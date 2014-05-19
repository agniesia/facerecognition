using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class Binary:image_Gray
    {
        public int[][] BinaryCanal;
        
        public Binary():base() { }
        public Binary(byte[][] orginal_tab, int w, int h):base()
            
        {
            BinaryCanal = new int[w][];
            base.w = w;
            base.h = h;
            for (int i = 0; i < w; i++)
            {
                BinaryCanal[i] = new int[h];
                for (int j = 0; j < h; j++)
                {
                  
                    BinaryCanal[i][j] = orginal_tab[i][j];
                }
            }
        }
        
        
    }
}
