using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{
    [SerializeField] int testNum;

    private void OnDestroy()
    {
        CameraManager.Instance.RemoveAction(() =>
        {

        });
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraManager.Instance.Action = () => 
        {
            Debug.Log($"저는 테스트 액션이고 저는 정수{testNum}를 가지고 있습니다.");
        };
    }
}
