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

            // firstCard�� ����ٸ�
        if (GameManager.instance.firstCard == null)
        {   
            // firstCard�� ������ �־��ְ�
            GameManager.instance.firstCard = this;
        }
        else//firstCard�� ������� �ʴٸ�

        {   //secondCard�� ������ �Ѱ��ְ�
            GameManager.instance.secondCard = this;

            //matched �Լ��� �ҷ��´�
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
