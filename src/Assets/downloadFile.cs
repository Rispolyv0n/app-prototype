using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
			/*UnityWebRequest download = UnityWebRequest.Get ("https://kevin.imslab.org:4000/adf_g?id=1");
			while (!download.isDone) {
				float result = download.downloadProgress * 100;
				// setting progress 
				progress.text = Mathf.Floor (result).ToString () + "/100";
				// setting slider (progress bar)
				progressbar.value = result;
				// print for mobile
				statusbar.text = "[PC] Waiting ... " + Mathf.Ceil(download.downloadProgress*100).ToString() + "%";
				yield return null;
			}
			statusbar.text = "Total size:" + (download.downloadedBytes/(1024*1024)).ToString() + "MB";*/
			Dictionary<string, string> header = new Dictionary<string, string> ();
			string userAgent = "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 5 Build/LMY48B; wv) AppleWebkit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/43.0.2357.65 Mobile Safari/537.36";
			header.Add ("user-agent",userAgent);
			// With android, using http instead of https
			WWW download = new WWW ("http://kevin.imslab.org:4001/adf_g?id=1",null,header);
			while (!download.isDone) {
				float result = download.progress * 100;
				// setting progress 
				progress.text = Mathf.Floor (result).ToString () + "/100";
				// setting slider (progress bar)
				progressbar.value = result;
				// print for mobile
				statusbar.text = "[ANDROID] Waiting ... " + Mathf.Ceil(download.progress*100).ToString() + "%";
				yield return null;
			}
			statusbar.text = "Total size:" + (download.bytes.Length/(1024*1024)).ToString() + "MB";
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
