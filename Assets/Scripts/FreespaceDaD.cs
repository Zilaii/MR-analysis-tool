using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreespaceDaD : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    public float rotationSpeed = 100.0f;


    void OnMouseDown()
    {
        zCoord = Camera.allCameras[0].WorldToScreenPoint(transform.position).z;
        offset = transform.parent.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        //return Camera.main.ScreenToWorldPoint(mousePoint);
        var cam = Camera.allCameras[0];
        var stw = cam.ScreenToWorldPoint(mousePoint);
        return stw;
    }

    void OnMouseDrag()
    {
        transform.parent.transform.position = GetMouseWorldPos() + offset;
    }


    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float rotationAmount = scroll * rotationSpeed;
            transform.Rotate(Vector3.up, rotationAmount, Space.World);
        }
    }
}
