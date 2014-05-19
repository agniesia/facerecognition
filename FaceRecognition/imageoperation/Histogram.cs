using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Histogram:Visitor

        //porpawka
        ///byte[] histobraz = new byte[184320];


            //for(int i=0;i<180;i++){

            //    for (int e = 0; e < 256; e++)
            //    {
            //        if (i < 180 - Hist[e]){
            //            histobraz[4*(i*256+e)] = 60;
            //            histobraz[4*(i*256+e)+1] = 20;
            //            histobraz[4*(i*256+e)+2] = 15;
            //            histobraz[4*(i*256+e)+3] = 255; }


            //        else{
            //            histobraz[4*(i*256+e)] = 20;
            //            histobraz[4*(i*256+e)+1] = 100;
            //            histobraz[4*(i*256+e)+2] = 255;
            //            histobraz[4*(i*256+e)+3] = 255;    
            //    }}


            //}
            //return histobraz;

    {
        public image_RGB HistogramObject;
        public void HistogramAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            HistogramAll(image);
        }
        public void Visit(image_RGB rgb)
        {
            int[] Hist = new int[256];
            int[] HistR = new int[256];
            int[] HistG = new int[256];
            int[] HistB = new int[256];
            for (int k = 0; k < 256; k++)
            {
                HistR[k] = rgb.R.SelectMany(x => x).ToArray().Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
                HistG[k] = rgb.G.SelectMany(x => x).ToArray().Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
                HistB[k] = rgb.B.SelectMany(x => x).ToArray().Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
            }
            Hist = HistR.Select((x, i) => x = (x + HistG[i] + HistB[i]) / 3).ToArray();
            int max = Hist.Max();
            for (int i = 0; i < 256; i++)
            {
                Hist[i] = 180 * Hist[i] / max;
            }

            byte[][] histobraz = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                histobraz[i] = new byte[180];

                for (int j = 0; j < 180; j++)
                {
                    if (j < (180 - Hist[i]))
                        histobraz[i][j] = 0;
                    else
                        histobraz[i][j] = 100;
                }
            }
            byte[] temp = new byte[256 * 180 * 4];

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 180; j++)
                {

                    temp[4 * (j * 256 + i)] = histobraz[i][j];
                    temp[4 * (j * 256 + i) + 1] = 50;
                    temp[4 * (j * 256 + i) + 2] = 20;
                    temp[4 * (j * 256 + i) + 3] = 255;


                }
            }

            HistogramObject= new image_RGB(temp, 256, 180);
        }
        public void Visit(image_Gray Grey)
        {
            int[] Hist = new int[256];
            for (int k = 0; k < 256; k++)
            {
                Hist[k] = Grey.utab.Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
            }

            int max = Hist.Max();
            for (int i = 0; i < 256; i++)
            {
                Hist[i] = 180 * Hist[i] / max;
            }

            byte[][] histobraz = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                histobraz[i] = new byte[180];

                for (int j = 0; j < 180; j++)
                {
                    if (j < (180 - Hist[i]))
                        histobraz[i][j] = 0;
                    else
                        histobraz[i][j] = 100;
                }
            }
            byte[] temp = new byte[256 * 180 * 4];

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 180; j++)
                {

                    temp[4 * (j * 256 + i)] = histobraz[i][j];
                    temp[4 * (j * 256 + i) + 1] = 50;
                    temp[4 * (j * 256 + i) + 2] =20;
                    temp[4 * (j * 256 + i) + 3] = 255;


                }
            }

            HistogramObject= new image_RGB(temp, 256, 180);
        

        }
    }
}
