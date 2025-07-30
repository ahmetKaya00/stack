using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        if(LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        CurrentCube = this;
    }
    internal void Stop()
    {
        moveSpeed = 0f;
        float breakZ = transform.position.z - LastCube.transform.position.z;

        SplitCubeZ(breakZ);
    }

    private void SplitCubeZ(float breakZ)
    {
        float newSize = LastCube.transform.localScale.z - Mathf.Abs(breakZ);
        float fallingBlockSize = transform.localScale.z - newSize;
        float newPosition = LastCube.transform.localPosition.z + (breakZ / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
