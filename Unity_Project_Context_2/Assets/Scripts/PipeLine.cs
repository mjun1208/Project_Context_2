using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLine : MonoBehaviour
{
    public enum PipeLine_State
    {
        PS_None,
        PS_Corner,
        PS_I,
        PS_T,
        PS_X
    };
    public PipeLine_State MyState;

    public enum PipeLine_RotState
    {
        PRS_Left,
        PRS_Top,
        PRS_Right,
        PRS_Bottom
    };
    public PipeLine_RotState MyRotState;

    public bool b_IsPlaced = true;

    public BoxCollider boxCollider;

    [SerializeField] private GameObject Corner_Pipe;
    [SerializeField] private GameObject I_Pipe;
    [SerializeField] private GameObject T_Pipe;
    [SerializeField] private GameObject X_Pipe;


    [HideInInspector] public bool[] b_OpenWay = new bool[4];  //left top right bottom       //   top     right 
    //[SerializeField] private GameObject[] CheckWaterFlowColl_GameObject = new GameObject[4];
                                                                                            //  left    bottom    
    /* [HideInInspector] */ public bool b_IsWater = false;

    [SerializeField] private MeshRenderer Test_WaterSwitch_MeshRenderer;
    [SerializeField] private Material[] Test_WaterSwitch_Material = new Material[2];

    [HideInInspector] public Vector3 v_MyPosition;

    private void Awake()
    {
        Reset_PipeLine_Info();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetState());
        StartCoroutine(SetRotState());
        StartCoroutine(SetOpenWay());
        ResetRotState();
    }

    public void Reset_PipeLine_Info()
    {
        MyState = (PipeLine_State)Random.Range(1, 5); //.PS_Corner;
        MyRotState = (PipeLine_RotState)Random.Range(0, 4);
        ResetRotState();
    }

    public void Exchange_PipeLine_Info(PipeLine Target_cs)
    {
        MyState = Target_cs.MyState;
        MyRotState = Target_cs.MyRotState;
        ResetRotState();
    }

    public void PipeLine_State_To_None()
    {
        MyState = PipeLine_State.PS_None;
        MyRotState = (PipeLine_RotState)Random.Range(0, 4);
        ResetRotState();
        //StartCoroutine(SetState());
        //StartCoroutine(SetRotState());

        b_IsPlaced = false;

        this.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    //bool IsCollWater = false;
    //    for (int i = 0; i < 4; i++)
    //    {
    //        if (CheckWaterFlowColl_GameObject[i].GetComponent<CheckWaterFlow>().IsColl)
    //            b_IsWater = true;
    //    }
    //
    //
    //}'

    private void Update()
    {
        b_IsWater = false;
    }

    private void LateUpdate()
    {
        if (b_IsWater)
            Test_WaterSwitch_MeshRenderer.material = Test_WaterSwitch_Material[1];
        else
            Test_WaterSwitch_MeshRenderer.material = Test_WaterSwitch_Material[0];
    }

    private void ResetRotState()
    {
        switch (MyRotState)
        {
            case PipeLine_RotState.PRS_Left:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case PipeLine_RotState.PRS_Top:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case PipeLine_RotState.PRS_Right:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case PipeLine_RotState.PRS_Bottom:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                break;
        }
    }

    IEnumerator SetRotState()
    {
        while (true)
        {
            switch (MyRotState)
            {
                case PipeLine_RotState.PRS_Left:
                    //this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 0.2f);
                    break;
                case PipeLine_RotState.PRS_Top:
                    //this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), 0.2f);
                    break;
                case PipeLine_RotState.PRS_Right:
                    //this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), 0.2f);
                    break;
                case PipeLine_RotState.PRS_Bottom:
                    //this.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(new Vector3(0, 270, 0)), 0.2f);
                    break;
            }
            yield return null;
        }
    }

    IEnumerator SetState()
    {
        while (true)
        {
            switch (MyState)
            {
                case PipeLine_State.PS_None:
                    Corner_Pipe.SetActive(false);
                    I_Pipe.SetActive(false);
                    T_Pipe.SetActive(false);
                    X_Pipe.SetActive(false);

                    boxCollider.enabled = false;

                    Test_WaterSwitch_MeshRenderer.enabled = false;
                    break;
                case PipeLine_State.PS_Corner:
                    Corner_Pipe.SetActive(true);
                    I_Pipe.SetActive(false);
                    T_Pipe.SetActive(false);
                    X_Pipe.SetActive(false);

                    boxCollider.enabled = true;

                    Test_WaterSwitch_MeshRenderer.enabled = true;
                    break;
                case PipeLine_State.PS_I:
                    Corner_Pipe.SetActive(false);
                    I_Pipe.SetActive(true);
                    T_Pipe.SetActive(false);
                    X_Pipe.SetActive(false);

                    boxCollider.enabled = true;

                    Test_WaterSwitch_MeshRenderer.enabled = true;
                    break;
                case PipeLine_State.PS_T:
                    Corner_Pipe.SetActive(false);
                    I_Pipe.SetActive(false);
                    T_Pipe.SetActive(true);
                    X_Pipe.SetActive(false);

                    boxCollider.enabled = true;

                    Test_WaterSwitch_MeshRenderer.enabled = true;
                    break;
                case PipeLine_State.PS_X:
                    Corner_Pipe.SetActive(false);
                    I_Pipe.SetActive(false);
                    T_Pipe.SetActive(false);
                    X_Pipe.SetActive(true);

                    boxCollider.enabled = true;

                    Test_WaterSwitch_MeshRenderer.enabled = true;
                    break;
            }

            yield return null;
        }
    }

    IEnumerator SetOpenWay()
    {
        while (true)
        {
            switch (MyState)
            {
                case PipeLine_State.PS_None:
                    for (int i = 0; i < 4; i++)
                        b_OpenWay[i] = false;
                    break;
                case PipeLine_State.PS_Corner:
                    //b_OpenWay[0] = false; //left
                    //b_OpenWay[1] = true;  //top
                    //b_OpenWay[2] = true;  //right
                    //b_OpenWay[3] = false; //bottom
                    switch (MyRotState)
                    {
                        case PipeLine_RotState.PRS_Left:
                            b_OpenWay[0] = false; //left
                            b_OpenWay[1] = true;  //top
                            b_OpenWay[2] = true;  //right
                            b_OpenWay[3] = false; //bottom
                            //top right
                            break;
                        case PipeLine_RotState.PRS_Top:
                            b_OpenWay[0] = false; //left
                            b_OpenWay[1] = false; //top
                            b_OpenWay[2] = true;  //right
                            b_OpenWay[3] = true;  //bottom
                            //right bottom
                            break;
                        case PipeLine_RotState.PRS_Right:
                            b_OpenWay[0] = true;  //left
                            b_OpenWay[1] = false; //top
                            b_OpenWay[2] = false; //right
                            b_OpenWay[3] = true;  //bottom
                            //left bottom
                            break;
                        case PipeLine_RotState.PRS_Bottom:
                            b_OpenWay[0] = true;  //left
                            b_OpenWay[1] = true;  //top
                            b_OpenWay[2] = false; //right
                            b_OpenWay[3] = false; //bottom
                            //left top
                            break;
                    }

                    break;
                case PipeLine_State.PS_I:
                    //b_OpenWay[0] = false; //left
                    //b_OpenWay[1] = true;  //top
                    //b_OpenWay[2] = false; //right
                    //b_OpenWay[3] = true;  //bottom
                    switch (MyRotState)
                    {
                        case PipeLine_RotState.PRS_Left:
                            b_OpenWay[0] = false; //left
                            b_OpenWay[1] = true;  //top
                            b_OpenWay[2] = false; //right
                            b_OpenWay[3] = true;  //bottom
                            //top , bottom
                            break;
                        case PipeLine_RotState.PRS_Top:
                            b_OpenWay[0] = true;  //left
                            b_OpenWay[1] = false; //top
                            b_OpenWay[2] = true;  //right
                            b_OpenWay[3] = false; //bottom
                            //left , right
                            break;
                        case PipeLine_RotState.PRS_Right:
                            b_OpenWay[0] = false; //left
                            b_OpenWay[1] = true;  //top
                            b_OpenWay[2] = false; //right
                            b_OpenWay[3] = true;  //bottom
                            //top , bottom
                            break;
                        case PipeLine_RotState.PRS_Bottom:
                            b_OpenWay[0] = true;  //left
                            b_OpenWay[1] = false; //top
                            b_OpenWay[2] = true;  //right
                            b_OpenWay[3] = false; //bottom
                            //left , right
                            break;
                    }

                    break;
                case PipeLine_State.PS_T:
                    //b_OpenWay[0] = true;  //left
                    //b_OpenWay[1] = true;  //top
                    //b_OpenWay[2] = true;  //right
                    //b_OpenWay[3] = false; //bottom
                    switch (MyRotState)
                    {
                        case PipeLine_RotState.PRS_Left:
                            b_OpenWay[0] = true;  //left
                            b_OpenWay[1] = true;  //top
                            b_OpenWay[2] = true;  //right
                            b_OpenWay[3] = false; //bottom
                            //left top right
                            break;
                        case PipeLine_RotState.PRS_Top:
                            b_OpenWay[0] = false; //left
                            b_OpenWay[1] = true;  //top
                            b_OpenWay[2] = true;  //right
                            b_OpenWay[3] = true;  //bottom
                            //top right bottom
                            break;
                        case PipeLine_RotState.PRS_Right:
                            b_OpenWay[0] = true;  //left
                            b_OpenWay[1] = false; //top
                            b_OpenWay[2] = true;  //right
                            b_OpenWay[3] = true;  //bottom
                            //left bottom right
                            break;
                        case PipeLine_RotState.PRS_Bottom:
                            b_OpenWay[0] = true;  //left
                            b_OpenWay[1] = true;  //top
                            b_OpenWay[2] = false; //right
                            b_OpenWay[3] = true;  //bottom
                            //top left bottom
                            break;
                    }

                    break;
                case PipeLine_State.PS_X:
                    for (int i = 0; i < 4; i++)
                        b_OpenWay[i] = true;
                    break;
            }

            //for (int i = 0; i < 4; i++) {
            //    if (b_OpenWay[i])
            //        CheckWaterFlowColl_GameObject[i].SetActive(true);
            //    else
            //        CheckWaterFlowColl_GameObject[i].SetActive(false);
            //}


            yield return null;
        }
    }
}
