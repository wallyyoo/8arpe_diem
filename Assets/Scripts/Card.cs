using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject Front;
    public GameObject Back;

    public SpriteRenderer frontImage;

    public Animator anim;
    
    public void setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }
   
    public void OpenCard()
    {

        anim.SetBool("isOpen", true);
        Front.SetActive(true);
        Back.SetActive(false);

        if (GameManager.instance.firstCard == null) 
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }

    }   

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

   public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen",false);
        Front.SetActive(false);
        Back.SetActive(true);
    }
}
