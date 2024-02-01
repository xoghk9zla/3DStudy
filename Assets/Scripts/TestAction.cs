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
            Debug.Log($"���� �׽�Ʈ �׼��̰� ���� ����{testNum}�� ������ �ֽ��ϴ�.");
        };
    }
}
