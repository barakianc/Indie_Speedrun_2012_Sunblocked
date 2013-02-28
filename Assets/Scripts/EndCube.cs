using UnityEngine;
using System.Collections;

public class EndCube : MonoBehaviour {
    public EndLevelScript House;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            House.inBox = true;
        }
    }

}
