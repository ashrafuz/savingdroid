// using UnityEngine;
// using System.Collections;
// using GooglePlayGames;
// using UnityEngine.SocialPlatforms;

// public class LeaderboardController : MonoBehaviour {

// 	public const string LEADERBOARD_ID = "CgkI5Jf17LwCEAIQBw";
	
// 	void Start(){
// 		PlayGamesPlatform.Activate();
// 	}//start
	
// 	public void OpenLeaderboard(){
// 		NotificationManager.msg = "Signing Into Leaderboard";
// 		if(Social.localUser.authenticated){
// 			NotificationManager.msg = "Post your own score while playing the game!";
// 			((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (LEADERBOARD_ID);
// 		} else {
// 			Social.localUser.Authenticate((bool success) => {
// 				if(success){
// 					NotificationManager.msg = "Signed In Successfully!";
// 					((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (LEADERBOARD_ID);
// 					NotificationManager.msg = "Post your own score while playing the game";
// 				} else {
// 					NotificationManager.msg = "Connection Lost!";
// 				}
// 			});
// 		}
// 	}//openLeaderboard
// }//class
