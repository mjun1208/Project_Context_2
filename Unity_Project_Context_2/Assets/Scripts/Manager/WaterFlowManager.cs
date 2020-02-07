using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowManager : MonoBehaviour
{
    public static WaterFlowManager instance;
    [HideInInspector] public List<GameObject> PlacedPipeLine = new List<GameObject>();
    public List<Vector3> v_WaterFlowPlace = new List<Vector3>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckWaterFlowPlace());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckWaterFlowPlace()
    {
        while (true)
        {
            for (int i = 0; i < v_WaterFlowPlace.Count; i++)
            {
                for (int j = 0; j < PlacedPipeLine.Count; j++)
                {
                    PipeLine Temp_PipeLine_cs = PlacedPipeLine[j].GetComponent<PipeLine>();
                    if (Temp_PipeLine_cs.b_IsPlaced && Temp_PipeLine_cs.v_MyPosition == v_WaterFlowPlace[i])
                    {
                        if (Temp_PipeLine_cs.b_OpenWay[1])
                        {
                            Temp_PipeLine_cs.b_IsWater = true;
                            for (int k = 0; k < 4; k++)
                            {
                                if (Temp_PipeLine_cs.b_OpenWay[k])
                                    CheckWaterFlow(Temp_PipeLine_cs.v_MyPosition, k);
                            }
                        }
                    }

                }
            }
            yield return null;
        }

    }

    void CheckWaterFlow(Vector3 v_Pos, int Way)
    {
        for (int i = 0; i < PlacedPipeLine.Count; i++)
        {
            PipeLine Temp_PipeLine_cs = PlacedPipeLine[i].GetComponent<PipeLine>();

            //Debug.Log(v_Pos +  "|| yee || " + Temp_PipeLine_cs.v_MyPosition);

            switch (Way)
            {
                case 0: //left
                    if (Temp_PipeLine_cs.v_MyPosition == v_Pos + new Vector3(0, 0, -1) && Temp_PipeLine_cs.b_OpenWay[2])
                    {

                        if (Temp_PipeLine_cs.b_IsWater)
                            return;

                        Temp_PipeLine_cs.b_IsWater = true;

                        for (int k = 0; k < 4; k++)
                        {
                            if (Temp_PipeLine_cs.b_OpenWay[k])
                                CheckWaterFlow(Temp_PipeLine_cs.v_MyPosition, k);
                        }
                        return;
                    }
                    break;
                case 1: //top
                    if (Temp_PipeLine_cs.v_MyPosition == v_Pos + new Vector3(0, -1, 0) && Temp_PipeLine_cs.b_OpenWay[3])
                    {
                        if (Temp_PipeLine_cs.b_IsWater)
                            return;

                        Temp_PipeLine_cs.b_IsWater = true;

                        for (int k = 0; k < 4; k++)
                        {
                            if (Temp_PipeLine_cs.b_OpenWay[k])
                                CheckWaterFlow(Temp_PipeLine_cs.v_MyPosition, k);
                        }
                        return;
                    }
                    break;
                case 2: //right
                    if (Temp_PipeLine_cs.v_MyPosition == v_Pos + new Vector3(0, 0, 1) && Temp_PipeLine_cs.b_OpenWay[0])
                    {
                        if (Temp_PipeLine_cs.b_IsWater)
                            return;

                        Temp_PipeLine_cs.b_IsWater = true;

                        for (int k = 0; k < 4; k++)
                        {
                            if (Temp_PipeLine_cs.b_OpenWay[k])
                                CheckWaterFlow(Temp_PipeLine_cs.v_MyPosition, k);
                        }
                        return;
                    }
                    break;
                case 3: //bottom
                    if (Temp_PipeLine_cs.v_MyPosition == v_Pos + new Vector3(0, 1, 0) && Temp_PipeLine_cs.b_OpenWay[1])
                    {
                        if (Temp_PipeLine_cs.b_IsWater)
                            return;

                        Temp_PipeLine_cs.b_IsWater = true;

                        for (int k = 0; k < 4; k++)
                        {
                            if (Temp_PipeLine_cs.b_OpenWay[k])
                                CheckWaterFlow(Temp_PipeLine_cs.v_MyPosition, k);
                        }
                        return;
                    }
                    break;
                default:
                    break;
            }
        }
        return;
    }
}
