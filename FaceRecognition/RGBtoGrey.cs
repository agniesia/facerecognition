using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class RGBtoGrey : Visitor
    {
        public image_Gray GreyElement;
        public void RGBtoGreyAll(image_as_tab  image)
        {
            image.Accept(this);
            
        }
        
        
        public void rob( image_as_tab image) {
            image.Accept(this);
            
           
        }
        public void Visit(image_RGB rgb)
        {
            
            GreyElement=rgb.greyimage();
            //byte z = 0;
            //byte[] temp = new byte[rgb.w * rgb.h * 4];
            //for (int i = 0; i < rgb.w; i++)
            //{
            //    for (int j = 0; j < rgb.h; j++)
            //    {
            //        z = (byte)((rgb.B[i][j] + rgb.G[i][j] + rgb.R[i][j]) / 3);
            //        temp[4 * (j * rgb.w + i)] = z;
            //        temp[4 * (j * rgb.w + i) + 1] = z;
            //        temp[4 * (j * rgb.w + i) + 2] = z;
            //        temp[4 * (j * rgb.w + i) + 3] = rgb.alfa[i][j];
                    
            //    }

           // }
            //GreyElement= new image_Gray(temp, rgb.h, rgb.w);

        }
        public void Visit(image_Gray Grey)
        {
            // uwaga nie było new oze sie wysypac
            GreyElement = Grey;
        }
    }
}
