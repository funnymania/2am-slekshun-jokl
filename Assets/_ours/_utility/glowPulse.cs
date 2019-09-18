using UnityEngine;
using MKGlowSystem;
using System.Collections;

// .868 .574

public class glowPulse : MonoBehaviour {
	
	MKGlow tmp;
	public float timeUp, timeDown, blurMiddle, blurEnd, intensityEnd;
	float time, blurGap, blurGap2, intensGap, blurBegin, intensBegin, blurMid, intensEnd;
	bool goingDown = true, downAgain = true, upAgain = true;
		
	void Start () {
		tmp = gameObject.GetComponent<MKGlow>();
		time = 0;
		blurGap = tmp.BlurSpread - blurMiddle;
		blurBegin = tmp.BlurSpread;
		intensGap = tmp.GlowIntensity - intensityEnd;
		intensBegin = tmp.GlowIntensity;
	}
		
    // Blur spread Down to .5, blur spread to .2 then GlowIntens to .337
	void Update () 
    { 
		if (goingDown)
        {
			if (downAgain)
            {
				time += Time.deltaTime;
				tmp.BlurSpread = blurBegin - (blurGap * time / timeDown * 2);
				if (time >= timeDown / 2)
                { 
					downAgain = false;
                    blurMid = tmp.BlurSpread;
					blurGap2 = blurMid - blurEnd;
                    time = 0;
                }
            }
			else
			{	
                time += Time.deltaTime;
				tmp.BlurSpread = blurMid - (blurGap2 * time / timeDown * 2);
				tmp.GlowIntensity = intensBegin - (intensGap * time / timeDown * 2);
				if (time >= timeDown / 2)
                {
					goingDown = false;
                    downAgain = true;
					blurGap2 = blurMiddle - tmp.BlurSpread;
                    time = 0;
					intensGap = intensBegin - tmp.GlowIntensity;
					blurMid = tmp.BlurSpread;
                    intensEnd = tmp.GlowIntensity;
                }
            }
        }
		else
		{	
            if (upAgain)
            {
				time += Time.deltaTime;
				tmp.BlurSpread = blurMid + (blurGap2 * time / timeUp * 2);
				tmp.GlowIntensity = intensEnd + (intensGap * time / timeUp * 2);
				if (time >= timeUp / 2)
                { 
					upAgain = false;
                    blurMid = tmp.BlurSpread;
					blurGap = blurBegin - blurMid;
                    time = 0;
					intensBegin = tmp.GlowIntensity;
                }
            }
			else
			{	
                time += Time.deltaTime;
				tmp.BlurSpread = blurMid + (blurGap * time / timeUp * 2);
				if (time >= timeUp / 2)
                {
					goingDown = true;
                    upAgain = true;
                    time = 0;
					blurGap = tmp.BlurSpread - blurMiddle;
                }
            }
        }
	}
}
