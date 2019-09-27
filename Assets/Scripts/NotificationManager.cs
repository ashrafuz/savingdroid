using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

	public static NotificationManager instance;
	public static string msg;
	
	void Awake(){
		msg = "Welcome!";
		MakeSingleton();
	}//awake
	
	void MakeSingleton(){
		if(instance != null){
			DestroyImmediate(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}//makeSingleton
	
	public static void ShowNotification(string message){
		msg = message;
	}//ShowNotification
	
}
