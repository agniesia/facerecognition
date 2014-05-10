using lab01biometria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace FaceRecognition
{
    class FaceBiom : lab01biometria.Visitor
    {
        public FaceBiom() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            FaceMask(rgb);
        }

        public void Visit(image_Gray Grey)
        {
        }

        private int[,] copyImage(image_RGB rgb)
        {

            int[,] Temp = new int[rgb.w, rgb.h];
            return Temp;
        }

        private void FaceMask(image_RGB rgb){
            image_as_tab temp= rgb.copy();
            image_RGB mask = new image_RGB(temp.utab,temp.w,temp.h);
            ThreeMethodJoin(mask);
            var dil=new Dilatation();
            var ero = new Erosion();
    
            ero.rob(mask);
            ero.rob(mask);
            ero.rob(mask);
            dil.rob(mask);
            dil.rob(mask);
            dil.rob(mask);

            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    if (mask.R[i][j]==0)
                    {
                        rgb.G[i][j]=255;
                        rgb.B[i][j]=255;
                        rgb.B[i][j]=255;
                    }
                    

                }
            }


        }
        private void RGBmethod(image_RGB rgb)
        {

            var Rcanal = copyImage(rgb);
            var Bcanal = copyImage(rgb);
            var Gcanal = copyImage(rgb);
            int difGR = 0;
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    difGR = rgb.R[i][j] - rgb.G[i][j];
                    if (difGR < 0)
                    {
                        Rcanal[i,j] = 0;
                        Bcanal[i,j] = 0;
                        Gcanal[i,j] = 0;
                    }
                    else
                    {
                        Rcanal[i, j] = difGR;
                        Bcanal[i, j] = difGR;
                        Gcanal[i, j] = difGR;
                    }
                }
            
            }
            tab2int(Rcanal, Gcanal, Bcanal, rgb);
           

        }

        public void SVbinary(image_RGB rgb)
        {
            var Rcanal = copyImage(rgb);
            var Bcanal = copyImage(rgb);
            var Gcanal = copyImage(rgb);
            double[] hsv = new double[3];
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    hsv = rgb2hsv(rgb.R[i][j], rgb.G[i][j], rgb.B[i][j]);
                    if ((hsv[1] < 0.69 && hsv[1] > 0.22) && (hsv[2] > 0.4 ))
                    {
                        Rcanal[i, j] = 255;
                        Gcanal[i, j] = 255;
                        Bcanal[i, j] = 255;
                    }
                    else
                    {
                        Rcanal[i, j] = 0;
                        Gcanal[i, j] = 0;
                        Bcanal[i, j] = 0;
                    }
                }
            }
            tab2int(Rcanal, Gcanal, Bcanal, rgb);


        }
        public void HSVbinary(image_RGB rgb)
        {
            var Rcanal = copyImage(rgb);
            var Bcanal = copyImage(rgb);
            var Gcanal = copyImage(rgb);
            double[] hsv= new double[3];
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    hsv = rgb2hsv(rgb.R[i][j], rgb.G[i][j], rgb.B[i][j]);
                    if ((hsv[1] < 0.69 && hsv[1] > 0.22) && (hsv[2] > 0.4 && hsv[0] < 50))
                    {
                        Rcanal[i, j] = 255;
                        Gcanal[i, j] = 255;
                        Bcanal[i, j] = 255;
                    }
                    else
                    {
                        Rcanal[i, j] = 0;
                        Gcanal[i, j] = 0;
                        Bcanal[i, j] = 0;
                    }
                }
            }
            tab2int(Rcanal,Gcanal,Bcanal, rgb);
            

        }
        private void ThreeMethodJoin(image_RGB rgb){
            var Rcanal = copyImage(rgb);
            var Bcanal = copyImage(rgb);
            var Gcanal = copyImage(rgb);
            
            image_as_tab temp= rgb.copy();
            image_RGB rgbHSV = new image_RGB(temp.utab,temp.w,temp.h);
            image_RGB rgbSV = new image_RGB(temp.utab,temp.w,temp.h);
            HSVbinary(rgbHSV);
            SVbinary(rgbSV);
            RGBmethod(rgb);
            var bin = new lab01biometria.imageoperation.Otsu();
            bin.rob(rgb);
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    if (rgb.B[i][j] == rgbHSV.B[i][j] && rgb.B[i][j] == rgbSV.B[i][j])
                    {
                        Rcanal[i, j] = rgb.B[i][j];
                        Gcanal[i,j] = rgb.B[i][j];
                        Bcanal[i, j] = rgb.B[i][j];
                    }
                    else
                    {
                        Rcanal[i, j] = 0;
                        Gcanal[i, j] = 0;
                        Bcanal[i, j] = 0;
                    }
                        
                }
            }
            tab2int(Rcanal, Gcanal, Bcanal, rgb);

        }
        private void  tab2int(int[,] R,int[,] G,int[,] B, image_RGB rgb){
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
            private static double[] rgb2hsv(int r, int g, int b){
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
