using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NormalDroidPlayer : MonoBehaviour {
	
	private Vector2 maxVelocity = new Vector2(3,5);
	
	private PlayerControllerMobile controller;
	//private PlayerController controller;

	public bool isStanding;
	
	private Animator anim;
	public Vector2 moving = new Vector2();
	
	private Rigidbody2D rigidbody2D;
	
	[SerializeField]
	private BoxParts bodyParts;

	[SerializeField]
	private AudioClip dieSoundClip,sheildSoundClip,gameOverClip;
		
	public static bool isAlive,isGameOver,isSoundOn;
	
	public float boundaryLeft, boundaryRight, boundaryUp;
	
	private GameObject sheild;
	public static float timeLeftForSheild;
	public static bool isSheildActive,isSheildLoaded;
	
	public static int score;
	
	private GameObject coinCollectedText;
	
	void Awake () {
		controller = GetComponent<PlayerControllerMobile>();
		anim = GetComponent<Animator>();
		sheild = this.gameObject.transform.GetChild(0).gameObject;
		rigidbody2D = GetComponent<Rigidbody2D>();
		coinCollectedText = GameObject.FindGameObjectWithTag("coin_collected_text") as GameObject;
		
		isStanding = true;
		isAlive = true;
		isGameOver = false;
		isSoundOn = GamePreferences.GetSoundSetting() == 1 ? true : false;
		DeactivateSheild();
		score = 0;
		
		boundaryRight =(CameraProperties.GetCameraWidth(Camera.main) / 2) - 1f;
		boundaryLeft = -boundaryRight;
		boundaryUp = CameraProperties.GetCameraHeight(Camera.main) / 2 - 0.7f;
		
	}// awake
	
	public static void DeactivateSheild(){
		isSheildActive = false;
		isSheildLoaded = false;
	}
	
	public static void ActivateSheild(){
		if(isAlive){
			timeLeftForSheild = 10f;
			isSheildActive = true;
		}
	}//activateSHeild
	
	public static void IncreaseScore(int plusPoints){
		if(isAlive){
			NormalDroidPlayer.score += plusPoints;
		}
	}//increasescore
	
	IEnumerator HoldSheildFor(){
		isSheildLoaded = true;
		if(timeLeftForSheild == 10 && isSoundOn){ 
			// first time
			AudioSource.PlayClipAtPoint(sheildSoundClip,transform.position);
		}
		yield return new WaitForSeconds(1f);
		timeLeftForSheild--;
		//print("time left for sheild" + timeLeftForSheild);
		if(timeLeftForSheild <= 0){
			DeactivateSheild();
			StopCoroutine("HoldSheildFor");
		} else {
			StartCoroutine("HoldSheildFor");
		}
	}// holdSheildFor
	
	
	void Update () {
	
		if(isAlive){
			var forceX = 0f;
			var forceY = 0f;
			
			var absValueX = Mathf.Abs(rigidbody2D.velocity.x);
			var absValueY = Mathf.Abs(rigidbody2D.velocity.y);
			
			isStanding = (absValueY < 0.1f) ? true : false;
			
			if(PlayerControllerMobile.sidewaysKey == -1){ // moving left
				if(absValueX < maxVelocity.x){
					forceX = -30f;
				}
				transform.localScale = new Vector3(-1f,1f,1f);
				anim.SetInteger("DroidAnimState", 1);
			} else if (PlayerControllerMobile.sidewaysKey == 1) { //moving right
				if(absValueX < maxVelocity.x){
					forceX = 30f;
				}
				transform.localScale = new Vector3(1f,1f,1f);
				anim.SetInteger("DroidAnimState", 1);
			} else {
				anim.SetInteger("DroidAnimState", 0);	
			}
			
			if(PlayerControllerMobile.upKey == 1){
				if(isStanding && (transform.position.y < boundaryUp)){
					if(absValueY < maxVelocity.y)
						forceY = 125f;
				}
				anim.SetInteger("DroidAnimState", 2);	
			}
			
			// checking boundary
			var temp = transform.position;
			if(transform.position.x < boundaryLeft){
				temp.x += 0.2f;
				transform.position = temp;
			} else if (transform.position.x > boundaryRight){
				temp.x -= 0.2f;
				transform.position = temp;
			}
			
			rigidbody2D.AddForce(new Vector2(forceX, forceY));
			
		} else { // dead, so end the game
			if(!isGameOver){
				DieAndExplode();
				GameOver();
			}
		}//if is alive
		
	}//update
	
	void FixedUpdate(){
		if(isAlive){
			if(isSheildActive){
				sheild.gameObject.SetActive(true);
				if(!isSheildLoaded){
					StartCoroutine("HoldSheildFor");
				}
			} else {
				sheild.gameObject.SetActive(false);
			}
		}
	}//fixed Update
	
	void GameOver(){
		isGameOver = true;
		ButtonController.setGameOverPanel = true;
		if(isSoundOn){
			AudioSource.PlayClipAtPoint(gameOverClip,transform.position);
		}
	}// gameover
	
	void DieAndExplode(){
		if(isSoundOn){
			AudioSource.PlayClipAtPoint(dieSoundClip,transform.position);
		}
		Destroy(gameObject);
		var trans = transform;
		for(int i=0;i<7; i++){
			//trans.TransformPoint(randomX,randomY,0);
			var temp = trans.position;
			temp.x += (0.5f * Random.Range(-1,1));
			//temp.y += (0.5f * Random.Range(-1,1));
			BoxParts clone = Instantiate(bodyParts,temp,Quaternion.identity) as BoxParts;
			// adding force to sideways
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Random.Range(-50,50));
			//adding force to up
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range(50,300));
		}//loop of explode
	
	}
}
