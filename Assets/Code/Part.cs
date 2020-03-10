using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public Vector3 myPosition;

    private Transform localTransform;

    private void Awake()
    {
        localTransform = transform;
    }

    private void Start()
    {
        myPosition = localTransform.position;
        myPosition.y = 0;
    }

    private void Update()
    {
        localTransform.position = Vector3.Lerp(localTransform.position, myPosition, 4 * Time.deltaTime);
    }
}
