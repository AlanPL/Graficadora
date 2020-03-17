using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complejo
{
    public float x, y;
    //forma polar
    public float radio, angulo;
    public Complejo(float x, float y)
    {

        this.x = x;
        this.y = y;
        radio = Mathf.Sqrt(x * x + y * y);
        if (x>0 && y==0) {
                  angulo = 0;
        }else if (x>0 && y>0) {
                  angulo = Mathf.Atan(y/x);
        }else if (x==0 && y>0) {
                  angulo = Mathf.PI/2;
        }else if(x<0 && y>0){
                  angulo = Mathf.Atan(y/x) + Mathf.PI;
        }else if(x<0 && y==0){
                  angulo = Mathf.PI;
        }else if(x<0 && y<0){
                  angulo = Mathf.Atan(y/x) + Mathf.PI;
        }else if(x==0 && y>0){
                  angulo = Mathf.PI*(3/4);
        }else{
                  angulo = Mathf.Atan(y/x) + 2*Mathf.PI;
        }
    }


    public override string ToString(){
              return "Z = "+x+" +("+y+")i  = "+radio+" < "+angulo ;
   }
    public string ToBinomialString(){
              return ""+System.Math.Round(x, 2)+" "+verifySign(System.Math.Round(y, 2))+"i";
   }
    public string ToPolarString(){
              return " "+System.Math.Round(radio, 2)+" ∡ "+System.Math.Round( 180*(angulo/Mathf.PI), 2) ;
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

   string verifySign(string s){
             if (s[0]=='-') {
                       return " "+s;
             }else{
                       return " +"+s;
             }
   }

   string verifySign(double s){
             return verifySign(s+"");             
   }

}
