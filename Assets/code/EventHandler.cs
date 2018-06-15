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
	
	// Load in Initial State values HERE.
	void Start(){
		yaka=player.GetComponent<Player>();
		an=GetComponent<Animation>();
		tmpy=GameObject.Find("Events/KeyMouth/Here");
		we=tmpy.GetComponent<Transform>();
		GameTable[0] = new List<List<Transform>>();
		GameTable[1] = new List<List<Transform>>();
		boop.Add(we);
		GameTable[0].Add(boop);   			// would add
		tmpy=GameObject.Find("Interactions/KeyMouth/Here");
		we=tmpy.GetComponent<Transform>();//GameTable[0,1][0]=tmpy.transform;
		boop.Add(we);
		GameTable[1].Add(boop);
	}
	
	void Update(){
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
		if(!eventFlag){
			if(Input.GetAxis("Attack")!=0){
				for(i=0;i<GameTable[1][gameState].Count;i++){
					if((player.position-GameTable[1][gameState][i].position).sqrMagnitude<64)
						eventFlag=true;break;//trigger the appropriate interaction
				}}
			for(i=0;i<GameTable[0][gameState].Count;i++){
				if((player.position-GameTable[0][gameState][i].position).sqrMagnitude<100){
					eventFlag=true;break;}}}
		else
		{   CutScene(gameState,i);timer+=Time.deltaTime;}
	}
	
	
	 
	void CutScene(int gameState,int scene){
						#region KeyMouth State
		if(gameState==0){
								#region KeyMouth Scene
			if(scene==0){
				if(entrance){
					GameTable[gameState][0][scene]=null;entrance=false;
					an.Play("KeyMouth");until=an["KeyMouth"].length;}
				if(timer>=until){
					eventFlag=false;timer=0;entrance=true;}}
			#endregion
		}
		#endregion
	}
	
	void Interaction(int gameState,int scene){
						#region KeyMouth State
		if(gameState==0){
								#region KeyMouth Talk
			if(scene==0){
				if(entrance){
					GameTable[gameState][1][scene]=null;entrance=false;
					an.Play("KeyMouth");until=an["KeyMouth"].length;}
				if(timer>=until){
					eventFlag=false;timer=0;entrance=true;}}
			#endregion
		}
		#endregion
	}
}

// integer corresponds to progress (game states)
// GameTable[][] has list of possible scenes for each state (positions where they will occur)
//
// And at GameTable[GameState][Positions] is list of position-triggered events.

// SO: At GameTable[Start], there might be only a few positions which, when InRange, will trigger an event.

// TYPES OF EVENTS: Hallucination (in which gameplay is not interrupted, character's speed forcibly slowed, camera
//								   looks at hallucination (instead of Luya) and is slowed accordingly, 
//								   as hallucination gets close to finish, camera looksat and motions back to Char as center)
//							       
//				    Event		  


// Interaction: potentially text, definitely audio

// EventFlag in CameraBlah... 

// Interaction consists in: Player presses 'A' near NPC, when within range, 


// SUPER STRUCTURE NEEDED!!!! : LOADING ZONES!!!


///////// Vector 3s representing "crossable lines" where sets of objects are loaded. DEFENSIVE!!!! 






