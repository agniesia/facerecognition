using lab01biometria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RGBmethod(rgb);
        }

        public void Visit(image_Gray Grey)
        {
        }

        private int[,] copyImage(image_RGB rgb)
        {

            int[,] Temp = new int[rgb.w, rgb.h];
            return Temp;
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
            for (int c = 0; c < rgb.w; c++)
            {
                for (int p = 0; p < rgb.h; p++)
                {
                    rgb.R[c][p] = (byte)Rcanal[c, p];
                    rgb.G[c][p] = (byte)Bcanal[c, p];
                    rgb.B[c][p] = (byte)Gcanal[c, p];
                }


            }
           

        }
    }
}
