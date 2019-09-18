using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour {
	public GameObject blah;
	EnemyInfo yada, yadaTarget;
	GameObject blah2;
	
	void Start () {
		yada = blah.GetComponent<EnemyInfo>();
	}
	
	void OnTriggerEnter (Collider other) {
		blah2 = other.transform.root.gameObject;
		if (blah != blah2) {
			yadaTarget = blah2.GetComponent<EnemyInfo>();
			if (yadaTarget != null && yadaTarget.vulnerable) {
				yadaTarget.inPain(
                    yada.myPain,
                    yada.painType,
                    yada.myHitstun,
                    yada.myKB,
                    yada.myAngle
                );
            }
        }
	}
}