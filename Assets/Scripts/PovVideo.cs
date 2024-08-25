using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PovVideo : MonoBehaviour
{

    public VideoClip clip;
    public GameObject frontPlane;
    public GameObject backPlane;
    private VideoPlayer frontPlayer;
    private VideoPlayer backPlayer;
    private double videoTimeScale;
    private bool videoIsPreparing = false;
    private bool pauseTest = false;


    // Start is called before the first frame update
    void Start()
    {
        frontPlayer = frontPlane.GetComponent<VideoPlayer>(); 
        //frontPlayer.clip = clip;
        backPlayer = backPlane.GetComponent<VideoPlayer>();
        //backPlayer.clip = clip;
        
    }

    public void changeVideo(VideoClip newClip)
    {
        if (!newClip.Equals(clip))
        {
            clip = newClip;
            frontPlayer.clip = clip;
            backPlayer.clip = clip;
        }
    }

    public void changeVideoSource(string videoURL)
    {
        double currentFrame = frontPlayer.frame;
        int maxFrame = (int)frontPlayer.frameCount;
        videoTimeScale = currentFrame/maxFrame;
        frontPlayer.url = videoURL;
        backPlayer.url = frontPlayer.url;
        frontPlayer.Prepare();
        backPlayer.Prepare();

        StartCoroutine(WaitForVideoToPrepare());

    }

    IEnumerator WaitForVideoToPrepare()
    {
        while (!frontPlayer.isPrepared)
        {
            videoIsPreparing = true;
            yield return null;
        }
        videoIsPreparing = false;
        frontPlayer.frame = (int)(frontPlayer.frameCount * videoTimeScale);
        backPlayer.frame = (int)(backPlayer.frameCount * videoTimeScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //frontPlayer.isPaused
            if (pauseTest)
            {
                Time.timeScale = 1f;
                pauseTest = false;
            } else
            {
                pauseTest = true;
                Time.timeScale = 0f;
            }
        }

        if (!pauseTest)
        {
            frontPlayer.Play();
            backPlayer.Play();
        } else
        {
            frontPlayer.Pause();
            backPlayer.Pause();
        }

        if (Input.GetMouseButtonDown(0) && !videoIsPreparing)
        {
            Vector3 mousePos = Input.mousePosition;
            float t = (float)frontPlayer.frameCount/(float)Screen.width;
            float frameCount = (float)frontPlayer.frameCount;
            float screenWidth = (float)Screen.width;
            float he = (frameCount / screenWidth);
            var v = (he * mousePos.x);

            frontPlayer.frame = (int)v;
            Debug.Log("old frame: " +backPlayer.frame +", new frame: " +v +", max Frames: " +(int)frontPlayer.frameCount + ", Screen width: " +Screen.width + ", mousePos.x: " +(int)mousePos.x +", he: " +he);
            backPlayer.frame = (int)v;
        }

    }
}
