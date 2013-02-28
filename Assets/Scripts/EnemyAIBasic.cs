using UnityEngine;
using System.Collections;

public class EnemyAIBasic : MonoBehaviour {

	public Vector3 player_pos;
	public float distToplayer = 250.0f;
	public float enemySpeed = .5f;
	private float wanderTimer = 0.0f;
	private float nextWalk = 0.0f;
	private float walkdir = 0.0f;
	public bool facingLeft = false;
	public bool attacking = false;
	public OTAnimatingSprite ourSprite;
	public int health = 5;
	public bool hit = false;
	public float hitFactor = 1.0f;
	
	public AudioClip hitClip;
	
	// Use this for initialization
	void Start () {
		player_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
		ourSprite = transform.GetComponent<OTAnimatingSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0){
			GameObject.Destroy(gameObject);
		}
		player_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
		//Debug.Log(Mathf.Abs(player_pos.x - transform.position.x));
		if (Mathf.Abs(player_pos.x - transform.position.x) < distToplayer)
		{
			wanderTimer = 0.0f;
			nextWalk = 0.0f;
			Vector3 newPos = transform.position;
			Vector3 direct = (player_pos - transform.position);
			if (direct.x < 0)
			{
				facingLeft = true;
				if (!attacking && !hit)
				{
					ourSprite.PlayLoop("WalkL");
				}
			}
			else
			{
				facingLeft = false;
				if (!attacking && !hit)
				{
					ourSprite.PlayLoop("Walk");
				}
			}
			direct.Normalize();
			newPos.x += enemySpeed * direct.x * hitFactor;
			transform.position = newPos;
		}
		else
		{
			
			if (nextWalk < 2.5)
			{
				nextWalk += Time.deltaTime;
				walkdir = Random.Range(-2, 2);
			}
			else if (nextWalk > 2.5f && wanderTimer < 2.0f)
			{
				if (walkdir < 0)
				{
					facingLeft = true;
					ourSprite.PlayLoop("WalkL");
				}
				else
				{
					facingLeft = false;
					ourSprite.PlayLoop("Walk");
				}
				wanderTimer += Time.deltaTime;
				Vector3 newPos = transform.position;
				newPos.x += enemySpeed * walkdir;// *Time.deltaTime;
				transform.position = newPos;
			}
			else
			{
				nextWalk = 0.0f;
				wanderTimer = 0.0f;
			}
		}
	}

	IEnumerator AttackingTimer()
	{
		yield return new WaitForSeconds(0.5f);
		attacking = false;
	}

	IEnumerator HitTimer()
	{

		yield return  new WaitForSeconds(0.25f);
		hit = false;
		hitFactor = 1.0f;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Player")
		{
			attacking = true;
			StartCoroutine("AttackingTimer");
			/*attack animation*/
			if (facingLeft)
			{
				ourSprite.PlayLoop("AttackL");
			}
			else
			{
				ourSprite.PlayLoop("Attack");
			}
		   
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.transform.tag == "Umbrella")
		{
			//Debug.Log("hit!!");
			health--;
			hit = true;
			hitFactor = -2.0f;
			if (facingLeft)
			{
				ourSprite.PlayLoop("HitL");
			}
			else
			{
				ourSprite.PlayLoop("Hit");
			}
			
			AudioSource.PlayClipAtPoint(hitClip, GameObject.Find("MainSprite").transform.position);
			
			StartCoroutine("HitTimer");
		}
	}
}
