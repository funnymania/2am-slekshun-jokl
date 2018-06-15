using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class battleCameraHell : MonoBehaviour {
    
#region Declarations
	public static Transform target;
	public static Transform tr;
	public static Transform sceneCam;
	public static bool eventFlag = false;
	public static bool movingWith = false;
	public static float sizeFactor=1;
	public static Vector3 a;
	
	public Transform beHere;
	public Transform lookHere;
	public bool camSteady = true;
	GameObject yada;
	Camera camera;
	Vector3 b,bPast;
	Vector3 c;
	Vector3 NewMovement;
	Vector3 d;
	Vector3 initial;
	Vector3 final;
	Vector3 posPast;
	Vector3 offset;
	Vector3 oldDist,current;
	float maxTranslate;
	float amt;
	float timer;
	float horiz;
	float vertic;
	float distance,dist1,dist2;
	int i;
	public bool entrance = false;
	public bool arranging = false;
	bool firstMode=false;
	public static bool isTiny =false;
	public Transform enemy;
	List <Transform> enemies;
	Transform targetedEnemy;
	RaycastHit left;
	RaycastHit right,up,down,back;
#endregion
	
    void Start () {
		tr=transform;
		tr.position=beHere.position;
		tr.LookAt(lookHere);
		distance=(beHere.position-lookHere.position).magnitude;
	}

	void FixedUpdate () {
		if (movingWith)
		{	dist2 = (Player.spriteLocale.position - lookHere.position).magnitude;
			dist1 = (tr.position - lookHere.position).magnitude;
			if (Player.charState != Player.CharacterState.Jump) {
				tr.position += Player.camOffset * distance / dist2 * sizeFactor;
            } else {
                tr.position += new Vector3(Player.camOffset.x,0,Player.camOffset.z) * distance / dist2 * sizeFactor;
            }
			tr.LookAt(lookHere);
			tr.Translate(new Vector3(0,0,dist1 - distance));}
	}
}