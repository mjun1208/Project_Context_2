using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLineManager : MonoBehaviour
{
    public static PipeLineManager instance;
    public List<PipeLine> BenchPipeLine_PipeLine_cs;
    public GameObject[,] PipeLines_Position_GameObject = new GameObject[4, 4];
    private bool[,,] b_IsPipeLinePlaced;
    private GameObject target_GameObject;
    private PipeLine target_PipeLine_cs;

    private bool b_IsMouseDown = false;
    private int i_Tile_LayerMask;// = 1 << LayerMask.NameToLayer("Tile");
    private bool b_CameraScroll;

    private Vector2 v_CurMousePos;
    private Vector2 v_LastMousePos;
    private float f_ScrollOffset;

    [HideInInspector] public int i_Floor;

    public TileManager tileManager;

    public GameObject PipeLine_GameObject;

    private bool b_IsPointerInButton = false;
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
        instance = this;
        b_IsPipeLinePlaced = new bool[tileManager.transform.childCount , 4, 4];
        i_Tile_LayerMask = 1 << LayerMask.NameToLayer("Tile");
        //i_Tile_LayerMask = ~i_Tile_LayerMask;


        int i_PipeLineCount = 0;
        for (int i = 0; i < 4; i++)
        {
             for (int j = 0; j < 4; j++)
             {
                PipeLines_Position_GameObject[i, j] = this.transform.GetChild(i_PipeLineCount).gameObject;
                i_PipeLineCount++;
             }
        }
    }

    void Start()
    {
        b_IsPointerInButton = false;
        f_ScrollOffset = 0;
        i_Floor = 0;
        //StartCoroutine(SetPipeLinePostion());

        ResetPipeLinePosition();
        b_IsMouseDown = false;

        for (int i = 0; i < tileManager.transform.childCount - 1; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    b_IsPipeLinePlaced[i, j, k] = false;
                }
            }
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (!b_CameraScroll)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position , new Vector3(Camera.main.transform.position.x, 18 - (i_Floor * 14), Camera.main.transform.position.z), 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !b_IsPointerInButton)
        {
            //Debug.Log("Down");
            if (!b_IsMouseDown)
            {
                target_GameObject = GetClickedObject();
                if (target_GameObject != null && target_GameObject.gameObject.tag == "PipeLine")  //선택된게 나라면
                {
                    //Debug.Log("MY tag is !!! : " + target_GameObject.gameObject.tag);
                    b_IsMouseDown = true;
                    target_PipeLine_cs = target_GameObject.GetComponent<PipeLine>();

                    //Debug.Log(target_PipeLine_cs.b_IsPlaced);

                    if (target_PipeLine_cs.b_IsPlaced)
                    {
                        if (target_PipeLine_cs.MyRotState == PipeLine.PipeLine_RotState.PRS_Bottom)
                            target_PipeLine_cs.MyRotState = PipeLine.PipeLine_RotState.PRS_Left;
                        else
                            target_PipeLine_cs.MyRotState++;

                        b_IsMouseDown = false;
                    }
                }
                else
                {
                    b_CameraScroll = true;
                    v_LastMousePos = Input.mousePosition;
                }
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Debug.Log("Press");
            if (b_IsMouseDown)
            {
                Vector3 v_MousePos;// = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Input.mousePosition;
                                   //v_MousePos = Camera.main.ScreenToWorldPoint(v_MousePos);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (true == Physics.Raycast(ray, out hit, 30f, i_Tile_LayerMask))
                {

                    //Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red, 0.1f);

                    //Debug.Log(hit.point);
                    //Debug.Log(hit.point);
                    v_MousePos = hit.point;

                    target_GameObject.transform.position = new Vector3(v_MousePos.x, -i_Floor * 14 + 1.2f, v_MousePos.z);
                }
            }
            else if (b_CameraScroll)
            {
                v_CurMousePos = Input.mousePosition;
                f_ScrollOffset = v_CurMousePos.y - v_LastMousePos.y;
                v_LastMousePos = Input.mousePosition;

                Camera.main.transform.position += new Vector3(0, f_ScrollOffset / 10f, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            if (b_IsMouseDown)
            {
                //Debug.Log("Up");
                b_IsMouseDown = false;

                //GameObject TempPipeLine_GameObject = PipeLine_GameObject;

                Vector3 v_TempPipeLine_Position = SetPipeLinePosition(target_GameObject.transform.position);

                if (v_TempPipeLine_Position != Vector3.zero)
                {
                    //Debug.Log("타겟 정보 : " + target_PipeLine_cs.MyState + " " + target_PipeLine_cs.MyRotState +
                    //          "생성 정보 : " + TempPipeLine_PipeLine_cs.MyState + " " + TempPipeLine_PipeLine_cs.MyRotState);
                    GameObject TempPipeLine_GameObject = Instantiate(PipeLine_GameObject, v_TempPipeLine_Position, target_GameObject.transform.rotation);

                    PipeLine TempPipeLine_PipeLine_cs = TempPipeLine_GameObject.GetComponent<PipeLine>();

                    TempPipeLine_PipeLine_cs.Exchange_PipeLine_Info(target_PipeLine_cs);
                    TempPipeLine_PipeLine_cs.b_IsPlaced = true;

                    target_GameObject.transform.localPosition = Vector3.zero;
                    target_PipeLine_cs.Reset_PipeLine_Info();
                    target_PipeLine_cs.MyState = PipeLine.PipeLine_State.PS_None;
                    target_PipeLine_cs.b_IsPlaced = false;
                }
                else
                {
                    target_GameObject.transform.localPosition = Vector3.zero;
                }
                target_GameObject = null;
            }
            else if (b_CameraScroll)
            {
                b_CameraScroll = false;

                if (Camera.main.transform.position.y - 18 <= 0)
                    i_Floor = (int)Mathf.Abs(Camera.main.transform.position.y - 18 - 7) / 14;
                else
                    i_Floor = 0;

                if (i_Floor > 4)
                    i_Floor = 4;

                ResetPipeLinePosition();
            }
        }

    }

    Vector3 SetPipeLinePosition(Vector3 v_MyPosition)
    {
        Vector3 v_PipeLinePosition = Vector3.zero;
        float f_MinDistance = 30f;
        int x = 0, y = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float f_CurDistance = Vector3.Distance(v_MyPosition, PipeLines_Position_GameObject[i, j].transform.position);
                if (f_CurDistance < f_MinDistance)
                {
                    f_MinDistance = f_CurDistance;
                    v_PipeLinePosition = PipeLines_Position_GameObject[i, j].transform.position;

                    x = i;
                    y = j;
                }
            }
        }
        //Debug.Log(f_MinDistance);

        if (!b_IsPipeLinePlaced[i_Floor, x, y] && f_MinDistance < 2f)
        {
            target_PipeLine_cs.b_IsPlaced = true;
            b_IsPipeLinePlaced[i_Floor, x, y] = true;
			//PipesSpawn.instance.TakePipeFromStorage(target_PipeLine_cs.gameObject);
		}
        else
        {
            //Debug.Log(b_IsPipeLinePlaced[i_Floor, x, y]);
            return Vector3.zero;
        }
        return v_PipeLinePosition;
    }

    void ResetPipeLinePosition()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                PipeLines_Position_GameObject[i, j].transform.position = new Vector3(3 - i * 2, -i_Floor * 14 + 1.2f, 3 - j * 2);
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

    public void BuyPipeLine()
    {
        for (int i = 0; i < BenchPipeLine_PipeLine_cs.Count; i++)
        {
            if (BenchPipeLine_PipeLine_cs[i].MyState == PipeLine.PipeLine_State.PS_None)
            {
                BenchPipeLine_PipeLine_cs[i].Reset_PipeLine_Info();
                return;
            }
        }

    }

    public void BuyButtonEnter()
    {
        //Debug.Log("Hi");
        //b_CameraScroll = false;
        //b_IsMouseDown = false;
        b_IsPointerInButton = true;
    }

    public void BuyButtonExit()
    {
        //Debug.Log("Bye");
        b_IsPointerInButton = false;
    }
}
