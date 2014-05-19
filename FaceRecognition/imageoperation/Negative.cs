using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Negative:Visitor
    {
        public void rob(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
           
           
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {

                   
                    rgb.B[i][j] =(byte)(255-rgb.B[i][j]);
                    rgb.G[i][j] = (byte)(255-rgb.G[i][j]);
                    rgb.R[i][j] = (byte)(255 - rgb.R[i][j]);
                    

                }
            }
        }
        public void Visit(image_Gray Grey)
        {
            
            for (int i = 0; i < Grey.w; i++)
            {
                for (int j = 0; j < Grey.h; j++)
                {


                    Grey.Greycanal[i][j] = (byte)(255 -Grey.Greycanal[i][j]);

       

                }
            }
        }
    }
}
