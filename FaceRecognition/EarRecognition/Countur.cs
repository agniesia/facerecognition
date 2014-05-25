using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarRecognition
{
    class Countur : lab01biometria.Visitor
    {
        public Countur() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            findeountour(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }

        private void findeountour(lab01biometria.image_RGB rgb)
        {
            lab01biometria.image_as_tab temp = rgb.copy();
            lab01biometria.image_RGB mask = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
            var skindetector = new FaceRecognition.JoinRecogntion();
            var otsu = new lab01biometria.imageoperation.Otsu();
            var erozja = new FaceRecognition.Erosion();
            var dylatacja = new FaceRecognition.Dilatation();
            var normalizaja = new lab01biometria.imageoperation.NormalizeImage();
            //normalizaja.rob(rgb);

            maska(mask);
           // var sobel = new lab01biometria.imageoperation.Sobel();
            rgb.greyimage();
            contrast(rgb, 100);
            Cannybyme(rgb);
            FaceRecognition.FaceBiomHelpMethod.FaceMask(rgb, mask);

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
        public void invert(lab01biometria.image_RGB rgb)
        {
            byte black = 0;
            byte white = 255;
            byte t;
            for (int y = 0; y < rgb.h; y++)
            {
                for (int x = 0; x < rgb.w; x++)
                {
                    t = rgb.R[x][y] != 0 ? black : white;
                    rgb.R[x][y] = t;
                    rgb.G[x][y] = t;
                    rgb.B[x][y] = t;

                }
            }
        }
        private void contrast(lab01biometria.image_RGB rgbkopia, double kontrast)
        {
            lab01biometria.image_as_tab temp = rgbkopia.copy();
            lab01biometria.image_RGB rgb = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
            double x = kontrast;
            double t1;
            if (x > 0)
            {
                t1 = 255 / (255 - 2 * x);
                for (int i = 0; i < rgb.w; i++)
                {
                    for (int j = 0; j < rgb.h; j++)
                    {
                        {
                            rgbkopia.R[i][j] = (byte)((rgb.R[i][j] < x) ? 0 : (rgb.R[i][j] > 255 - x) ? 255 : t1 * (rgb.R[i][j] - x));
                            rgbkopia.G[i][j] = (byte)((rgb.G[i][j] < x) ? 0 : (rgb.G[i][j] > 255 - x) ? 255 : t1 * (rgb.G[i][j] - x));
                            rgbkopia.B[i][j] = (byte)((rgb.B[i][j] < x) ? 0 : (rgb.B[i][j] > 255 - x) ? 255 : t1 * (rgb.B[i][j] - x));
                        }
                    }
                }
            }
            else
            {
                double t2 = (255 + 2 * x) / 255;
                for (int i = 0; i < rgb.w; i++)
                {
                    for (int j = 0; j < rgb.h; j++)
                    {
                        rgbkopia.R[i][j] = (byte)(t2 * rgb.R[i][j] - x);
                        rgbkopia.G[i][j] = (byte)(t2 * rgb.G[i][j] - x);
                        rgbkopia.B[i][j] = (byte)(t2 * rgb.B[i][j] - x);
                    }
                }
            }
        }
        private void Cannybyme(lab01biometria.image_RGB rgb)
        {
            int[,] t = new int[rgb.w, rgb.h];
            rgb.grey();
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    t[i, j] = rgb.R[i][j];

                }
            }

            var canny = new CannyEdgeDetectionCSharp.Canny(t, rgb.w, rgb.h, 70F, 4F, 9, 2.3F);
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    rgb.R[i][j] = (byte)canny.GNH[i, j];
                    rgb.G[i][j] = (byte)canny.GNH[i, j];
                    rgb.B[i][j] = (byte)canny.GNH[i, j];

                }
            }
            for (int i = 0; i < 3; i++)
            {
                // erozja.rob(rgb);
                // dylatacja.rob(rgb);

            }
            //erozja.rob(rgb);
            var skeleton = new lab01biometria.Binaryoperation.Skeleton();
            skeleton.rob(rgb);
            var segment = new lab01biometria.Binaryoperation.Segmentation();
            segment.rob(rgb);
        }
      
    }
}
