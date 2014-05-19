using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    class Erosion:lab01biometria.Visitor
    {
        public Erosion() { }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);


        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            DilatationMethod(rgb);
        }

        public void Visit(lab01biometria.image_Gray Grey)
        {
        }

        private void DilatationMethod(lab01biometria.image_RGB rgb)
        {
            var change = 0;
            
            for (int i=0; i<rgb.w; i++){
                for (int j=0; j<rgb.h; j++){
                        if (rgb.B[i][j] == 255){
                            if (i>0 && rgb.B[i-1][j]==0){
                                rgb.R[i-1][j] = 2;
                                rgb.G[i-1][j] = 2;
                                rgb.B[i-1][j] = 2;
                                
                            
                            } 
                            if (j>0 && rgb.B[i][j-1]==0){
                                rgb.R[i][j-1] = 2;
                                rgb.B[i][j-1] = 2;
                                rgb.G[i][j-1] = 2;
                                
                            } 
                            if (i+1<rgb.w && rgb.B[i+1][j]==0){
                                rgb.R[i+1][j] = 2;
                                rgb.G[i+1][j] = 2;
                                rgb.B[i+1][j] = 2;
                                
                            } 
                            if (j+1<rgb.h && rgb.B[i][j+1]==0){
                                rgb.R[i][j+1] = 2;
                                rgb.G[i][j+1] = 2;
                                rgb.B[i][j+1] = 2;
                                
                            } 
                        }
                    }
                }
             for (int i=0; i<rgb.w; i++){
                    for (int j=0; j<rgb.h; j++){
                        if (rgb.R[i][j] == 2){
                            rgb.R[i][j] = 255;
                            rgb.G[i][j] = 255;
                            rgb.B[i][j] = 255;
                        }
                    }
                }
              
            }
    }
}
