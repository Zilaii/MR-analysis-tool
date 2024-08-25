using UnityEngine;


public class Timeline : MonoBehaviour
{

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 mouseLook = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log("Mouse Position: " + mousePos + ", Mouse Look: " + mouseLook);
        }
    }

}
