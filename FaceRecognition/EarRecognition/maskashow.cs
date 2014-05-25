using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarRecognition
{
    class maskashow:lab01biometria.Visitor
    {
   
        public maskashow() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            maska(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }

        public void maska(lab01biometria.image_RGB mask)
        {
            var skindetector = new FaceRecognition.JoinRecogntion();
            var erozja = new FaceRecognition.Erosion();
            var dylatacja = new FaceRecognition.Dilatation();
            skindetector.ThreeMethodJoin(mask);
            var fill = new lab01biometria.Binaryoperation.FillSmallElements();


            for (int i = 0; i < 9; i++)
            {
                erozja.rob(mask);

            }
            for (int i = 0; i < 6; i++)
            {
                dylatacja.rob(mask);

            }
            for (int i = 0; i < 5; i++)
            {
                erozja.rob(mask);

            }

            fill.rob(mask);

        }
    }
}
