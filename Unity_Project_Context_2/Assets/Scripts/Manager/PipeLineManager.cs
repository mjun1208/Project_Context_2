using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLineManager : MonoBehaviour
{
    public List<GameObject> BenchPipeLine;
    public GameObject[,] PipeLines_GameObject = new GameObject[4, 4];
    private GameObject target_GameObject;
    private PipeLine target_PipeLine_cs;

    private bool b_IsMouseDown = false;
    private int i_Tile_LayerMask;// = 1 << LayerMask.NameToLayer("Tile");
    ////2.95,1,3   || 2.95,1,1  || 2.95,1,-1,  || 2.95,1,-3
    ////0,95,1,3   || 0,95,1,1  || 0,95,1,-1,  || 0,95,1,-3
    ////-0.95,1,3  || -0.95,1,1 || -0.95,1,-1, || -0.95,1,-3
    ////-2,95,1,3  || -2,95,1,1 || -2,95,1,-1, || -2,95,1,-3

    //
    //            (0,3)
    //        (0,2)   (1,3)
    //    (0,1)   (1,2)   (2,3)
    //(0,0)   (1,1)   (2,2)   (3,3)
    //    (1,0)   (2,1)   (3,2)
    //        (2,0)   (3,1)
    //            (3,0)  
    //

    // Start is called before the first frame update
    private void Awake()
    {
        i_Tile_LayerMask = 1 << LayerMask.NameToLayer("Tile");
        //i_Tile_LayerMask = ~i_Tile_LayerMask;


        int i_PipeLineCount = 0;
        for (int i = 0; i < 4; i++)
        {
             for (int j = 0; j < 4; j++)
             {
                PipeLines_GameObject[i, j] = this.transform.GetChild(i_PipeLineCount).gameObject;
                i_PipeLineCount++;
             }
        }
    }

    void Start()
    {
        //StartCoroutine(SetPipeLinePostion());
        ResetPipeLinePosition();
        b_IsMouseDown = false;
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !b_IsMouseDown)
        {
            //Debug.Log("Down");
            target_GameObject = GetClickedObject();
            if (target_GameObject != null && target_GameObject.gameObject.tag == "PipeLine")  //선택된게 나라면
            {
                //Debug.Log("MY tag is !!! : " + target_GameObject.gameObject.tag);
                b_IsMouseDown = true;
                target_PipeLine_cs = target_GameObject.GetComponent<PipeLine>();

                if (!target_PipeLine_cs.b_IsPlaced)
                {
                    target_PipeLine_cs.b_IsPlaced = true;
                }
                else
                {
                    if (target_PipeLine_cs.MyRotState == PipeLine.PipeLine_RotState.PRS_Bottom)
                        target_PipeLine_cs.MyRotState = PipeLine.PipeLine_RotState.PRS_Left;
                    else
                        target_PipeLine_cs.MyRotState++;

                    b_IsMouseDown = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && b_IsMouseDown)
        {
            //Debug.Log("Press");
            Vector3 v_MousePos;// = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Input.mousePosition;
            //v_MousePos = Camera.main.ScreenToWorldPoint(v_MousePos);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (true == Physics.Raycast(ray, out hit, 30f, i_Tile_LayerMask))
            {

                //Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red, 0.1f);

                //Debug.Log(hit.point);
                v_MousePos = hit.point;

                target_GameObject.transform.position = new Vector3(v_MousePos.x, 1.2f, v_MousePos.z);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && b_IsMouseDown)
        {
            //Debug.Log("Up");
            b_IsMouseDown = false;

            target_GameObject.transform.position = SetPipeLinePosition(target_GameObject.transform.position);
            target_GameObject = null;
        }

    }

    Vector3 SetPipeLinePosition(Vector3 v_MyPosition)
    {
        Vector3 v_PipeLinePosition = Vector3.zero;
        float f_MinDistance = 30f;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float f_CurDistance = Vector3.Distance(v_MyPosition, PipeLines_GameObject[i, j].transform.position);
                if (f_CurDistance < f_MinDistance)
                {
                    f_MinDistance = f_CurDistance;
                    v_PipeLinePosition = PipeLines_GameObject[i, j].transform.position;
                }
            }
        }

        return v_PipeLinePosition;
    }

    void ResetPipeLinePosition()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                PipeLines_GameObject[i, j].transform.position = new Vector3(3 - i * 2, 1.2f, 3 - j * 2);
            }
        }
    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        target_GameObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 

        if (true == (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("PipeLine"))))   //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장한다.
            target_GameObject = hit.collider.gameObject;
        }
        return target_GameObject;
    }
}
