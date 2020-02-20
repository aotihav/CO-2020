using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;
public class Connected : State<Node>
{
    private static Connected _instance;

    public float gametime;
    public int seconds = 0;

    bool centered;
    private Connected()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static Connected Instance
    {
        get
        {
            if (_instance == null)
                new Connected();

            return _instance;
        }
    }

    public override void enterState(Node _node)
    {
        _node.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        _node.connected = true;
        centered = false;
        Debug.Log("Entering Connected state");
    }

    public override void exitState(Node _node)
    {
        _node.StopAllCoroutines();
        Debug.Log("Exiting Connected state");
    }

    public override void updateState(Node _node)
    {
        if (!_node.connected)
        {
            _node.stateMachine.changeState(Separating.Instance);
        }

        if (Time.time > gametime + 1)
        {
            gametime = Time.time;
            seconds++;
            Debug.Log(seconds);
        }

        if (seconds == 4)
        {
            _node.connected = false;
        }
        _node.rb.AddForce(_node.Seek(new Vector3(0, 0, 0), 3.5f));
    }
}
