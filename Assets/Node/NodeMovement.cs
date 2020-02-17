using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeMovement : MonoBehaviour
{

    private bool waiting = false;
    private Vector3 randomPos;
    public float moveDelay = 1f;

    Rigidbody rb;

    Vector2 [] positions = new Vector2[] { new Vector2(-5f, 0), new Vector2(-4f, 0.75f), new Vector2(-5f, 2), new Vector2(-3, 1.5f), new Vector2(-1.5f,2),
                                           new Vector2(0,2.25f), new Vector2(1.5f, 2), new Vector2(3,1.5f), new Vector2(5,2), new Vector2(4, 0.75f), 
                                           new Vector2(5,0), new Vector2(4, -0.75f), new Vector2(5,-2), new Vector2(3,-1.5f), new Vector2(1.5f, -2), 
                                           new Vector2(0,-2.25f), new Vector2(-1.5f,-2), new Vector2(-3,-1.5f), new Vector2(-5,-2), new Vector2(-4, -0.75f)};
    int posIndex = 0;
    public int dir = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posIndex = Random.Range(0, positions.Length);

        assignDirection();
    }

    private void assignDirection()
    {
        int temp = Random.Range(0, 2);

        if (temp == 1)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
    }

    void FixedUpdate()
    {
        if (waiting == false)
        {
            StartCoroutine(LerpNode());
        }
        else
        {
            rb.AddForce(Seek(randomPos, 2f));
        }
    }

    IEnumerator LerpNode()
    {
        posIndex += Random.Range(1, 3) * dir;

        if(posIndex >= positions.Length)
        {
            posIndex %= positions.Length;
        }

        if(posIndex < 0)
        {
            posIndex = positions.Length + posIndex;
        }

        randomPos = new Vector3(positions[posIndex].x, positions[posIndex].y) + (new Vector3(Random.Range(0.5f, -0.5f), Random.Range(0.5f, -0.5f), Random.Range(1,-1)));
        waiting = true;
        yield return new WaitForSeconds(3f);
        
        waiting = false;
    }

    public Vector3 Seek(Vector3 targetPosition, float maxSpeed)
    {
        // Figure out out "perfect" desired velocity
        Vector3 desiredVelocity = targetPosition - this.transform.position;

        // Calculate how much we need to turn to
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        // face our desired velocity
        Vector3 steerForce = desiredVelocity;
        return steerForce;
    }
}
