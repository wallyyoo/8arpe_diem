using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;
    public GameObject endPanel;
    public Text timeTxt;
    public Text nowScore;
    public Text bestScore;

    bool isPlay = true;

    float time = 0.0f;

    string key = "bestScore";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
            if (time > 10.0f)
            {
                endPanel.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
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
    public void GameOver()
    {
        isPlay = false;
        Time.timeScale = 0.0f;
        nowScore.text = time.ToString("N2");

        // �ְ������� �ִٸ�
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            // �ְ� ���� < ���� ����
            if (best < time)
            {
                //���� ������ �ְ� ������ �����Ѵ�.
                PlayerPrefs.SetFloat(key, time);
                bestScore.text = time.ToString("N2");
            }
            else
            {
                bestScore.text = best.ToString("N2");
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time);
            bestScore.text = time.ToString("N2");
        }

        endPanel.SetActive(true);
    }
}
