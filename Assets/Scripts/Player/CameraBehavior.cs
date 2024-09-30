using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Transform followObject;
    [SerializeField] private Vector3 offSet = new(0, 9, 5.61f);

    private void Update()
    {
        transform.position = followObject.position + offSet;
    }
}
