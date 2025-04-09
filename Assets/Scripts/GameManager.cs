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
    public GameObject Profile;
    public Text timeTxt;
    public Text nowScore;
    public Text bestScore;
    public Text endTitle;

    AudioSource audioSource;
    public AudioClip success;
    public AudioClip fail;
    public AudioClip congrate;
    public AudioClip game_over;

    public int cardCount = 0;

    bool isPlay = true;

    float time = -2.35f;

    string key = "bestScore";
    public int endtime;

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
        //PlayerPrefs.DeleteKey(key);
        timeTxt.enabled = false;
        
        audioSource = GetComponent<AudioSource>();
       AudioManager.instance.audioSource.Play();
            }


    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
            if (time > endtime)
            {
                endPanel.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
                GameOver();
            }
        }

        if(time >= 0.0f)
        {
            timeTxt.enabled = true;
        }
    }
    public void Matched() //카드를 대조하는 함수
    {
        if (firstCard.idx == secondCard.idx) //첫째 둘째 카드가 같다면
        {
            StartCoroutine(PlayDelayedSound(success));
            firstCard.DestroyCard();
            secondCard.DestroyCard();//파괴해라
            cardCount -= 2;

            if(cardCount == 0)
            {

                Invoke("GameOver", 1.0f);
            }
        }

        else // 아니라면
        {
            StartCoroutine(PlayDelayedSound(fail));
            firstCard.CloseCard();
            secondCard.CloseCard();// 닫아라
        }
        firstCard = null;
        secondCard = null; //카드 정보 초기화
    }

    public void GameOver()
    {
        AudioManager.instance.audioSource.Stop();

        isPlay = false;
        Time.timeScale = 0.0f;
        endPanel.gameObject.SetActive(true);


        // 최고점수가 있다면
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            // 최고 점수 < 현재 점수
            if (cardCount == 0)
            {
                audioSource.PlayOneShot(congrate);
              
                if (best > time)
                {
                    //현재 점수를 최고 점수에 저장한다.
                    PlayerPrefs.SetFloat(key, time);
                    endTitle.text = "게임 클리어!";
                    bestScore.text = best.ToString("N2");
                    nowScore.text = time.ToString("N2");
                    Profile.SetActive(true);
                    endPanel.transform.position = new Vector2(380, 390);
                }
                else
                {
                    endTitle.text = "게임 클리어!";
                    bestScore.text = best.ToString("N2");
                    nowScore.text = time.ToString("N2");
                    Profile.SetActive(true);
                    endPanel.transform.position = new Vector2(380, 390);
                }
                
            }
            else
            {
                audioSource.PlayOneShot(game_over);

                endTitle.text = "게임 오버";
                bestScore.text = best.ToString("N2");
                nowScore.text = "시간 초과";
            }
        }
        else
        {
            float best = PlayerPrefs.GetFloat(key);
            if (cardCount == 0)
            {
                audioSource.PlayOneShot(congrate);

                endTitle.text = "게임 클리어!";
                PlayerPrefs.SetFloat(key, time);
                bestScore.text = best.ToString("N2");
                nowScore.text = time.ToString("N2");
                Profile.SetActive(true);
                endPanel.transform.position = new Vector2(380, 390);
            }
            else
            {
                audioSource.PlayOneShot(game_over);

                endTitle.text = "게임 오버";
                bestScore.text = best.ToString("N2");
                nowScore.text = "시간 초과";
            }
        }
        endPanel.SetActive(true);
    }

    public IEnumerator PlayDelayedSound(AudioClip clip)
    {
        yield return new WaitForSeconds(0.6f);
        audioSource.PlayOneShot(clip);
    }
}
