using System.Globalization;
using System.IO;
using UnityEngine;

public class LogReplayer : MonoBehaviour
{
    public string logPath = "Assets/Logs/UseThis/";
    public string logName = "Cube_log_28-05-2024_08-12-33.txt";
    public bool spawnActionText = true;
    private StreamReader sr;
    private string line;
    private int logCountPerSec = 0;
    public int currentLog = 0;
    public int logCount = 0;
    public bool paused = false;
    public GameObject pfb_Text;
    public string actionOrigin;


    // Start is called before the first frame update
    void Start()
    {

        sr = new StreamReader(logPath + logName);
        line = sr.ReadToEnd();
        string[] subs = line.Split(',');
        line = subs[subs.Length - 2];
        logCount = int.Parse(line);
        sr.Close();
        line = "";

        sr = new StreamReader(logPath + logName);
        line = sr.ReadLine();
        line = sr.ReadLine();
        logCountPerSec = int.Parse(line.Substring(11));


        line = sr.ReadLine();
        InvokeRepeating("ReadLog", 1f / logCountPerSec, 1f / logCountPerSec);
    }


        /*
        position_x = 0,
        position_y = 1,
        position_z = 2,
        rotation_x = 3,
        rotation_y = 4,
        rotation_z = 5,
        log_count = 6,
        action_Origin = 7,
        action_string = 8
        */    
    void ReadLog()
    {
        if (line == null) return;

        string[] subs = line.Split(',');

        if (int.Parse(subs[6]) > ++currentLog) return;

        float x = float.Parse(subs[0].Substring(1), CultureInfo.InvariantCulture.NumberFormat);
        float y = float.Parse(subs[1], CultureInfo.InvariantCulture.NumberFormat);
        float z = float.Parse(subs[2].Remove(subs[2].Length - 1), CultureInfo.InvariantCulture.NumberFormat);
        Vector3 newPos = new Vector3(x, y, z);
        this.gameObject.transform.position = newPos;

        float xQ = float.Parse(subs[3].Substring(2), CultureInfo.InvariantCulture.NumberFormat);
        float yQ = float.Parse(subs[4], CultureInfo.InvariantCulture.NumberFormat);
        float zQ = float.Parse(subs[5].Remove(subs[5].Length-1), CultureInfo.InvariantCulture.NumberFormat);
        this.gameObject.transform.eulerAngles = new Vector3(xQ, yQ, zQ);
        //float wQ = float.Parse(subs[6].Remove(subs[6].Length - 1), CultureInfo.InvariantCulture.NumberFormat);
        //Quaternion newQuat = new Quaternion(xQ, yQ, zQ, wQ);
        //this.gameObject.transform.rotation = newQuat;

        if (spawnActionText && actionOrigin != null && actionOrigin.Equals(subs[7]) && !actionOrigin.Equals(""))
        {
            spawnText(subs[8]);
        }

        currentLog = int.Parse(subs[6]);
        line = sr.ReadLine();
    }

    private void spawnText(string textToSpawn)
    {
        GameObject spawnText = Instantiate(pfb_Text);
        spawnText.transform.position = this.gameObject.transform.position;
        spawnText.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
        spawnText.GetComponent<TextRiser>().actionText.text = textToSpawn;
        spawnText = null;
    }

    void JumpToLogNumber(int logNumber)
    {
        CancelInvoke();
        DestroyAllTexts();
        sr.Close();
        sr = new StreamReader(logPath + logName);
        sr.ReadLine();
        sr.ReadLine();
        while (true)
        {
            line = sr.ReadLine();
            string[] subs = line.Split(',');
            if (int.Parse(subs[6]) + 1 == logNumber)
            {
                InvokeRepeating("ReadLog", 1f / logCountPerSec, 1f / logCountPerSec);
                currentLog = int.Parse(subs[6]);
                return;
            }
            else if (int.Parse(subs[6]) + 1 > logNumber)
            {
                return;
            }
        }

        
    }

    private void DestroyAllTexts()
    {
        // Find Objects of Type "Action Text" and eliminate those guys
        GameObject[] textObjects = GameObject.FindGameObjectsWithTag("Action Text");

        if (textObjects == null) return;

        foreach (GameObject text in textObjects)
        {
            Destroy(text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            JumpToLogNumber(5);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            JumpToLogNumber(logCount / Screen.width * (int)mousePos.x);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                InvokeRepeating("ReadLog", 1f / logCountPerSec, 1f / logCountPerSec);
                paused = false;
            }
            else
            {
                CancelInvoke();
                paused = true;
            }
        }
    }

    private void OnDestroy()
    {
        sr.Close();
    }

}
