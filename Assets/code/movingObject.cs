using UnityEngine;
using System.Collections;

public class movingObject : MonoBehaviour {
	public static Transform mover;
	public static Vector3 past;
	
	public bool onBlock;
	GameObject weeaboo;
	CharacterController blah;
	Transform tr;

	void Start () {
		weeaboo = GameObject.FindWithTag("Player");
		blah = weeaboo.GetComponent<CharacterController>();
	}
	
// Raycast Drawn down is not updating at same rate of block.
// TurnTranny's movement is not updating at the same rate of OnAnimatorMove.
// May have to utilize a FixedUpdate
	void OnAnimatorMove () 
    {
		if (Player.onLine && onBlock)
        {
			Player.straightDown = Physics.Raycast(  
                    blah.transform.position,
                    new Vector3(0,-1,0),
                    out Player.downwards,
                    .98F 
            );
			Debug.DrawRay(blah.transform.position, new Vector3(0,-1,0));
			
            if (Player.downwards.transform != null)
            {
				if (Player.downwards.transform.tag == "block" || Player.downwards.transform.tag == "faller")
                {
					if (Player.downwards.transform != mover)
                    {
						mover = Player.downwards.transform;
						past = mover.position;
                    }
					blah.Move(mover.position - past);
					past = mover.position;
                }
				else 
                {
                    Player.onLine = false;
                }
            }
			else
			{	
                Player.onLine = false;
				mover = null; 
                past = Vector3.zero;
            }
        }
	}
}