/* GameTable[GameState][Type][transform.Positions]
 * 
 * 
 * GameState is the total number of states in the game
 * Type is the sort of event/interaction
 * 		- 0 is an interaction
 * 		- 1 is an event
 * Positions are the places where these events occur
 * 
 * 
 * if 'A' down, check through GameTable[CurrentGameState][0]
 * 		to see if near any interaction spaces. 
 * 
 * also, always be checking through GameTable[CurrentGameState][1]
 * 
 * array of arrays of vector3s
 * 
 * must think about what triggering events actually consists in doing... the so-called loading of a scene.
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * interactions[gameState][type][stuff]
 *  - different for each interaction.... somethings are people, some people ask for a decision, some things are just items, etc.
 *  - send gameState and i to some function, which is some mega fucking huge switch statement.......
 *  - gamestate will iterate through this until interaction over, which causes a RETURN;
 *  - Need proper protection against 'A' staying held down. 
 *  - 
 * 
 * events[gameState][animationClips]
 * 	- array of array of animation clips.
 *  - disable player script, set scene camera upfront, mega
 * 
 * 
 * 
 *  Upon GameState changes, load in all relevant Transforms, manually. 
 *  This means, finding properly named gameObjects... GetComponent
 * 
 * 
 * 
 * 
 * 
 * certain events trigger change in gameState. 
 * 
 * 
 * 
 * so, keymouth, you talk to him. Let's say you give him something.
 * 
 * this cutscene is removed from the list... however, the interaction with KeyMouth is added...
 * 
 * RIGHT NOW... Just remove itself from list. 
 * 
 * We will deal on a case-by-case basis. Each scene can potentially change things. must adjust. 
 * 
 * 
 * 
 * SO, we are dealing with an array of lists
 * 
 * [Type][Transforms]
 * 
 * let's go!
 * 
 * 
 * 
 * 1.) string comparison on unique name of transform
 * 
 * 
 * gamestates
 * 
 * 
 * 
 * could, ultimately, consolidate entirely into vector3 IF everything was static.
 * 
 * so, already need transforms, which do not exist until loaded in. 
 * 
 * at least, things ARE loaded in categorically..
 * 
 * gamestates
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * maximal efficiency: straight hash table
 * 
 * preferred route: regional hash table, with bool/int checkups
 * 	+ in this corner, actual environmental triggers are constantly being tested against. 
 *  + also, everything IS hashed. 
 * 
 * 
 * 
 * so NOW.. gameStates are regional, type is the same, transforms are the same.
 * 
 * completing of cutscene is the setting of a particular garbage value to that node in the matrix.
 * 
 * for instance, for keymouth.
 * 
 * 
 * 
 * 
 * 
 * 
 *  OK, so...
 * 
 * what is a scene?
 * 
 * playing camera animation
 * and then there is, literally, a script. 
 * calling animations, at the same time as triggering music.
 * 
 * additive animation stuff.... hm... 
 * 
 * 
 * 
 * 
 * 
 * 
 * NOW.
 * 
 * an interaction is... some camera change... this should be experimented with considerably, and there should be
 * more than one angle/approach used
 * 
 * then there is a difference
 * 
 * we can have scripted things as in a cutscene, with the dialed up graphics.
 * 
 * 
 * hm, it would have to be by hand!! 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 1.) List local to FBX. On Awake, check eventHandler bool to know whether to, IN the local event Awake, add scene to list of transforms.
 * 
 * The checking for scenes-occurences happens in the FBXs. On scene occurring, set appropriate eventHandler global to false.
 * 
 * 
 * PROS: 
 * 		More sensible to deal with from a modular perspective. 
 * 		Less Overhead AFTER scene has loaded.
 * 		Auto-saves automatically. (if these globals are hashed into memory)
 * 
 * 
 * 
 * 
 * STEP ONE: Make Save State conglomeration.
 * STEP TWO: Write local FBX script which checks for (more or less) gameState through bools, inside of OnAwake,
 * 				and then updates as this does, iterating through transforms.
 * STEP THREE: A scene getting triggered means... 
 * 
 * setting CameraBlah's Camera to proper scene cam.
 * 
 * Enable "FBX/Luya", and any other characters/things one wants to appear.
 * 
 * Disable meshRenderer of Player character.
 * 
 * Trigger eventFlag.
 * 
 * Play animation of .FBX here.
 * 
 * After scene, flip eventFlag.
 * 
 * Make Player character visible, reposition player to proper position/rotation, make it so scene cannot be triggered twice.
 * 				
 * At some point need to play around with asyncleveladditions.
 * 
 * 
 * 
 * 
 * 
 * On Awake, FBX checks bools and adds its scenes to genHandler. 
 * genHandler has a queue of transforms, which are, now, valid and possible scenes to trigger based on what is loaded.
 * On UnLoading, FBX removes its scenes from genHandler
 * 
 * this means locally storing the transforms as a list in FBX. 
 * 
 * that's cool with me!
 * 
 * 
 * 
 * alt 
 * */