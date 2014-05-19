using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    class RGBfaceRecognition:lab01biometria.Visitor
    {
        public RGBfaceRecognition() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            RGBrecognition(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }
        public void RGBmethod(lab01biometria.image_RGB rgb)
        {

            var canal = FaceBiomHelpMethod.copyImage(rgb);

            int difGR = 0;
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    difGR = rgb.R[i][j] - rgb.G[i][j];
                    if (difGR < 0)
                    {
                        canal[i, j] = 0;

                    }
                    else
                    {
                        canal[i, j] = difGR;

                    }
                }

            }
            FaceBiomHelpMethod.tab2int(canal, canal, canal, rgb);


        }
        private void RGBrecognition(lab01biometria.image_RGB rgb)
        {
            lab01biometria.image_as_tab o = rgb.copy();
            lab01biometria.image_RGB orginal = new lab01biometria.image_RGB(o.utab, o.w, o.h);
            RGBmethod(orginal);
            var otsumethod = new lab01biometria.imageoperation.Otsu();
            otsumethod.rob(orginal);
            FaceBiomHelpMethod.MaskaElementsDelete(orginal);
            FaceBiomHelpMethod.FaceMask(rgb,orginal);


        }
    }
}
