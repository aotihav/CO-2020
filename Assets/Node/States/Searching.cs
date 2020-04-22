using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;
public class Searching : State<Node>
{
    public int dir;
    Vector3 randomPos;

    bool waiting;

    public override void enterState(Node _node)
    {
        _node.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        waiting = false;
        dir = 1;
        Debug.Log("Entering Searching state");
    }

    public override void exitState(Node _node)
    {
        _node.StopAllCoroutines();
        Debug.Log("Exiting Searching state");
    }

    public override void updateState(Node _node)
    {
        if (_node.connectedNode != null)
        {
            _node.stateMachine.changeState(new Forming());
        }
        if (waiting == false)
        {
            _node.StartCoroutine(GetNextPosition(_node));
        }
        else
        {
            _node.rb.AddForce(_node.Seek(randomPos, 0.5f));
        }
    }

    IEnumerator GetNextPosition(Node _node)
    {
        _node.nodeManager.getRandomPosition();

        waiting = true;
        yield return new WaitForSeconds(3f);
        waiting = false;
    }
}