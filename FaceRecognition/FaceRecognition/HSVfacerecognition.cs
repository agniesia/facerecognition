using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    class HSVfaceRecognition:lab01biometria.Visitor
    {
        public HSVfaceRecognition() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            HSVrecognition(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }

        public void HSVbinary(lab01biometria.image_RGB rgb)
        {
            var canal = FaceBiomHelpMethod.copyImage(rgb);

            double[] hsv = new double[3];
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    hsv = FaceBiomHelpMethod.rgb2hsv(rgb.R[i][j], rgb.G[i][j], rgb.B[i][j]);
                    if ((hsv[1] < 0.69 && hsv[1] > 0.22) && (hsv[2] > 0.4 && hsv[0] < 50))
                    {
                        canal[i, j] = 255;

                    }
                    else
                    {
                        canal[i, j] = 0;

                    }
                }
            }
            FaceBiomHelpMethod.tab2int(canal, canal, canal, rgb);


        }
        private void HSVrecognition(lab01biometria.image_RGB rgb)
        {
            lab01biometria.image_as_tab o = rgb.copy();
            lab01biometria.image_RGB orginal = new lab01biometria.image_RGB(o.utab, o.w, o.h);
            HSVbinary(orginal);
            FaceBiomHelpMethod.MaskaElementsDelete(orginal);
            FaceBiomHelpMethod.FaceMask(rgb, orginal);
        }
    }
}
