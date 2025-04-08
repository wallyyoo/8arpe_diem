using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject Front;
    public GameObject Back;

    public Animator anim;
    
    public void setting(int number)
    {
        idx = number;  

    }
   
    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        Front.SetActive(true);
        Back.SetActive(false);   
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
