using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.FaceRecognition
{
    class HistogramFaceElement
    {
            private void  Histogram(lab01biometria.image_RGB rgb){
                int[] histogram = new int[rgb.h];
                int suma = 0;
                for (int j = 0; j < rgb.w; j++)
                {
                    suma = 0;
                    for (int i = 0; i < rgb.w; i++)
                    {
                        suma += rgb.R[j][i]==0? 1:0;
                    }
                    histogram[j] = suma;
                }
            }
            
    }
}
