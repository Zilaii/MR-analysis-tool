using UnityEngine;
using UnityEngine.Windows.WebCam;
using System.Linq;

public class WebcamRecorder : MonoBehaviour
{
    static readonly float MaxRecordingTime = 5000.0f;

    VideoCapture m_VideoCapture = null;
    float m_stopRecordingTimer = float.MaxValue;

    void Start()
    {
        StartVideoCaptureTest();
    }

    void Update()
    {
        if (m_VideoCapture == null || !m_VideoCapture.IsRecording)
        {
            return;
        }

        if (Time.time > m_stopRecordingTimer)
        {
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        }
    }

    private void OnDestroy()
    {
        m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
    }

    void StartVideoCaptureTest()
    {
        int targetWidth = 1920;
        int targetHeight = 1080;

        Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width == targetWidth && res.height == targetHeight).First();
        //FirstOrDefault((res) => res.refreshRate != 0);
        Debug.Log(cameraResolution);
        Debug.Log(VideoCapture.SupportedResolutions.First());

        float targetFramerate = 30;

        float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
        //float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).FirstOrDefault(res => res == targetFramerate);
        Debug.Log(cameraFramerate);

        VideoCapture.CreateAsync(false, delegate (VideoCapture videoCapture)
        {
            if (videoCapture != null)
            {
                m_VideoCapture = videoCapture;
                Debug.Log("Created VideoCapture Instance!");

                CameraParameters cameraParameters = new CameraParameters();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate = cameraFramerate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

                m_VideoCapture.StartVideoModeAsync(cameraParameters,
                    VideoCapture.AudioState.ApplicationAndMicAudio,
                    OnStartedVideoCaptureMode);
            }
            else
            {
                Debug.LogError("Failed to create VideoCapture Instance!");
            }
        });
    }

    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Video Capture Mode!");

        string timeStamp = System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Month.ToString()
            + "_" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString()
            + "-" + System.DateTime.Now.Second.ToString() + "-" + System.DateTime.Now.Millisecond.ToString();
        string filename = string.Format("Webcam_Recording_{0}.mp4", timeStamp);
        string filepath = "Assets/Video/WebcamVideo/";
        Debug.Log(filepath + filename);
        m_VideoCapture.StartRecordingAsync(filepath + filename, OnStartedRecordingVideo);
    }

    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Video Capture Mode!");
    }

    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Recording Video!");
        m_stopRecordingTimer = Time.time + MaxRecordingTime;
    }

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Recording Video!");
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

}


