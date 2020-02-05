using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //private List<GameObject> Tiles_GameObject;
    //private List<BoxCollider> Tiles_BoxColl;
    private BoxCollider[] Tiles_BoxColl;
    private int i_LastFloor = 0;

    private void Awake()
    {
        Tiles_BoxColl = new BoxCollider[this.transform.childCount];
    
        for (int i = 0; i < Tiles_BoxColl.Length; i++)
        {
            Tiles_BoxColl[i] = this.transform.GetChild(i).GetComponent<BoxCollider>();
        }

        //for (int i = 0; i < this.transform.childCount - 1; i++)
        //{
        //    Debug.Log(this.transform.childCount);
        //    //Tiles_GameObject.Add(this.transform.GetChild(i).gameObject);
        //    Tiles_BoxColl.Add(this.transform.GetChild(i).GetComponent<BoxCollider>()); 
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        i_LastFloor = 0;
        StartCoroutine(SelectFloor());
    }

    // Update is called once per frame
    void Update()
    {
        if (i_LastFloor != PipeLineManager.instance.i_Floor)
        {
            i_LastFloor = PipeLineManager.instance.i_Floor;
            StartCoroutine(SelectFloor());
        }
    }


    IEnumerator SelectFloor()
    {
        for (int i = 0; i < Tiles_BoxColl.Length; i++)
        {
            Tiles_BoxColl[i].enabled = false;
        }
        Tiles_BoxColl[i_LastFloor].enabled = true;
        Debug.Log(i_LastFloor);
        yield return null;
    }
}
