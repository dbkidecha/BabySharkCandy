using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game1 : MonoBehaviour
{
    public static Game1 instance;

    public Candy candy;
    public Transform parent;
    public GameObject shark;
    public MoveCamera moveCamera;
    public TextMeshProUGUI totalCandyText;
    public TextMeshProUGUI catchCandyText;

    public GameWin gameOver;
    public GameObject g_winEffect;

    [Space(5)]
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

            if (Container.game1Point < value)
                Container.game1Point = value;
        }
    }

    private int _newCandy = 0;
    public int newCandy
    {
        get { return _newCandy; }
        set
        {
            _newCandy = value;
            if (value >= totalCandy)
            {
                CancelInvoke(nameof(GenerateCandy));
                Invoke(nameof(ShowGameOver), 3f);
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
        GlobalScript.instance.CloseLoading();
        InvokeRepeating(nameof(GenerateCandy), 1f, 2f);
        totalCandyText.text = totalCandy.ToString();

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

    private void GenerateCandy()
    {
        Transform pos = parent.GetChild(Random.Range(0, parent.childCount)).transform;
        Candy obj = Instantiate(candy, pos);
        obj.transform.position = pos.position;
        newCandy++;

        if (newCandy >= 5)
            obj.RandomGravity();
    }

    private void ShowGameOver()
    {
        shark.SetActive(false);        

        gameOver.gameObject.SetActive(true);
        gameOver.SetData(totalCandy, catchCandy);
        moveCamera.enabled = false;

        AdsManager.instance?.ShowInterstitialAd();
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }
}