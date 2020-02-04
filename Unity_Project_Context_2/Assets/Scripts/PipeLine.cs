using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLine : MonoBehaviour
{
    public enum PipeLine_State
    {
        PS_None,
        PS_Straight,
        PS_Corner
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

    public bool b_IsPlaced = false;

    [SerializeField] private GameObject Straight_Pipe;
    [SerializeField] private GameObject Corner_Pipe;

    // Start is called before the first frame update
    void Start()
    {
        MyState = PipeLine_State.PS_Straight;
        MyRotState = PipeLine_RotState.PRS_Left;
        StartCoroutine(SetState());
        StartCoroutine(SetRotState());
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
                    Straight_Pipe.SetActive(false);
                    Corner_Pipe.SetActive(false);
                    break;
                case PipeLine_State.PS_Straight:
                    Straight_Pipe.SetActive(true);
                    Corner_Pipe.SetActive(false);
                    break;
                case PipeLine_State.PS_Corner:
                    Straight_Pipe.SetActive(false);
                    Corner_Pipe.SetActive(true);
                    break;
            }
            yield return null;
        }
    }
}
