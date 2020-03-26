using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;
using System;

public class Forming : State<Node>
{
    public int dir;
    Vector3 randomPos;

    bool centered;

    public override void enterState(Node _node)
    {
        _node.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
        centered = false;
        dir = -1;
        Debug.Log("Entering Forming state");
    }

    public override void exitState(Node _node)
    {
        _node.StopAllCoroutines();
        Debug.Log("Exiting Forming state");
    }

    public override void updateState(Node _node)
    {

        if(centered)
        {
            _node.stateMachine.changeState(new Connected());
        }

        _node.rb.AddForce(_node.Seek(new Vector3(0,0,0), 1.5f));
        CheckPosition(_node);
    }

    private void CheckPosition(Node _node)
    {
        if (Vector3.Distance(_node.transform.position, new Vector3(0, 0, 0)) < 0.5f)
        {
            centered = true;
        }
    }
}
