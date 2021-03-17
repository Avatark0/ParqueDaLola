using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtoMenu : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            CursorLockControl.UnlockCursor();
            MenuCanvasScript.Inicio();
        }
    }
}
