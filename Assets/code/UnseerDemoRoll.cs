using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UnseerDemoRoll : MonoBehaviour {
	#region Declarations
	public static bool isitCool = false;
	public static bool toTheGame = false;
	public static bool toTheWakeUp = false;
	public static bool toTheLucidDreaming = false;
	public static bool camFollows=false; /* i should be false */
	public static bool camFollowsY=false;
	/* have the deffins fade in */
	public GameObject dialog,yada,birdo,partic1,partic2,Deffins;
	public Transform dedication;
	public GameObject eyeL,eyeL2,eyeR,eyeR2;
	public Animator birdAnim;
	public AudioSource aud2;
	public AudioSource audSFX;
	public SpriteRenderer birdRend;
	public SpriteRenderer sprito;
	public List<Transform> SeekTo = new List<Transform>();
	public Transform lookHere;
	public AudioClip yope,birdIn,birdCrack,funnyTimes,fensil;
	public Texture logo;
	public CameraBlah cameraBlah;
	public Text dash,GUI,slekshun,jok,prn,fade,fade2,enter,click,music,lolgo,titit,dummy;
	public Image Y,space;
	public Image fadeOut,fadeOutFinish;
	public List<List<float>> timers=new List<List<float>>(); //timers[x][0] = counter, timers[x][1] = counterAmt,timers[x][2]=direction;
	public List<groupText> titleElements=new List<groupText>();
	public List<groupText> newGuys=new List<groupText>();
	public float distance,speed,speedCinematic;
	Transform target;
	Camera camera;
	AudioSource aud;
	Transform tr;
	Texture2D black;
	RectTransform dumdum;
	Vector3 startPos;
	Vector3 serialMons=Vector3.zero;
	Color color,colorFade,curColor,curColorOpaq;
	Color opaq,trans,go;
	Color first,rand;
	List<float> blahblah;
	float drawDepth = -1000,fadeSpeed=0.8F;
	float placement;
	float flyTime=0;
	float timey=0,twoTimey=0,birdTimer,audTimer=0;
	float t=0,otherTimer=0,otherTimer2=0;
	float finalTrans=0;
	float fadeTimer=0;
	float distText;
	float blahFloat;
	float waste;
	int i=0,j=0,coin;
	float journeyTime=40;
	bool starting=false;
	bool fadeVomit=false;
	bool audioMoment=false,logoDone=false,changing=false,audioMoment2=false,pressSpace=false;
	bool yHit=false;
	[System.Serializable]
	public class groupText{
		public Text titleElement;
		public RectTransform current;
		public RectTransform before;
		public RectTransform after;
		public bool seen;
		
		public groupText(Text titleElement,RectTransform current,RectTransform before,RectTransform after){
			this.titleElement=titleElement;
			this.current=current;
			this.before=before;
			this.after=after;
			this.seen=false;}
	}
	#endregion
	void Start(){
		tr=transform;
		startPos=tr.position;
		color=new Vector4(1,1,1,0);
		black=new Texture2D(1,1,TextureFormat.ARGB32,false);
		black.SetPixel(0,0,Color.black);
		black.Apply();
		first=Color.white;first.a=0;
		rand=new Color(Random.Range(0.2F,1.0F),Random.Range(0.0F,1.0F),Random.Range(0.1F,0.8F));
		rand.a=0;
		colorFade=new Vector4(0,0,0,0);
		opaq=new Color(1,1,1,1);
		trans=new Color(1,1,1,0);
		camera = GetComponent<Camera>();
		target=yada.transform;
		/*if(!CameraTitleScreen.isitCool){
			CameraTitleScreen.isitCool=true;}*/
		tr.position=new Vector3(-2.24F,-6.81F,-83.1F-distance);
		lolgo.text="booPboP";
		lolgo.text="b Y e e Y 3";
		lolgo.text="b Y ";
		lolgo.text="be it yourself";
		lolgo.text="biy";
		lolgo.text="by I";
		lolgo.text="be why";
		lolgo.text="B Y ";
		lolgo.text="Y";
		lolgo.text="enDed up enDever";
		lolgo.color=trans;curColor=lolgo.color;
		curColorOpaq.a=1;
		fadeOutFinish.color=Color.white;
		fade.color=trans;fade2.color=trans;
		enter.color=trans;
		click.color=trans;
		aud=GetComponent<AudioSource>();yada.SetActive(false);
		birdo.SetActive(false);Deffins.SetActive(false);
		for(i=0;i<titleElements.Count;i++){
			titleElements[i].titleElement.color=trans;
			blahblah=new List<float>();
			if(i+1==titleElements.Count){
				waste=Mathf.Pow(2,Random.Range(2,5));
				blahblah.Add(waste);
				blahblah.Add(waste);
				blahblah.Add(1);}
			else
			{	blahblah.Add(0);
				blahblah.Add(Mathf.Pow(2,Random.Range(2,5)));
				blahblah.Add(0);}
			timers.Add(blahblah);}
	}
	void FixedUpdate(){ // pressSpace=false;logoDone=false; Uncomment for non-entrance
		if(Input.GetAxis("Target")!=0){pressSpace=true;logoDone=true;}
		if(!pressSpace){
			if(!yHit){
				t+=Time.deltaTime;otherTimer+=Time.deltaTime;otherTimer2+=Time.deltaTime;
				if(t>=speedCinematic){
					first=rand;
					blahFloat=Random.Range(0.9F,1);
					rand=new Color(blahFloat,blahFloat,blahFloat);
					rand=Color.Lerp(first,rand,t/speedCinematic);
					t-=speedCinematic;}
				else 
				{	go=Color.Lerp(first,rand,t/speedCinematic);
					/*Y.color=go;*/space.color=go;}
				
				if(otherTimer>=speed-1){
					Y.color=new Color(Y.color.r,Y.color.g,Y.color.b,1);
					space.color=Y.color;}
				else 
				{	Y.color=new Color(Y.color.r,Y.color.g,Y.color.b,otherTimer/(speed-1));
					space.color=Y.color;}
				if(Input.GetAxis("Eyes")!=0) {yHit=true;otherTimer2=0;}}
			else
			{	if(otherTimer2>=speed){
					pressSpace=true;t=0;Y.color=trans;space.color=trans;otherTimer2=0;Debug.Log("fdsfsafdfa");
					aud.Play();rand=new Color(Random.Range(0.3F,1.0F), Random.Range(0.2F,1.0F), Random.Range(0.0F,0.8F));}
				else
				{	if(changing){
						fadeOutFinish.color=Color.Lerp(Color.white,colorFade,(otherTimer2)/speed);}
					else 
					{	Y.color=new Color(Y.color.r,Y.color.g,Y.color.b,1-((otherTimer2)/speed)*1.2F);
						space.color=Y.color;}
					otherTimer2+=Time.deltaTime;}}}
		else if(!logoDone){
			if(otherTimer2<9){
				t+=Time.deltaTime;otherTimer+=Time.deltaTime;otherTimer2+=Time.deltaTime;
				if(t>=speedCinematic){
					first=rand;
					rand=new Color(Random.Range(0.3F,1.0F), Random.Range(0.2F,1.0F), Random.Range(0.0F,0.8F));
					rand=Color.Lerp(first,rand,t/speedCinematic);
					t-=speedCinematic;}
				else 
				{	go=Color.Lerp(first,rand,t/speedCinematic);
					lolgo.color=go;}
				
				if(otherTimer>=speed-1){
					lolgo.color=new Color(lolgo.color.r,lolgo.color.g,lolgo.color.b,1);}
				else 
				{	lolgo.color=new Color(lolgo.color.r,lolgo.color.g,lolgo.color.b,otherTimer/(speed-1));}}
			else
			{	if(otherTimer2>=9+speed){
					if(changing){
						logoDone=true;t=0;lolgo.color=trans;
						fadeOutFinish.color=colorFade;lolgo.text="";}
					else
					{	if(!birdo.activeSelf){
							/*yada.SetActive(true);*/birdo.SetActive(true);birdRend.material.color=colorFade;
							}
							otherTimer2=9;//audSFX.PlayOneShot(birdCrack);
							changing=true;}}
				else
				{	if(changing){
						fadeOutFinish.color=Color.Lerp(Color.white,colorFade,(otherTimer2-9)/speed);
						birdRend.material.color=Color.Lerp(colorFade,Color.white,(otherTimer2-9)/speed);
						if(birdRend.material.color.a>0.54F){fadeVomit=true;}}
					else lolgo.color=new Color(lolgo.color.r,lolgo.color.g,lolgo.color.b,1-((otherTimer2-9)/speed)*1.2F);
					otherTimer2+=Time.deltaTime;}}
		}
		else if(!starting){
			if(timey>11){
				if(Input.GetButtonDown("Eyes")){
					for(i=0;i<titleElements.Count;i++){
						if(!titleElements[i].seen) 
							titleElements[i].titleElement.enabled=false;}}
				else if(Input.GetButtonUp("Eyes")){
					for(i=0;i<titleElements.Count;i++){
						titleElements[i].titleElement.enabled=true;}}
				for(i=0;i<timers.Count;i++){
					if(timers[i][2]==1){
						if(timers[i][0]<=0){
							titleElements[i].titleElement.color=trans;
							newGuys.Add(titleElements[i]);
							timers[i][2]=0;
							timers[i][1]=Mathf.Pow(2,Random.Range(2,4));
							timers[i][0]=0;
							if(newGuys.Count>0){
								coin=Random.Range(0,2);
								if(titleElements[i].after==null)
									distText=3000;
								else distText=titleElements[i].after.position.x-titleElements[i].current.position.x;
								if(coin==0){ //close to beforeGetComponent<RectTransform>()
									j=0;
									foreach(Transform child in newGuys[0].current){
										dumdum=child as RectTransform;
										if(dumdum.rect.width<distText){
											dummy=child.GetComponent<Text>();
											titleElements[i].titleElement.enabled=false;
											titleElements[i].titleElement=dummy;
											titleElements[i].titleElement.enabled=true;
											dumdum.position=titleElements[i].current.position;
											break;}
										j++;}
									if(j==newGuys.Count)
									{	titleElements[i].titleElement.enabled=false;
										titleElements[i].titleElement=jok;
										titleElements[i].titleElement.enabled=true;
										dumdum=jok.GetComponent<RectTransform>();
										dumdum.position=titleElements[i].current.position;}
									newGuys.RemoveAt(j);}
								else
								{	j=0;
									foreach(Transform child in newGuys[0].current){
										dumdum=child as RectTransform;
										if(dumdum.rect.width<distText){
											dummy=child.GetComponent<Text>();
											titleElements[i].titleElement.enabled=false;
											titleElements[i].titleElement=dummy;
											titleElements[i].titleElement.enabled=true;
											if(titleElements[i].after==null)
												dumdum.position=titleElements[i].current.position;
											else dumdum.position=new Vector3(titleElements[i].after.position.x-dumdum.rect.width,titleElements[i].current.position.y,0);
											break;}
										j++;}
									if(j==newGuys.Count)
									{	titleElements[i].titleElement.enabled=false;
										titleElements[i].titleElement=jok;
										titleElements[i].titleElement.enabled=true;
										dumdum=jok.GetComponent<RectTransform>();
										if(titleElements[i].after==null)
											dumdum.position=titleElements[i].current.position;
										else dumdum.position=new Vector3(titleElements[i].after.position.x-dumdum.rect.width,titleElements[i].current.position.y,0);}
									newGuys.RemoveAt(j);}}
							else 
							{	titleElements[i].titleElement.enabled=false;
								titleElements[i].titleElement=jok;
								titleElements[i].titleElement.enabled=true;
								dumdum=jok.GetComponent<RectTransform>();
								dumdum.position=titleElements[i].current.position;}
							coin=Random.Range(0,4);
							if(coin==0)
								titleElements[i].seen=true;
							else titleElements[i].seen=false;}
						else
						{	titleElements[i].titleElement.color=ColorSlerp(trans,opaq,timers[i][0]/timers[i][1]);
							timers[i][0]-=Time.deltaTime;}}
					else
					{	if(timers[i][0]>=timers[i][1]){
							titleElements[i].titleElement.color=opaq;
							timers[i][2]=1;
							timers[i][1]=Mathf.Pow(2,Random.Range(0,5));
							timers[i][0]=timers[i][1];}
						else 
						{	titleElements[i].titleElement.color=ColorSlerp(trans,opaq,timers[i][0]/timers[i][1]);
							timers[i][0]+=Time.deltaTime;}}}}
			else if(timey>6){
				if(t>=speed){
					fade.color=opaq;/*fade2.color=opaq;*/titit.enabled=false;eyesOpen.linesAffect=true;}
				else 
				{	go=Color.Lerp(trans,opaq,t/speed);
					fade.color=go;/*fade2.color=go;*/
					t+=Time.deltaTime;}}
			else if(timey>3.8F){
				titit.enabled=true;}
			/*else if(timey>3.07F && aud.clip==funnyTimes){ 
				aud.Stop();aud.clip=fensil;}*/
			else if(timey>2 && !audioMoment2)
			{	partic1.SetActive(true);partic2.SetActive(true);yada.SetActive(true);/*aud.PlayOneShot(birdIn);*/
				audioMoment2=true;Deffins.SetActive(true);
				if(!audioMoment){aud2.Play();audioMoment=true;}}
			timey+=Time.deltaTime;
			if(Input.GetAxis("Start")!=0 && timey>11){//birdAnim.Play("roar");
				aud2.Stop();aud2.PlayOneShot(birdIn);birdTimer=birdIn.length;
				starting=true;timey=9;audioMoment=false;
				t=0;}}
		else if(starting && timey<13){
			if(!audioMoment){
				if(t>=speed-2){
					fadeOut.color=opaq;t=0;aud2.clip=yope;aud2.loop=false;//birdo.SetActive(false);yada.SetActive(false);
					aud2.Play();audioMoment=true;timey=0;}
				else if(birdTimer<=0)
				{	fadeOut.color=Color.Lerp(trans,opaq,t);
					birdRend.material.color=Color.Lerp(opaq,trans,t);
					sprito.material.color=birdRend.material.color;
					t+=Time.deltaTime;}
				else birdTimer-=Time.deltaTime;}
			else
			{	if(timey>7.8F) music.color=trans;
				timey+=Time.deltaTime;
				if(timey>=13){
					dialog.SetActive(true);
					//start loading next scene.
					StartCoroutine(sceneManager.LoadIn("glowattempts",false));
					first=fadeOut.color;
					rand=new Color(Random.Range(0.3F,1.0F), Random.Range(0.2F,1.0F), Random.Range(0.0F,0.8F));}}}
	/* Color Fader */
		else if(!toTheGame)
		{	if(toTheWakeUp){
				birdRend.material.color=opaq;
				sprito.material.color=opaq;
				/*eyeL.SetActive(true);eyeL2.SetActive(true);eyeR.SetActive(true);eyeR2.SetActive(true);
				SceneManager.LoadSceneAsync("lite", LoadSceneMode.Additive)*/;}	
			else
			{	if(t>=speedCinematic){
					first=rand;
					rand=new Color(Random.Range(0.3F,1.0F), Random.Range(0.2F,1.0F), Random.Range(0.0F,0.8F));
					rand=Color.Lerp(first,rand,t/speedCinematic);
					t-=speedCinematic;}
				else 
				{	go=Color.Lerp(first,rand,t/speedCinematic);
					fadeOut.color=go;
					t+=Time.deltaTime;}}}
		else if(toTheGame){
			if(finalTrans>=3.5F){
				//if(fadeOutFinish.color!=Color.black)
				fadeOutFinish.color=Color.black;
				//Application.LoadLevel("glowattempts");
				}
			else
			{	go=Color.Lerp(colorFade,Color.black,finalTrans/3.5F);
				fadeOutFinish.color=go;
				finalTrans+=Time.deltaTime;}}
		if(fadeVomit){audTimer+=Time.deltaTime;
			aud.volume=Mathf.Lerp(1,0,audTimer/2);
			if(aud.volume==0){
				fadeVomit=false;audTimer=0;}}
		if(camFollows){
			if(camFollowsY) tr.position+=Player.camOffset;
			else tr.position+=new Vector3(Player.camOffset.x,0,Player.camOffset.z);
			//new Vector3(Player.camOffset.x,0,0);
			dedication.position-=new Vector3(Player.camOffset.x*3.6F,0,0);
			for(i=0;i<titleElements.Count;i++){
				titleElements[i].current.position-=new Vector3(Player.camOffset.x*6.8F,0,Player.camOffset.z*6.4F);}
		}
	}
	
	Color ColorSlerp(Color from,Color to,float time){
		Vector3 f=new Vector3(from.r,from.g,from.b);
		Vector3 t=new Vector3(to.r,to.g,to.b);
		f=Vector3.Slerp(f,t,time);
		t.x=0;t.y=0;t.z=from.a;
		Vector3 tmp=new Vector3(0,0,to.a);
		tmp=Vector3.Lerp(t,tmp,time);
		return new Color(f.x,f.y,f.z,tmp.z);
	}
	
	
	
	/*
switchable options, which tumble into certain variations at alternate time thoughts
				global switch times chosen overriding variations-> taken from fading to opaq
				(letter groups twinkle&fade to pickup variations)
				 
				Press starP -> Trepest sta77 -> Trepest rats -> 
				
				V :GUI 2D -> 
				
				2 am slekshun -> 
				(jok uhl) -> 
				jok:L -> dotM
		
				when I can actually approach this..
				groups of each set of variations.
				one group whose children are different forms of the words
				
				nothing special, have a lot of these at posX+width OR before.... so, posX-replacerWidth
				
				find of all groupRenderer's the one with least alpha. when that one dies, replace it with this.
				
				an issue of spatial workings
				
				keep track of befores, short variations to build up to long
				long variations to break down to short 
				trying this without queues and massive data.
				
				g
				

										Generate meaninful variations as a child of some central tone
										just fade out from on variation to the next.
											variations are stored as children.
												transistions are fast.
										
										
										don't look at me like that
										Seem 7!!
										to live => i //l/i/f/e 
										jessuUPpinCRUreArTouGH air=MEu_u
										
										
										   so if you go to CHINA
										   and you want to get a job from CHYNA
										   
										   newGuy[0]
										   
											while alpha not zero ... fade while switching variations
											alpha goes to zero, groupText.titleElement goes to queues
												search for variation through children of newGuy for proper width
													newGuy is set to that child
												if not found, move to back of queue
												else newGuy is set to 








	void OnGUI(){
		if(timey>10){
			color.a+=(timey-10)/20;
			GUI.color=color;}
		else GUI.color=new Vector4(1,1,1,0);
		GUI.DrawTexture(new Rect(cameraBlah.toResize[0].rect.x*(float)Screen.width,cameraBlah.toResize[0].rect.y*(float)Screen.height,
		cameraBlah.toResize[0].rect.width*(float)Screen.width/2.5F,cameraBlah.toResize[0].rect.height*(float)Screen.height/2.5F),
		logo,ScaleMode.ScaleToFit);
		
		if(starting){
			colorFade.a+=Time.deltaTime/3.5F;
			GUI.color=colorFade;
			GUI.DrawTexture(new Rect(0,0,(float)Screen.width,(float)Screen.height),
			black,ScaleMode.StretchToFill);}
	}*/
/*    START SCREEN: workinonit
		preBuild
		- 1 bird animation of opening wings, neck craning up. (re-use with bird call-to-opening)
		- 1 of it flapping its wings less strongly, and then tucking them back in
		- 1 bird idle animation
		+ polish fencil song
		+ polish bird calls
		+ IDEA: replace text of Texts with slight differences when it *blinks brighter* (like 'starP' to 'Parss') until 
				the rest loads in. fill the entire screen with text with sentence switches
		+ IDEA: opening your eyes shows lines you can travel on, which lead you to the left of the screen.
				where the credits will be. Dedicated to Bill Donahue https://www.youtube.com/channel/UCinvwSeMdmuuzyMWrS0tGTg
														Ben Eshbach
														haruhi suzumiya
															LEAVES
														come from TREES
				Right side has philosophers shown when eyes closed
						Ones and zeroes, not film and pasta. Good nature, or soft rules? 
		- OPT: paste picture of fencil on flare50mm
		- OPT: 1 bird animation of bird on ground flapping wings and shooting particles at fensil
		- OPT: idle animations for in and out
		postBuild
		+ fix the UI discrepancies in build*/
/*	  OPENING: In process.
		preBuild
		+ re-contextualize song to fit with story (as freakout-to-dream-enter moment). song was meant to be as much about
			a breakerdown as a mechanical industrial ambience
		+ re-work demo1 as the song of dreaming. tambourines i will eed them
		+ Dialogue: fencil is having a conversation with other people
					they're talking about rails and the color fuschia
					and then people in the dream start to dream about other people who are dreaming of other colors.
					
		- and then fencil wakes up briefly in a room, 
			his eyes are closed the whole time
			mirror body sphere fencil,
			fencil behind wall, try to control mirror fencil and this REVEALS real fencil when you move
					downwards. interact with the mirror. 
			"i am dying"
			bird is not hanging upside downt h ewhole time
			when you approach the mirror, whether you interact with it or not, the bird will show up
			the bird is facing the camera
			the bird opens up its wings and the lights go out, but those particles bounce around the world.
			all you see are the bird and the particles while the bird is fading out
			the colors fade in. a voice pipes up "you are dead"

		-	words shift way into sktzzyring vouels to glitchedfx as colors fade in and out
				slowly the sound field is shifting from the right side to the left, with the left
					becoming distorted and messed up, until there is only the right channel, mostly only coming
						out of the right channel
				and also the visual field is slowly starting to see differently, at first, altogether, and
					then like the sound the right side becomes distorted until there is only the left side
						mostly taking up the left of the screen
			
			his eyes are open while he sleeps, as are all of these things. so he is trying to close them to wake up,
				but can't, and is afraid. 
			
			he starts to worry while you gain control of the character. 
			
			and then this whole scene kinda gets strange as the two sides of the screen representing both eyes "sink"
			into their proper please in the UI of the game.
			
			"why can't I close my eyes?"
					
		- because his eyes are open you are treated to a dream level with lines only
			travelling around a changing space.
			
			when you approach lines going in and out appear and disappear. 
			
			when you get to a certain place (maybe a dead end, one you pass in, OnTriggerExit) you start to hear a sound..
					.... a bird sound
					.... and flapping wing sounds
					.... and chirpy noises
					until you finally, through the coming and going paths, come across a missingno. like shrinks out of the room
			
			... ... 
			
			"I can't close my eyes! why... they won't close!"*/
/*    INGAME: Unhandled.
		- start on an antenna house
		- fencil model, right ear and left eye golded
		- mesh mountain cave VERY FAR in the distance, but can move there very quickly
		- character there sees you, "that could be dangerous. I don't know fencil like, what if it's contagious? you sleep in Closer Quarters."
									"i really just want to take to ralf abou the new order"
									"No, that's unacceptable, what if it mutates into some kind of writhing freak spirit of Death?"
									"that's a myth"
									"why can't you see that people are disappearing?"
									"it's ALWAYS been a myth. they're just *gone*, literally."
									"no they're not. what if this is how people die?"
									*fencil takes a breath*
									"OK, you don't have to believe in what I think is absolutely occurring. but-"
									"im concerned."
									"..maybe Jam can do something it. see what he did for reihl?"
									"awesome."
									"awesome! it's like the perfect excuse to talk to him."
									*fencil is unconvinced*
									"ill tell ralf youll be coming soon."
									"*posture* no, we'll just walk together. I think you have a better clue of what to ask for"
									"... and this is one step closer to you believing in death"
									"maybe when Im dead, lol."
									----(while walking)
									"do you want to dead?"
									"I don't know"
									""
									
									----
									
									----
									*conversation about death and laughter ensues*
		- on their way into another antennae landarea
		- "freestyle 4" sounds of mindskip, fencil is a layer "out" from person, and sees them reacting
				person reacting, but cannot reach them. *the fucking birds!*
		+ fencil goes through a level with the sounds of a bird constantly around. 
				similar to the dream level but actually with background and other things
		+ at part of level, densil shows up
		- fencil falls through a line again (again conparable to a mind skip or if there was a scratching sound and brisk flash-cut
																									to a new room)
		- fencil is lying facedown on the ground in a terrifying space as a bird makes dark glitchy noises
			but heavenly music plays while fencil is murmuring (actually record this, your voice)
			fencil crawls over to the bird and it begins pecking a part of his body off. 
			fencil moans and hysterical laughter bounders about as he begins to dream
		+ OPT: single pixels of flickering stars, almost like dead pixels that come and go
		+ OPT: expressions,sfx of shimmer,sfx of pop
		
		
		
		
		_O_
	    /_\
		| |
	__O_| |
		| |
		| |
		| |__O_
		| |
		| |             _
		| |            / \
		| |___O		   \_||			
		| |				  /			/\
		| |				  |		   /  \
		| |______________/________/____\______
		
		
		

		
		
*/
}
