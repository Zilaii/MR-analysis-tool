using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
public class LogRecorder : MonoBehaviour
{
    private static string logPath = "Assets/Logs/";
    private string fileName;
    private StreamWriter logWriter;
    public int logCountPerSec = 30;
    public int logCount = 0;
    private string actionLog = "";
    private LogManager logManager;

    // Start is called before the first frame update
    void Start()
    {
        string time = System.DateTime.Now.Day.ToString() +"-" +System.DateTime.Now.Month.ToString() 
            +"_" +System.DateTime.Now.Hour.ToString() +"-" +System.DateTime.Now.Minute.ToString() 
            +"-" +System.DateTime.Now.Second.ToString();
        time = time.Replace(".", "-");
        time = time.Replace(" ", "_");
        time = time.Replace(":", "-");
        string name = this.gameObject.name + "_log_" + time;
        fileName = (name + ".txt");
        logWriter = new StreamWriter(logPath + fileName);
        logWriter.WriteLine("Path: " + logPath + fileName);
        logWriter.WriteLine("Log Speed: " + logCountPerSec);
        logManager = GameObject.Find("LogManager").GetComponent<LogManager>();
        logCountPerSec = logManager.getLogCountPerSec();
        InvokeRepeating("WriteLogLine", 1f / logCountPerSec, 1f / logCountPerSec);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void WriteLogLine()
    {
        logWriter.WriteLine(this.gameObject.transform.position + ", " + this.gameObject.transform.localEulerAngles + "," + logManager.getLogCount() + "," + actionLog);
        logCount = logManager.getLogCount();
        actionLog = "";
    }

    private void OnDestroy()
    {
        if (logWriter != null) logWriter.Close();

    }

    // Usage Example: WriteActionLogLine("Grip Button", "Left Controller");
    public void WriteActionLogLine(string actionString, string actionOrigin)
    {
        if (!actionString.Equals("") && !actionOrigin.Equals("")) actionLog = actionOrigin + "," + actionString;
        else Debug.Log("LogRecorder: actionString or actionOrigin empty!");
    }

}