using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBtn : MonoBehaviour
{
    public SpriteRenderer backImage;

    public int idx2 = 1;



    public void Load()
    {
        SceneManager.LoadScene("MainScene");
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);
    }

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(this, this.transform);
        }
        
        backImage.sprite = Resources.Load<Sprite>($"CardBack{idx2}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
