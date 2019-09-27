// using UnityEngine;
// using System.Collections;
// using GoogleMobileAds;
// using GoogleMobileAds.Api;
// using GoogleMobileAds.Common;

// public class AdManager : MonoBehaviour {
	
// 	public static AdManager instance; 
	
// 	public string BANNERID = "ca-app-pub-5840485895093255/6542498923";
// 	public string INTERID = "ca-app-pub-5840485895093255/8019232124";
	
// 	public BannerView bannerView;
// 	public InterstitialAd interstitialView;
	
// 	void Awake(){
// 		MakeSingleton();
// 	}//awake
	
// 	void MakeSingleton(){
// 		if(instance != null){
// 			DestroyImmediate(gameObject);
// 		} else {
// 			instance = this;
// 			DontDestroyOnLoad(gameObject);
// 		}
// 	}//makeSingleton
	
// 	void Start(){
// 		//NotificationManager.ShowNotification("Loading Ads");
// 		LoadAds();
// 	}//start
	
// 	void LoadAds(){
// 		bannerView = new BannerView(BANNERID, AdSize.Banner,AdPosition.Top);
// 		bannerView.LoadAd(createRequest());
// 		interstitialView = new InterstitialAd(INTERID);
// 		interstitialView.LoadAd(createRequest());
// 	}//loadAds
	
// 	void DestroyAds(){
// 		bannerView.Destroy();
// 		interstitialView.Destroy();
// 	}//DestoryAds
	
// 	void OnLevelWasLoaded(){
// 		int random = Random.Range(0,100) % 5;
// 		//NotificationManager.ShowNotification("New Level Loaded");
// 		if(Application.loadedLevelName == "MainMenuStaging" ){
// 			if(random == 0){
// 				//NotificationManager.ShowNotification("loading Inter Ad");
// 				ShowInterAd();
// 			} else if (random == 1){
// 				//NotificationManager.ShowNotification("loading banner Ad");
// 				ShowBannerAd();
// 			} else {
// 				//NotificationManager.ShowNotification("loading new ads");
// 				DestroyAds();
// 				LoadAds();
// 			}
// 		} else {
// 			bannerView.Hide();
// 		}
// 	}//OnLevelWasLoaded
	
// 	AdRequest createRequest(){
// 		return new AdRequest.Builder().Build();
// 	}//createRequest
	
// 	public void ShowBannerAd(){
// 		//NotificationManager.ShowNotification("banner ad should be loaded now");
// 		if(bannerView != null)
// 			bannerView.Show();
// 	}//showBannerAd
	
// 	public void ShowInterAd(){
// 		if(interstitialView.IsLoaded()){
// 			interstitialView.Show();
// 			//NotificationManager.ShowNotification("Should be seeing a inter ad");
// 		}
// 	}//showBannerAd
	
	
	
// }
