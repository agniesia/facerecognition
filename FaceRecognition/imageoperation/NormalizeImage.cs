using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{

    
    class NormalizeImage: Visitor
    {
        public NormalizeImage() { }
        public void NormalizeAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            NormalizeAll(image);
        }
        public void Visit(image_RGB rgb) {
            var maxR = rgb.R.SelectMany(x => x).ToArray().Max();
            var maxB = rgb.B.SelectMany(x => x).ToArray().Max();
            var maxG = rgb.G.SelectMany(x => x).ToArray().Max();
            //min
            var minG = rgb.G.SelectMany(x => x).ToArray().Min();
            var minB = rgb.B.SelectMany(x => x).ToArray().Min();
            var minR = rgb.R.SelectMany(x => x).ToArray().Min();
            var normalizeConstR = maxR - minR;
            var normalizeConstG = maxR - minG;
            var normalizeConstB = maxR - minB;


            ///pixel[x,y]=255*(pixel[x,y]-minPix)/(maxPix-minPix) 

            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    rgb.R[i][j] = (byte)(255 * (rgb.R[i][j] - minR) / normalizeConstR);
                    rgb.G[i][j] = (byte)(255 * (rgb.G[i][j] - minG) / normalizeConstG);
                    rgb.B[i][j] = (byte)(255 * (rgb.B[i][j] - minB) / normalizeConstB);
                }
            }





        }
        public void Visit(image_Gray Grey){

            //max
            var max = Grey.Greycanal.SelectMany(x => x).ToArray().Max();

            //min
            var min = Grey.Greycanal.SelectMany(x => x).ToArray().Min();

            var normalizeConst = max - min;



            ///pixel[x,y]=255*(pixel[x,y]-minPix)/(maxPix-minPix) 
            //[] utab1=  utab.Select((x,i )=> i%4!=0 ).ToArray().Select(x => 255 * (utab[ - min) / normalizeConst).ToArray();
            for (int i = 0; i < Grey.w; i++)
            {
                for (int j = 0; j < Grey.h; j++)
                {
                    Grey.Greycanal[i][j] = (byte)(255 * (Grey.Greycanal[i][j] - min) / normalizeConst);
                    
                }
            }
        }
    }
}
