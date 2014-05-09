using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Sobel : Visitor
    {
        public int[,] ElemMaski = new int[,]{
            { -1, -2,-1},
            { 0, 0, 0 },
            { 1, 2, 1 }
            };
        public int[,] ElemMaski1 = new int[,]{
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 } ,
            };
        int Norma = 1;
        byte Rozmiar = 3;
        public void SobelAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            SobelAll(image);
        }
        public void Visit(image_RGB rgb)
        {
            int[,] TempR = new int[rgb.w, rgb.h];
            int[,] TempG = new int[rgb.w, rgb.h];
            int[,] TempB = new int[rgb.w, rgb.h];

            for (int x = 1; x < rgb.w - 1; x++)
            {
                for (int y = 1; y < rgb.h - 1; y++)
                {
                    int SumaR = 0;
                    int SumaB = 0;
                    int SumaG = 0;

                    int SumaR1 = 0;
                    int SumaB1 = 0;
                    int SumaG1 = 0;

                    for (int i = 0; i < Rozmiar; i++)
                    {
                        for (int j = 0; j < Rozmiar; j++)
                        {
                            SumaR += rgb.R[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaB += rgb.G[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaG += rgb.G[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaR1 += rgb.R[x + i - 1][y + j - 1] * ElemMaski1[i, j];
                            SumaB1 += rgb.G[x + i - 1][y + j - 1] * ElemMaski1[i, j];
                            SumaG1 += rgb.G[x + i - 1][y + j - 1] * ElemMaski1[i, j];

                        }
                    }
                    double[] temp1 = new double[] { SumaR, SumaG, SumaB };
                    double[] temp2 = new double[] { SumaR1, SumaG1, SumaB1 };


                    double tR = (Math.Sqrt(Math.Pow(temp1[0], 2) + Math.Pow(temp2[0], 2)));
                    double tG = (Math.Sqrt((temp1[1]) * (temp1[1]) + (temp2[1]) * (temp2[1])));
                    double tB = (Math.Sqrt((temp1[2]) * (temp1[2]) + (temp2[2]) * (temp2[2])));
                    if (tR > 255)
                        tR = 255;
                    if (tG > 255)
                        tG = 255;
                    if (tB > 255)
                        tB = 255;

                    if (tR < 0)
                        tR = 0;
                    if (tG < 0)
                        tG = 0;
                    if (tB < 0)
                        tB = 0;

                    TempR[x, y] = (byte)tR;
                    TempG[x, y] = (byte)tG;
                    TempB[x, y] = (byte)tB;
                }
            }
            for (int c = 0; c < rgb.w; c++)
            {
                for (int p = 0; p < rgb.h; p++)
                {
                    rgb.R[c][p] = (byte)TempR[c, p];
                    rgb.G[c][p] = (byte)TempG[c, p];
                    rgb.B[c][p] = (byte)TempB[c, p];
                }


            }
        }
        public void Visit(image_Gray Grey) {
            int[,] Temp = new int[Grey.w, Grey.h];
            
            for (int x = 1; x < Grey.w - 1; x++)
            {
                for (int y = 1; y < Grey.h - 1; y++)
                {
                    int Suma = 0;
                   

                    int Suma1 = 0;
                    

                    for (int i = 0; i < Rozmiar; i++)
                    {
                        for (int j = 0; j < Rozmiar; j++)
                        {
                            Suma += Grey.Greycanal[x + i - 1][y + j - 1] * ElemMaski[i, j];
                           
                            Suma1 += Grey.Greycanal[x + i - 1][y + j - 1] * ElemMaski1[i, j];
                          

                        }
                    }
                    


                    double t = (Math.Sqrt(Math.Pow(Suma, 2) + Math.Pow(Suma1, 2)));
                   
                    if (t> 255)
                        t = 255;
                    
                    if (t< 0)
                        t = 0;
                   
                    Temp[x, y] = (byte)t;
                   
                }
            }
            for (int c = 0; c < Grey.w; c++)
            {
                for (int p = 0; p < Grey.h; p++)
                {
                    Grey.Greycanal[c][p] = (byte)Temp[c, p];
                    
                }


            }
        }



    }
}
