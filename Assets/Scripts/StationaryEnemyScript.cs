using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class StationaryEnemyScript : MonoBehaviour {

	
	public bool chasing = false;
	private Transform player;
	
	private AudioSource aSource;
	private bool playSound = true;

	private NavMeshAgent nav;
	private Vector3 startPos;
	private float returnTimer = 5f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		aSource = GetComponent<AudioSource> ();
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (transform.position, player.position);
		if (dist < 20) {
			chasing = true;
			if (playSound) {
				aSource.Play ();
				playSound = false;
			}
		}
		if (chasing) {
			nav.destination = player.position;
            if (dist >30)
            {
				returnTimer -= Time.deltaTime;
				if(returnTimer <= 0)
                {
					returnTimer = 5f;
					transform.position = startPos;
					nav.destination = startPos;
					aSource.Stop();
					chasing = false;
                }
            }
            else
            {
				returnTimer = 5f;
            }
		}
	}
	void OnCollisionStay(Collision other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerScript> ().TakeDamage ();
		}
	}
}
