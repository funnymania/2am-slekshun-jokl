using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialogSequence : MonoBehaviour {
	public GameObject logo,theroom;
	AudioSource ford;
	RectTransform bluj;
	Text talk;
	float timer = 0;
	//CameraTitleScreen.toTheGame=true;
	
    void Start () {
		talk = GetComponent<Text>();
		ford = GetComponent<AudioSource>();
		bluj = GetComponent<RectTransform>();
	}

	void Update(){
		if (!CameraTitleScreen.toTheWakeUp) {
			if (timer > 35) {
				talk.text = "r: so many puffballs..";
				bluj.anchoredPosition = new Vector2(90, -152);
            } else if (timer > 32) {
				theroom.SetActive(true);
				CameraTitleScreen.toTheWakeUp = true;
                ford.Stop();
            } else if (timer > 23) {
				if (!ford.isPlaying) {
					ford.Play();
                    ford.loop = true;
                }
				logo.SetActive(false);
            } else if (timer > 20) {
				logo.SetActive(true);
            } else if (timer > 17) {
				talk.text="";
            } else if (timer > 13) {
				talk.text = "Queues?";
            } else if (timer > 12) {
				talk.text = "";
            } else if (timer > 11) { 
				talk.text = "a talk of outsider interest?";
            } else if (timer > 7) {
				talk.text = "";
			} else if (timer > 4) {
				talk.text = "peesh.. run outta here now";
				bluj.anchoredPosition = new Vector2(90, -84);
            }
			timer += Time.deltaTime;
        }
	}
}