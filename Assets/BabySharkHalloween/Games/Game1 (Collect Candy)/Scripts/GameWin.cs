using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    public int levelNo = 1;
    public TextMeshProUGUI g_catchCandyText;
    public TextMeshProUGUI g_missedCandyText;
    public TextMeshProUGUI g_messageText;
    public TextMeshProUGUI g_titleText;
    public Transform g_stars;
    public Image g_basketImg;
    public GameObject retryBtn, nextBtn, playAgainBtn, superbBtn;
    public Transform emojiList;
    public Sprite[] g_baskets;

    public void SetData(int totalCandy, int catchCandy)
    {
        g_catchCandyText.text = catchCandy.ToString();
        g_missedCandyText.text = (totalCandy - catchCandy).ToString();

        int length = 1;
        if (catchCandy <= 7)
        {
            g_messageText.text = "Retry";
            length = 1;
        }
        else if (catchCandy >= 8 && catchCandy <= 10)
        {
            g_messageText.text = "Retry";
            length = 1;
        }
        else if (catchCandy >= 11 && catchCandy <= 15)
        {
            g_messageText.text = "Retry";
            length = 2;
        }
        else if (catchCandy >= 16 && catchCandy <= 20)
        {
            g_messageText.text = "Good";
            length = 3;
        }
        else if (catchCandy >= 21 && catchCandy <= 25)
        {
            g_messageText.text = "Superb";
            length = 4;
        }
        else if (catchCandy >= 26)
        {
            g_messageText.text = "You Win";
            length = 5;
        }

        for (int i = 0; i < length; i++)
        {
            g_stars.GetChild(i).gameObject.SetActive(true);
        }
        if (length >= 4)
        {

            if (levelNo.Equals(1))
            {
                Game1.instance.g_winEffect.gameObject.SetActive(length >= 4);
                if (length > 5)
                    SoundManager.instance.PlaySound(3);
                else
                    SoundManager.instance.PlaySound(2);
            }
            else if (levelNo.Equals(2))
            {
                Game2.instance.g_winEffect.gameObject.SetActive(length >= 4);
                if (length > 5)
                    SoundManager.instance.PlaySound(5);
                else
                    SoundManager.instance.PlaySound(4);
            }
            else if (levelNo.Equals(3))
            {
                Game3.instance.g_winEffect.gameObject.SetActive(length >= 4);
                if (length > 5)
                    SoundManager.instance.PlaySound(5);
                else
                    SoundManager.instance.PlaySound(4);
            }
        }
        if (levelNo < 3)
        {
            retryBtn.SetActive(length <= 4);
            nextBtn.SetActive(length > 2);
        }
        else
        {
            retryBtn.SetActive(length <= 2);
            playAgainBtn.SetActive(length > 2 && length < 5);
            superbBtn.SetActive(length > 4);
        }
        g_basketImg.sprite = g_baskets[catchCandy <= 7 ? 0 : length];
        g_titleText.text = length > 2 ? "Level " + levelNo + " Completed!" : "Level " + levelNo + " Failed!";
        emojiList.GetChild(length).gameObject.SetActive(true);
    }

    public void Exit()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public void Retry()
    {
        Container.life = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}