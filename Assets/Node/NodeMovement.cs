using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeMovement : MonoBehaviour
{

    private bool waiting = false;
    private Vector3 randomPos;

    Rigidbody rb;

    public List<Vector2> positions;

    int posIndex = 0;
    public int dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

        List<GameObject> targets = GameObject.FindGameObjectsWithTag("movementTarget").ToList();
        positions = targets.Select(x => new Vector2(x.transform.position.x,x.transform.position.y)).ToList();
        positions = positions.OrderBy((a) => Mathf.Atan2(a.y, a.x)).ToList();
        posIndex = 0;
        dir = 1;
    }

    void FixedUpdate()
    {
        if (waiting == false)
        {
            StartCoroutine(GetNextPosition());
        }
        else
        {
            rb.AddForce(Seek(randomPos, 2f));
        }
    }

    IEnumerator GetNextPosition()
    {
        posIndex += Random.Range(1,3) * dir;

        if(posIndex >= positions.Count)
        {
            posIndex %= positions.Count;
        }

        if(posIndex < 0)
        {
            posIndex = positions.Count + posIndex;
        }

        randomPos = new Vector3(positions[posIndex].x, positions[posIndex].y) + (new Vector3(Random.Range(0.2f, -0.2f), Random.Range(0.2f, -0.2f), Random.Range(1,-1)));
        
        waiting = true;
        yield return new WaitForSeconds(2.5f);
        waiting = false;
    }

    public Vector3 Seek(Vector3 targetPosition, float maxSpeed)
    {
        Vector3 desiredVelocity = targetPosition - this.transform.position;

        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        Vector3 steerForce = desiredVelocity;
        return steerForce;
    }
}
