using UnityEngine;
using System.Collections;

public class respawnCont : MonoBehaviour {
	public GameObject virgin;
	public GameObject after;
	public respawnCont next;
	public bool state = false;
	Collider killMe;
	
	bool workIt = false;

	void Start () {
		after.SetActive(false);
		next.virgin.SetActive(false);
		next.after.SetActive(false);
	}
	
	void OnTriggerEnter (Collider col) 
    {
		if (col.tag == "Player")
        {
			if (state) 
            {
				// if(state) after.SetActive(false);
				// else virgin.SetActive(false); //turn off self and respawn past if != null
				after.SetActive(!after.activeSelf);
				
                if (!next.state) 
                {
                    next.virgin.SetActive(!next.virgin.activeSelf);
                }
                else 
                {
                    next.after.SetActive(!next.after.activeSelf);
				}
            }
			else
			{	
                virgin.SetActive(false);
				after.SetActive(false);
				next.virgin.SetActive(true);
				state = true;
            }
        }
	}
}
