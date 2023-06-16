using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class LightControl : MonoBehaviour {

	[SerializeField]private GameObject cameraObject;
	[SerializeField] private TextMeshProUGUI chargeText;
	[SerializeField] private GameObject torch;
	private float maxCharge = 50;
	private float currentCharge;
	private bool isOn = false;
	private Light torchLight;
	private Animator anim;
	
	private bool cameraOn = false;
	// Use this for initialization
	void Start () {
		currentCharge = maxCharge;
		torchLight = torch.GetComponent<Light> ();
		anim = GetComponent<Animator> ();
		cameraObject.SetActive(false);
	}
	void Update () {
		if (cameraOn && Input.GetKeyDown (KeyCode.C)) {
			cameraOn = false;
			cameraObject.SetActive(false);
		}
		else if(!cameraOn && Input.GetKeyDown (KeyCode.C)&& currentCharge >10){
			cameraOn = true;
			cameraObject.SetActive(true);
			currentCharge -= Time.deltaTime;
		}
        if (cameraOn)
        {
            currentCharge -= Time.deltaTime*2;
        }

        if (isOn && Input.GetKeyDown (KeyCode.T)) {
			isOn = false;
			anim.SetBool("PowerOn", false);
		}
		else if(!isOn && Input.GetKeyDown (KeyCode.T) && currentCharge > 10)
		{
			isOn = true;
			anim.SetBool("PowerOn", true);
		}
		if (isOn) {
			torch.SetActive (true);
			currentCharge -= Time.deltaTime*2;
		} else {
			torch.SetActive (false);
			
		}
		if(isOn == false && cameraOn == false)
        {
			currentCharge += Time.deltaTime;
		}
		if (currentCharge > maxCharge) {
			currentCharge = maxCharge;
		}
		if (currentCharge < 0) {
			currentCharge = 0;
		}
		chargeText.text = currentCharge.ToString ("F0") +" Power";
		if (currentCharge < 1) {
			anim.SetBool ("PowerOn",false);
			cameraObject.SetActive(false);
			torch.SetActive(false);
			cameraOn = false;
		}
		if (currentCharge > 1 && currentCharge < 20) {
			anim.SetBool ("Flicker",true);
        }
        else
        {
			anim.SetBool("Flicker", false);
		}
	}
}
