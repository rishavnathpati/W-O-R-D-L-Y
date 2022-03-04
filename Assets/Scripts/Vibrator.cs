using UnityEngine;

public class Vibrator
{
#if UNITY_ANDROID && !UNITY_EDITOR
   public static readonly AndroidJavaClass unityPlayer = new AndroidJavaClass("com.rishavnathpati.Wordle.UnityPlayer");
   public static readonly AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
   public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif


    private static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    public static void Vibrate(long milliseconds = 250)
    {
        if (IsAndroid())
            
            vibrator.Call("vibrate", milliseconds);
    }

    public static void Cancel()
    {
        if (IsAndroid()) vibrator.Call("cancel");
    }
}