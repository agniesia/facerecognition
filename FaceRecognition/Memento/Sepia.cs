using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Sepia:Visitor
    {
        
        int Waga;
        image_RGB rgb=null;
        public Sepia(int Waga){
            this.Waga = Waga;
            
        }
        public void SepiaAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public string rob(image_as_tab image) {
            SepiaAll(image);
            return opis;
        }
        public void Visit(image_RGB rgb)
        {

            byte[] temp = new byte[rgb.w * rgb.h * 4];

            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    //R=R+2*W, G=G+W, B=B

                    if (rgb.G[i][j] + Waga <= 255)
                        rgb.G[i][j] = (byte)(rgb.G[i][j] + Waga);
                    else
                        rgb.G[i][j] = 255;
                    if (rgb.R[i][j] + 2 * Waga <= 255)
                        rgb.R[i][j] = (byte)(rgb.R[i][j] + 2 * Waga);
                    else
                        rgb.R[i][j] = 255;

                }

            }

        }
        public void Visit(image_Gray Grey) {
            rgb = new image_RGB();
            byte[] temp = new byte[rgb.w * rgb.h * 4];

            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    //R=R+2*W, G=G+W, B=B

                    if (Grey.Greycanal[i][j] + Waga <= 255)
                        rgb.G[i][j] = (byte)(Grey.Greycanal[i][j] + Waga);
                    else
                        rgb.G[i][j] = 255;
                    if (Grey.Greycanal[i][j] + 2 * Waga <= 255)
                        rgb.R[i][j] = (byte)(Grey.Greycanal[i][j] + 2 * Waga);
                    else
                        rgb.R[i][j] = 255;

                }

            }
            
        }
    }
}
