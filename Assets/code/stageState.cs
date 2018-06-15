using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class stageState : MonoBehaviour {
	public List<Transform> spwns=new List<Transform>();
	public Transform camHere,camLookHere;
	public battleCameraHell batCam;
	public int noPlayers; //max number of players allowable
	int i;

	public void openBattle(int playerNum,List<Transform> enemies){
		batCam.beHere=camHere;batCam.lookHere=camLookHere;
		batCam.enabled=true;Player.isBattle=true;
		if(enemies==null){
			;}
		else
		{	Player.spriteLocale.position=spwns[0].position;
			for(i=0;i<enemies.Count;i++){
				enemies[i].position=spwns[i+1].position;
				enemies[i].gameObject.SetActive(true);}}
	}
}
