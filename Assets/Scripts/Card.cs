using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        Back.SetActive(false);

            // firstCard가 비었다면
        if (GameManager.instance.firstCard == null)
        {   
            // firstCard에 정보를 넣어주고
            GameManager.instance.firstCard = this;
        }
        else//firstCard가 비어있지 않다면

        {   //secondCard에 정보를 넘겨주고
            GameManager.instance.secondCard = this;

            //matched 함수를 불러온다
            GameManager.instance.Matched();
        }

     }

    public void DestoryCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
