using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CameraTitleScreen : MonoBehaviour {

#region Declarations
	public static bool isitCool = false;
	public static bool toTheGame = false;
	public static bool toTheWakeUp = false;
	public static bool toTheLucidDreaming = false;
	public static bool camFollows = false; /* i should be false */
	public static bool camFollowsY = false;
	public GameObject dialog, yada, birdo, partic1, partic2, Deffins;
	public Transform dedication;
	public GameObject eyeL, eyeL2, eyeR, eyeR2;
	public Animator birdAnim;
	public AudioSource aud2;
	public AudioSource audSFX;
	public SpriteRenderer birdRend;
	public SpriteRenderer sprito;
	public List<Transform> SeekTo = new List<Transform>();
	public Transform lookHere;
	public AudioClip yope, birdIn, birdCrack, funnyTimes, fensil;
	public Texture logo;
	public CameraBlah cameraBlah;
	public Text dash, GUI, slekshun, jok, prn, fade, fade2, enter, click, music, lolgo, titit, dummy;
	public Image Y, space;
	public Image fadeOut, fadeOutFinish;
	public List<List<float>> timers = new List<List<float>>(); //timers[x][0] = counter, timers[x][1] = counterAmt,timers[x][2]=direction;
	public List<groupText> titleElements = new List<groupText>();
	public List<groupText> newGuys = new List<groupText>();
	public float distance, speed, speedCinematic;
	Transform target;
	Camera camera;
	AudioSource aud;
	Transform tr;
	Texture2D black;
	RectTransform dumdum;
	Vector3 startPos;
	Vector3 serialMons = Vector3.zero;
	Color color, colorFade, curColor, curColorOpaq;
	Color opaq, trans, go;
	Color first, rand;
	List<float> blahblah;
	float drawDepth = -1000, fadeSpeed = 0.8F;
	float placement;
	float flyTime = 0;
	float timey = 0, twoTimey = 0, birdTimer, audTimer = 0;
	float t = 0, otherTimer = 0, otherTimer2 = 0;
	float finalTrans = 0;
	float fadeTimer = 0;
	float distText;
	float blahFloat;
	float waste;
	int i = 0, j = 0, coin;
	float journeyTime = 40;
	bool starting = false;
	bool fadeVomit = false;
	bool audioMoment = false, logoDone = false, changing = false, audioMoment2 = false, pressSpace = false;
	bool yHit = false;
	[System.Serializable]
	public class groupText {
		public Text titleElement;
		public RectTransform current;
		public RectTransform before;
		public RectTransform after;
		public bool seen;
		
		public groupText(Text titleElement, RectTransform current, RectTransform before, RectTransform after) {
			this.titleElement = titleElement;
			this.current = current;
			this.before = before;
			this.after = after;
			this.seen = false;
        }
	}
	#endregion
	void Start ()
    {
		tr = transform;
		startPos = tr.position;
		color = new Vector4(1,1,1,0);
		black = new Texture2D(1,1,TextureFormat.ARGB32,false);
		black.SetPixel(0,0,Color.black);
		black.Apply();
		first = Color.white;first.a=0;
		rand = new Color(Random.Range(0.2F,1.0F), Random.Range(0.0F,1.0F), Random.Range(0.1F,0.8F));
		rand.a = 0;
		colorFade = new Vector4(0,0,0,0);
		opaq = new Color(1,1,1,1);
		trans = new Color(1,1,1,0);
		camera = GetComponent<Camera>();
		target = yada.transform;
		tr.position = new Vector3(-2.24F, -6.81F, -83.1F - distance);
		lolgo.text="booPboP";
		lolgo.text="b Y e e Y 3";
		lolgo.text="b Y ";
		lolgo.text="be it yourself";
		lolgo.text="biy";
		lolgo.text="by I";
		lolgo.text="be why";
		lolgo.text="B Y ";
		lolgo.text="Y";
		lolgo.text="enD up enDever";
		lolgo.color = trans; curColor = lolgo.color;
		curColorOpaq.a = 1;
		fadeOutFinish.color = Color.white;
		fade.color = trans; fade2.color = trans;
		enter.color = trans;
		click.color = trans;
		aud = GetComponent<AudioSource>();
        yada.SetActive(false);
		birdo.SetActive(false);
        Deffins.SetActive(false);
		for (i=0; i < titleElements.Count; i++) {
			titleElements[i].titleElement.color = trans;
			blahblah = new List<float>();
			if ((i + 1) == titleElements.Count) {
				waste=Mathf.Pow(2,Random.Range(2,5));
				blahblah.Add(waste);
				blahblah.Add(waste);
				blahblah.Add(1);
            } else {	
                blahblah.Add(0);
				blahblah.Add(Mathf.Pow(2,Random.Range(2,5)));
				blahblah.Add(0);
            }
			timers.Add(blahblah);
        }
	}
	void FixedUpdate () { // pressSpace=false;logoDone=false; Uncomment for non-entrance
		if (Input.GetAxis("Target") !=0 ) {
            pressSpace = true;
            logoDone = true;
        }
		if (!pressSpace) {
			if (!yHit) {
				t += Time.deltaTime;
                otherTimer += Time.deltaTime;
                otherTimer2 += Time.deltaTime;
				if (t >= speedCinematic) {
					first = rand;
					blahFloat = Random.Range(0.9F,1);
					rand = new Color(blahFloat, blahFloat, blahFloat);
					rand = Color.Lerp(first, rand, t / speedCinematic);
					t -= speedCinematic;
                } else {	
                    go = Color.Lerp(first, rand, t / speedCinematic);
					/*Y.color=go;*/
                    space.color = go;
                }
				
				if (otherTimer >= speed - 1) {
					Y.color = new Color(Y.color.r,Y.color.g,Y.color.b,1);
					space.color = Y.color;
                } else {	
                    Y.color = new Color(Y.color.r, Y.color.g, Y.color.b, otherTimer / (speed - 1));
					space.color = Y.color;
                }
				if (Input.GetAxis("Eyes") != 0) {
                    yHit = true;
                    otherTimer2 = 0;
                }
            } else {	
                if (otherTimer2 >= speed) {
					pressSpace = true;
                    t = 0;
                    Y.color = trans;
                    space.color = trans;
                    otherTimer2 = 0;
                    aud.Play();
                    rand = new Color(Random.Range(0.3F,1.0F), Random.Range(0.2F,1.0F), Random.Range(0.0F,0.8F));
                } else {	
                    if (changing) {
						fadeOutFinish.color = Color.Lerp(Color.white, colorFade, otherTimer2 / speed);
                    } else {	
                        Y.color = new Color(Y.color.r, Y.color.g, Y.color.b, 1 - ((otherTimer2) / speed) * 1.2F);
						space.color=Y.color;
                    }
					otherTimer2 += Time.deltaTime;
                }
            }
        } else if (!logoDone) {
			if (otherTimer2 < 9) {
				t += Time.deltaTime;
                otherTimer += Time.deltaTime;
                otherTimer2 += Time.deltaTime;
				if (t >= speedCinematic) {
					first = rand;
					rand = new Color(Random.Range(0.3F,1.0F), Random.Range(0.2F,1.0F), Random.Range(0.0F,0.8F));
					rand = Color.Lerp(first, rand, t / speedCinematic);
					t -= speedCinematic;
                } else {	
                    go = Color.Lerp(first, rand, t / speedCinematic);
					lolgo.color = go;
                }
				
				if (otherTimer >= speed - 1) {
					lolgo.color = new Color(lolgo.color.r, lolgo.color.g, lolgo.color.b, 1);
                } else {	
                    lolgo.color = new Color(lolgo.color.r, lolgo.color.g, lolgo.color.b, otherTimer / (speed - 1));
                }
            } else {	
                if (otherTimer2 >= 9 + speed) {
					if (changing) {
						logoDone = true;
                        t = 0;
                        lolgo.color = trans;
						fadeOutFinish.color = colorFade;
                        lolgo.text = "";
                    } else {	
                        if (!birdo.activeSelf) {
							/*yada.SetActive(true);*/
                            birdo.SetActive(true);
                            birdRend.material.color = colorFade;
                        }
                        otherTimer2 = 9;//audSFX.PlayOneShot(birdCrack);
                        changing = true;
                    }
                } else {	
                    if (changing) {
						fadeOutFinish.color = Color.Lerp(Color.white, colorFade, (otherTimer2 - 9) / speed);
						birdRend.material.color = Color.Lerp(colorFade, Color.white, (otherTimer2 - 9) / speed);
						if (birdRend.material.color.a > 0.54F) {
                            fadeVomit=true;
                        }
                    } else {
                        lolgo.color = new Color(lolgo.color.r, lolgo.color.g, lolgo.color.b, 1 - ((otherTimer2 - 9) / speed) * 1.2F);
                    }
                    otherTimer2 += Time.deltaTime;
                }
            }
		}
		else if (!starting) {
			if (timey > 11) {
				if (Input.GetButtonDown("Eyes")) {
					for (i = 0; i < titleElements.Count; i++) {
						if (!titleElements[i].seen) 
							titleElements[i].titleElement.enabled = false;
                    }
                } else if (Input.GetButtonUp("Eyes")) {
					for (i = 0; i < titleElements.Count; i++) {
						titleElements[i].titleElement.enabled = true;
                    }
                }
				for (i = 0; i < timers.Count; i++) {
					if (timers[i][2] == 1) {
						if (timers[i][0] <= 0) {
							titleElements[i].titleElement.color = trans;
							newGuys.Add(titleElements[i]);
							timers[i][2] = 0;
							timers[i][1] = Mathf.Pow(2, Random.Range(2,4));
							timers[i][0] = 0;
							if (newGuys.Count > 0) {
								coin = Random.Range(0,2);
								if (titleElements[i].after == null)
									distText = 3000;
								else distText = titleElements[i].after.position.x - titleElements[i].current.position.x;
								if (coin == 0) {
									j = 0;
									foreach (Transform child in newGuys[0].current) {
										dumdum = child as RectTransform;
										if (dumdum.rect.width < distText) {
											dummy = child.GetComponent<Text>();
											titleElements[i].titleElement.enabled = false;
											titleElements[i].titleElement = dummy;
											titleElements[i].titleElement.enabled = true;
											dumdum.position = titleElements[i].current.position;
											break;
                                        }
										j++;
                                    }
									if (j == newGuys.Count) {	
                                        titleElements[i].titleElement.enabled = false;
										titleElements[i].titleElement = jok;
										titleElements[i].titleElement.enabled = true;
										dumdum = jok.GetComponent<RectTransform>();
										dumdum.position = titleElements[i].current.position;
                                    }
									newGuys.RemoveAt(j);
                                } else {	
                                    j = 0;
									foreach (Transform child in newGuys[0].current) {
										dumdum = child as RectTransform;
										if (dumdum.rect.width < distText) {
											dummy = child.GetComponent<Text>();
											titleElements[i].titleElement.enabled = false;
											titleElements[i].titleElement = dummy;
											titleElements[i].titleElement.enabled = true;
											if (titleElements[i].after == null)
												dumdum.position = titleElements[i].current.position;
											else dumdum.position = new Vector3(titleElements[i].after.position.x - dumdum.rect.width, titleElements[i].current.position.y, 0);
											break;
                                        }
										j++;
                                    }
									if (j == newGuys.Count) {	
                                        titleElements[i].titleElement.enabled = false;
										titleElements[i].titleElement = jok;
										titleElements[i].titleElement.enabled = true;
										dumdum = jok.GetComponent<RectTransform>();
										if (titleElements[i].after == null)
											dumdum.position = titleElements[i].current.position;
										else dumdum.position = new Vector3(titleElements[i].after.position.x - dumdum.rect.width, titleElements[i].current.position.y, 0);
                                    }
									newGuys.RemoveAt(j);
                                }
                            } else {	
                                titleElements[i].titleElement.enabled = false;
								titleElements[i].titleElement = jok;
								titleElements[i].titleElement.enabled = true;
								dumdum = jok.GetComponent<RectTransform>();
								dumdum.position = titleElements[i].current.position;
                            }
							coin = Random.Range(0,4);
							if (coin == 0)
								titleElements[i].seen = true;
							else titleElements[i].seen = false;
                        } else {	
                            titleElements[i].titleElement.color = ColorSlerp(trans, opaq, timers[i][0] / timers[i][1]);
							timers[i][0] -= Time.deltaTime;
                        }
                    } else {	
                        if (timers[i][0] >= timers[i][1]) {
							titleElements[i].titleElement.color = opaq;
							timers[i][2] = 1;
							timers[i][1] = Mathf.Pow(2, Random.Range(0,5));
							timers[i][0] = timers[i][1];
                        } else {	
                            titleElements[i].titleElement.color = ColorSlerp(trans, opaq, timers[i][0] / timers[i][1]);
							timers[i][0] += Time.deltaTime
                        }
                    }
                }
            } else if (timey > 6) {
				if (t >= speed) {
					fade.color = opaq;
                    // fade2.color=opaq;
                    titit.enabled = false;
                    eyesOpen.linesAffect = true;
                } else {	
                    go = Color.Lerp(trans, opaq, t / speed);
					fade.color = go;
                    // fade2.color=go;
					t += Time.deltaTime;
                }
            } else if (timey > 3.8F) {
				titit.enabled = true;
            }
			/*else if(timey>3.07F && aud.clip==funnyTimes){ 
				aud.Stop();aud.clip=fensil;}*/
			else if (timey > 2 && !audioMoment2) {	
                partic1.SetActive(true);
                partic2.SetActive(true);
                yada.SetActive(true);
                // aud.PlayOneShot(birdIn);
				audioMoment2 = true;
                Deffins.SetActive(true);
				if (!audioMoment) {
                    aud2.Play();
                    audioMoment = true;
                }
            }
			timey += Time.deltaTime;
			if (Input.GetAxis("Start") != 0 && timey > 11) {
                // birdAnim.Play("roar");
				aud2.Stop();
                aud2.PlayOneShot(birdIn);
                birdTimer = birdIn.length;
				starting = true;
                timey = 9;
                audioMoment = false;
				t = 0;
            }
        } else if (starting && timey < 13) {
			if (!audioMoment) {
				if (t >= speed - 2) {
					fadeOut.color = opaq;
                    t = 0;
                    aud2.clip = yope;
                    aud2.loop = false;
                    // birdo.SetActive(false);
                    // yada.SetActive(false);
					aud2.Play();
                    audioMoment = true;
                    timey = 0;
                } else if (birdTimer <= 0) {	
                    fadeOut.color = Color.Lerp(trans, opaq, t);
					birdRend.material.color = Color.Lerp(opaq, trans, t);
					sprito.material.color = birdRend.material.color;
					t += Time.deltaTime;
                } else {
                    birdTimer -= Time.deltaTime;
                }
            } else {	
                if (timey > 7.8F) {
                    music.color = trans;
                }
				timey += Time.deltaTime;
				if (timey >= 13) {
					dialog.SetActive(true);
					// start loading next scene.
					StartCoroutine(sceneManager.LoadIn("glowattempts", false));
					first = fadeOut.color;
					rand = new Color(Random.Range(0.3F, 1.0F), Random.Range(0.2F, 1.0F), Random.Range(0.0F, 0.8F));
                }
            }
        } else if (!toTheGame) {	
            if (toTheWakeUp) {
				birdRend.material.color = opaq;
				sprito.material.color = opaq;
				/*eyeL.SetActive(true);eyeL2.SetActive(true);eyeR.SetActive(true);eyeR2.SetActive(true);
				SceneManager.LoadSceneAsync("lite", LoadSceneMode.Additive)*/;
            } else {	
                if (t >= speedCinematic) {
					first = rand;
					rand = new Color(Random.Range(0.3F, 1.0F), Random.Range(0.2F, 1.0F), Random.Range(0.0F, 0.8F));
					rand = Color.Lerp(first, rand, t / speedCinematic);
					t -= speedCinematic;
                } else {	
                    go = Color.Lerp(first, rand, t / speedCinematic);
					fadeOut.color = go;
					t += Time.deltaTime;
                }
            }
        } else if (toTheGame) {
			if (finalTrans >= 3.5F) {
				// if(fadeOutFinish.color!=Color.black)
				fadeOutFinish.color = Color.black;
				// Application.LoadLevel("glowattempts");
            } else {	
                go = Color.Lerp(colorFade, Color.black, finalTrans / 3.5F);
				fadeOutFinish.color = go;
				finalTrans += Time.deltaTime;
            }
        }
		if (fadeVomit) {
            audTimer += Time.deltaTime;
			aud.volume = Mathf.Lerp(1, 0, audTimer / 2);
			if (aud.volume == 0) {
				fadeVomit = false;
                audTimer = 0;
            }
        }
		if (camFollows) {
			if (camFollowsY) {
                tr.position += Player.camOffset;
            } else {
                tr.position += new Vector3(Player.camOffset.x, 0, Player.camOffset.z);
            }
			// new Vector3(Player.camOffset.x,0,0);
			dedication.position -= new Vector3(Player.camOffset.x * 3.6F, 0, 0);
			for (i = 0; i < titleElements.Count; i++) {
				titleElements[i].current.position -= new Vector3(Player.camOffset.x * 6.8F, 0, Player.camOffset.z * 6.4F);
            }
		}
	}
	
	Color ColorSlerp(Color from, Color to, float time) {
		Vector3 f = new Vector3(from.r, from.g, from.b);
		Vector3 t = new Vector3(to.r, to.g, to.b);
		f = Vector3.Slerp(f, t, time);
		t.x = 0;
        t.y = 0;
        t.z = from.a;
		Vector3 tmp = new Vector3(0, 0, to.a);
		tmp = Vector3.Lerp(t, tmp, time);
		return new Color(f.x, f.y, f.z, tmp.z);
	}
}
