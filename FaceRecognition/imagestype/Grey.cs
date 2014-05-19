using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class image_Gray : image_as_tab

    {
        public byte[][] Greycanal;
        
        public byte[][] alfa;
        public image_Gray() : base() { }

        public image_Gray(byte[] orginal_tab, int wight, int hight)
            : base(orginal_tab, wight, hight)
        {

            Greycanal = new byte[wight][];
            alfa = new byte[wight][];


            var k = 0;
            for (int i = 0; i < wight; i++)
            {

                Greycanal[i] = new byte[hight];
               alfa[i] = new byte[hight];
                for (int j = 0; j < hight; j++)
                {
                    k = 4 * (j * w + i);
                    Greycanal[i][j] = orginal_tab[k];
                    alfa[i][j] = orginal_tab[k + 3];
                }
            }
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
        public override image_Gray greyimage()
        {
            return this;
        }
        public override image_as_tab copy()
        {
            image_Gray temp = new image_Gray();
            temp.Greycanal = (byte[][])this.Greycanal.Clone();
            temp.h = this.h;
            temp.w = this.w;
            temp.alfa = (byte[][])this.alfa.Clone();
            temp.utab = (byte[])this.utab.Clone();
            return temp;
        }
        public override byte[] show()
        {
            byte[] temp = new byte[w * h * 4];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {

                    temp[4 * (j * w + i)] = this.Greycanal[i][j];
                    temp[4 * (j * w + i) + 1] = this.Greycanal[i][j];
                    temp[4 * (j * w + i) + 2] = this.Greycanal[i][j];
                    temp[4 * (j * w + i) + 3] = alfa[i][j];


                }
            }
            return temp;
        }
        //public static explicit operator image_RGB(image_Gray rgb)
        //{
        //    // trzeba zrobic coś z akt
        //    image_Gray temp = new image_Gray(rgb.imagearray3Dto1D(), rgb.w, rgb.h);


        //    return temp;
        //}
        






        
    }
}

