using UnityEngine;
using System.Collections;

public class CollectablesForSaveDroid : MonoBehaviour {

	[SerializeField]
	private AudioClip coinDingClip;
	
	void Start () {
		Invoke("DestroyItem", Random.Range(2,4));
	}
	
	void DestroyItem () {
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.name == "NormalDroid"){
			if(gameObject.tag == "coin"){
				if(NormalDroidPlayer.isSoundOn){
					AudioSource.PlayClipAtPoint(coinDingClip,transform.position);
				}
				//for animation
				CoinConfirmationText.isCoinCollected = true;
				CoinConfirmationText.coinPositionX = gameObject.transform.position.x;
				NormalDroidPlayer.IncreaseScore(3);
			} else if (gameObject.tag == "save_droid_sheild"){
				NormalDroidPlayer.IncreaseScore(10);
				NormalDroidPlayer.ActivateSheild();
			}
			Destroy(gameObject);
		}
	}
}
