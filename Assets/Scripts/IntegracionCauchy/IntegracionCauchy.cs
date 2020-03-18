using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntegracionCauchy : MonoBehaviour
{
          float limits=4f;
          public GameObject originalCircle;
          //public GameObject pointPrefab;
          public GameObject point;

          public GameObject originalxCoord;
          public GameObject originalyCoord;
          public Text textOriginalCoords;

          public Text textOriginalxLabels;
          public Text textOriginalyLabels;

          public GameObject xLabelsParent;
          public GameObject xLabelPrefab;

          public GameObject yLabelsParent;
          public GameObject yLabelPrefab;

          public Text output;
          public Text outputSimple;
          public Text zPolar;
          public Text mouseCoords;

          public GameObject camera1;
          float heightCam;
          float widthCam;


          Camera cam1;
          Circulo originalC;
          Complejo A,B,C,D;

    void Start()
    {
               cam1 = camera1.GetComponent<UnityEngine.Camera>();
               DrawAxis();
               DrawxLabels(10.0f);
               DrawyLabels(10.0f);
    }

    void Update(){
              Vector3 v3 = Input.mousePosition;
                     v3.z = 0.3f;
                     v3 = Camera.main.ScreenToWorldPoint(v3);
                    /*Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                    mousePos.y = Camera.current.pixelHeight - mousePos.y;
                    Vector3 position =  cam1.ScreenToWorldPoint(Input.mousePosition);*/

              mouseCoords.text = "( "+System.Math.Round(v3.x, 2)+", "+System.Math.Round(v3.y, 2)+" )";

   }
    void DrawAxis(){
              heightCam = 2f * cam1.orthographicSize;
              widthCam = heightCam * cam1.aspect;

              originalxCoord.DrawLine(  new Vector3(-widthCam/2,0,0), new Vector3(widthCam/2 ,0,0), 0.02f*limits);
             originalyCoord.DrawLine(  new Vector3(0,-heightCam/2,0), new Vector3(0,heightCam/2,0), 0.02f*limits);
    }
    void DrawxLabels(float range){
              //15 elementos
                    for(int i = 0; i < xLabelsParent.transform.childCount; i++)
                    {
                              GameObject g = xLabelsParent.transform.GetChild(i).gameObject;
                              Destroy(g);
                    }
              for (int i=-370; i<400; i+=55) {
                        GameObject label = Instantiate(xLabelPrefab, new Vector3(i, 65, 0), Quaternion.identity) as GameObject;
                        label.transform.SetParent(xLabelsParent.transform, false);
                        label.GetComponent<Text>().text = "|\n "+(float)System.Math.Round(
                              Camera.main.ScreenToWorldPoint(label.transform.position).x, 2);
              }

   }
    void DrawyLabels(float range){
                    for(int i = 0; i < yLabelsParent.transform.childCount; i++)
                    {
                              GameObject g = yLabelsParent.transform.GetChild(i).gameObject;
                              Destroy(g);
                    }

              for (int i=-150; i<300; i+=55) {
                        if (i!=70) {
                                  GameObject label = Instantiate(yLabelPrefab, new Vector3(36, i, 0), Quaternion.identity) as GameObject;
                                  label.transform.SetParent(yLabelsParent.transform, false);
                                  label.GetComponent<Text>().text = "— "+(float)System.Math.Round(
                                  Camera.main.ScreenToWorldPoint(label.transform.position).y, 2);
                        }
              }

   }

   void DrawRLabel(float r){
             GameObject labelR = Instantiate(xLabelPrefab, new Vector3(191, 94, 0), Quaternion.identity) as GameObject;
             labelR.transform.SetParent(xLabelsParent.transform, false);
             labelR.GetComponent<Text>().text = " "+r+"\n|";
   }
    public void Integrar(float Zx, float Zy, float r, int n, int p, float a, float b, string function){
              limits=r;
              originalC = new Circulo(0,0,r);
              float mayor = Mathf.Abs(Zx) > Mathf.Abs(Zy)? Mathf.Abs(Zx): Mathf.Abs(Zy);
                 if ( mayor >= r ) {
                           limits = mayor;
                 }


              GenerarCirculo(originalC, originalCircle, cam1, originalxCoord, originalyCoord, textOriginalCoords);

              //Destroy(point);
              //point = Instantiate(pointPrefab, new Vector3(Zx, Zy, -0.15f), Quaternion.identity) as GameObject;
              point.transform.position = new Vector3(Zx, Zy, -0.15f);
             point.transform.localScale = new Vector3(1,1,1)*0.3f*limits;

             Complejo Z0  = new Complejo(Zx, Zy);
             //Debug.Log( Z0.ToString() );
             zPolar.text = Z0.ToPolarString();
             if (Z0.radio == r && n!=0 ) {
                       output.text = "indeterminado";
                       outputSimple.text="";
            }else if (Z0.radio > r || n==0) {
                       output.text = "0";
                       outputSimple.text="";
             }else{
                       string outTxt;
                       Complejo dospi;
                       if (n==1) {
                                 outTxt = "2πi*";
                                 dospi = new Complejo(0,2*Mathf.PI);
                       }else{
                                 outTxt = "2πi/"+(n-1)+"!*(";
                                 dospi = new Complejo(0, (2*Mathf.PI)/ComplexFunction.Factorial(n-1) );
                       }

                       Complejo multiplicando = new Complejo(0,0);
                       switch (function) {
                                 case "Z^n":
                                        outTxt+="("+Z0.ToBinomialString()+")^"+p;
                                        multiplicando = ComplexFunction.dzPow(Z0, p,n-1);

                                 break;
                                 case "e^z":
                                        outTxt+="e^("+Z0.ToBinomialString()+")";
                                        multiplicando = ComplexFunction.Exp( Z0 );
                                 break;
                                 case "sen(z)":
                                        outTxt+="sen("+Z0.ToBinomialString()+")";
                                        multiplicando = ComplexFunction.dSin( Z0, n-1 );
                                 break;
                                 case "cos(z)":
                                        outTxt+="cos("+Z0.ToBinomialString()+")";
                                        multiplicando = ComplexFunction.dCos( Z0, n-1 );
                                 break;
                                 case "cosh(z)":
                                        outTxt+="cosh("+Z0.ToBinomialString()+")";
                                        multiplicando = ComplexFunction.dCosh( Z0, n-1 );
                                 break;
                                 case "senh(z)":
                                        outTxt+="senh("+Z0.ToBinomialString()+")";
                                        multiplicando = ComplexFunction.dSinh( Z0, n-1 );
                                 break;
                                 case "a+ib":
                                        Complejo aib = new Complejo(a,b);
                                        outTxt+="("+aib.ToBinomialString()+")";
                                        if (n<=1){
                                                  multiplicando = aib ;
                                        }
                                 break;

                       }
                       if (function!="Z^n") {
                                 outputSimple.fontSize=20;
                                 outputSimple.text="= "+(multiplicando*dospi).ToBinomialString();
                       }else{
                                 outputSimple.fontSize=20;
                                 outputSimple.text="= "+(multiplicando*dospi).ToBinomialString();
                                 /*if (Z0.x==0 && Z0.y==0) {
                                          outputSimple.fontSize=20;
                                          outputSimple.text ="= 2πi * 0";
                                 }else{
                                           outputSimple.fontSize=15;
                                           outputSimple.text="= 2πi * "+System.Math.Round( Z0.radio, 2)+"^n ( cos(n*"+System.Math.Round( 180*(Z0.angulo/Mathf.PI), 2)+
                                           ") + sen(n*"+System.Math.Round( 180*(Z0.angulo/Mathf.PI), 2)+"))";
                                 }*/
                       }
                       if (n>1){
                                 outTxt += ")^("+(n-1)+") deriv.";

                       }
                       output.text = outTxt;
            }
   }



   void GenerarCirculo(Circulo circulo, GameObject objeto, Camera cam, GameObject xCoord, GameObject yCoord, Text textOriginalCoords){

             float zCoord = objeto.transform.position.z;
             objeto.transform.position = new Vector3(circulo.h, circulo.k, zCoord);
             cam.transform.position = new Vector3( 0, 0, zCoord-1);

            ajustarEscala(limits*1.2f);
            DrawxLabels(circulo.r*1.95f);
            DrawyLabels(circulo.r*1.95f);

            //DrawRLabel(circulo.r);

            //ajustarTxtScale(limits, textOriginalxLabels , textOriginalyLabels);
   }

   void ajustarTxtScale(float limits, Text xLbl, Text yLbl){
             string xlabel="";
             string ylabel="";
             for (float i = -(float)Mathf.RoundToInt(limits); i<limits; i +=limits/10 ) {
                       xlabel+= i.ToString("F1")+"        ";
                       ylabel+= (-i).ToString("F1")+System.Environment.NewLine+System.Environment.NewLine;
             }
             xlabel+=(int)Mathf.RoundToInt(limits);
             ylabel+=(int)Mathf.RoundToInt(-limits);
             xLbl.text = xlabel;
             yLbl.text = ylabel;
   }
   void ajustarEscala(float limits){
             cam1.orthographicSize = limits;
             originalCircle.DrawCircle(originalC.r, 0.02f*limits);
             DrawAxis();
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

}
