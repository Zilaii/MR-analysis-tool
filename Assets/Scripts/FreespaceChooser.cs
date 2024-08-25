using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FreespaceChooser : MonoBehaviour
{

    public Texture2D tex1;
    public Texture2D tex2;
    public GameObject frontPlane;
    public GameObject backPlane;
    Renderer planeRendererFront;
    Renderer planeRendererBack;
    public GameObject _pfbFileChooser;


    // Start is called before the first frame update
    void Start()
    {
        var info = new DirectoryInfo("Assets\\FreespaceData");
        var fileInfo = info.GetFiles();
        foreach (var file in fileInfo)
        {
            if (!file.Name.EndsWith(".meta"))
            {
                // spawn einzelnen button mit text
                
                GameObject x = Instantiate(_pfbFileChooser, this.gameObject.transform);
                x.GetComponent<PictureFileHandler>()._fileUrl = "Assets/FreespaceData/" + file.Name;
                x.GetComponent<PictureFileHandler>()._textObject.text = file.Name;
                for (int i = 0; i < this.gameObject.transform.childCount; i++)
                {
                    x.transform.position = new Vector3(x.transform.position.x, (x.transform.position.y + 0.5f), x.transform.position.z);
                }
                x.transform.position = new Vector3(x.transform.position.x, (x.transform.position.y + 1.5f), x.transform.position.z);
            
                Debug.Log(file.Name);
            }
        }


        Material mat = new Material(Shader.Find("Standard"));
        mat.mainTexture = tex1;

        planeRendererFront = frontPlane.GetComponent<Renderer>();
        planeRendererBack = backPlane.GetComponent<Renderer>();

        setTexture(mat);
    }

    void setTexture(Material tex)
    {
        planeRendererFront.material = tex;
        planeRendererBack.material = tex;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.L))
        {
            Material mat2 = new Material(Shader.Find("Standard"));
            mat2.mainTexture = tex2;
            setTexture(mat2);
        }
        
    }
}
