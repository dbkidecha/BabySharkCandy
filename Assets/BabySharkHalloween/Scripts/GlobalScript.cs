using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript instance;

    [SerializeField] private GameObject loadScreen;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScreen(int screenNo)
    {
        loadScreen.SetActive(true);
        loadScreen.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
        StartCoroutine(ChangeScene(screenNo, 1f));
    }

    public IEnumerator ChangeScene(int screenNo, float wait)
    {
        yield return new WaitForSeconds(wait);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(screenNo);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void CloseLoading()
    {
        StartCoroutine(IE_CloseLoading());
        //if (!Container.noBannerInScene.Contains(SceneManager.GetActiveScene().buildIndex))
        //{
        //    AdsManager.instance?.ShowBannerView();
        //}
    }

    private IEnumerator IE_CloseLoading()
    {
        yield return new WaitForSeconds(0.5f);
        loadScreen.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
    }
}