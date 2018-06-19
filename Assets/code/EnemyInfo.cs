using UnityEngine;
using System.Collections;

public class EnemyInfo : MonoBehaviour {
	public enum PainType {
			VeryLight = 1,
			Light = 2,
			Med = 3,
			Heavy = 4,
			VeryHeavy = 5
}
	public Color batColor;
	public PainType painType;
	public Vector3 theirAngle, myAngle;
	public float myPain, myKB, myHitstun;
	public float theirPain, theirKB, theirHitstun;
	public float health;
	public bool vulnerable = true;
	public bool inHitstun = false;
	public bool isHit = false;
	bool stackHit;

	public void inPain(
        float pain,
        PainType painType,
        float hitstun,
        float KB,
        Vector3 angle
    ) {
		health -= pain;
		theirAngle = angle;
		theirHitstun = hitstun;
		theirKB = KB;
		if (inHitstun) 
            stackHit = true;
		inHitstun = true;
		isHit = true;
    }
}
