using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class battleManager : MonoBehaviour {
	static List<Transform> players = new List<Transform>();
	static List<Transform> stageOp = new List<Transform>();
	static Transform dmy;
	static int i;

	public List<Transform> playrs = new List<Transform>();
	public List<Transform> stageOptions = new List<Transform>();
	public GameObject batCamStuff;
	List<Transform> opps = new List<Transform>();
	Transform stagePick;
	stageState curStage;
	int playNum;

	public void Begin (List<Transform> enemies, Transform stagey) {
		opps = enemies;
        CameraTitleScreen.camFollows = false;
		batCamStuff.SetActive(true);
		if (enemies.Count != 0) { // enemy decided by situation
			stagey.gameObject.SetActive(true);
			curStage = stagey.GetComponent<stageState>();
			curStage.openBattle(enemies.Count,enemies);
        } else { // random encounter
            stagePick = stageOptions[Random.Range(0,stageOptions.Count)];
			playNum = Random.Range(2,curStage.noPlayers);
			curStage.openBattle(playNum,null);
        }
	}

	public Transform farthestOpp (Transform user, Vector3 res) {
		if (Player.spriteLocale != user) {
			dmy = Player.spriteLocale;
        } else {
            dmy = opps[0];
        }
		for (i = 1; i < opps.Count; i++) {
			if ((dmy.position - res).sqrMagnitude < (dmy.position - opps[i].position).sqrMagnitude)
				dmy = opps[i];
        }
		return dmy;
	}

	///stage, opponents, opponent placements, 
}
