using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.Binaryoperation
{
    
    class Segmentation:Visitor
    {
        public void SegemtationAll(image_as_tab binary )
        {
            binary.Accept(this);

        }
        public void rob(image_as_tab image)
        {
            SegemtationAll(image);
        }
        public void Visit(image_RGB rgb)
        {
            Binary binary = new Binary(rgb.B, rgb.w, rgb.h);
            
            var wspolrzedne=segment(binary);
            wspolrzedne.RemoveAt(0);
            foreach (Tuple<int, int, int, int> wsp in wspolrzedne)
            {
                for (int i = wsp.Item1-3; i < wsp.Item3-3; i++)
                {
                    for (int j = wsp.Item2-3; j < wsp.Item4-3; j++)
                    {
                        binary.BinaryCanal[i][j]=0;
                    }

                }
            }
            

            byte white = 255;
            byte temp = 0;
            for (int x = 0; x < binary.w; x++)
            {
                for (int y = 0; y < binary.h; y++)
                {
                    
                    temp= binary.BinaryCanal[x][y] == 0 ? white:(byte)0;
                    rgb.B[x][y] = temp;
                    rgb.G[x][y] = temp;
                    rgb.R[x][y] = temp;
                
                //if (x > binary.w - 6)
                //{
                //    rgb.B[x][y] = 0;
                //    rgb.G[x][y] = 0;
                //    rgb.R[x][y] = 0;
                //}
                //}
                }
            }
        }
        public void Visit(image_Gray grey) { }
        private List<Tuple<int, int, int, int>> segment(Binary binary)
        {
            List<Tuple<int, int>> bufor = new List<Tuple<int, int>>();
            List<Tuple<int, int, int, int>> wspolrzedne = new List<Tuple<int, int, int, int>>();
            
            int[,] canalnew = new int[binary.w + 6, binary.h + 6];
            for (int y = 0; y < binary.h+3; y++)
            {
                for (int x = 0; x < binary.w+3; x++)
                {
                    if ((y >= 3 && x >= 3) && (y < binary.h+3  && x < binary.w+3 ))
                        canalnew[x, y] = binary.BinaryCanal[x-3][y-3] < 1 ? 1 : 0;
                    
                }
            }
            
           

            
            
            int wsp_minx = binary.w;
            int wsp_miny = binary.h;
            int wsp_maxx = 0;
            int wsp_maxy = 0;
            byte kolor = 100;
            for (int y = 3; y < binary.h; y++)
            {
                for (int x = 3; x < binary.w; x++)
                {
                    if (canalnew[x,y] == 1)
                    {
                        bufor.Add(new Tuple<int, int>(x, y));
                        canalnew[x, y] = kolor;
                        while (bufor.Count != 0)
                        {


                            foreach (Tuple<int, int> element in bufor.ToList())
                            {
                                canalnew[element.Item1,element.Item2] = kolor;
                                wsp_minx = element.Item1 < wsp_minx ? element.Item1 : wsp_minx;
                                wsp_maxx = element.Item1 > wsp_maxx ? element.Item1 : wsp_maxx;

                                wsp_miny = element.Item2 < wsp_miny ? element.Item2 : wsp_miny;
                                wsp_maxy = element.Item2 > wsp_maxy ? element.Item2 : wsp_maxy;

                                

                                int p;
                                int d;
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {

                                        p = element.Item1 + i - 1;
                                        d = element.Item2 + j - 1;
                                        if (canalnew[p,d] != 1)
                                            continue;

                                        canalnew[p,d] = kolor;
                                        bufor.Add(new Tuple<int, int>(p, d));
                                    }
                                }

                                bufor.RemoveAt(0);
                            }


                            if (bufor.Count == 0)
                            {

                                wspolrzedne.Add(new Tuple<int, int, int, int>(wsp_minx, wsp_miny, wsp_maxx+1, wsp_maxy+1));
                                wsp_minx = binary.w;
                                wsp_miny = binary.h;
                                wsp_maxx = 0;
                                wsp_maxy = 0;
                                var tem=kolor+20;
                                kolor = (byte)tem;
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < binary.w; x++)
            {

                for (int y = 0; y <binary.h; y++)
                {
                    binary.BinaryCanal[x][y] = canalnew[x +3, y + 3];
                }
            }
            return wspolrzedne;
        }

            
        
            
          
              
            
             
              
              
          
         
   
        
    }
}
