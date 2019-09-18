using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Does not handle audio.

public class EventHandler : MonoBehaviour {
	
	public enum GameState {
		Start = 0,
	}
	
	public static int gameState = 0;
	public static bool eventFlag = false;
	public Transform player;
	public Transform we;
	public float timer;
	float until;
	bool entrance = true;
	Player yaka;
	GameObject tmpy;
	Animation an;
	List<Transform> boop = new List<Transform>(); //GameState
	List<List<Transform>>[] GameTable = new List<List<Transform>>[2]; // [type (0 or 1)][gameStates][stuff]
	
	int i;
	
	// A super gametable is a 2-space array of gametables
	// A gametable is a list of gamestates
	// A gamestate is a list of transforms
	// A transform is an event
	
	// Type 0 is a cutscene
	// Type 1 is an interaction
	
	void Start () {
		yaka = player.GetComponent<Player>();
		an = GetComponent<Animation>();
		tmpy = GameObject.Find("Events/KeyMouth/Here");
		we = tmpy.GetComponent<Transform>();
		GameTable[0] = new List<List<Transform>>();
		GameTable[1] = new List<List<Transform>>();
		boop.Add(we);
		GameTable[0].Add(boop);
		tmpy = GameObject.Find("Interactions/KeyMouth/Here");
        
        // GameTable[0,1][0]=tmpy.transform;
		we = tmpy.GetComponent<Transform>();
		
        boop.Add(we);
		GameTable[1].Add(boop);
	}
	
	void Update () {
//		for(i=0;i<GameTable[gameState].length;i++)
//		  		if(InRange(GameTable[gameState][i]),Player.position)
//		  			eventFlag = 1;
//		  			GameChanger(gameState,i);
//		  for(i=0;i<Interactions.NPCTable.length;i++)	//Check for char interaction
//		       if(InRange(Interactions.NPCTable.AtIndex(i).position,Player.position)
//		  			if(interactFlag==1)
//		  				// slowndownFlag = 1;		 //Player will set this... function will run, dropping speed until walking state
//		  				// camera stuff
//		  				// You are now having a conversation with an NPC (Do stuff, Michael!!)
//		  
		if (!eventFlag) {
			if (Input.GetAxis("Attack") != 0) {
				for (i = 0; i < GameTable[1][gameState].Count; i++) {
					if ((player.position - GameTable[1][gameState][i].position).sqrMagnitude < 64) {
						// trigger the appropriate interaction
                        eventFlag = true;
                        break;
                    }       
				}
            }
			for (i = 0; i < GameTable[0][gameState].Count; i++) {
				if ((player.position - GameTable[0][gameState][i].position).sqrMagnitude < 100) {
					eventFlag = true;
                    break;
                }
            }
        } else {   
            CutScene(gameState, i);
            timer += Time.deltaTime;
        }
	}
	 
	void CutScene (int gameState, int scene) {
		if (gameState == 0) {
            
        #region KeyMouth Scene
			if (scene == 0) {
				if (entrance) {
					GameTable[gameState][0][scene] = null;
                    entrance = false;
					an.Play("KeyMouth");
                    until = an["KeyMouth"].length;
                }
				if (timer >= until) {
					eventFlag = false;
                    timer = 0;
                    entrance = true;
                }
            }
			#endregion
		}
	}
	
	void Interaction (int gameState, int scene) {
		if (gameState == 0) {
  
            #region KeyMouth Talk
			if (scene == 0) {
				if (entrance) {
					GameTable[gameState][1][scene] = null;
                    entrance = false;
					an.Play("KeyMouth");
                    until = an["KeyMouth"].length;
                }
				if (timer >= until) {
					eventFlag = false;
                    timer = 0;
                    entrance = true;
                }
            }
			#endregion

		}
	}
}