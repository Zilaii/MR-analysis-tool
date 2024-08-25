using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoOpenCloseButton : MonoBehaviour
{
    GameObject fileChooser;
    private void OnMouseDown()
    {
        if (fileChooser == null) fileChooser = GameObject.Find("File Chooser");
        
        if (fileChooser.active)
        {
            fileChooser.SetActive(false);
        } else
        {
            fileChooser.SetActive(true);
        }

    }
}
