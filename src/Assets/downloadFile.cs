using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class downloadFile : MonoBehaviour {
	public Text statusbar;
	public Text progress;
	public Button dl;
	public Slider progressbar;

	// Use this for initialization
	void Start () {
		// Btn event
		Button btn = dl.GetComponent<Button> ();
		btn.onClick.AddListener (fork_DL);
		// setting max,min 
		progressbar.maxValue = 100;
		progressbar.minValue = 0;
		// disable slider 
		progressbar.enabled = false;
	}

	void fork_DL(){
		StartCoroutine (get_DL());
	}

	IEnumerator get_DL(){
		// yield return www; // waiting for completed
		// using isDone() 
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			WWW download = new WWW ("https://kevin.imslab.org:4000/adf_g?id=1");
			while (!download.isDone) {
				float result = download.progress * 100;
				// setting progress 
				progress.text = Mathf.Floor (result).ToString () + "/100";
				// setting slider (progress bar)
				progressbar.value = result;
				// print for mobile
				statusbar.text = "[PC] Waiting ... " + Mathf.Ceil(download.progress*100).ToString() + "%";
				yield return null;
			}
			statusbar.text = "Total size:" + (download.bytes.Length/(1024*1024)).ToString() + "MB";
		} else if(Application.platform == RuntimePlatform.Android) {
			WWW download = new WWW ("https://kevin.imslab.org:4000/adf_g?id=1");
			yield return download;
			/*while (!download.isDone) {
				float result = download.progress * 100;
				// setting progress 
				progress.text = Mathf.Floor (result).ToString () + "/100";
				// setting slider (progress bar)
				progressbar.value = result;
				// print for mobile
				statusbar.text = "[Mobile] Waiting ... "+ Mathf.Ceil(download.progress*100).ToString() + "%";
				yield return null;
			}*/
			statusbar.text = "Total size:" + download.bytes.Length.ToString();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
