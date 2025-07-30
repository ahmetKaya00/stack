using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if(Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                if(MovingCube.CurrentCube != null)
                    MovingCube.CurrentCube.Stop();
                FindObjectOfType<CubeRespawn>().SpawnCube();
            }
        }
    }
}
