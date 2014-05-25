using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class BinaryLocalMean:Visitor
    {
        int local;
        public BinaryLocalMean(int local)
        {
            this.local = local;
        }
        public void rob(image_as_tab image) {
            BinaryLocalMeanAll(image);
        }
        public void BinaryLocalMeanAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            // do poprawy
            image_Gray Grey = new image_Gray();
            RGBtoGrey v = new RGBtoGrey();
            v.RGBtoGreyAll(rgb);

            Grey = v.GreyElement;
            Visit(Grey);
            
        }
        public void Visit(image_Gray Grey)
        {
            int range = (int)local / 2;
            byte[,] Temp = new byte[Grey.w, Grey.h];
            Temp=binarylocla(Grey.Greycanal, Grey.w, Grey.h);
            for (int x = 0; x < Grey.w; x++)
            {

                for (int y = 0; y < Grey.h; y++)
                {
                    Grey.Greycanal[x][y] = Temp[x+range, y+range];
                }
            }

        }
        private byte[,] binarylocla(byte[][] canal, int w, int h)
        {   
            int range = (int)local / 2;
            byte zero = 0, one = 255;
            byte[,] Temp = new byte[w+2*range,h+2*range];
           
            int[,] canalnew=new int[w+2*range,h+2*range];
            for (int x = 0; x < w+2*range; x++)
            {
              
                for (int y = 0; y < h+2*range ; y++)
                {
                    if ((y >= range && x >= range)&& ((y<h+range)&&(x<w+range)))
                        canalnew[x, y] = canal[x - range][y - range];
                    
                }
            }
            for (int x = range; x < w+range; x++)
            {
               
                for (int y = range; y < h+range; y++)
                {
                    var Suma = 0;
                    for (int i = 0; i < local; i++)
                    {
                        for (int j = 0; j < local; j++)
                        {
                            Suma += canalnew[x + i -range ,y + j - range];

                        }

                    }
                    var localmean = Suma / Math.Pow(local, 2);
                    Temp[x,y] = canalnew[x,y] >= localmean ? one : zero;
                }
            }
            return Temp; 
        }
        
    }
}
