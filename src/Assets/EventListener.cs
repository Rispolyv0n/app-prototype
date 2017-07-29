using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventListener : MonoBehaviour {
	public Camera MainCamera;
	// For Push button
	public Button LoginBtn;
	// For Status bar
	public Text statusBar;
	public int flag;
	private Vector3 rotateValue;
	private int limit = 90;
	private int count = 0;
	// Use this for initialization
	void Start () {
		print ("Event Listener here!");
		flag = 0;
		Button btn = LoginBtn.GetComponent<Button> ();
		btn.onClick.AddListener (user_login);
	}

	/* User Login */
	void user_login(){
		// Print out login => Change current text info
		// StartCoroutine(sleep(1));
		flag = 1;
		statusBar.text = "旭民進來囉!";
		print ("Login !");
	}

	IEnumerator sleep(int sec){
		yield return new WaitForSeconds (sec);
		rotateValue = new Vector3(0, 10, 0);
		MainCamera.transform.eulerAngles = transform.eulerAngles + rotateValue;
	}

	// Update is called once per frame
	void Update () {
		if (flag == 1) {
			rotateValue = new Vector3(0, ++count, 0);
			MainCamera.transform.eulerAngles = transform.eulerAngles + rotateValue;
			if (count == limit)
				flag = 0;
		}
	}
}
