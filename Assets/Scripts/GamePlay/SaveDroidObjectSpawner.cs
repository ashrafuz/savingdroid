using UnityEngine;
using System.Collections;

public class SaveDroidObjectSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] objects;
	
	public static float minRangeTime = 0.3f;
	public static float maxRangeTime = 2.5f;
	
	void Start () {
		StartCoroutine(GenerateObjects());
	}
	
	IEnumerator GenerateObjects(){
		yield return new WaitForSeconds(Random.Range(minRangeTime,maxRangeTime));
		if(!NormalDroidPlayer.isGameOver){ // GAME IS ON
			float halfOfCameraWidth = CameraProperties.GetCameraWidth(Camera.main) / 2 - 0.5f;
			int positiveNegativeMultiplier = Random.Range(1,99)%2 == 0 ? 1 : -1;
			
			var position = new Vector3(
				Random.Range(0, halfOfCameraWidth) * positiveNegativeMultiplier, 
				transform.position.y, 
				0f
				);	
			Instantiate(objects[Random.Range(0,objects.Length)],position,Quaternion.identity);
			StartCoroutine(GenerateObjects());
		}
	}
}
