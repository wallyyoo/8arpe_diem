using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBtn : MonoBehaviour
{
    public SpriteRenderer backImage;

    public int idx2 = 0;



    public void Load()
    {
        SceneManager.LoadScene("MainScene");
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);
    }

    void Start()
    {
        backImage.sprite = Resources.Load<Sprite>($"CardBack{idx2}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
