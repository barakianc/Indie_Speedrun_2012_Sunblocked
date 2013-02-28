using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {
	public float character_speed = 10.0f;
	public bool inair = false;
	public float jump_force = 500.0f;
	public float camera_deadzone = 3.0f;
	private float delta_movement = 0.0f;
	public float player_health = 10.0f;
	public bool inSun = false;
	public bool inShade = true;
	public bool UmbrellaUp = false;
	public bool facingRight = true;
	public OTAnimatingSprite ourSprite;
	public float Max_Jump = 125.0f;
	public float Downward_Force = -100.0f;
	public bool hit_Cealing = false;
	public float jump_Start = 0.0f;
	public float initCamY = 0.0f;
	public bool can_jump = true;
	public bool immune = false;
	public bool attacking = false;
	public bool hasUmbrella;
	public float UmbrellaHealth = 0.0f;
	public GameObject Umbrella1;
	public GameObject Umbrella2;
	public GameObject Umbrella3;
	public GameObject Umbrella4;
	
	public AudioClip hitClip;
	public AudioClip jumpClip;
	
	// Use this for initialization
	void Start () {
		ourSprite = transform.GetComponent<OTAnimatingSprite>();
		initCamY = Camera.main.transform.position.y;
		
		audio.volume = 0.0f;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
		//only plays if player health falls below at least 30% of health
		float sizzleVolumeOffset = (10.0f - player_health);
		audio.volume = .6f * sizzleVolumeOffset;

		if (hasUmbrella && UmbrellaHealth > 0.0f && !attacking)
		{
			UmbrellaUp = true;
			Vector3 newScale = new Vector3(1,1,1);
			newScale.x = Mathf.Lerp(0.4f,1.0f,UmbrellaHealth/10);
			newScale.y = Mathf.Lerp(0.4f,1.0f,UmbrellaHealth/10);
			Umbrella1.transform.localScale = newScale;
			Umbrella2.transform.localScale = newScale;
			Umbrella3.transform.localScale = newScale;
			Umbrella4.transform.localScale = newScale;
		}
		else
		{
			UmbrellaUp = false;
		}
		
		//check for horizontal movement
	   
		if (Input.GetButton("Horizontal"))
		{
			
			Vector3 newpos = transform.position;
			float deltaPosX = 0.0f;
			if (inair)
			{
				deltaPosX = (Input.GetAxis("Horizontal") * character_speed * Time.deltaTime * 1.0f);
			}
			else
			{
				deltaPosX = (Input.GetAxis("Horizontal") * character_speed * Time.deltaTime);
			}
			if (deltaPosX > 0)
			{
				facingRight = true;
			}
			else if (deltaPosX < 0)
			{
				facingRight = false;
			}

			if (!attacking)
			{

				if (facingRight)
				{
					if (!UmbrellaUp)
					{
						Umbrella3.SetActive(false);
						if (player_health > 8.0f)
						{
							ourSprite.PlayLoop("Walk1");
						}
						else if (player_health > 5.0f)
						{
							ourSprite.PlayLoop("Walk2");
						}
						else if (player_health > 2.0f)
						{
							ourSprite.PlayLoop("Walk3");
						}
						else if (player_health > 0.0f)
						{
							ourSprite.PlayLoop("Walk4");
						}
					}
					else
					{
						Umbrella4.SetActive(false);
						if (player_health > 8.0f)
						{
							ourSprite.PlayLoop("WalkU1");
							Umbrella3.SetActive(true);
						}
						else if (player_health > 5.0f)
						{
							ourSprite.PlayLoop("WalkU2");
							Umbrella3.SetActive(true);
						}
						else if (player_health > 2.0f)
						{
							ourSprite.PlayLoop("WalkU3");
							Umbrella3.SetActive(true);
						}
						else if (player_health > 0.0f)
						{
							ourSprite.PlayLoop("WalkU4");
							Umbrella3.SetActive(true);
						}
					}
				}
				else
				{
					if (!UmbrellaUp)
					{
						Umbrella4.SetActive(false);
						if (player_health > 8.0f)
						{
							ourSprite.PlayLoop("WalkL1");
						}
						else if (player_health > 5.0f)
						{
							ourSprite.PlayLoop("WalkL2");
						}
						else if (player_health > 2.0f)
						{
							ourSprite.PlayLoop("WalkL3");
						}
						else if (player_health > 0.0f)
						{
							ourSprite.PlayLoop("WalkL4");
						}
					}
					else
					{
						Umbrella3.SetActive(false);
						if (player_health > 8.0f)
						{
							ourSprite.PlayLoop("WalkUL1");
							Umbrella4.SetActive(true);
						}
						else if (player_health > 5.0f)
						{
							ourSprite.PlayLoop("WalkUL2");
							Umbrella4.SetActive(true);
						}
						else if (player_health > 2.0f)
						{
							ourSprite.PlayLoop("WalkUL3");
							Umbrella4.SetActive(true);
						}
						else if (player_health > 0.0f)
						{
							ourSprite.PlayLoop("WalkUL4");
							Umbrella4.SetActive(true);
						}
					}
				}
			}
			else
			{
				Umbrella3.SetActive(false);
				Umbrella4.SetActive(false);
			}
			newpos.x += deltaPosX;
			delta_movement += deltaPosX;
			transform.position = newpos;

			if (Mathf.Abs(delta_movement) < camera_deadzone)
			{
				Vector3 newCamPos = Camera.main.transform.position;
				newCamPos.x -= deltaPosX;
				Camera.main.transform.position = newCamPos;
			}
		}
		//else reset the delta movement
		else if (!attacking)
		{
			if (facingRight)
			{
				if (!UmbrellaUp)
				{
					Umbrella3.SetActive(false);
					if (player_health > 8.0f)
					{
						ourSprite.PlayLoop("Idle1");
					}
					else if (player_health > 5.0f)
					{
						ourSprite.PlayLoop("Idle2");
					}
					else if (player_health > 2.0f)
					{
						ourSprite.PlayLoop("Idle3");
					}
					else if (player_health > 0.0f)
					{
						ourSprite.PlayLoop("Idle4");
					}
				}
				else
				{
					Umbrella4.SetActive(false);
					if (player_health > 8.0f)
					{
						ourSprite.PlayLoop("IdleU1");
						Umbrella3.SetActive(true);

					}
					else if (player_health > 5.0f)
					{
						ourSprite.PlayLoop("IdleU2");
						Umbrella3.SetActive(true);
					}
					else if (player_health > 2.0f)
					{
						ourSprite.PlayLoop("IdleU3");
						Umbrella3.SetActive(true);
					}
					else if (player_health > 0.0f)
					{
						ourSprite.PlayLoop("IdleU4");
						Umbrella3.SetActive(true);
					}
				}
			}
			else
			{
				if (!UmbrellaUp)
				{
					Umbrella4.SetActive(false);
					if (player_health > 8.0f)
					{
						ourSprite.PlayLoop("IdleL1");
					}
					else if (player_health > 5.0f)
					{
						ourSprite.PlayLoop("IdleL2");
					}
					else if (player_health > 2.0f)
					{
						ourSprite.PlayLoop("IdleL3");
					}
					else if (player_health > 0.0f)
					{
						ourSprite.PlayLoop("IdleL4");
					}
				}
				else
				{
					Umbrella3.SetActive(false);
					if (player_health > 8.0f)
					{
						ourSprite.PlayLoop("IdleUL1");
						Umbrella4.SetActive(true);
					}
					else if (player_health > 5.0f)
					{
						ourSprite.PlayLoop("IdleUL2");
						Umbrella4.SetActive(true);
					}
					else if (player_health > 2.0f)
					{
						ourSprite.PlayLoop("IdleUL3");
						Umbrella4.SetActive(true);
					}
					else if (player_health > 0.0f)
					{
						ourSprite.PlayLoop("IdleUL4");
						Umbrella4.SetActive(true);
					}
				}
			}

			delta_movement = 0.0f;
		}
		else
		{
			Umbrella3.SetActive(false);
			Umbrella4.SetActive(false);
		}

		//check for jump button
		if (Input.GetButtonDown("Jump") && !inair && can_jump)
		{
			can_jump = false;
			rigidbody.AddForce(0.0f, jump_force, 0.0f);
			jump_Start = transform.position.y;
			inair = true;
			
			AudioSource.PlayClipAtPoint(jumpClip, transform.position);
		}
		else if (Input.GetButton("Jump") && !hit_Cealing && inair)
		{
			rigidbody.AddForce(0.0f, jump_force/10, 0.0f);
			inair = true;
		}
		else if (Input.GetButtonUp("Jump"))
		{
			can_jump = true;
		}

		/* //check for hold umbrella up
		if (Input.GetButton("Vertical"))
		{
			/*hold umberlla up
			UmbrellaUp = true;
		}
		//check if we were attacking
		else if (Input.GetButtonDown("Attack"))
		{
			UmbrellaUp = false;
			/*attacked
		}
		else
		{
			UmbrellaUp = false;
		}*/

		if (inair && !hit_Cealing)
		{
			if (transform.position.y - jump_Start >= Max_Jump)
			{
				hit_Cealing = true;
				jump_Start = 0.0f;
			}
		}

		if(inair && !attacking){
			if (facingRight)
			{
				if (UmbrellaUp)
				{
					Umbrella3.SetActive(true);
					Umbrella4.SetActive(false);
				}
				else
				{
					Umbrella3.SetActive(false);
					Umbrella4.SetActive(false);
				}

				if (player_health > 8.0f)
				{
					ourSprite.PlayLoop("Jump1");
				}
				else if (player_health > 5.0f)
				{
					ourSprite.PlayLoop("Jump2");
				}
				else if (player_health > 2.0f)
				{
					ourSprite.PlayLoop("Jump3");
				}
				else if (player_health > 0.0f)
				{
					ourSprite.PlayLoop("Jump4");
				}
			}
			else
			{
				if (UmbrellaUp)
				{
					Umbrella4.SetActive(true);
					Umbrella3.SetActive(false);
				}
				else
				{
					Umbrella4.SetActive(false);
					Umbrella3.SetActive(false);
				}

				if (player_health > 8.0f)
				{
					ourSprite.PlayLoop("JumpL1");
				}
				else if (player_health > 5.0f)
				{
					ourSprite.PlayLoop("JumpL2");
				}
				else if (player_health > 2.0f)
				{
					ourSprite.PlayLoop("JumpL3");
				}
				else if (player_health > 0.0f)
				{
					ourSprite.PlayLoop("JumpL4");
				}
			}
		}

		/*Attack Button*/
		if (Input.GetButtonDown("Attack") && UmbrellaUp)
		{
			Umbrella3.SetActive(false);
			Umbrella4.SetActive(false);
			UmbrellaUp = false;
			/*attacked*/
			if (facingRight)
			{
				if (player_health > 8.0f)
				{
					ourSprite.PlayLoop("Attack1");
					Umbrella1.SetActive(true);
				}
				else if (player_health > 5.0f)
				{
					ourSprite.PlayLoop("Attack1");
					Umbrella1.SetActive(true);
				}
				else if (player_health > 2.0f)
				{
					ourSprite.PlayLoop("Attack1");
					Umbrella1.SetActive(true);
				}
				else if (player_health > 0.0f)
				{
					ourSprite.PlayLoop("Attack1");
					Umbrella1.SetActive(true);
				}
			}
			else
			{
				if (player_health > 8.0f)
				{
					ourSprite.PlayLoop("AttackL1");
					Umbrella2.SetActive(true);
				}
				else if (player_health > 5.0f)
				{
					ourSprite.PlayLoop("AttackL2");
					Umbrella2.SetActive(true);
				}
				else if (player_health > 2.0f)
				{
					ourSprite.PlayLoop("AttackL3");
					Umbrella2.SetActive(true);
				}
				else if (player_health > 0.0f)
				{
					ourSprite.PlayLoop("AttackL4");
					Umbrella2.SetActive(true);
				}
			}
			StartCoroutine("Attack");
		}
		else if(!attacking)
		{
			Umbrella1.SetActive(false);
			Umbrella2.SetActive(false);
		}
		/*deal sun damage*/
		if (inSun)
		{
			if (!UmbrellaUp || attacking)
			{
				player_health -= .01f;
			}
			else if (UmbrellaUp && UmbrellaHealth > 0.0f)
			{
				UmbrellaHealth -= 0.015f;
			}
		}
		/*heal in shade*/
		else if(inShade && (player_health < 10.0f) || UmbrellaHealth < 10.0f){
			if (player_health < 10.0f)
			{
				player_health += .25f;
			}
			if (UmbrellaHealth < 10.0f)
			{
				UmbrellaHealth += 0.5f;
			}
		}

		/*if player hits 0*/
		if(player_health <= 0.0f){
			/*death*/
			Application.LoadLevel("newScene");
		}

		/*fixed camera to the same y*/
		Vector3 newCamPos2 = Camera.main.transform.position;
		newCamPos2.y = initCamY;
		Camera.main.transform.position = newCamPos2;
	}
	void FixedUpdate()
	{
		if (inair && hit_Cealing)
		{
			transform.rigidbody.AddForce(0.0f, Downward_Force, 0.0f);
		}
		else if(inair){
			transform.rigidbody.AddForce(0.0f, Downward_Force, 0.0f);
		}
		
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.transform.tag == "Ground")
		{
			inair = false;
			hit_Cealing = false;
			rigidbody.Sleep();
		}

	   

		if (collision.transform.tag == "Enemy")
		{
			player_health -= 1.0f;
			if (collision.transform.position.x > transform.position.x)
			{
				rigidbody.AddForce(-100000.0f, 0.0f, 0.0f);
			}
			else
			{
				Debug.Log("Hit Left");
				rigidbody.AddForce(100000.0f, 0.0f, 0.0f);
			}
			
			AudioSource.PlayClipAtPoint(hitClip, transform.position);
		}
	}
	void OnCollisionStay(Collision collisionInfo)
	{
		if (collisionInfo.transform.tag == "Enemy")
		{
			if (!immune)
			{
				StartCoroutine("Immunity");
			}
			if (collisionInfo.transform.position.x > transform.position.x)
			{
				rigidbody.AddForce(-100000.0f, 0.0f, 0.0f);
			}
			else
			{
				//Debug.Log("Hit Left");
				rigidbody.AddForce(100000.0f, 0.0f, 0.0f);
			}
		}
	}

	void OnCollisionExit(Collision collisionInfo)
	{
		if(collisionInfo.transform.tag == "Ground"){
			inair = true;
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.transform.tag == "Sun")
		{
			inSun = true;
			inShade = false;
		}
		else if (collision.transform.tag == "Shade")
		{
			inShade = true;
			inSun = false;
		}

		if(collision.transform.tag == "UmbrellaPickUp"){
			hasUmbrella = true;
			UmbrellaHealth = 10.0f;
		}
	}

	void OnTriggerExit(Collider collision)
	{
		if (collision.transform.tag == "Shade")
		{
			inSun = true;
			inShade = false;
		}
	}

	IEnumerator Immunity()
	{
		immune = true;
		yield return new WaitForSeconds(0.25f);
		Physics.IgnoreLayerCollision(8, 9, true);
		for (int i = 0; i < 12; i++)
		{
			yield return new WaitForSeconds(0.25f);
			renderer.enabled = !renderer.enabled;
		}
		renderer.enabled = true;
		Physics.IgnoreLayerCollision(8, 9, false);
		immune = false;
	}

	IEnumerator Attack()
	{
		attacking = true;
		yield return new WaitForSeconds(0.25f);
		attacking = false;
	}

	public void RegisterUmbrellaHit(){
		UmbrellaHealth--;
	}
}
