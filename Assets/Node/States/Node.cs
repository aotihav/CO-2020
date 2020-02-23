using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;

public class Node : MonoBehaviour
{
    public bool forming;
    public bool connected;

    public float gameTimer;
    public int seconds = 0;
    public StateMachine<Node> stateMachine { get; set; }

    public List<Vector2> positions;
    public int posIndex;

    public Rigidbody rb;

    public Node connectedNode;

    // Start is called before the first frame update
    private void Start()
    {
       

        gameTimer = Time.time;

        rb = this.GetComponent<Rigidbody>();

        List<GameObject> targets = GameObject.FindGameObjectsWithTag("movementTarget").ToList();
        positions = targets.Select(x => new Vector2(x.transform.position.x, x.transform.position.y)).OrderBy((a) => Mathf.Atan2(a.y, a.x)).ToList();
        posIndex = Random.Range(0, positions.Count);


        stateMachine = new StateMachine<Node>(this);
        stateMachine.changeState(new Rest());
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
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
