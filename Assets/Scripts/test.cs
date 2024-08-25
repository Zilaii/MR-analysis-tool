using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Bindings;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.IO;

public class test : MonoBehaviour
{
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        var info = new DirectoryInfo("Assets\\Video");
        var fileInfo = info.GetFiles();
        text.text = "";
        foreach (var file in fileInfo)
        {
            if (!file.Name.EndsWith(".meta"))
            {
                text.text += file.Name +"\n";
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}
