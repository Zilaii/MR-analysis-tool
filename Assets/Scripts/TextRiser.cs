using TMPro;
using UnityEngine;

public class TextRiser : MonoBehaviour
{
    public TextMeshPro actionText;
    Vector3 tempPos = new Vector3();
    public System.DateTime m_EndTime;
    private double m_TimeToStayAlive = 3;
    public bool m_IsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        m_EndTime = System.DateTime.Now.AddSeconds(m_TimeToStayAlive);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_EndTime.CompareTo(System.DateTime.Now) < 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!m_IsPaused)
            {
                m_IsPaused = true;
            } else
            {
                m_IsPaused = false;
            }
        }


        if (!m_IsPaused)
        {
            tempPos = this.gameObject.transform.position;
            tempPos.y = this.gameObject.transform.position.y + 0.005f;
            transform.position = tempPos;
        } 

    }

    private void FixedUpdate()
    {
        if (m_IsPaused)
        {
            m_EndTime = m_EndTime.AddSeconds(Time.fixedDeltaTime);
        }
    }

    public void CurrentLogHasChanged()
    {
        Destroy(gameObject);
    }
}
