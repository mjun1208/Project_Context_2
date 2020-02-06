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

    [SerializeField] private GameObject Corner_Pipe;
    [SerializeField] private GameObject I_Pipe;
    [SerializeField] private GameObject T_Pipe;
    [SerializeField] private GameObject X_Pipe;

    private void Awake()
    {
        Reset_PipeLine_Info();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Reset_PipeLine_Info()
    {
        MyState = (PipeLine_State)Random.Range(1, 5); //.PS_Corner;
        MyRotState = (PipeLine_RotState)Random.Range(0, 4);
        StartCoroutine(SetState());
        StartCoroutine(SetRotState());
    }

    public void Exchange_PipeLine_Info(PipeLine Target_cs)
    {
        MyState = Target_cs.MyState;
        MyRotState = Target_cs.MyRotState;

        //b_IsPlaced = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SetRotState()
    {
        while (true)
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
                    break;
                case PipeLine_State.PS_Corner:
                    Corner_Pipe.SetActive(true);
                    I_Pipe.SetActive(false);
                    T_Pipe.SetActive(false);
                    X_Pipe.SetActive(false);
                    break;
                case PipeLine_State.PS_I:
                    Corner_Pipe.SetActive(false);
                    I_Pipe.SetActive(true);
                    T_Pipe.SetActive(false);
                    X_Pipe.SetActive(false);
                    break;
                case PipeLine_State.PS_T:
                    Corner_Pipe.SetActive(false);
                    I_Pipe.SetActive(false);
                    T_Pipe.SetActive(true);
                    X_Pipe.SetActive(false);
                    break;
                case PipeLine_State.PS_X:
                    Corner_Pipe.SetActive(false);
                    I_Pipe.SetActive(false);
                    T_Pipe.SetActive(false);
                    X_Pipe.SetActive(true);
                    break;
            }
            yield return null;
        }
    }
}
