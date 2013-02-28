using UnityEngine;
using System.Collections;

public class EndLevelScript : MonoBehaviour {

	public GameObject Player;
	public MainCharacter playerscript;
	public Transform box;
	public bool inBox = false;
	private float totalDelta = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision)
	{
		if(collision.transform.tag == "Player"){
			if (playerscript.UmbrellaUp)
			{
				Player.GetComponent<OTAnimatingSprite>().PlayLoop("WalkU1");
			}
			else
			{
				Player.GetComponent<OTAnimatingSprite>().PlayLoop("Walk1");
			}
			playerscript.enabled = false;
			StartCoroutine("EndSequence");
		}
	}

	IEnumerator EndSequence()
	{
		while (!inBox)
		{
			totalDelta += Time.deltaTime;
			Debug.Log(totalDelta);
			Debug.Log(inBox);
			Vector3 newPos = new Vector3(Mathf.Lerp(Player.transform.position.x,box.position.x,totalDelta/15.0f),Mathf.Lerp(Player.transform.position.y,box.position.y,totalDelta/15.0f),Mathf.Lerp(Player.transform.position.z,box.position.z,totalDelta/15.0f));
			Vector3 newScale = new Vector3(Mathf.Lerp(100,75,totalDelta/15.0f),Mathf.Lerp(100,75,totalDelta/15.0f),1.0f);
			Player.transform.position = newPos;
			Player.transform.localScale = newScale;
			yield return new WaitForEndOfFrame();
		}
		Debug.Log(inBox);
        Application.LoadLevel("start");
	}
}
