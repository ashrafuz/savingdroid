using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	
	public Vector2 speed = Vector2.zero;
	
	[SerializeField]
	private Rigidbody2D myRigidBody;
	
	private float minimumSpeedRangeX = 2f;
	private float maximumSpeedRangeX = 4f;
	
	[SerializeField]
	private BoxParts bodyParts;
	
	void Start(){
		int randomX = Random.Range(0,50) % 2;
		int scaleX = (randomX == 0) ? 1:-1;
		//int scaleX = 1; //debug
		transform.localScale = new Vector3(scaleX*(-1), 1f,1f);
		speed = new Vector2(Random.Range(minimumSpeedRangeX,maximumSpeedRangeX) * scaleX, 0f);
	}//start
	
	void Update(){
		float halfOfCamWidth = CameraProperties.GetCameraWidth(Camera.main)/ 2;
		var positionX = transform.position.x;
		if(positionX < (-halfOfCamWidth)){
			Destroy(gameObject);
		}else if (positionX > (halfOfCamWidth)){
			Destroy(gameObject);
		}
	}//update
	
	void FixedUpdate () {
		myRigidBody.velocity = speed;
	}//fixedUpdate
	
	void OnCollisionEnter2D(Collision2D target){
		if(target.gameObject.name == "NormalDroid"){
			if(NormalDroidPlayer.isSheildActive){
				print("Sheild is active");
				OnExplode();
			} else {
				OnExplode();
				NormalDroidPlayer.isAlive = false;
			}
		} else if (target.gameObject.tag == "save_droid_spider" || target.gameObject.name == "Spider(Clone)"){ 
			//spider is colliding with spider
			NormalDroidPlayer.IncreaseScore(5);
			OnExplode();
		}
		
	}//collisionEnter2d
	
	void OnExplode(){
		Destroy(gameObject);
		var trans = transform;
		for(int i=0;i<5; i++){
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
	}//onexplode
}
