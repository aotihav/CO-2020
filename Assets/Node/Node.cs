using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;

public class Node : MonoBehaviour
{
    public int id;

    public bool searching;
    public bool connected;

    public float gameTimer;
    public int seconds = 0;
    public StateMachine<Node> stateMachine { get; set; }

    public Rigidbody rb;

    public Node connectedNode;
    public Node Neighbor; //References the node associated with the other hand

    public NodeManager nodeManager;

    public KeyCode OnKey; //test variable that is a place holder for the arduino controllers

    // Start is called before the first frame update
    private void Start()
    {
        gameTimer = Time.time;

        rb = this.GetComponent<Rigidbody>();

        stateMachine = new StateMachine<Node>(this);
        stateMachine.changeState(new Rest());

        nodeManager = GameObject.FindObjectOfType<NodeManager>();
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

    public void ActivateNode()
    {
        stateMachine.changeState(new Searching());
    }

    public void DeactivateNode()
    {
        if (connected)
            stateMachine.changeState(new Separating());
        else
            stateMachine.changeState(new Rest());
    }
}
