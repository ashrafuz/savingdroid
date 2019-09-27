using UnityEngine;
using System.Collections;

public class CoinConfirmationText : MonoBehaviour {


	public static bool isCoinCollected; 
	public static float coinPositionX,coinPositionY;
	
	private bool isAnimRunning;
	private GameObject coinPointConfrimText;
	
	void Awake () {
		coinPointConfrimText = GameObject.FindGameObjectWithTag("coin_collected_text") as GameObject;
		coinPointConfrimText.SetActive(false);
		isCoinCollected = false;
		isAnimRunning = false;
		coinPositionX = 0f;
		coinPositionY = -1f;
	}// awake
	

	void Update () {
		
		if(isCoinCollected){
			isCoinCollected = false;
			var temp = coinPointConfrimText.transform.position;
			temp.x = coinPositionX;
			temp.y = coinPositionY;
			coinPointConfrimText.transform.position = temp;
			//print("coinPosition " + coinPositionX);
			coinPointConfrimText.SetActive(true);
			isAnimRunning = true;
		}
		
		if(isAnimRunning){
			var temp = coinPointConfrimText.transform.position;
			temp.y += Time.deltaTime;
			coinPointConfrimText.transform.position = temp;
			if(temp.y > 1f){
				//print("Anim Stopped ");
				isAnimRunning=false;
				coinPointConfrimText.SetActive(false);
			}
		}
	}//update
}
