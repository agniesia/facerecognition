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
            var canal = copyImage(rgb);
            

            byte z = 0;
            
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    z = (byte)((rgb.B[i][j] + rgb.G[i][j] + rgb.R[i][j]) / 3);
                    canal[i, j] = z;
                   

                }

            }
            tab2int(canal, canal, canal, rgb);

        }
        public void Visit(image_Gray Grey)
        {
            // uwaga nie było new oze sie wysypac
            GreyElement = Grey;
        }
        private int[,] copyImage(image_RGB rgb)
        {

            int[,] Temp = new int[rgb.w, rgb.h];
            return Temp;
        }
        private void tab2int(int[,] R, int[,] G, int[,] B, image_RGB rgb)
        {
            for (int c = 0; c < rgb.w; c++)
            {
                for (int p = 0; p < rgb.h; p++)
                {
                    rgb.R[c][p] = (byte)R[c, p];
                    rgb.G[c][p] = (byte)B[c, p];
                    rgb.B[c][p] = (byte)G[c, p];
                }


            }

        }
    }
}
