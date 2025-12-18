using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game2 : MonoBehaviour
{
    public static Game2 instance;

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
    
    private int totalCandy = 30;
    private int _catchCandy = 0;
    public int catchCandy
    {
        get { return _catchCandy; }
        set
        {
            _catchCandy = value;
            catchCandyText.text = value.ToString();

            if (Container.game2Point < value)
                Container.game2Point = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalScript.instance.CloseLoading();
        totalCandyText.text = totalCandy.ToString();

        introPanel.SetActive(true);
        StartCoroutine(ShowBubbles());

        AdsManager.instance.ChangeBannerView(GoogleMobileAds.Api.AdPosition.Top);
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

    public void Tap()
    {
        if (shark.transform.localPosition.y < 400)
        {
            shark.gravityScale = 0.8f;
            shark.linearVelocity = Vector2.zero;
            shark.AddForce(Vector2.up * 200);
            SoundManager.instance.PlaySound(1);
        }
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameWin()
    {
        shark.gravityScale = 0f;
        shark.constraints = RigidbodyConstraints2D.FreezePositionY;

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
        shark.constraints = RigidbodyConstraints2D.FreezePositionY;

        StartCoroutine(DoTask(() => { ShowGameOver(); }, 1f));
    }

    private IEnumerator DoTask(Action task, float wait)
    {
        yield return new WaitForSeconds(wait);
        task?.Invoke();
    }
}