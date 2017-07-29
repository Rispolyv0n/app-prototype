using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PingServer : MonoBehaviour {
	// Get the item from UI system 
	public InputField username;
	public InputField passwd;
	public Text serverResponse;
	public Text statusBar;
	public Button send;
	// Login URL
	public string testurl = "https://kevin.imslab.org:4000/test";
	// Use this for initialization
	void Start () {
		serverResponse.text = "初始化狀態...";
		// Init button
		Button btn = send.GetComponent<Button> ();
		btn.onClick.AddListener (sendrequest);
	}

	/* user-defined function */
	void sendrequest(){
		// Check out the uname 
		string uname = username.text;
		string upass = passwd.text;
		if (uname.Length <= 0 || upass.Length <= 0) {
			// Display information in statusbar
			statusBar.text = "必須輸入字元!";
		} else {
			// clear statusbar 
			statusBar.text = "";
			// Get the messenger and using post command to send data to server
			WWWForm formdata = new WWWForm();
			formdata.AddField ("username",uname);
			formdata.AddField ("password",upass);
			// send data 
			StartCoroutine(write(formdata));
		}
	}

	IEnumerator write(WWWForm form){
		/*
		WWW sending = new WWW (testurl);
		// wait for downloading,answering from server
		yield return sending;*/
		UnityWebRequest sending = UnityWebRequest.Post(testurl,form);
		yield return sending.Send ();

		if (sending.error != null) {
			serverResponse.text = sending.error;
		} else {
			serverResponse.text = sending.downloadHandler.text;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
