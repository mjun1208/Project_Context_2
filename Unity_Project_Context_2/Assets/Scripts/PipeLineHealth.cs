using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put this class on the pipelines
public class PipeLineHealth : MonoBehaviour
{
	[SerializeField]
	private float f_minRandomLife = 2f, f_maxRandomLife = 10f;
	private float f_randomLiveTime;

    void Start()
    {
		f_randomLiveTime = Random.Range(f_minRandomLife, f_maxRandomLife);	
	}

	//call this funtion when a pipe is placed
	public void StartBreaking() 
	{
		StartCoroutine(BreakDelay());
	}

	//break after a random time
    IEnumerator BreakDelay() 
	{
		yield return new WaitForSeconds(f_randomLiveTime);
		Break();
	}

	void Break() 
	{
		transform.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_None;
		transform.GetComponent<PipeLine>().b_IsPlaced = false;
		PipesSpawn.instance.ReturnPipeToPool(gameObject);
	}
}
