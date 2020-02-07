using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWaterFlow : MonoBehaviour
{

    [SerializeField] private PipeLine MyPipeLine;
    [HideInInspector] public bool IsColl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //MyPipeLine.b_IsWater = false; 
    //}

    private void FixedUpdate()
    {
        //MyPipeLine.b_IsWater = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "PipeLine")
    //    {
    //        if (other.gameObject.GetComponentInParent<PipeLine>().b_IsWater && other.gameObject.GetComponentInParent<PipeLine>().b_IsPlaced && MyPipeLine.b_IsPlaced)
    //            //IsColl = true;
    //            MyPipeLine.b_IsWater = true;
    //    }
    //    if (other.gameObject.tag == "WaterFlowPlace" && MyPipeLine.b_IsPlaced)
    //    {
    //        //IsColl = true;
    //        MyPipeLine.b_IsWater = true;
    //    }
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "PipeLine")
    //    {
    //        if (other.gameObject.GetComponentInParent<PipeLine>().b_IsWater && other.gameObject.GetComponentInParent<PipeLine>().b_IsPlaced && MyPipeLine.b_IsPlaced)
    //            //IsColl = false;
    //            MyPipeLine.b_IsWater = false;
    //    }
    //    if (other.gameObject.tag == "WaterFlowPlace" && MyPipeLine.b_IsPlaced)
    //    {
    //        //IsColl = false;
    //        MyPipeLine.b_IsWater = false;
    //    }
    //}
}
