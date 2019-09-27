using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControllerMobile : MonoBehaviour{
	
	public static int sidewaysKey=0;
	public static int upKey=0;
	
	void Start(){
		sidewaysKey=0;
		upKey=0;
	}
	
	public void LeftButtonIsPressed(){
		sidewaysKey = -1;
	}
	
	public void RightButtonIsPressed(){
		sidewaysKey = 1;
	}
	public void ButtonReleased(){
		sidewaysKey = 0;
		upKey = 0;
	}
	
	public void UpButtonIsPressed(){
		upKey = 1;
		sidewaysKey = 0;
	}
	
}
