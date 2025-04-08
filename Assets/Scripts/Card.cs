using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    public float[] arr = { 0.0f, 0.0f };

    public GameObject Front;
    public GameObject Back;

    public SpriteRenderer frontImage;

    public Animator anim;

    Vector2 vel = new Vector2(0, 0);
    void Update()
    {
        Invoke("MoveCard", 0.8f);
    }
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

    public void GetNum(float x, float y)
    {
        arr[0] = x;
        arr[1] = y;
    }

    void MoveCard()
    {
        transform.position = Vector2.SmoothDamp(gameObject.transform.position, new Vector2(arr[0], arr[1]), ref vel, 0.3f);
    }
}
