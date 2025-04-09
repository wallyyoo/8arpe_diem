using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBtn : MonoBehaviour
{
    bool m_bPause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_bPause == true)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Pause()
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
