using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBtn : MonoBehaviour
{
    public GameObject Resume;
    public GameObject Pause;
    bool m_bPause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_bPause == true)
        {
            Resume.SetActive(true);
            Pause.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        { 
            Resume.SetActive(false);
            Pause.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void Paused()
    {
        if (m_bPause == false)
        {
            this.m_bPause = true;
        }
        else
        {
            this.m_bPause = false;
        }
    }
}
