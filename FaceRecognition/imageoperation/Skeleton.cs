using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace lab01biometria.Binaryoperation
{
    class Skeleton:Visitor
    {
        private static int[][] A = new int[][] {
         new int []   {3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135,143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254},
         new int []   {7, 14, 28, 56, 112, 131, 193, 224},
         new int []   {7, 14, 15, 28, 30, 56, 60, 112, 120, 131, 135, 193, 195, 224, 225, 240},
         new int []   {7, 14, 15, 28, 30, 31, 56, 60, 62, 112, 120, 124, 131, 135, 143, 193, 195, 199, 224, 225, 227, 240, 241, 248},
         new int []   {7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 193, 195, 199, 207, 224, 225, 227, 231, 240, 241, 243, 248, 249, 252},
         new int []   {7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 191, 193, 195, 199, 207, 224, 225, 227, 231, 239, 240, 241, 243, 248, 249, 251, 252, 254}};

        private static int[] A1pix = { 3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254 };
        private static int[][] maska = { new int[] { 128, 64, 32 }, new int[] { 1, 0, 16 }, new int[] { 2, 4, 8 } };
        public void SkeletonAll(image_as_tab binary)
        {
            binary.Accept(this);
        }
        public void rob(image_as_tab binary)
        {
            SkeletonAll(binary);
        }
        public void Visit(image_Gray g ) {
            
        }
        public void  Visit(image_RGB Grey){
            byte black = 0;
            byte white = 255;
            byte t;
            for (int y = 0; y < Grey.h; y++)
            {
                for (int x = 0; x < Grey.w; x++)
                {
                    t = (byte)Grey.R[x][y] > 0 ? black : white;
                    Grey.R[x][y] = t;
                    Grey.G[x][y] = t;
                    Grey.B[x][y] = t;

                }
            }
            Skeletonize(Grey);
           
            for (int y = 0; y < Grey.h; y++)
            {
                for (int x = 0; x < Grey.w; x++)
                {
                    t = (byte)Grey.R[x][y] > 0 ? black : white;
                    Grey.R[x][y] = t;
                    Grey.G[x][y] = t;
                    Grey.B[x][y] = t;

                }
            }
        }
        
        private void wagaforbinary(ref int[,] binary,int w, int h)
        {
            for (int x = 3; x < w+3; x++)
            {
                for (int y = 3; y < h + 3; y++)
                {
                    
                    if (binary[x,y] == 1)
                    {
                        var Suma = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {

                                Suma += binary[x + i - 1,y + j - 1] >= 1 ? maska[i][j] : 0;
                            }
                        }
                        binary[x,y] = Suma;
                    }
                }
            }
        }
            
            private void delete(ref int[,] binary,int[] maskax,int [][]maska,int w, int h){
                var Suma = 0;
                for (int x = 3; x < w+3; x++)
                {
                    
                    for (int y = 3; y < h+3; y++)
                    {
                        if (binary[x, y] > 1)
                        {
                            Suma = 0;
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {

                                    Suma += binary[x + i - 1, y + j - 1] >= 1 ? maska[i][j] : 0;
                                }
                            }
                            

                            if (maskax.Contains(Suma))
                            {
                                binary[x, y] = 0;
                                flage = true;
                            }
                        }
                    }
                }
            }
            private void border(ref int[,] binary, int[] maskax,int[][] maska,int w, int h)
            {
                var Suma=0;
                for (int x = 3; x< w+3; x++)
                {
                    for (int y = 3; y < h+3; y++)
                    {
                        if (binary[x, y] == 1)
                        {
                            Suma = 0;
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {

                                    Suma += binary[x + i - 1, y + j - 1] >= 1 ? maska[i][j] : 0;
                                }
                            }


                            if (maskax.Contains(Suma))
                            {
                                binary[x, y] = 2;
                            }
                        }
                    }
                }
            }
            private void changetoone(ref int[,] binary,int w ,int h)
            {
                for (int y = 3; y < h+3; y++)
                {
                    for (int x = 3; x <w+3; x++)
                    {
                        if (binary[x,y]>0)
                            binary[x,y] = 1;
                    }
                }
            }
            static bool flage;
            private  void Skeletonize( image_RGB binary)
            {
                int[,] tempbinary = new int[binary.w + 6, binary.h + 6];
                for (int y = 0; y < binary.h; y++)
                {
                    for (int x = 0; x < binary.w; x++)
                    {
                        if (binary.R[x][y] > 0)
                            tempbinary[x + 3, y + 3] = 0;
                        else
                            tempbinary[x + 3, y + 3] = 1;

                    }
                }
                flage = true;
                while (flage == true)
                {

                    
                    
                   
                    border(ref tempbinary, A[0],maska, binary.w, binary.h);
                    flage = false;
                    for (int r = 1; r < 6; r++)
                    {
                        
                        delete(ref tempbinary, A[r],maska, binary.w, binary.h);

                    }

                    changetoone(ref tempbinary, binary.w, binary.h);

                }
               wagaforbinary(ref tempbinary, binary.w, binary.h);
               delete(ref tempbinary, A1pix,maska, binary.w, binary.h);



                for (int y = 0; y < binary.h; y++)
                {
                    for (int x = 0; x < binary.w; x++)
                    {
                        if (tempbinary[x + 3, y + 3] > 0)
                            binary.R[x][y] = 255;
                        else
                            binary.R[x][y] = 0;

                    }
                }
            }

            
    }
}


