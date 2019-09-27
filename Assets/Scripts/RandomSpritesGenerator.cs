using UnityEngine;
using System.Collections;

public class RandomSpritesGenerator : MonoBehaviour {
	
	public Sprite[] sprites;
	public string resourceName;
	public int currentSprite = -1;
	
	// Use this for initialization
	void Start () {
		if(resourceName != ""){
			sprites = Resources.LoadAll<Sprite>(resourceName);
			
			// to have more control over the randomize object
			if(currentSprite == -1){
				currentSprite = Random.Range(0,sprites.Length);
			} else if(currentSprite > sprites.Length) {
				currentSprite = sprites.Length -1;
			}
			GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
		}
	}//start
}
