using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarRecognition
{
    class ShowContour:lab01biometria.Visitor
    {
        public ShowContour() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            showskielett(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }
        public void showskielett(lab01biometria.image_RGB rgb)
        {

            lab01biometria.image_as_tab temp = rgb.copy();
            lab01biometria.image_RGB szkielet = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
            var contour = new Countur();

            contour.rob(szkielet);
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    if (szkielet.R[i][j] == 0)
                    {
                        rgb.R[i][j] = 255;
                        rgb.G[i][j] = 255;
                        rgb.B[i][j] = 255;
                    }

                }
            }
        }

    }
}
