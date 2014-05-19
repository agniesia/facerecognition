using lab01biometria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace FaceRecognition
{
    class FaceBiomHelpMethod 
    {
       
        
        public static int[,] copyImage(image_RGB rgb)
        {

            int[,] Temp = new int[rgb.w, rgb.h];
            return Temp;
        }

        public static void   MaskaElementsDelete(image_RGB mask){
                 
            var dil=new Dilatation();
            var ero = new Erosion();
            ero.rob(mask);

            ero.rob(mask);
            ero.rob(mask);
            dil.rob(mask);
            dil.rob(mask);
            dil.rob(mask);
            var Seg = new lab01biometria.Binaryoperation.Segmentation();
            Seg.rob(mask);
            
        }
        public static void FaceMask(image_RGB rgb, image_RGB mask )
        {
             

            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    if (mask.R[i][j]==0)
                    {
                        rgb.G[i][j]=255;
                        rgb.B[i][j]=255;
                        rgb.R[i][j]=255;
                    }
                    

                }
            }


        }
        
        public static void  tab2int(int[,] R,int[,] G,int[,] B, image_RGB rgb){
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
            public static double[] rgb2hsv(int r, int g, int b){
                double hue, sat, val;
                double min, f, i;
                double[] result = new double[3];
                double red, grn, blu;
                red = r/ 255.0; grn = g / 255.0; blu = b / 255.0;
                min= Math.Min(Math.Min(red, grn), blu);
                val = Math.Max(Math.Max(red, grn), blu);
                if (min == val){
                    hue = 0;
                    sat = 0;
                    val = min;
                }
                else {
                    f = (red == min) ? grn-blu : ((blu == min) ? red-grn:blu-red );
                    i = (red == min) ? 3 : ((blu == min) ? 1 : 5);
                    hue = ((i-f/(val-min))*60);
                    sat = ((val-min)/val);
                    }
                    result[0] = hue;
                    result[1] = sat;
                    result[2] = val;
                    return result;
                
            }
            
    }
}
