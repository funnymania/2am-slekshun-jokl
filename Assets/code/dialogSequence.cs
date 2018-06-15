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
	void Start(){
		talk=GetComponent<Text>();
		ford=GetComponent<AudioSource>();
		bluj=GetComponent<RectTransform>();
	}
	void Update(){
		if(!CameraTitleScreen.toTheWakeUp){
			if(timer>35){
				talk.text="r: so many puffballs..";
				bluj.anchoredPosition=new Vector2(90,-152);}
			else if(timer>32){
				theroom.SetActive(true);
				CameraTitleScreen.toTheWakeUp=true;ford.Stop();}
			else if(timer>23){
				if(!ford.isPlaying){
					ford.Play();ford.loop=true;}
				logo.SetActive(false);}
			else if(timer>20)
				logo.SetActive(true);
			else if(timer>17)
				talk.text="";
			else if(timer>13)
				talk.text="Queues?";
			else if(timer>12)
				talk.text="";
			else if(timer>11) 
				talk.text="a talk of outsider interest?";
			else if(timer>7)
				talk.text="";
			else if(timer>4){
				talk.text="peesh.. run outta here now";
				bluj.anchoredPosition=new Vector2(90,-84);}
			timer+=Time.deltaTime;}
	}
}
/*
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
	void Start(){
		talk=GetComponent<Text>();
		ford=GetComponent<AudioSource>();
		bluj=GetComponent<RectTransform>();
	}
	void Update(){
		if(timer>143 || Input.GetButtonDown("Start")){theroom.SetActive(true);
			CameraTitleScreen.toTheWakeUp=true;ford.Stop();}
		else if(timer>141){
			talk.text="c me: AAAh!";
			bluj.anchoredPosition=new Vector2(8.3F,136.3F);}
		else if(timer>141){
			talk.text="me c: whoa is that fensil?";
			bluj.anchoredPosition=new Vector2(8.3F,136.3F);}
		else if(timer>137){
			talk.text="r me: I can see.. myself!";
			bluj.anchoredPosition=new Vector2(12.3F,-36.5F);}
		else if(timer>126){
			talk.text="b: and rails!";
			bluj.anchoredPosition=new Vector2(142F,-110.8F);}
		else if(timer>122){
			talk.text="r: im dreaming about people thinking about the color Fuschia.";
			bluj.anchoredPosition=new Vector2(-60F,10F);}
		else if(timer>118){
			talk.text="r: *smiling* it's not just me, then";
			bluj.anchoredPosition=new Vector2(143F,-45.2F);}
		else if(timer>114){
			talk.text="b: me, too!";
			bluj.anchoredPosition=new Vector2(-222.1F,124.7F);}
		else if(timer>110){
			talk.text="me: im having a dream.";
			bluj.anchoredPosition=new Vector2(-222.1F,146.8F);}
		else if(timer>102){
			talk.text="g: rails take you through Canti-ty (cantiti)";
			bluj.anchoredPosition=new Vector2(-233.2F,0);}
		else if(timer>96){
			talk.text="g: and rails";
			bluj.anchoredPosition=new Vector2(23.4F,5.3F);}
		else if(timer>93){
			talk.text="r: i love the color fuschia";
			bluj.anchoredPosition=new Vector2(-2.3F,138.1F);}
		else if(timer>86){
			talk.text="i am living my whole life in a massive state of anxiety.";
			bluj.anchoredPosition=new Vector2(-80,100);}
		else if(timer>82){
			talk.text="fighting a continuous war... we are entering a new one.";}
		else if(timer>78){
			talk.text="i am living my whole life in a massive state of anxiety.";
			bluj.anchoredPosition=new Vector2(0,-84);}
		else if(timer>74){
			talk.text="are you happy?";}
		else if(timer>70){
			talk.text="are you happy?";}
		else if(timer>66){
			talk.text="b: oh how awesome";}
		else if(timer>62){
			talk.text="me: wow!";
			bluj.anchoredPosition=new Vector2(90,-84);}// colors shifting drastically
		else if(timer>58){
			talk.text="b: look!";}
		else if(timer>54){
			talk.text="me: are you happy?";
			bluj.anchoredPosition=new Vector2(90,-84);}
		else if(timer>51){
			talk.text="";}
		else if(timer>47){
			talk.text="g: are you happy?";
			bluj.anchoredPosition=new Vector2(-212,-105);}
		else if(timer>43){
			talk.text="me: I didn't want this. Thanky you! for the ice cream.";
			bluj.anchoredPosition=new Vector2(90,-84);}
		else if(timer>39){
			talk.text="r: are you happy now? i have an icecream!";
			bluj.anchoredPosition=new Vector2(90,-152);}
		else if(timer>35){
			talk.text="r: so many puffballs..";
			bluj.anchoredPosition=new Vector2(90,-152);}
		else if(timer>31){
			talk.text="r: Oh shit.";
			bluj.anchoredPosition=new Vector2(90,-152);} //relative to fencil's 2d field of vision these should be placed
		else if(timer>27){								// as though they are entering a park, they are walking together
			talk.text="g: where should we go?";			// and being seated
			bluj.anchoredPosition=new Vector2(-212,-105);}//make jok:L appear while particles running
		else if(timer>23){
			logo.SetActive(false);ford.Play();ford.loop=false;}
		else if(timer>20)
			logo.SetActive(true);
		else if(timer>17)
			talk.text="";
		else if(timer>13)
			talk.text="Queues?";
		else if(timer>12)
			talk.text="";
		else if(timer>11) 
			talk.text="a talk of outsider interest?";
		else if(timer>7)
			talk.text="";
		else if(timer>4){
			talk.text="peesh.. run outta here now";
			bluj.anchoredPosition=new Vector2(90,-84);}
		timer+=Time.deltaTime;
	}
}

*/
