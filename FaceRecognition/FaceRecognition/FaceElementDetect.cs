using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    class FaceElementDetect:lab01biometria.Visitor
    {
        public FaceElementDetect() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            FaceElement(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }

        private void  FaceElement(lab01biometria.image_RGB rgb){

            lab01biometria.image_as_tab temp = rgb.copy();
            lab01biometria.image_RGB mask = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
            JoinRecogntion join = new JoinRecogntion();
            join.ThreeMethodJoin(mask);
            FaceBiomHelpMethod.MaskaElementsDelete(mask);
           
            imageprocesing(rgb);
            
            var dilatationMaskEfect= new Dilatation();

            for (int i = 0; i < 10; i++)
            {
                dilatationMaskEfect.rob(mask);
            }
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    if (mask.R[i][j] == 0)
                    {
                        rgb.G[i][j] = 255;
                        rgb.B[i][j] = 255;
                        rgb.R[i][j] = 255;
                    }


                }
            }

        }

        private void  imageprocesing(lab01biometria.image_RGB rgb){
            var greyEffect=new lab01biometria.imageoperation.RGBtoGrey();
            var sobelEffect= new lab01biometria.imageoperation.Sobel();
            var negativEffect= new lab01biometria.imageoperation.Negative();
            var otsuEffect= new lab01biometria.imageoperation.Otsu();
            greyEffect.rob(rgb);
            sobelEffect.rob(rgb);
            negativEffect.rob(rgb);
            otsuEffect.rob(rgb);
        }
    }
}
