using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCanvasScript : MonoBehaviour{
    private static GameObject menuPause;
    private GameObject tempoVal;
    private GameObject placarVal;

    private GameObject vitoriaObj;
    private GameObject nomeLola;
    private GameObject imagemPessego;

    private static bool jogoPausado;
    private static int placar;
    private static int tempoMax;
    private static int tempoIni;

    private bool vitoria=false; 

    void Start(){
        menuPause = transform.Find("MenuPause").gameObject;
        tempoVal = transform.Find("JogoStatus").Find("TempoVal").gameObject;
        placarVal = transform.Find("JogoStatus").Find("PlacarVal").gameObject;

        vitoriaObj = menuPause.transform.Find("Vitoria").gameObject;
        nomeLola = menuPause.transform.Find("NomeLola").gameObject;
        imagemPessego = menuPause.transform.Find("ImagePessego").gameObject;

        ResetaValores();
        Debug.Log("Start");
    }

    void Update(){
        placar=LolaScript.pontos;
        placarVal.GetComponent<Text>().text = placar.ToString();
        
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(!jogoPausado)
            {
                CursorLockControl.UnlockCursor();
                Pausar();
            }
            else
            {
                CursorLockControl.LockCursor();
                Continuar();
            }
        }  

        int tempoCont = Mathf.FloorToInt(Time.time) - tempoIni;

        if(Mathf.FloorToInt(Time.time) - tempoIni >= 6)//6 segundos da animação inicial da camera
            tempoVal.GetComponent<Text>().text = (tempoMax - tempoCont).ToString();

        if (tempoMax - tempoCont <= 0)
        {
            if (placar >= 150) 
                Vitoria();

            FimDeJogo();
        }
    }

    private void ResetaValores(){
        placar = 0;
        tempoMax = 186;//180 + 6 da animação de intro
        tempoIni=Mathf.FloorToInt(Time.time);
        jogoPausado=false;
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        vitoria=false;
    }

    private void Pausar()
    {
        menuPause.SetActive(true);
        jogoPausado = true;
        Time.timeScale = 0f;
    }

    public static void Continuar()
    {
        menuPause.SetActive(false);
        jogoPausado = false;
        Time.timeScale = 1f;
    }

    public void Sair(){
        SceneManager.LoadScene("Menu");
    }

    public void FimDeJogo(){
        if(vitoria)
            SceneManager.LoadScene("Vitoria");
        else 
            SceneManager.LoadScene("Derrota");
    }

    public void Vitoria(){
        //vitoriaObj.SetActive(true);
        //nomeLola.SetActive(false);
        //imagemPessego.SetActive(false);
        //if(!vitoria)Pausar();
        vitoria=true;
    }
}
