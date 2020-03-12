using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graficadora : MonoBehaviour
{
          float limits;
          public GameObject originalCircle;
          public GameObject originalxCoord;
          public GameObject originalyCoord;
          public Text textOriginalCoords;
          public Text textTransformedCoords;

          public Text textOriginalxLabels;
          public Text textOriginalyLabels;
          public Text textTransformedxLabels;
          public Text textTransformedyLabels;

          public GameObject transformedxCoord;
          public GameObject transformedyCoord;
          public GameObject transformedCircle;
          public GameObject camera1;
          public GameObject camera2;
          Camera cam1;
          Camera cam2;
          Circulo originalC;
          Circulo transformedC;
          Complejo A,B,C,D;
    // Start is called before the first frame update
    void Start()
    {
               cam1 = camera1.GetComponent<UnityEngine.Camera>();
               cam2 = camera2.GetComponent<UnityEngine.Camera>();
               originalxCoord.DrawLine(  new Vector3(-10,0,0), new Vector3(10,0,0), 0.1f);
               originalyCoord.DrawLine(  new Vector3(0,-10,0), new Vector3(0,10,0), 0.1f);
               transformedxCoord.DrawLine(  new Vector3(-10,0,0), new Vector3(10,0,0), 0.1f);
               transformedyCoord.DrawLine(  new Vector3(0,-10,0), new Vector3(0,10,0), 0.1f);
               //originalC = new Circulo(0,0,5);
               //GenerarCirculo(originalC, originalCircle, cam1);
              //
    }

    public void Graficar(float h, float k, float r, float Ax, float Ay, float Bx, float By, float Cx, float Cy, float Dx, float Dy){
              limits=0;
              originalC = new Circulo(h,k,r);
              GenerarCirculo(originalC, originalCircle, cam1, originalxCoord, originalyCoord, textOriginalCoords);

              A = new Complejo(Ax, Ay);
              B = new Complejo(Bx, By);
              C = new Complejo(Cx, Cy);
              D = new Complejo(Dx, Dy);
              Complejo T1 = D/C;
              transformedC = TransormarPorSuma( originalC, T1);
              transformedC = TransormarConInversion( transformedC);

              // bc-ad / c*c
              Complejo T3Den = C*C;
              Complejo T3a = B*C;
              Complejo T3b = A*D;
              Complejo T3resta = T3a-T3b;
              Complejo T3 = T3resta/T3Den;
              transformedC = TransormarPorMultiplicacion( transformedC, T3);
              Complejo T4 = A/C;
              transformedC = TransormarPorSuma( transformedC, T4);
              
              //transformedC = TransormarPorMultiplicacion( originalC, A);
              GenerarCirculo(transformedC, transformedCircle, cam2, transformedxCoord, transformedyCoord, textTransformedCoords);
   }


   Circulo TransormarPorMultiplicacion( Circulo cir, Complejo comp){

             float h = cir.h*comp.x - cir.k*comp.y;
             float k = cir.h*comp.y + cir.k*comp.x;
             float r = Mathf.Sqrt((comp.x*comp.x + comp.y*comp.y)*cir.r*cir.r);

             return new Circulo(h,k,r);
   }

   Circulo TransormarConInversion( Circulo cir){

             float denominador = cir.h*cir.h + cir.k*cir.k - cir.r*cir.r;
             float h,k,r;
             if (denominador!=0) {
                      h = cir.h/denominador;
                      k = -cir.k/denominador;
                      r =  cir.r / denominador;
                      return new Circulo(h,k,r);
             }else{

                      return new Circulo(0,0,1);
            }

   }

   Circulo TransormarPorSuma( Circulo cir, Complejo comp){

             float h = cir.h+comp.x;
             float k =  cir.k+comp.y;
             float r = cir.r;

             return new Circulo(h,k,r);
   }
   void GenerarCirculo(Circulo circulo, GameObject objeto, Camera cam, GameObject xCoord, GameObject yCoord, Text textOriginalCoords){

             float zCoord = objeto.transform.position.z;
             objeto.transform.position = new Vector3(circulo.h, circulo.k, zCoord);
             cam.transform.position = new Vector3( 0, 0, zCoord-1);
             float newLimits;
             if ( Mathf.Abs(circulo.h) > Mathf.Abs(circulo.k) ) {
                       newLimits = Mathf.Abs(circulo.h)+circulo.r*1.1f;
             }else{
                       newLimits = Mathf.Abs(circulo.k)+circulo.r*1.1f;
            }
            if (newLimits>limits) {
                      limits=newLimits;
            }

            ajustarEscala(limits);
            ajustarTxtScale(limits, textOriginalxLabels , textOriginalyLabels);
            ajustarTxtScale(limits, textTransformedxLabels , textTransformedyLabels);
            textOriginalCoords.text = "centro: ("+circulo.h.ToString("F1")+", "+circulo.k.ToString("F1")+")   radio: "+circulo.r.ToString("F1");
            //textOriginalCoords.transform.position = new Vector3( 432+circulo.h*scale , 324+circulo.k*scale, 0);
   }

   void ajustarTxtScale(float limits, Text xLbl, Text yLbl){
             string xlabel="";
             string ylabel="";
             for (float i = -(float)Mathf.RoundToInt(limits); i<limits; i +=limits/5 ) {
                       xlabel+= i.ToString("F1")+"        ";
                       ylabel+= (-i).ToString("F1")+System.Environment.NewLine+System.Environment.NewLine;
             }
             xlabel+=(int)Mathf.RoundToInt(limits);
             ylabel+=(int)Mathf.RoundToInt(-limits);
             xLbl.text = xlabel;
             yLbl.text = ylabel;
   }
   void ajustarEscala(float limits){
             cam2.orthographicSize = limits;
             transformedCircle.DrawCircle(transformedC.r, 0.02f*limits);
             transformedxCoord.DrawLine(  new Vector3(-limits,0,0), new Vector3(limits,0,0), 0.02f*limits);
             transformedyCoord.DrawLine(  new Vector3(0,-limits,0), new Vector3(0,limits,0), 0.02f*limits);

             cam1.orthographicSize = limits;
             originalCircle.DrawCircle(originalC.r, 0.02f*limits);
             originalxCoord.DrawLine(  new Vector3(-limits,0,0), new Vector3(limits,0,0), 0.02f*limits);
             originalyCoord.DrawLine(  new Vector3(0,-limits,0), new Vector3(0,limits,0), 0.02f*limits);
   }
}



