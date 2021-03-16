using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceToMenu : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CursorLockControl.UnlockCursor();
            MenuCanvasScript.Inicio();
        }
    }
}
