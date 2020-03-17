using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputControlIC : MonoBehaviour
{
          // Start is called before the first frame update
          public IntegracionCauchy integracionCauchy;
          public Button button;
          public InputField inR;
          public InputField inN;
          public InputField inZx;
          public InputField inZy;
          public Dropdown funcDropDown;
          public Text funcDropDownText;
          public Text func;
          public Text Z0;
          public Text output;
          public Text outputSimple;

          public Text nOfFuncLbl;
          public InputField nOfFunc;

          string potencia=" )^n";

          void Start()
          {

                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener( () => onClick() );

                    //m_Dropdown = inFunc.GetComponent<Dropdown>();
                  //Add listener for when the value of the Dropdown changes, to take action
                  funcDropDown.onValueChanged.AddListener(delegate {
                      DropdownValueChanged(funcDropDown);
                  });

                  inZx.onValueChanged.AddListener(delegate {
                      OnZValueChanged();
                  });
                  inZy.onValueChanged.AddListener(delegate {
                      OnZValueChanged();
                  });
                  inR.onValueChanged.AddListener(delegate {
                      OnRValueChanged();
                  });
                  inN.onValueChanged.AddListener(delegate {
                           OnNValueChanged();
                  });
                  nOfFunc.onValueChanged.AddListener(delegate {
                            OnPowValueChanged();
                  });

          }

          public void onClick(){
                    if (funcDropDownText.text != "Z^n") {
                              func.text =  funcDropDownText.text;
                    }

                    float xParsed, yParsed, rParsed;
                    int nParsed, nFuncParsed;
                    bool validX = float.TryParse(inZx.text, out xParsed);
                    bool validY = float.TryParse(inZy.text, out yParsed);
                    bool validR = float.TryParse(inR.text, out rParsed);

                    bool validN = int.TryParse(inN.text, out nParsed);
                    bool validNFunc = int.TryParse(nOfFunc.text, out nFuncParsed);

                    if (validX && validY && validR && validN && rParsed>0 && nParsed>0) {
                              if (funcDropDownText.text == "Z^n") {
                                        if (validNFunc && nFuncParsed>=0) {
                                                  integracionCauchy.Integrar( xParsed, yParsed, rParsed, nParsed, nFuncParsed, funcDropDownText.text);
                                        }else{
                                                  output.text = "Campos Inválidos";
                                                  outputSimple.text ="";
                                        }
                              }else{
                                        integracionCauchy.Integrar( xParsed, yParsed, rParsed, nParsed, 0, funcDropDownText.text);
                              }
                    }else{
                              output.text = "Campos Inválidos";
                              outputSimple.text ="";
                    }

         }

         void DropdownValueChanged(Dropdown change){
                   if (funcDropDownText.text == "Z^n") {
                             nOfFuncLbl.gameObject.SetActive(true);
                             nOfFunc.gameObject.SetActive(true);
                   }else{
                             nOfFuncLbl.gameObject.SetActive(false);
                             nOfFunc.gameObject.SetActive(false);
                   }
                 func.text =  funcDropDownText.text;
                 onClick();
          }
         void OnZValueChanged(){
                   if (inZx.text!="" && inZy.text!="") {
                            Z0.text =  "(Z - ("+inZx.text+verifySign(inZy.text)+" i )";
                            //Z0.text =  "Z - ("+inZx.text+" + ("+ inZy.text+") i "+")";
                  }else if (inZx.text!="" && inZy.text==""){
                            Z0.text =  "(Z - ("+inZx.text+")";
                  }else if (inZx.text=="" && inZy.text!=""){
                            Z0.text =  "(Z - ("+inZy.text+" i )";
                  }else if (inZx.text=="" && inZy.text==""){
                            Z0.text =  "(Z - Z0";
                  }
                  Z0.text =  Z0.text+potencia;
                  onClick();
          }

          string verifySign(string s){
                    if (s[0]=='-') {
                              return " "+s;
                    }else{
                              return " +"+s;
                    }
          }

         void OnNValueChanged(){
                   int nParsed;
                   bool validN = int.TryParse(inN.text, out nParsed);
                   if (validN) {
                             if(nParsed<0){
                                       inN.text ="";
                                       potencia=" )^n";
                             }else{
                                       potencia = " )^"+nParsed;
                             }

                   }else{
                             potencia=" )^n";
                   }
                   OnZValueChanged();
          }

         void OnPowValueChanged(){
                   int pParsed;
                   bool validP = int.TryParse(nOfFunc.text, out pParsed);
                   if (validP) {
                             if(pParsed<0){
                                       nOfFunc.text ="";
                                       func.text="Z^n";
                             }else{
                                       func.text = "Z^"+pParsed;
                             }

                   }else{
                             func.text="Z^n";
                   }
                   onClick();
          }
         void OnRValueChanged(){
                   float rParsed;
                   bool validR = float.TryParse(inR.text, out rParsed);
                   if (validR) {
                             if(rParsed<0){
                                       inR.text ="";
                             }

                   }
                   onClick();
          }

          public void ChangeZValues(float Zx, float Zy){

                    inZx.text = System.Math.Round(Zx, 2)+"";
                    inZy.text = System.Math.Round(Zy, 2)+"";
          }
          public void ChangeRValue(float r){
                    inR.text = System.Math.Round(r, 2)+"";                    
          }

}
