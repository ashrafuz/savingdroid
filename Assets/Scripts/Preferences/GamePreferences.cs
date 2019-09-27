using UnityEngine;
using System.Collections;

public class GamePreferences : MonoBehaviour {
	
	GamePreferences instance;
	
	//player personal records
	public static string HIGH_SCORE_KEY = "high_score";

	
	//gameplay elements
	public static string GAME_PLAYED_ATLEAST_ONCE = "game_played_atleast_once";
	public static string IS_SOUND_ON_OR_OFF = "sound_option";
	
	
	//GETTERS
	public static int GetHighScore(){
		return PlayerPrefs.GetInt(GamePreferences.HIGH_SCORE_KEY);
	}//getHighScore
	
	public static int GetSoundSetting(){
		return PlayerPrefs.GetInt(GamePreferences.IS_SOUND_ON_OR_OFF);
	}//getSoundSettings
	
	
	//SETTERS
	public static void SetHighScore(int value){
		PlayerPrefs.SetInt(GamePreferences.HIGH_SCORE_KEY, value);
	}//setHighScore
	
	public static void SetSoundSetting(int value){
		PlayerPrefs.SetInt(GamePreferences.IS_SOUND_ON_OR_OFF, value);
	}//setSoundSetting
	
	
	void Awake(){
		MakeSingleton();
		
		if(PlayerPrefs.HasKey(GamePreferences.GAME_PLAYED_ATLEAST_ONCE)){ 
			// game has been played atleast once
		} else {
			GamePreferences.SetHighScore(0);
			GamePreferences.SetSoundSetting(1);
			PlayerPrefs.SetInt(GamePreferences.GAME_PLAYED_ATLEAST_ONCE, 1);
		}
		
	}//awake
	
	void MakeSingleton(){
		if(instance != null){
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}//makesingleton
}
