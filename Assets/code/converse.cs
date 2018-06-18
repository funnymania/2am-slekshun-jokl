using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class converse : MonoBehaviour {
	public List<speaking> speechBubbles = new List<speaking>();
	public List<speaking> lastOne = new List<speaking>();
	public List<speaking> Soliloquy = new List<speaking>();
	public List<Transform> enemies = new List<Transform>();
	public Transform playo, renderPlane, stagePick;
	public RectTransform namePlane, nameTopB, nameLeftB, nameRightB, nameBottomB, topB, leftB, rightB, bottomB;
	public battleManager phew;
	public Text bla, nameBla;
	public bool willFade = false;

	GameObject poop;
	Transform tr2, curSpeaking;
	RectTransform rtr, rtr2;
	Animator moi;
	Renderer self;
	Color yada;
	Vector3 colOpaq, tempo;
	int i = -1, j = -1, k = -1;
	float textWidth, timeAdd = 1, soloTimer = 0;
	bool ended = false, endAgain = false, curTalking = false, startWait = false;

	[System.Serializable]
	public class speaking {
		public GameObject speaker;
		public string content;
		public string animationState;
		public string choiceResult;
		public int style;
		public bool important;
		public bool narrator;
		public bool choose;
		
		public speaking (GameObject speaker, string content, string animationState, string choiceResult, int style,
            bool important, bool narrator, bool choose) 
        {
			this.speaker = speaker;
			this.content = content;
			this.animationState = animationState;
			this.important = true;
			this.narrator = false;
			this.choose = false;
			this.style = 0; // default,1:thought
			this.choiceResult = choiceResult;
        }
	}

	void Start () {
		moi = GetComponent<Animator>();
		tr2 = transform;
        curSpeaking = tr2;
		rtr = renderPlane as RectTransform;
		rtr.sizeDelta = new Vector2(bla.rectTransform.rect.width, bla.rectTransform.rect.height);
		textWidth = bla.rectTransform.rect.width;
		colOpaq = new Vector3(0, 0, 1);
		self = GetComponent<Renderer>();
    }

    void Update () {
		if ((tr2.position - playo.transform.position).sqrMagnitude < 4F) {
			if (Input.GetButtonDown("Attack")) { 
				if (i < speechBubbles.Count) {
					if (i >= 0 && speechBubbles[i].choose) {
						if (speechBubbles[i].choiceResult == "battle") {
							phew.Begin(enemies, stagePick);
                            Player.dialogFlag = false;
							curTalking = false;
							bla.text = "";
                            renderPlane.gameObject.SetActive(false);
                        }
                    } else {	
                        i++;
						curTalking = true;
						Player.dialogFlag = speechBubbles[i].important;
						renderPlane.gameObject.SetActive(true);
						if (i == 0) {
							startWait = true;
                            Player.dialogFlag = true;
                        }
						bla.text = speechBubbles[i].content;
						
                        if (!speechBubbles[i].narrator) {
                            nameBla.text = speechBubbles[i].speaker.name;
                        } else {
                            nameBla.text = "Your Feelings Say...";
                        }
						
                        moi = speechBubbles[i].speaker.GetComponent<Animator>();
						moi.Play(speechBubbles[i].animationState);
						curSpeaking = speechBubbles[i].speaker.transform;

						renderPlane.position = new Vector3
                        (
                            curSpeaking.position.x + 1.2F,
                            curSpeaking.position.y + 3,
                            curSpeaking.position.z
                        );
						namePlane.sizeDelta = new Vector2
                        (
                            nameBla.preferredWidth * nameBla.rectTransform.localScale.x - 5,
                            namePlane.rect.height
                        );
						nameTopB.sizeDelta = new Vector2
                        (
                            nameBla.preferredWidth * nameBla.rectTransform.localScale.x,
                            nameTopB.rect.height
                        );
						nameBottomB.sizeDelta = new Vector2
                        (
                            nameBla.preferredWidth * nameBla.rectTransform.localScale.x,
                            nameBottomB.rect.height
                        );
						nameLeftB.sizeDelta = new Vector2
                        (
                            nameLeftB.rect.width, 
                            nameBla.preferredHeight * nameBla.rectTransform.localScale.y
                        );
						nameRightB.sizeDelta = new Vector2
                        (
                            nameLeftB.rect.width,
                            nameBla.preferredHeight * nameBla.rectTransform.localScale.y
                        );

						if (bla.preferredWidth > textWidth) {
							rtr.sizeDelta = new Vector2
                            (
                                textWidth * bla.rectTransform.localScale.x,
                                bla.preferredHeight * bla.rectTransform.localScale.y
                            );
							topB.sizeDelta = new Vector2
                            (
                                textWidth * bla.rectTransform.localScale.x + 10,
                                topB.rect.height
                            );
							bottomB.sizeDelta = new Vector2
                            (
                                textWidth * bla.rectTransform.localScale.x + 10,
                                topB.rect.height
                            );
							leftB.sizeDelta = new Vector2
                            (
                                leftB.rect.width, 
                                bla.preferredHeight * bla.rectTransform.localScale.y + 10
                            );
							rightB.sizeDelta = new Vector2
                            (   
                                leftB.rect.width, 
                                bla.preferredHeight * bla.rectTransform.localScale.y + 10
                            );
                        } else {	
                            rtr.sizeDelta = new Vector2
                            (
                                bla.preferredWidth * bla.rectTransform.localScale.x,
                                bla.preferredHeight * bla.rectTransform.localScale.y
                            );
							topB.sizeDelta = new Vector2
                            (
                                bla.preferredWidth * bla.rectTransform.localScale.x + 10,
                                topB.rect.height
                            );
							bottomB.sizeDelta = new Vector2
                            (
                                bla.preferredWidth * bla.rectTransform.localScale.x + 10,
                                bottomB.rect.height
                            );
							leftB.sizeDelta = new Vector2
                            (
                                leftB.rect.width, 
                                bla.preferredHeight * bla.rectTransform.localScale.y + 10
                            );
							rightB.sizeDelta = new Vector2
                            (
                                leftB.rect.width, 
                                bla.preferredHeight * bla.rectTransform.localScale.y + 10
                            );
                        }
                    }
                } else {	
                    if (ended && willFade) {
						if (k < Soliloquy.Count) {
							curTalking = true;
							Player.dialogFlag = Soliloquy[k].important;
							renderPlane.gameObject.SetActive(true);
							bla.text = Soliloquy[k].content;
							nameBla.text = Soliloquy[k].speaker.name;
							curSpeaking = Soliloquy[k].speaker.transform;
							namePlane.sizeDelta = new Vector2
                            (
                                nameBla.preferredWidth * nameBla.rectTransform.localScale.x - 5,
								namePlane.rect.height
                            );
							nameTopB.sizeDelta = new Vector2
                            (
                                nameBla.preferredWidth * nameBla.rectTransform.localScale.x,
                                nameTopB.rect.height
                            );
							nameBottomB.sizeDelta = new Vector2
                            (
                                nameBla.preferredWidth * nameBla.rectTransform.localScale.x,
                                nameBottomB.rect.height
                            );
							nameLeftB.sizeDelta = new Vector2
                            (
                                nameLeftB.rect.width,
                                nameBla.preferredHeight * nameBla.rectTransform.localScale.y
                            );
							nameRightB.sizeDelta = new Vector2
                            (
                                nameLeftB.rect.width,
                                nameBla.preferredHeight * nameBla.rectTransform.localScale.y
                            );
							moi = Soliloquy[k].speaker.GetComponent<Animator>();
							moi.Play(Soliloquy[k].animationState);
							if (bla.preferredWidth>textWidth) {
								rtr.sizeDelta = new Vector2
                                (
                                    textWidth * bla.rectTransform.localScale.x,
                                    bla.preferredHeight * bla.rectTransform.localScale.y
                                );
								topB.sizeDelta = new Vector2
                                (
                                    textWidth * bla.rectTransform.localScale.x + 10,
                                    topB.rect.height
                                );
								bottomB.sizeDelta = new Vector2
                                (
                                    textWidth * bla.rectTransform.localScale.x + 10,
                                    topB.rect.height
                                );
								leftB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10
                                );
								rightB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10
                                );
                            } else {	
                                rtr.sizeDelta = new Vector2
                                (
                                    bla.preferredWidth * bla.rectTransform.localScale.x,
                                    bla.preferredHeight * bla.rectTransform.localScale.y
                                );
								topB.sizeDelta = new Vector2
                                (
                                    bla.preferredWidth * bla.rectTransform.localScale.x + 10,
                                    topB.rect.height
                                );
								bottomB.sizeDelta = new Vector2
                                (
                                    bla.preferredWidth * bla.rectTransform.localScale.x + 10,
                                    bottomB.rect.height
                                );
								leftB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10
                                );
								rightB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10);
                                }
							k++;
                        } else {	
                            Player.dialogFlag = false;
                            endAgain = true;
                        }
                    } else {	
                        if (j == -1) {
							j++;
                            moi.Play("idle");
                            Player.dialogFlag = false;
                            curTalking = false;
							bla.text = "";
                            renderPlane.gameObject.SetActive(false);
                        } else if (j < lastOne.Count) {
							curTalking = true;
							Player.dialogFlag = lastOne[j].important;
							renderPlane.gameObject.SetActive(true);
							bla.text = lastOne[j].content;
							nameBla.text = lastOne[j].speaker.name;
							curSpeaking = lastOne[j].speaker.transform;
							namePlane.sizeDelta = new Vector2
                            (
                                nameBla.preferredWidth * nameBla.rectTransform.localScale.x - 5,
                                namePlane.rect.height
                            );
							nameTopB.sizeDelta = new Vector2
                            (
                                nameBla.preferredWidth * nameBla.rectTransform.localScale.x,
                                nameTopB.rect.height
                            );
							nameBottomB.sizeDelta = new Vector2
                            (
                                nameBla.preferredWidth * nameBla.rectTransform.localScale.x,
                                nameBottomB.rect.height
                            );
							nameLeftB.sizeDelta = new Vector2
                            (
                                nameLeftB.rect.width,
                                nameBla.preferredHeight * nameBla.rectTransform.localScale.y
                            );
							nameRightB.sizeDelta = new Vector2
                            (
                                nameLeftB.rect.width,
                                nameBla.preferredHeight * nameBla.rectTransform.localScale.y
                            );
							moi = lastOne[j].speaker.GetComponent<Animator>();
							moi.Play(lastOne[j].animationState);
							if (bla.preferredWidth > textWidth) {
								rtr.sizeDelta = new Vector2
                                (
                                    textWidth * bla.rectTransform.localScale.x,
                                    bla.preferredHeight * bla.rectTransform.localScale.y
                                );
								topB.sizeDelta = new Vector2
                                (
                                    textWidth * bla.rectTransform.localScale.x + 10,
                                    topB.rect.height
                                );
								bottomB.sizeDelta = new Vector2
                                (
                                    textWidth * bla.rectTransform.localScale.x + 10,
                                    topB.rect.height
                                );
								leftB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10
                                );
								rightB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10
                                );
                            } else {	
                                rtr.sizeDelta = new Vector2
                                (
                                    bla.preferredWidth * bla.rectTransform.localScale.x,
                                    bla.preferredHeight * bla.rectTransform.localScale.y
                                );
								topB.sizeDelta = new Vector2
                                (
                                    bla.preferredWidth * bla.rectTransform.localScale.x + 10,
                                    topB.rect.height
                                );
								bottomB.sizeDelta = new Vector2
                                (
                                    bla.preferredWidth * bla.rectTransform.localScale.x + 10,
                                    bottomB.rect.height
                                );
								leftB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10
                                );
								rightB.sizeDelta = new Vector2
                                (
                                    leftB.rect.width,
                                    bla.preferredHeight * bla.rectTransform.localScale.y + 10
                                );
                            }
							j++;
                        } else {	
                            bla.text = "";
                            j = 0;
                            Player.dialogFlag = false;
                            k = 0;
							renderPlane.gameObject.SetActive(false);
                            curTalking = false;
							ended = true;
                        }
                    }
                }
            } else if (Input.GetButtonDown("Roll")) {
				if (i < speechBubbles.Count) {
					if (i >= 0 && speechBubbles[i].choose) {
						if (speechBubbles[i].choiceResult == "battle") {
							phew.Begin(enemies, stagePick);
                            Player.dialogFlag = false;
							curTalking = false;
							bla.text = "";
                            renderPlane.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
		if (curTalking) { 
            renderPlane.position = new Vector3
            (
                curSpeaking.position.x + 1.2F,
                curSpeaking.position.y + 3,
                curSpeaking.position.z
            );
        }
		if (ended && willFade) {	
            if (timeAdd <= 0) {
				yada = self.material.color;
				yada.a = 0;
				self.material.color = yada;
				self.gameObject.layer = 0;
            } else {	
                yada = self.material.color;
				tempo = Vector3.Slerp(Vector3.zero, colOpaq, timeAdd);
				yada.a = tempo.z;
				self.material.color = yada;
				timeAdd -= (Time.deltaTime * 2);
            }
        }
		if (k == Soliloquy.Count) {
            if (soloTimer > 4F) {
				renderPlane.gameObject.SetActive(false);
                curTalking = false;
                k = -1;
            } else {	
                soloTimer += Time.deltaTime;
            }
        }
		if (startWait) {
			if (soloTimer > 1) {
				startWait = false;
                Player.dialogFlag = false;
				soloTimer = 0;
            }
			soloTimer += Time.deltaTime;
		}
	}
}