using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] Cards; //
    public Card firstCard;
    public Card secondCard;
    public GameObject endPanel;
    public GameObject Profile;
    public GameObject Resume;
    public GameObject Pause;
    public Text timeTxt;
    public Text nowScore;
    public Text bestScore;
    public Text endTitle;
    public Text hint2Btn;//

    public GameObject hint2;//
    public int[] Hint2Num;//

    AudioSource audioSource;
    public AudioClip success;
    public AudioClip fail;
    public AudioClip congrate;
    public AudioClip game_over;

    public int cardCount = 0;
    public int OpenedCard = 0;

    bool isPlay = true;
    bool isHint2;

    float time = -2.35f;

    public string key = "bestScore";
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

        Cards = new GameObject[20];
        Hint2Num = new int[2];
        isHint2 = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            if (cardCount != 0)
                time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
            if (time > endtime)
            {
                endPanel.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
                GameOver();
            }
        }

        if (time >= 0.0f)
        {
            timeTxt.enabled = true;
        }

        if (time >= 45.0f && !isHint2)
        {
            Hint2Enable();
            StartCoroutine(BlinkIn());
        }

        if (cardCount == 10 && !isHint2)
        {
            Hint2Enable();
            StartCoroutine(BlinkIn());
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

            if (cardCount == 0)
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
                    bestScore.text = time.ToString("N2");
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
                bestScore.text = time.ToString("N2");
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

    public void Hint2()
    {
        StopAllCoroutines();
        hint2.GetComponent<SpriteRenderer>().color = new Color(0.1698113f, 0.1319729f, 0.1273585f, 1.0f);
        int[] arr = new int[2];
        for (int i = 0; i < 20; i++)
        {
            if (Cards[i] == null)
            {

                continue;
            }
            else if (Cards[i] != null)
            {
                int num = Cards[i].GetComponent<Card>().idx;
                arr = Hint2Find(num, i);
                break;
            }
        }
        Cards[arr[0]].GetComponent<Card>().OpenCard();
        Cards[arr[1]].GetComponent<Card>().OpenCard();
        Hint2Destroy(arr);
    }

    int[] Hint2Find(int num, int i)
    {
        int[] arr = new int[2];
        arr[0] = i;
        for (int j = i + 1; j < 20; j++)
        {
            if (Cards[j] != null && Cards[j].GetComponent<Card>().idx == num)
            {
                arr[1] = j;
                return arr;
            }
        }
        return null;
    }
    void Hint2Destroy(int[] arr)
    {
        Cards[arr[0]].GetComponent<Card>().DestroyCard();
        Cards[arr[1]].GetComponent<Card>().DestroyCard();
        hint2Btn.enabled = false;
    }

    void Hint2Enable()
    {
        hint2Btn.enabled = true;
        isHint2 = true;
        hint2.GetComponent<SpriteRenderer>().color = new Color(0.9215686f, 0.6f, 0.5607843f, 1.0f);
    }

    public IEnumerator BlinkIn()
    {
        for (float g = 0f; g < 60; g += 0.0001f)
        {
            for (float f = 1f; f > 0.5f; f -= 0.002f)
            {
                Color c = hint2.GetComponent<SpriteRenderer>().color;
                c.a = f;
                hint2.GetComponent<SpriteRenderer>().color = c;
                yield return null;
            }
            yield return new WaitForSeconds(0.05f);

            for (float f = 0.5f; f < 1f; f += 0.002f)
            {
                Color c = hint2.GetComponent<SpriteRenderer>().color;
                c.a = f;
                hint2.GetComponent<SpriteRenderer>().color = c;
                yield return null;
            }
        }
    }
}
