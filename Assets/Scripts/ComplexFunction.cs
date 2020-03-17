using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComplexFunction
{


          public static Complejo Exp(Complejo z){
                    Complejo zOut = new Complejo( Mathf.Exp(z.x)*Mathf.Cos(z.y), Mathf.Exp(z.x)*Mathf.Sin(z.y) );
                    return zOut;
          }

          public static Complejo Sin(Complejo z){
                    Complejo zOut = new Complejo( Mathf.Sin(z.x)*(float)System.Math.Cosh(z.y), Mathf.Cos(z.x)*(float)System.Math.Sinh(z.y) );
                    return zOut;
          }

          public static Complejo dSin(Complejo z, int n){

                    Complejo zOut = Sin(z + new Complejo( (n*Mathf.PI)/2, 0f) );
                    return zOut;
          }

          public static Complejo Cos(Complejo z){
                    Complejo zOut = new Complejo( Mathf.Cos(z.x)*(float)System.Math.Cosh(z.y), -1*Mathf.Sin(z.x)*(float)System.Math.Sinh(z.y) );
                    return zOut;
          }

          public static Complejo dCos(Complejo z, int n){

                    Complejo zOut = Cos(z + new Complejo( (n*Mathf.PI)/2, 0f) );
                    return zOut;
          }

          public static Complejo Sinh(Complejo z){
                    Complejo zOut = new Complejo( (float)System.Math.Sinh(z.x)*Mathf.Cos(z.y) , (float)System.Math.Cosh(z.x)*Mathf.Sin(z.y) );
                    return zOut;
          }

          public static Complejo dSinh(Complejo z, int n){
                    Complejo zOut = null;
                    if (n%2 == 0) {
                              zOut = Sinh(z);
                    }else{
                              zOut = Cosh(z);
                    }
                    return zOut;
          }

          public static Complejo Cosh(Complejo z){
                    Complejo zOut = new Complejo( (float)System.Math.Cosh(z.x)*Mathf.Cos(z.y) , (float)System.Math.Sinh(z.x)*Mathf.Sin(z.y) );
                    return zOut;
          }

          public static Complejo dCosh(Complejo z, int n){

                    if (n%2 != 0) {
                              return Sinh(z);
                    }else{
                              return Cosh(z);
                    }

          }

          public static int Factorial( int x ){
            if( x<0){
                      return -1;
            }else if( x==1 || x==0 ){
                      return 1;
            }else{
                      return x* Factorial(x-1);
            }
          }

}
