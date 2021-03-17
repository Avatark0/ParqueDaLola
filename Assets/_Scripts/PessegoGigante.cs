using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PessegoGigante : MonoBehaviour
{
    private static int pessegosGigantesColetados = 0;
    private static bool pessegoNO = false;
    private static bool pessegoSO = false;
    private static bool pessegoCE = false;
    private static bool pessegoSE = false;
    private static bool pessegoNE = false;

    public string ID()
    {
        return gameObject.name;
    }

    public static void Coletar(string idDoPessego)
    {
        switch(idDoPessego)
        {
            case "PessegoGiganteNO":
                if(!pessegoNO)
                {
                    pessegoNO=true;
                    pessegosGigantesColetados++;
                }
                break;
            case "PessegoGiganteSO":
                if(!pessegoSO)
                {
                    pessegoSO=true;
                    pessegosGigantesColetados++;
                }
                break;
            case "PessegoGiganteCE":
                if(!pessegoCE)
                {
                    pessegoCE=true;
                    pessegosGigantesColetados++;
                }
                break;
            case "PessegoGiganteSE":
                if(!pessegoSE)
                {
                    pessegoSE=true;
                    pessegosGigantesColetados++;
                }
                break;
            case "PessegoGiganteNE":
                if(!pessegoNE)
                {
                    pessegoNE=true;
                    pessegosGigantesColetados++;
                }
                break;
        }
        
        if(pessegosGigantesColetados==5)
        {
            pessegosGigantesColetados=0;
            
            pessegoNO = false;
            pessegoSO = false;
            pessegoCE = false;
            pessegoSE = false;
            pessegoNE = false;
            
            SceneManager.LoadScene("FinalSecreto");
        }
    }
}
