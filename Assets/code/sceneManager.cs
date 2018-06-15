using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneManager : MonoBehaviour {
	public static Transform tr;
	public static bool isOK=false;
	static AsyncOperation yaa;
	
	void Start(){
		tr=transform;
		DontDestroyOnLoad(gameObject);
	}
	
	public static IEnumerator LoadIn(string sceneName,bool canPlayerMove){
		yaa=SceneManager.LoadSceneAsync(sceneName);
		yaa.allowSceneActivation=false;
		yield return new WaitUntil(() => isOK==true);
		yaa.allowSceneActivation=true;
		Player.noMovement=canPlayerMove;Player.isFalling=true;
		isOK=false;
	}
}

/*
public static void LoadIn(string sceneName){
		DontDestroyOnLoad(tr.gameObject);
		StartCoroutine(Loader(string sceneName));
	}

	IEnumerator Loader(string sceneName){
		yaa=SceneManager.LoadSceneAsync(sceneName);
		yaa.allowSceneActivation=false;
		yield return (isOK);
		yaa.allowSceneActivation=true;
	}
*/