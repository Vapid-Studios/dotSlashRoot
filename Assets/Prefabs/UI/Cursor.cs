using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    [SerializeField] private Texture2D cursorTexture;

    private CursorMode _cursorMode = CursorMode.ForceSoftware;

    public Vector2 hotSpot = Vector2.zero;


    private void OnMouseEnter()
    {
        UnityEngine.Cursor.SetCursor(cursorTexture,hotSpot,_cursorMode);
    }

    private void OnMouseExit()
    {
        UnityEngine.Cursor.SetCursor(null, Vector2.zero,_cursorMode);
    }
}
