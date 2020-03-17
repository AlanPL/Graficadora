using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputControl : MonoBehaviour
{
          // Start is called before the first frame update
          public Graficadora graficadora;
          public Button button;
          public InputField inH;
          public InputField inK;
          public InputField inR;
          public InputField inAx;
          public InputField inAy;
          public InputField inBx;
          public InputField inBy;
          public InputField inCx;
          public InputField inCy;
          public InputField inDx;
          public InputField inDy;


          void Start()
          {

                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener( () => onClick() );
          }

          public void onClick(){


                    graficadora.Graficar( float.Parse( (inH.text=="")?"0":inH.text),
                                                  float.Parse( (inK.text=="")?"0":inK.text),
                                                  float.Parse( (inR.text=="")?"0":inR.text),
                                                  float.Parse( (inAx.text=="")?"0":inAx.text ),
                                                  float.Parse( (inAy.text=="")?"0":inAy.text ),
                                                  float.Parse( (inBx.text=="")?"0":inBx.text ),
                                                  float.Parse( (inBy.text=="")?"0":inBy.text ),
                                                  float.Parse( (inCx.text=="")?"0":inCx.text ),
                                                  float.Parse( (inCy.text=="")?"0":inCy.text ),
                                                  float.Parse( (inDx.text=="")?"0":inDx.text ),
                                                  float.Parse( (inDy.text=="")?"0":inDy.text ) );
         }
}
