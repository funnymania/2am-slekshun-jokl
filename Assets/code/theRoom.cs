using UnityEngine;
using System.Collections;

public class theRoom : MonoBehaviour {
	public GameObject playo,birdo,a,b;
	public Transform cam,mirror;
	public Animator birdAnim;
	public AudioClip mbv;
	float timerTil=0;
	bool boop=false;
	AudioSource jj;
	
	void Awake(){
		cam.position=new Vector3(transform.position.x,transform.position.y,transform.position.z-14);
		playo.transform.position=new Vector3(transform.position.x,transform.position.y-1,transform.position.z);
		birdo.transform.position=new Vector3(502F,-43.5F,-62.4F);
		birdo.transform.eulerAngles=new Vector3(0,0,180);
		//birdo.SetActive(false);
		birdAnim.Play("hangingIdle");
		a.transform.position=birdo.transform.position;
		b.transform.position=birdo.transform.position;
		a.SetActive(false);b.SetActive(false);
		jj=GetComponent<AudioSource>();
		jj.Play();
	}
	
	void Update(){
		if(!boop){
			if((mirror.position-playo.transform.position).sqrMagnitude<1.31F && Input.GetAxis("Attack")!=0 && a.activeSelf==false){
				a.SetActive(true);b.SetActive(true);birdo.SetActive(true);birdAnim.Play("hangingSpread");
				CameraTitleScreen.toTheWakeUp=false;boop=true;
				SetLayerRecursively(transform.parent.gameObject,0);}}
		else
		{	if(timerTil>5){
				sceneManager.isOK=true;
				musicManager.bgm.clip=mbv;
				/*musicManager.bgm.Play();*/}
			timerTil+=Time.deltaTime;}
	}

    public static void SetLayerRecursively(GameObject obj, int layer) {
        foreach (Transform trans in obj.GetComponentsInChildren<Transform>(true)) {
			trans.gameObject.layer = layer;
		}
    }
}
