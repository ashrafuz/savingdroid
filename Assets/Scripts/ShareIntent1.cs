using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ShareIntent1 : MonoBehaviour {
	// public
	// private
	private bool isProcessing = false;
	public Image buttonShare;
	public string message,subject;
	
	void Awake(){
		message = "Playing #SavingPrivateDroid ! Check this out: https://goo.gl/U0ykvw";
		subject = "Saving Private Droid";
	}//awake
	
	public void ButtonShare ()
	{
		buttonShare.enabled = false;
		if(!isProcessing){
			StartCoroutine( ShareScreenshot() );
		}
	}
	public IEnumerator ShareScreenshot()
	{
		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame();

		if(!Application.isEditor)
		{
			// block to open the file and share it ------------START
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
		isProcessing = false;
		buttonShare.enabled = true;
	}
}