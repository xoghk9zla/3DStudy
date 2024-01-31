using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Cams
{
    MainCam, SubCam1, SubCam2, SubCam3,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] List<Camera> listCam;
    [SerializeField] List<Button> listBtn;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        int enumCount = System.Enum.GetValues(typeof(Camera)).Length;

        int intEnum = (int)Cams.MainCam;
        Cams enumData = (Cams)intEnum;

        string stringEnum = Cams.MainCam.ToString();
        enumData = (Cams)System.Enum.Parse(typeof(Cams), stringEnum);
        */
        SwitchCamera(Cams.MainCam);
        InitBtns();
    }

    private void InitBtns()
    {
        listBtn[0].onClick.AddListener(()=>SwitchCamera(0));
        listBtn[1].onClick.AddListener(()=>SwitchCamera(1));
        listBtn[2].onClick.AddListener(()=>SwitchCamera(2));
        listBtn[3].onClick.AddListener(()=>SwitchCamera(3));

        // 람다식 for문을 만났을 때 조건이 되는 변수가 계속 변하게 하는게
        // 그 변하는 데이터의 주소를 계속 전달하기 때문에 문제를 야기
        int count = listCam.Count;
        for(int i  = 0; i< count; ++i)
        {
            int num = i;
            listBtn[num].onClick.AddListener(() => SwitchCamera(num));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCamera(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCamera(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCamera(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchCamera(3);
        }
    }

    // 기능: 매개변수로 전달받은 카메라는 켜주고, 나머지 카메라는 꺼주는 함수
    private void SwitchCamera(Cams _value)
    {
        int count = listCam.Count;
        int findNum = (int)_value;

        for(int i = 0; i < count; i++)
        {
            Camera cam = listCam[i];
            cam.enabled = false;
            //cam.enabled = i == findNum;
            if(i == findNum)
            {
                cam.enabled = true;
            }
        }
    }
    private void SwitchCamera(int _value)
    {
        int count = listCam.Count;

        for(int i = 0; i < count; i++)
        {
            Camera cam = listCam[i];
            cam.enabled = false;

            if(i == _value)
            {
                cam.enabled = true;
            }
        }
    }
}
