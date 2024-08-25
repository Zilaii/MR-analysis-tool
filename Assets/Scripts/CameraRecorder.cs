using UnityEditor.Media; // Import the necessary namespace
using UnityEngine;
using Unity.Collections;
using System;
using System.IO;
using UnityEditor;
using System.Collections;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

public class CameraRecorder : MonoBehaviour
{
    RecorderController m_RecorderController;
    public bool m_RecordAudio = true;
    internal MovieRecorderSettings m_Settings = null;

    public FileInfo OutputFile
    {
        get
        {
            var fileName = m_Settings.OutputFile + ".mp4";
            return new FileInfo(fileName);
        }
    }

    void OnEnable()
    {
        Initialize();
    }

    internal void Initialize()
    {
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        m_RecorderController = new RecorderController(controllerSettings);

        var mediaOutputFolder = new DirectoryInfo(Path.Combine(Application.dataPath, "..", "SampleRecordings"));

        // Video
        m_Settings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        m_Settings.name = "My Video Recorder";
        m_Settings.Enabled = true;

        // This example performs an MP4 recording
        m_Settings.OutputFormat = MovieRecorderSettings.VideoRecorderOutputFormat.MP4;
        m_Settings.VideoBitRateMode = VideoBitrateMode.High;

        m_Settings.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = 1920,
            OutputHeight = 1080
        };

        m_Settings.AudioInputSettings.PreserveAudio = m_RecordAudio;

        // Simple file name (no wildcards) so that FileInfo constructor works in OutputFile getter.
        string timeStamp = System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Month.ToString()
    + "_" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString()
    + "-" + System.DateTime.Now.Second.ToString() + "-" + System.DateTime.Now.Millisecond.ToString();
        string filename = string.Format("Frustum_Recording_{0}.mp4", timeStamp);
        m_Settings.OutputFile = "Assets/Video/FrustumVideo/" +filename;
        Debug.Log(mediaOutputFolder.FullName + "/" + "video");

        // Setup Recording
        controllerSettings.AddRecorderSettings(m_Settings);
        controllerSettings.SetRecordModeToManual();
        controllerSettings.FrameRate = 60.0f;

        RecorderOptions.VerboseMode = false;
        m_RecorderController.PrepareRecording();
        m_RecorderController.StartRecording();

        Debug.Log($"Started recording for file {OutputFile.FullName}");
    }

    void OnDisable()
    {
        m_RecorderController.StopRecording();
    }
}