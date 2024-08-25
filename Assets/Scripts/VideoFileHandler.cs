using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class VideoFileHandler : MonoBehaviour
{


    public string _clipURL;
    public TextMeshPro _textObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.current != null)
        {
            //gameObject.transform.LookAt(Camera.current.transform.position);
            //gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y + 180, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
        }
    }

    public void changeVideo()
    {
        GameObject.Find("POV Video Player").gameObject.GetComponent<PovVideo>().changeVideoSource(_clipURL);
    }
}
