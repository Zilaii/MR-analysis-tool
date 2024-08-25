using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Video;

public class FileChooser : MonoBehaviour
{

    public VideoClip _clip;
    public GameObject _pfbFileChooser;

    // Start is called before the first frame update
    void Start()
    {
        var info = new DirectoryInfo("Assets\\Video");
        var fileInfo = info.GetFiles();
        foreach (var file in fileInfo)
        {
            if (!file.Name.EndsWith(".meta"))
            {
                // spawn einzelnen button mit text
                GameObject x = Instantiate(_pfbFileChooser, this.gameObject.transform);
                x.GetComponent<VideoFileHandler>()._clipURL = "Assets/Video/" +file.Name;
                x.GetComponent<VideoFileHandler>()._textObject.text = file.Name;
                for (int i = 0; i < this.gameObject.transform.childCount; i++)
                {
                    x.transform.position = new Vector3(x.transform.position.x, (x.transform.position.y+0.5f), x.transform.position.z);
                }
                x.transform.position = new Vector3(x.transform.position.x, (x.transform.position.y + 1.5f), x.transform.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
