using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Game IDs (from Unity Dashboard)")]
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;

    [Header("Ad Settings")]
    [SerializeField] private bool isTesting = true;

    private string gameId;

    private void Awake()
    {
        // Choose correct Game ID
#if UNITY_IOS
        gameId = iosGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
        gameId = androidGameId; // Use Android ID in Editor
#endif

        // Initialize only if not already done
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Debug.Log($"Initializing Unity Ads with Game ID: {gameId} | TestMode: {isTesting}");
            Advertisement.Initialize(gameId, isTesting, this);
        }
        else
        {
            Debug.Log("Unity Ads already initialized.");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("✅ Unity Ads Initialized Successfully");

        // Safely load ad units after init (if AdsManager is present)
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.bannerAds?.LoadBannerAd();
            AdsManager.Instance.interstitialAds?.LoadInterstitialAd();
            AdsManager.Instance.rewardedAds?.LoadRewardedAd();
        }
        else
        {
            Debug.LogWarning("⚠️ AdsManager.Instance is null — cannot load ad units.");
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"❌ Unity Ads Initialization Failed: {error} - {message}");
    }
}