struct Circulo
{
    public float h,k,r;
    public Circulo(float h, float k, float r)
    {

        this.h = h;
        this.k = k;
        this.r = r;
    }

    public void toString(){
              Debug.Log("h = "+h+", k = "+k+", r = "+r);
   }
}

class Complejo
{
    public float x, y;
    public Complejo(float x, float y)
    {

        this.x = x;
        this.y = y;
    }

    public string toString(){
              return "Z = "+x+" +("+y+")i";
   }
    public static Complejo operator +(Complejo z1, Complejo z2){
              return new Complejo(z1.x+z2.x, z1.y+z2.y);
   }
    public static Complejo operator -(Complejo z1, Complejo z2){
              return new Complejo(z1.x-z2.x, z1.y-z2.y);
   }
    public static Complejo operator *(Complejo z1, Complejo z2){
              float real =z1.x*z2.x - z1.y*z2.y;
              float imaginario = z1.y*z2.x + z1.x*z2.y;
              return new Complejo(real, imaginario);
   }
    public static Complejo operator /(Complejo z1, Complejo z2){
              float divisor = z2.x*z2.x +z2.y*z2.y;
              if (divisor!=0) {
                        float real =(z1.x*z2.x + z1.y*z2.y)/divisor;
                        float imaginario = (z1.y*z2.x - z1.x*z2.y)/divisor;
                        return new Complejo(real, imaginario);
              }else{
                        return new Complejo(0, 0);
              }

   }

}
