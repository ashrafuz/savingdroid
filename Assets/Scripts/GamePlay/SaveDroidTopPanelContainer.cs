using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveDroidTopPanelContainer : MonoBehaviour {

	[SerializeField]
	private Button pauseButton;
	
	[SerializeField]
	private Text scoreText;
	
	[SerializeField]
	private Image sheildMeter;
	
	private int score, sheildTime;
	private float sheildMeterHeight, sheildMeterWidth;
	
	void Awake () {
		
		score = NormalDroidPlayer.score;
		scoreText.text = "Score : " + score;
		
		sheildMeterHeight = sheildMeter.rectTransform.sizeDelta.y;
		sheildMeterWidth = 5f;
		
		sheildMeter.rectTransform.sizeDelta = new Vector2 (0f,sheildMeterHeight);
	}//awake
	
	void FixedUpdate () {
		score = NormalDroidPlayer.score;
		scoreText.text = "Score : " + score;
		if(NormalDroidPlayer.isSheildActive){
			sheildMeter.rectTransform.sizeDelta = new Vector2
				(NormalDroidPlayer.timeLeftForSheild*sheildMeterWidth,
				 sheildMeterHeight);
		} else {
			sheildMeter.rectTransform.sizeDelta = new Vector2 (0f,sheildMeterHeight);
		}
	}//fixedUpdate
}
