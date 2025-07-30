using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    [SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        if(LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0.1f, 1f), UnityEngine.Random.Range(0.1f, 1f), UnityEngine.Random.Range(0.1f, 1f));
    }

    internal void Stop()
    {
        moveSpeed = 0f;
        float breakZ = transform.position.z - LastCube.transform.position.z;

        if(Math.Abs(breakZ) >= LastCube.transform.localScale.z)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = breakZ > 0 ? 1f : -1f; 

        SplitCubeZ(breakZ, direction);
        LastCube = this;
    }

    private void SplitCubeZ(float breakZ, float direction)
    {
        float newSize = LastCube.transform.localScale.z - Mathf.Abs(breakZ);
        float fallingBlockSize = transform.localScale.z - newSize;
        float newPosition = LastCube.transform.localPosition.z + (breakZ / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
        float cubeEdge = transform.position.z + (newSize / 2 * direction);
        float fallingBlockZPos = cubeEdge + fallingBlockSize / 2f * direction;

        DropCube(fallingBlockZPos, fallingBlockSize);
    }

    private void DropCube(float fallingBlockZPos, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPos);
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    }

    void Update()
    {
        if (MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
