using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{

    public int logCount = 0;
    public int logCountPerSec = 30;


    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("logCountUp", 1f / logCountPerSec, 1f / logCountPerSec);
    }

    private void logCountUp()
    {
        logCount++;
    }

    public int getLogCount() { return logCount; }
    public int getLogCountPerSec() { return logCountPerSec; }
}
