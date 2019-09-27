// using UnityEngine;
// using System.Collections;
// using GooglePlayGames;
// using UnityEngine.SocialPlatforms;
// using UnityEngine.UI;

// public class LeaderboardControllerGameplay : MonoBehaviour {

// 	public const string LEADERBOARD_ID = "CgkI5Jf17LwCEAIQBw";
	
// 	[SerializeField]
// 	private Text highscoreText;
	
// 	void Awake(){
// 		PlayGamesPlatform.Activate();
// 	}//start
	
// 	public void OpenLeaderboard(){
// 		if(NormalDroidPlayer.score < 11){
// 			highscoreText.text = "Atleast 11 is required to be on leaderboard!";
// 		} else {
// 			highscoreText.text = "Signing In...";
// 			if(Social.localUser.authenticated){
// 				PostScore();
// 			} else {
// 				Social.localUser.Authenticate((bool success) => {
// 					if(success){
// 						highscoreText.text = "Signed In!";
// 						PostScore();
// 					}
// 				});
// 			}
// 		}
// 	}//openLeaderboard
	
	
// 	public void PostScore(){
// 		Social.ReportScore(NormalDroidPlayer.score, LEADERBOARD_ID, (bool success) => {
// 			if(success){
// 				//successfully score posted, show show the leaderboard
// 				highscoreText.text = "Current score is posted! Thanks!";
// 				((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (LEADERBOARD_ID);
// 			} else {
// 				highscoreText.text = "Connection Lost!";
// 			}
// 		});
// 	}//postScore
// }
