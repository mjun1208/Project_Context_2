using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesSpawn : MonoBehaviour
{
    //public static PipesSpawn instance;
    //[SerializeField]
    //private GameObject pipeObjects;
    //[SerializeField]
    //private Transform poolBaseParent;
    //[SerializeField]
    //private Transform[] pipeStoragePositions = new Transform[3];
    //private GameObject[] pipeStorage;
    //
    //[SerializeField]
    //private int i_pipePoolSize = 20;
    //
    //private int i_PipesInStorage;
    //private List<GameObject> PipePool = new List<GameObject>();
    //
    //private void Awake() 
    //{
    //	instance = this;
    //}
    //
    ////at the start of the game, create a pool and get random pipes
    //void Start()
    //{
    //	pipeStorage = new GameObject[pipeStoragePositions.Length];
    //	AddPipesToPool(i_pipePoolSize);
    //
    //	for (int i = 0; i < pipeStoragePositions.Length; i++) 
    //	{
    //		pipeStoragePositions[i].GetComponent<MeshRenderer>().enabled = false;
    //		BuyPipe(0, 0);
    //	}
    //}
    //
    ////~~  Pool to precreate pipes (saves performance)
    //	//create a pool of pipes
    //public void AddPipesToPool(int i_amount) 
    //{
    //	for(int i = 0; i < i_amount; i++) 
    //	{
    //		GameObject newPipe = Instantiate(pipeObjects);
    //		newPipe.transform.parent = poolBaseParent;
    //		PipePool.Add(newPipe);
    //		newPipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_None;
    //	}
    //}
    //
    //	//return a broken pipe back to the pool
    //public void ReturnPipeToPool(GameObject pipe) 
    //{
    //	PipePool.Add(pipe);
    //	pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_None;
    //}
    ////~~
    //
    ////~~ Storage of avaible pipes for the player to pickup
    //	//Add new obtained pipes to the storage (bought? won?)
    //public void AddPipeToStorage(int i_typeId) 
    //{
    //	GameObject newPipe = PipePool[0];
    //	ObtainPipeType(newPipe, i_typeId);
    //	PipePool.Remove(newPipe);
    //
    //	//place new pipe on an empty poition
    //	for (int i = 0; i < pipeStoragePositions.Length; i++)
    //	{
    //		if(pipeStorage[i] == null) 
    //		{
    //			newPipe.transform.position = pipeStoragePositions[i].position;
    //			pipeStorage[i] = newPipe;
    //			i_PipesInStorage++;
    //			break;
    //		}
    //	}		
    //}
    //
    ////Remove from storage when the player takes a pipe.
    //public void TakePipeFromStorage(GameObject pipe) 
    //{
    //	int i_pipeIndex = System.Array.IndexOf(pipeStorage, pipe);
    //	if (i_pipeIndex >= 0 && i_pipeIndex < pipeStorage.Length)
    //		pipeStorage[i_pipeIndex] = null;
    //	i_PipesInStorage--;
    //}
    ////~~
    //
    ////~~ TODO: Fix nestled code after gamejam
    //public void BuyRandomPipe() 
    //{
    //	if (i_PipesInStorage < pipeStoragePositions.Length) 
    //	{
    //		BuyPipe(0, 0);
    //	}
    //}
    //
    //public void BuyPipe(int i_typeId, int i_cost) 
    //{
    //	if(i_cost <= Currency.Instance.i_myCurrency) 
    //	{
    //		AddPipeToStorage(i_typeId);
    //		Currency.Instance.i_myCurrency -= i_cost;
    //	}
    //}
    ////~~ 
    //
    //void ObtainPipeType(GameObject pipe, int i_typeId) 
    //{
    //	switch(i_typeId) 
    //	{
    //		case 0: //is random
    //			RandomPipeType(pipe);
    //			break;
    //		case 1:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_I;
    //			break;
    //		case 2:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_Corner;
    //			break;
    //		case 3:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_T;
    //			break;
    //		case 4:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_X;
    //			break;
    //	}
    //}
    //
    ////generates a random pipe
    //void RandomPipeType(GameObject pipe) 
    //{
    //	int i_randomNumber = Random.Range(1, 5);
    //	//TOTO: (after gamejam) put check for repeated random
    //
    //	switch(i_randomNumber) 
    //	{
    //		case 1:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_I;
    //			break;
    //		case 2:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_Corner;
    //			break;
    //		case 3:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_T;
    //			break;
    //		case 4:
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_X;
    //			break;
    //		default: //in case anything goes wrong
    //			pipe.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_None;
    //			break;
    //	}	
    //}

    /////////////////////////////////////////
    public static PipesSpawn instance;
    private List<GameObject> PipePool = new List<GameObject>();
    //[SerializeField] private int i_pipePoolSize = 20;

    private void Awake() 
    {
    	instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<PipeLine>().PipeLine_State_To_None();
            PipePool.Add(this.transform.GetChild(i).gameObject);    
        }
    }

    public void ReturnPipeToPool(GameObject pipe)
    {
        PipePool.Add(pipe);
        WaterFlowManager.instance.PlacedPipeLine.Remove(pipe);
        pipe.GetComponent<PipeLine>().PipeLine_State_To_None();
    }

    public GameObject TakePipeFromPool(Vector3 v_Pos)
    {
        if (PipePool.Count > 0)
        {
            GameObject Temp = PipePool[PipePool.Count - 1];
            Temp.transform.position = v_Pos;
            WaterFlowManager.instance.PlacedPipeLine.Add(Temp);
            PipePool.Remove(PipePool[PipePool.Count - 1]);

            return Temp;
        }

        return null;    
    }
}
