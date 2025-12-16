using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game3 : MonoBehaviour
{
    public static Game3 instance;

    public Rigidbody2D shark;
    public MoveCamera moveCamera;
    public TextMeshProUGUI totalCandyText;
    public TextMeshProUGUI catchCandyText;

    [Header("GameOver")]
    public GameObject introPanel;
    public GameWin gameWin;
    public GameObject gameOver;
    public GameObject g_winEffect;
    public GameObject obstacleEff;

    public GameObject hand;
    public GameObject[] bubbles;

    public GameObject[] lifeImg;

    private Vector2 startPos;
    private Vector2 endPos;

    private int totalCandy = 30;
    private int _catchCandy = 0;
    public int catchCandy
    {
        get { return _catchCandy; }
        set
        {
            _catchCandy = value;
            catchCandyText.text = value.ToString();
        }
    }

    private int _life;
    public int life
    {
        get { return _life; }
        set
        {
            _life = value;
            for (int i = 1; i <= 3; i++)
            {
                if (value >= i)
                {
                    lifeImg[i - 1].SetActive(true);
                }
                else
                {
                    lifeImg[i - 1].SetActive(false);
                }
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        life = Container.life;

        GlobalScript.instance.CloseLoading();
        totalCandyText.text = totalCandy.ToString();

        if (!Container.showGame3Intro)
            moveCamera.enabled = true;
        introPanel.SetActive(Container.showGame3Intro);
        StartCoroutine(ShowBubbles());

        AdsManager.instance.HideBannerView();
    }

    private IEnumerator ShowBubbles()
    {
        yield return new WaitForSeconds(3.5f);
        bubbles[0].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        bubbles[1].SetActive(true);
    }

    public void HideIntro()
    {
        moveCamera.enabled = true;
        introPanel.SetActive(false);
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public void Retry()
    {
        Container.life = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnBeginDrag()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 diff = startPos - endPos;

        if (diff.y >= 0.6f) // Down
        {
            if (shark.transform.localPosition.y <= 350f && shark.transform.localPosition.y > 1f)
            {
                shark.transform.DOLocalMoveY(0, 0.2f);
                SoundManager.instance.PlaySound(1);
            }
            else if (shark.transform.localPosition.y == 0f)
            {
                shark.transform.DOLocalMoveY(-350, 0.2f);
                SoundManager.instance.PlaySound(1);
            }
        }
        else if (diff.y <= -0.6f) // Up
        {
            if (shark.transform.localPosition.y >= -350f && shark.transform.localPosition.y < 0f)
            {
                shark.transform.DOLocalMoveY(0, 0.2f);
                SoundManager.instance.PlaySound(1);
            }
            else if (shark.transform.localPosition.y <= 0f)
            {
                shark.transform.DOLocalMoveY(350, 0.2f);
                SoundManager.instance.PlaySound(1);
            }
        }
    }

    public void ShowGameWin()
    {
        shark.gravityScale = 0f;

        gameWin.gameObject.SetActive(true);
        gameWin.SetData(totalCandy, catchCandy);
        moveCamera.enabled = false;

        AdsManager.instance?.ShowInterstitialAd();
    }

    public void ShowGameOver()
    {
        moveCamera.enabled = false;
        gameOver.SetActive(true);

        SoundManager.instance.PlaySound(6);

        if (Container.showAds)
            AdsManager.instance?.ShowInterstitialAd();

        Container.showAds = !Container.showAds;
    }

    public void TouchObstacle(Vector2 pos)
    {
        GameObject obj = Instantiate(obstacleEff, pos, Quaternion.identity, null);
        Destroy(obj, 3f);

        moveCamera.enabled = false;

        if (life <= 0)
            StartCoroutine(DoTask(() => { ShowGameOver(); }, 1f));
    }

    private IEnumerator DoTask(Action task, float wait)
    {
        yield return new WaitForSeconds(wait);
        task?.Invoke();
    }
}