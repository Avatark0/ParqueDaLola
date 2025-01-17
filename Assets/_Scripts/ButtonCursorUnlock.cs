using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonCursorUnlock : MonoBehaviour, IPointerDownHandler// required interface when using the OnPointerDown method.
{
    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        CursorLockControl.LockCursor();
        GameCanvasScript.Continuar();
    }
}
