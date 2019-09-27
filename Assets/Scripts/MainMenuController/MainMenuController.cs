using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private Image soundButton;
	
	[SerializeField]
	private Text notificationText;
	
	private Color fade, normal;
	
	//for sharing
	private bool isProcessing = false;
	public string message,subject;
	
	void Awake(){
		bool isNowActive = GamePreferences.GetSoundSetting() == 1 ? true : false;
		fade = new Color(soundButton.color.r, soundButton.color.g, soundButton.color.b,0.35f);
		normal = new Color(soundButton.color.r, soundButton.color.g, soundButton.color.b,1f);
		
		if(isNowActive){
			soundButton.color = normal;
		}else{
			soundButton.color = fade;
		}
		int highScore = GamePreferences.GetHighScore();
		if(highScore>0){
			NotificationManager.msg  = "Highscore " + highScore;
		} else {
			NotificationManager.msg = "";
		}
		
		message = "Playing #SavingPrivateDroid ! Check this out: https://goo.gl/U0ykvw";
		subject = "Saving Private Droid";
	}//awake
	
	void Update(){
		notificationText.text = NotificationManager.msg;
		
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	public void OnSoundButtonClick(){
		bool isNowActive = GamePreferences.GetSoundSetting() == 1 ? true : false;
		//toggle
		if(isNowActive){
			isNowActive = false;
			soundButton.color = fade;
			GamePreferences.SetSoundSetting(0);
		} else {
			isNowActive = true;
			soundButton.color = normal;
			GamePreferences.SetSoundSetting(1);
		}
	}
	
	public void OnPlayButtonClick(){
		Application.LoadLevel(AllSceneNames.SAVE_DROID);
	}//playButton
	
	public void OnExitButtonClick(){
		Application.Quit();
	}//leaderboardbutton
	
	
	public void OnRateUsButtonClick(){
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.thebinarywolf.saving_private_droid");
	}
	
	
	public void OnShareButtonClick(){
		if(!Application.isEditor){
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			
			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ""+ message);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
			
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			
			currentActivity.Call("startActivity", intentObject);
		}
	}
}
