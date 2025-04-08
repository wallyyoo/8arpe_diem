using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Matched() //ī�带 �����ϴ� �Լ�
    {
        if (firstCard.idx == secondCard.idx) //ù° ��° ī�尡 ���ٸ�
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();//�ı��ض�
        }

        else // �ƴ϶��
        {
            firstCard.CloseCard();
            secondCard.CloseCard();// �ݾƶ�
        }
        firstCard = null;
        secondCard = null; //ī�� ���� �ʱ�ȭ
    }
}
