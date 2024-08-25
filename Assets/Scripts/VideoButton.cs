using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class VideoButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        gameObject.transform.parent.gameObject.transform.parent.GetComponent<VideoFileHandler>().changeVideo();
    }

}
