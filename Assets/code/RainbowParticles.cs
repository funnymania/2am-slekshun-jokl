using UnityEngine;
using System.Collections;

public class RainbowParticles : MonoBehaviour {
	ParticleSystem ps;
	float timer;
	float yada;
	Color bleh;
	Color blah;
	int i;
	int j;

	void Start () {
		ps=GetComponent<ParticleSystem>();
		yada=0;
	}
	

	void Update () {
		ParticleSystem.Particle[] ParticleList=new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles(ParticleList);
		for(i=0;i<ParticleList.Length;i++){
			bleh=ParticleList[i].color;
			for(j=0;j<yada;j++){
				blah=ParticleList[j].color;
				if(bleh==blah)
					break;}
			if(j==yada || bleh==Color.white){
				ParticleList[i].color=new Color(Random.Range(0.3F,1),Random.Range(0.3F,1),Random.Range(0.3F,1));}
		}
		ParticleSystem.Particle[] ParticleList2=new ParticleSystem.Particle[ps.particleCount];
		yada=ps.particleCount;
		ps.GetParticles(ParticleList2);
		ps.SetParticles(ParticleList,ps.particleCount);}
}
