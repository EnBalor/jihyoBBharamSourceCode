using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    private void Start()
    {
        cinemachine.Follow = GameManager.Instance.Player.transform;
    }
}
