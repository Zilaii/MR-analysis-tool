using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Camera cameraHMD;
    public Camera cameraScreen;
    private Camera cameraHolder;
    private bool directorMode = false;


    // Start is called before the first frame update
    void Start()
    {
        cameraHolder = cameraScreen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleDirectorMode()
    {
        if (directorMode)
        {
            //cameraScreen.enabled = false;
            cameraScreen = cameraHMD;
            directorMode = false;
        }
        else if (!directorMode)
        {
            //cameraScreen.enabled = true;
            cameraScreen = cameraHolder;
            directorMode = true;
        }
    }
}
