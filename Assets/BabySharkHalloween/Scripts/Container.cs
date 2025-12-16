using System.Collections.Generic;
using UnityEngine;

public static class Container
{
    public static int rateCount = 0;
    public static RequestDialog requestedDialog;
    public static List<int> noBannerInScene = new List<int>() { 0, 1, 8, 9, 10 };

    public static bool rated
    {
        get { return PlayerPrefs.GetInt("rated", 0) == 0 ? false : true; }
        set { PlayerPrefs.SetInt("rated", value ? 1 : 0); }
    }

    public static bool privacyAgreed
    {
        get { return PlayerPrefs.GetInt("privacyAgreed", 0) == 0 ? false : true; }
        set { PlayerPrefs.SetInt("privacyAgreed", value ? 1 : 0); }
    }

    public static int music
    {
        get { return PlayerPrefs.GetInt("music", 1); }
        set { PlayerPrefs.SetInt("music", value); }
    }

    public static int noAds
    {
        get { return PlayerPrefs.GetInt("noAds", 0); }
        set
        {
            PlayerPrefs.SetInt("noAds", value);

            if (value.Equals(1) && AdsManager.instance != null)
                AdsManager.instance.DestroyBannerView();
        }
    }

    public static int gamePlayed
    {
        get { return PlayerPrefs.GetInt("gamePlayed", 0); }
        set { PlayerPrefs.SetInt("gamePlayed", value); }
    }

    public static int game1Point
    {
        get { return PlayerPrefs.GetInt("game1Point", 0); }
        set { PlayerPrefs.SetInt("game1Point", value); }
    }

    public static int game2Point
    {
        get { return PlayerPrefs.GetInt("game2Point", 0); }
        set { PlayerPrefs.SetInt("game2Point", value); }
    }

    public static int life = 3;
    public static bool showGame3Intro = true;
    public static bool game1Hand = true;
    public static bool showAds = false;

    public static void RateUs()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/app/baby-shark-candy-challenge/id6670761697");
#endif
    }

    public static void PrivacyPolicy()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://www.appletreeappstudio.com/privacyPolicy.html");
#elif UNITY_IOS
        Application.OpenURL("https://www.appletreeappstudio.com/privacyPolicy.html");
#endif
    }

    public static void TermsOfUse()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://www.appletreeappstudio.com/termsOfUse.html");
#elif UNITY_IOS
        Application.OpenURL("https://www.appletreeappstudio.com/termsOfUse.html");
#endif
    }
}

public enum RequestDialog
{
    None = 0,
    Settings = 1,
    RemoveAds = 2,
    RateUs = 3
}