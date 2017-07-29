using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dataloader : MonoBehaviour {
	public Text statusBar;
	/* Change camera
	private float x;
	private float y; */
	private Vector3 rotateValue;
	string serverURL = "https://kevin.imslab.org:4000/";
	// Use this for initialization
	void Start () {
		StartCoroutine(getFromServer(serverURL));
	}
	
	IEnumerator getFromServer(string url){
		WWW itemData = new WWW(url);
		yield return itemData;
		// itemData.error (String)
		if(itemData.error != null){
			print ("Error: " + itemData.error);
		}
		else{
			// print out the result => check out 
			print (itemData.text);
		}
	}
	
	// Update is called once per frame
	void Update () {
		/* Change camera (By using mouse)
		y = Input.GetAxis("Mouse X");
		x = Input.GetAxis("Mouse Y");
		//Debug.Log(x + ":" + y);
		rotateValue = new Vector3(x, y * -1, 0);
		transform.eulerAngles = transform.eulerAngles - rotateValue;*/

		// Change camera (By using touch pad)
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			// (FIXME) Move object across XY plane 
			transform.eulerAngles += new Vector3(touchDeltaPosition.x , touchDeltaPosition.y*-1, 0);
			statusBar.text = touchDeltaPosition.x + " : " + touchDeltaPosition.y;
		}
	}
}
