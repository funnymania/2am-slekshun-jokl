using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class colorFader : MonoBehaviour {

    public float speed;
    [Tooltip("0 is not an eye, 1 is special eye, 2 is normal")]
    public int eyeType;
    
    Image me;
    Text moi;
    Camera cammy;
    Color first, rand, go;
    float t = 0;
    
    void Start () {
        me = GetComponent<Image>();
        moi = GetComponent<Text>();
        cammy = GetComponent<Camera>();
        first = Color.black;
        if (eyeType == 0) {
            rand = new Color(Random.Range(0.2F, 1.0F), Random.Range(0.0F, 1.0F), Random.Range(0.1F, 0.8F));
        } else if (eyeType == 1) {
            rand = new Color(Random.Range(0.4F, 0.8F), Random.Range(0.7F, 1.0F), Random.Range(0.0F, 0.1F));
        } else {
            rand = new Color(Random.Range(0F, 0.2F), Random.Range(0F, .87F), Random.Range(0.9F, 1.0F));
        }
    }

    void Update () {
        if (t >= speed) {
            first = rand;
            
            if (eyeType == 0) {
                rand = new Color(Random.Range(0.2F, 1.0F), Random.Range(0.0F, 1.0F), Random.Range(0.1F, 0.8F));
            } else if (eyeType == 1) {
                rand = new Color(Random.Range(0.4F, 0.8F), Random.Range(0.6F, 1.0F), Random.Range(0.0F, 0.1F));
            } else {
                rand = new Color(Random.Range(0.0F, 0.3F), Random.Range(0.67F, 1F), Random.Range(0.7F, .9F));
            }
            
            t -= speed;
            go = Color.Lerp(first, rand, t / speed);
            
            if (me != null) {
                me.color = go;
            } else if (moi != null) {
                moi.color = go;
            } else if (cammy != null) {
                cammy.backgroundColor = go;
            }
            
            t += Time.deltaTime;
        } else {	
            go = Color.Lerp(first, rand, t / speed);
            if (me != null) {
                me.color = go;
            } else if (moi != null) {
                moi.color = go;
            } else if (cammy != null) {
                cammy.backgroundColor = go;
            }
            t += Time.deltaTime;
        }
    }
}
