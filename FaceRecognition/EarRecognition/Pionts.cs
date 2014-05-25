using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarRecognition
{
    class Points : lab01biometria.Visitor
    {
        public Points() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            Extract(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }


        private void Extract(lab01biometria.image_RGB rgb)
        {
            lab01biometria.image_as_tab temp = rgb.copy();
            lab01biometria.image_RGB maska = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
            lab01biometria.image_RGB szkielet = new lab01biometria.image_RGB(temp.utab, temp.w, temp.h);
          
            ExtratctAll(szkielet, maska,rgb);
            //showskielett(szkielet, rgb);
           

        
            
        }
        private void srodek(List<Tuple<int, int>> punkty)
        {
            int sumax = 0;
            int sumay = 0;
            foreach (Tuple<int, int> wsp in punkty)
            {
                sumax += wsp.Item1;
                sumay += wsp.Item2;
            }
            var srx = sumax / punkty.Count;
            var sry = sumay / punkty.Count;
        }
       
        private void ExtratctAll(lab01biometria.image_RGB szkielet, lab01biometria.image_RGB rgb, lab01biometria.image_RGB orginal)
        {
            var contour = new Countur();
            contour.maska(rgb);
            contour.invert(rgb);
            contour.rob(szkielet);
            var prze = przekatna(rgb,orginal);
            float wspa = (prze.Item3 - prze.Item4) / (prze.Item1 - prze.Item2);
            float wspb = (prze.Item4*prze.Item1 - prze.Item3*prze.Item2) / (prze.Item1 - prze.Item2);
            int t = 0;

            for (int x = 0; x < orginal.w; x++)
            {
                t = (int)(wspa * x + wspb);

                if (t < rgb.h && t > 0)
                {
                    orginal.R[x][t] = (byte)255;
                    orginal.G[x][t] = (byte)255;
                    orginal.B[x][t] = (byte)0;


                    if (szkielet.R[x][t] == 0)
                    {
                        orginal.R[x][t] = (byte)0;
                        orginal.G[x][t] = (byte)0;
                        orginal.B[x][t] = (byte)255;
                        //wsp.Add(new Tuple<int, int>(x, t));
                    }
                     if (t>(int)((prze.Item3 - prze.Item4) / 2)-2&&(t<(int)((prze.Item3 - prze.Item4) / 2)+2) ) {
                   

                        orginal.R[x][t] = (byte)0;
                        orginal.G[x][t] = (byte)255;
                        orginal.B[x][t] = (byte)255;}
                
                
                }
            }
                 
            

           t = 0;

            List<Tuple<int, int>> wsp = new List<Tuple<int, int>>();

            var inkrement = Math.Abs(prze.Item3 - prze.Item4) / 10.0;
            for (int s = (int)inkrement; s < prze.Item3 - inkrement; s += (int)inkrement)
            {
                for (int x = 0; x < rgb.w; x++)
                {

                    t = (int)(((prze.Item2 - prze.Item1) / (prze.Item3 - prze.Item4)) * x + s);
                    if (t < rgb.h && t > 0)
                    {
                        orginal.R[x][t] = (byte)255;
                        orginal.G[x][t] = (byte)255;
                        orginal.B[x][t] = (byte)0;
                        if (szkielet.R[x][t] == 0)
                        {
                            orginal.R[x][t] = (byte)0;
                            orginal.G[x][t] = (byte)0;
                            orginal.B[x][t] = (byte)255;
                            wsp.Add(new Tuple<int, int>(x, t));
                        }
                    }
                }
                   
            }
        }
        private Tuple<float, float, float, float> przekatna(lab01biometria.image_RGB rgb, lab01biometria.image_RGB orginal)
        {
            var findeear = new FindEar();
            findeear.rob(rgb);
            //try
            //{
            //    orginal.R[findeear.punkt1.Item1][findeear.punkt1.Item2] = 0;
            //    orginal.G[findeear.punkt1.Item1][findeear.punkt1.Item2] = 0;
            //    orginal.B[findeear.punkt1.Item1][findeear.punkt1.Item2] = 255;
            //}
            //catch (Exception b) { }
            ////------------------
            //try
            //{
            //    orginal.R[findeear.punkt2.Item1][findeear.punkt2.Item2] = 0;
            //    orginal.G[findeear.punkt2.Item1][findeear.punkt2.Item2] = 0;
            //    orginal.B[findeear.punkt2.Item1][findeear.punkt2.Item2] = 255;
            //}
            //catch (Exception b) { }
            //try
            //{
            //    orginal.R[findeear.punkt3.Item1][findeear.punkt3.Item2] = 0;
            //    orginal.G[findeear.punkt3.Item1][findeear.punkt3.Item2] = 0;
            //    orginal.B[findeear.punkt3.Item1][findeear.punkt3.Item2] = 255;
            //}
            //catch (Exception b) { }
            //try
            //{
            //    orginal.R[findeear.punkt4.Item1][findeear.punkt4.Item2] = 0;
            //    orginal.G[findeear.punkt4.Item1][findeear.punkt4.Item2] = 0;
            //    orginal.B[findeear.punkt4.Item1][findeear.punkt4.Item2] = 255;
            //}
            //catch (Exception b) { }
            var x = findeear.punkt4.Item1 - findeear.punkt2.Item1;
           var y= findeear.punkt4.Item2-findeear.punkt2.Item2;
            var s= findeear.punkt1.Item1-findeear.punkt3.Item1;
            var t= findeear.punkt1.Item2 - findeear.punkt3.Item2;
            if((Math.Pow(x,x)+Math.Pow(y,y)>Math.Pow(s,s)+ Math.Pow(t,t)))
            {

                return new Tuple<float, float, float, float>((float)findeear.punkt4.Item1, (float)findeear.punkt2.Item1, (float)findeear.punkt4.Item2, (float)findeear.punkt2.Item2);
            }
            else
                return new Tuple<float, float, float, float>(findeear.punkt3.Item1, findeear.punkt1.Item1, findeear.punkt3.Item2, findeear.punkt1.Item2);

                
            ///return new Tuple<int,int,int,int>(findeear.punkt1.Item1)
        }
    }
}
