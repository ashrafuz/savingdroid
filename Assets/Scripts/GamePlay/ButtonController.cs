using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
	
	[SerializeField]
	private GameObject pausePanel,gameOverPanel,startPanel, player,platform,playerMovementPanel;
	
	[SerializeField]
	private Text gameOverScoreText,gameOverHighScoreText, pausePanelScoreText;
	
	//should be fixed
	public static bool setGameOverPanel=false;

	public void OnStartButtonClick(){
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		startPanel.SetActive(false);
		player.SetActive(true);
		platform.SetActive(true);
		playerMovementPanel.SetActive(true);
		Time.timeScale = 1f;
	}
	
	public void OnPauseButtonClick(){
		Time.timeScale = 0f;
		pausePanel.SetActive(true);
		pausePanelScoreText.text = "Score "+ NormalDroidPlayer.score ;
		player.SetActive(false);
		platform.SetActive(false);
		playerMovementPanel.SetActive(false);
	}
	
	public void OnResumeButtonClick(){
		pausePanel.SetActive(false);
		player.SetActive(true);
		platform.SetActive(true);
		playerMovementPanel.SetActive(true);
		Time.timeScale = 1f;
	}
	
	public void OnGoBackToMainMenuButtonClick(){
		Time.timeScale = 1f;
		Application.LoadLevel(AllSceneNames.MAIN_MENU);
	}
	
	public void OnPlayAgainButtonClick(){
		Time.timeScale = 1f;
		Application.LoadLevel(AllSceneNames.SAVE_DROID);
	}
	
	public void OnGameOverShareButtonClick(){
		string message = "Hey! I scored "+NormalDroidPlayer.score+" Playing #SavingPrivateDroid ! Check this out here https://goo.gl/U0ykvw ";
		string subject = "Saving Private Droid";
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
	
	void Awake(){
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		player.SetActive(false);
		platform.SetActive(false);
		playerMovementPanel.SetActive(false);
		startPanel.SetActive(true);
		setGameOverPanel = false;
		Time.timeScale = 0f;
	}
	
	void FixedUpdate(){
		if(NormalDroidPlayer.isGameOver && setGameOverPanel){ // gameOver
			setGameOverPanel = false;
			gameOverPanel.SetActive(true);
			player.SetActive(false);
			platform.SetActive(false);
			playerMovementPanel.SetActive(false);
			int highScore = 0;
			if(PlayerPrefs.HasKey(GamePreferences.HIGH_SCORE_KEY)){
				highScore = GamePreferences.GetHighScore();
			}
			int score = NormalDroidPlayer.score;
			if(score > highScore){
				gameOverScoreText.text = "YOUR NEW HIGHSCORE!";
				gameOverHighScoreText.text = "Highscore : " + score;
				GamePreferences.SetHighScore(score);
			} else {
				gameOverScoreText.text = "Score : " + score;
				gameOverHighScoreText.text = "High Score : " + highScore;
			}
		}
	}
}
