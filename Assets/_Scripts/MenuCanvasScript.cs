using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCanvasScript : MonoBehaviour{

    public static void Jogar(){
        SceneManager.LoadScene("Jogo");
    }

    public static void Inicio(){
        SceneManager.LoadScene("Menu");
    }

    public void Fechar(){
        Debug.Log("Fechar");
        Application.Quit();
    }
}

