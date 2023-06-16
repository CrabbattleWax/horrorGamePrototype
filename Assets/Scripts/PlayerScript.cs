using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour {
	public CanvasGroup cGroup;
	public int health = 30;
	private float healthTimer;
	public float healthReset = 1;
	public AudioSource aSource;
	public AudioClip[] aClip;
	public TextMeshProUGUI warningText;
	// Use this for initialization
	void Start () {
		aSource = GetComponent<AudioSource> ();
		warningText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		healthTimer -= Time.deltaTime;
		if (cGroup.alpha > 0) {
			cGroup.alpha = Mathf.Lerp (cGroup.alpha, 0, Time.deltaTime);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			TakeDamage ();
		}
		if (health <= 0) {
			SceneManager.LoadScene ("FailScene");
		}
	}
	public void TakeDamage(){
		if (healthTimer < 0) {
			cGroup.alpha = 1f;
			healthTimer = healthReset;
			health -= 10;
			int randomSound = Random.Range (0, aClip.Length);
			aSource.clip = aClip [randomSound];
			aSource.Play ();
		}
	}
    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "WarningCollider")
		{
			warningText.enabled = true;
			warningText.text = "Turn around, Go Back!";
		}
		if (other.gameObject.tag == "Cabin")
		{
			SceneManager.LoadScene("StartScene");
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "WarningCollider")
		{
			warningText.enabled = false;
		}
		
	}
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "ResetCollider")
		{
			SceneManager.LoadScene("OutOfBounds");
		}
		
	}
}
