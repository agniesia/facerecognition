using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    class JoinRecogntion:lab01biometria.Visitor
    {
        public JoinRecogntion() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            joinRecognition(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }
        public void ThreeMethodJoin(lab01biometria.image_RGB rgb)
        {
            var canal = FaceBiomHelpMethod.copyImage(rgb);


            lab01biometria.image_as_tab temp = rgb.copy();
            lab01biometria.image_RGB rgbHSV = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
            lab01biometria.image_RGB rgbSV = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
            HSVfaceRecognition HsvMethod = new HSVfaceRecognition();
            SVfaceRecognition SVmethod = new SVfaceRecognition();
            RGBfaceRecognition RGBmethod = new RGBfaceRecognition();
            HsvMethod.HSVbinary(rgbHSV);
            SVmethod.SVbinary(rgbSV);
            RGBmethod.RGBmethod(rgb);
            var bin = new lab01biometria.imageoperation.Otsu();
            bin.rob(rgb);
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    if (rgb.B[i][j] == rgbHSV.B[i][j] && rgb.B[i][j] == rgbSV.B[i][j])
                    {
                        canal[i, j] = rgb.B[i][j];

                    }
                    else
                    {
                        canal[i, j] = 0;

                    }

                }
            }
            FaceBiomHelpMethod.tab2int(canal, canal, canal, rgb);

        }
        private void  joinRecognition( lab01biometria.image_RGB rgb){
            lab01biometria.image_as_tab o = rgb.copy();
            lab01biometria.image_RGB orginal = new lab01biometria.image_RGB(o.utab, o.w, o.h);
            ThreeMethodJoin (orginal);
            
           
            FaceBiomHelpMethod.MaskaElementsDelete(orginal);
            FaceBiomHelpMethod.FaceMask(rgb,orginal);
        }
    }
}
