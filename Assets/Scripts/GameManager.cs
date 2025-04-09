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

    public int cardCount = 0;

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

        }
    }
    public void Matched() //카드를 대조하는 함수
    {
        if (firstCard.idx == secondCard.idx) //첫째 둘째 카드가 같다면
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();//파괴해라
            cardCount -= 2;
            if(cardCount == 0)
            {
                endPanel.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }

        else // 아니라면
        {
            firstCard.CloseCard();
            secondCard.CloseCard();// 닫아라
        }
        firstCard = null;
        secondCard = null; //카드 정보 초기화
    }
    public void GameOver()
    {
        isPlay = false;
        Time.timeScale = 0.0f;
        nowScore.text = time.ToString("N2");

        // 최고점수가 있다면
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            // 최고 점수 < 현재 점수
            if (best < time)
            {
                //현재 점수를 최고 점수에 저장한다.
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
