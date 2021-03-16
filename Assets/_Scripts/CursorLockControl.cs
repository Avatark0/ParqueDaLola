using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockControl : MonoBehaviour
{
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
