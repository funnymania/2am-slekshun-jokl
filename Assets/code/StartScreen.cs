using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	
	Player disable;
	WhileStart booya;
	StartScreen sowa;
	public Transform PinealSpace;
	public Transform cameraStart;
	GameObject clone;
	GameObject fartClone;
	public GameObject FartInMyFace;
	Transform tr;
	Color color;
	Color squash;
	public float timer = 0;
	Vector3 gimpy;
	
	void Start () {
		disable = gameObject.GetComponent<Player>();
		booya = gameObject.GetComponent<WhileStart>();
		disable.enabled = false;
        booya.enabled = true;
        tr = transform;
		clone = Instantiate(tr.parent.gameObject) as GameObject;
		// fartClone = GameObject.Find("Player(Clone)/Luyaka");
		clone.transform.localScale = new Vector3(.008F,.008F,.008F);
		clone.transform.position = new Vector3
        (
            PinealSpace.position.x,
            PinealSpace.position.y - .018F,
            PinealSpace.position.z
        );
		clone.transform.rotation = tr.rotation;
        // Clone's stuff.
		sowa = clone.GetComponentInChildren<StartScreen>(); 
		booya = clone.GetComponentInChildren<WhileStart>();
		sowa.enabled = false;
        booya.enabled = false;
		fartClone = GameObject.Find("Player(Clone)/Luyaka/polySurface424");
		disable = clone.GetComponentInChildren<Player>();
		disable.tinyDude = .008F; 
        CameraHell.isTiny = true;
		color = fartClone.GetComponent<Renderer>().material.color;
        color.a = 0;	
		fartClone.GetComponent<Renderer>().material.color = color;
		squash = FartInMyFace.GetComponent<Renderer>().material.color;
        // Luya skin
        squash.a = 1; 
		FartInMyFace.GetComponent<Renderer>().material.color = squash; 
		CameraHell.tr.position = cameraStart.position;
		CameraHell.tr.LookAt(PinealSpace);
	}
	
	void Update () {
		if (timer >= 3)
        {
			// Need to reverse normals on Luyaka (when camera penetrates)
			color.a += Time.deltaTime / 1.5F;
			fartClone.GetComponent<Renderer>().material.color = color;
			
			if (timer > 4.5F)
            {
				Player.startFlag = false;
                disable.enabled = true;
                disable.blurb = false;
				CameraHell.tr.position = new Vector3
                (
                    clone.transform.position.x + (6.4F * .008F),
                    clone.transform.position.y,
                    clone.transform.position.z
                );
				CameraHell.a = clone.transform.position;
				clone=GameObject.Find("Player(Clone)/CameraLooksHere");
				CameraHell.target = clone.transform;
				CameraHell.sizeFactor = .008F;
				enabled = false;
            }
        }
		else
		{	
            CameraHell.tr.position = Vector3.Lerp(cameraStart.position, PinealSpace.position, timer / 3.6F);
			// Luya Body becoming transparent
            squash.a -= Time.deltaTime / 3;
			FartInMyFace.GetComponent<Renderer>().material.color = squash;
        }
		timer += Time.deltaTime;
	}
}

/* Luya closes eyes, inhales.
 * Camera moves just behind eyes over a ~2 second span,
 * during which skin becomes transparent (over a less time span), and normals flip.
 * It is a general DARKNESS inside of your body, with the exception of the outside, played along the film of your skin.
 * you then fall until a platform just above belly button.   
 * */

/* Must, still, have inside of body non-transparent
 * 		- Try Tinting body. 
 * Pass
{
	Cull Front
	Color (0,0,0,1)
}
 * Gravity problems :/ ..
 * NaN on tiny transform after certain amount of time
 * 
 * 
 * 
 * 
 * 
 * 		MeshFilter filter = GetComponent(typeof (MeshFilter)) as MeshFilter;
		if (filter != null)
		{
			Mesh mesh = filter.mesh;
 
			Vector3[] normals = mesh.normals;
			for (int i=0;i<normals.Length;i++)
				normals[i] = -normals[i];
			mesh.normals = normals;
 
			for (int m=0;m<mesh.subMeshCount;m++)
			{
				int[] triangles = mesh.GetTriangles(m);
				for (int i=0;i<triangles.Length;i+=3)
				{
					int temp = triangles[i + 0];
					triangles[i + 0] = triangles[i + 1];
					triangles[i + 1] = temp;
				}
				mesh.SetTriangles(triangles, m);
			}
		}		
 * 
 * */