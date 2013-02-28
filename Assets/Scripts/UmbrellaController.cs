using UnityEngine;
using System.Collections;

public class UmbrellaController : MonoBehaviour {

	public MainCharacter player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision)
	{
		player.RegisterUmbrellaHit();
	}
}
