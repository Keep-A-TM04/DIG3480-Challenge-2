using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 moveTemp;

    public float speed;
    public float xDifference;

    public float movemenThreshold;

    //private Vector3 offset;

    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if(player.transform.position.x > transform.position.x)
        {
            xDifference = player.transform.position.x - transform.position.x;
        }
        else
        {
            xDifference = transform.position.x - player.transform.position.x;
        }

        if (xDifference >= movemenThreshold)
        {
            moveTemp = player.transform.position;
            moveTemp.z = -10;
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
    }
}
