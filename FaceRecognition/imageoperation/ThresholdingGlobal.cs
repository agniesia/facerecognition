using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class ThresholdingGlobal:Visitor
    {
        public void ThresholdingGlobalAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            image_Gray Grey = new image_Gray();
            RGBtoGrey v = new RGBtoGrey();
            v.rob(rgb);
            Grey = v.GreyElement;
            Visit(Grey);
            rgb.R = Grey.Greycanal;
            rgb.G = Grey.Greycanal;
            rgb.B = Grey.Greycanal;

            
        }
        public void rob(image_as_tab image) {
            ThresholdingGlobalAll(image);

        }

        public void Visit(image_Gray Grey)
        {
            byte one = 255;
            byte zero = 0;
            var Suma = 0;
            for (int j = 0; j < Grey.h; j++)
            {
                for (int i = 0; i < Grey.w; i++)
                {
                    Suma += Grey.Greycanal[i][j];
                }
            }
            var mean = Suma / (Grey.h * Grey.w);
            for (int j = 0; j < Grey.h; j++)
            {
                for (int i = 0; i < Grey.w; i++)
                {
                    Grey.Greycanal[i][j] = Grey.Greycanal[i][j] >= mean ? one : zero;

                }
            }

            

        }
    }
}
