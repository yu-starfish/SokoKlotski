using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseDraggingVector : MonoBehaviour
{
    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    private Vector2 dragDirection;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = GetMouseWorldPos();
        }
        else if (Input.GetMouseButton(0))
        {
            dragEndPos = GetMouseWorldPos();
            dragDirection = dragEndPos - dragStartPos;
            Debug.Log("Drag Direction: " + dragDirection);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragDirection = Vector2.zero;
        }
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}
