using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	private int totalParts = 9;
	public BoxParts bodyParts;
	
	[SerializeField]
	private GameObject spider, coin , sheild;
	
	[SerializeField]
	private AudioClip explodeClip;
	
	
	void OnTriggerEnter2D(Collider2D target){
		if(target.name == "Platform"){
			//print("Should explode box");
			NormalDroidPlayer.IncreaseScore(1);
			OnExplode();
			RevealItem();
		}
		
		if(target.name == "NormalDroid"){
			if(NormalDroidPlayer.isSheildActive){
				NormalDroidPlayer.IncreaseScore(1);
				OnExplode();
			} else {
				OnExplode();
				NormalDroidPlayer.isAlive = false;
			}
		}
	}//ontriggerenter
	
	void OnExplode(){
		if( NormalDroidPlayer.isSoundOn ){
			AudioSource.PlayClipAtPoint(explodeClip,transform.position);
		}
		Destroy(gameObject);
		var trans = transform;
		for(int i=0;i<totalParts; i++){
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
	

	void RevealItem(){
		int randomItemChooser = Random.Range(1,100) % 7;
		if(randomItemChooser == 0){  
			// show a sheild
			//Instantiate(sheild,transform.position,Quaternion.identity);
			
			// making it more randomized
			int random = Random.Range(0,5);
			if(random >= 3){
				Instantiate(sheild,transform.position,Quaternion.identity);
			}
		} else if(randomItemChooser == 1 || randomItemChooser == 2){
			// show a spider
			var temp = transform.position;
			temp.y -= 0.4f;
			Instantiate(spider,temp,Quaternion.identity);
			print("spider");
		} else { //show coin
			Instantiate(coin,transform.position,Quaternion.identity);
		}
		
	}//reveal item
	
	
}//class
