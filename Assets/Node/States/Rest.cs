using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;

public class Rest : State<Node> 
{
    public int dir;
    Vector3 randomPos;

    bool waiting; 

    public override void enterState(Node _node)
    {
        _node.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        waiting = false;
        dir = 1;
        Debug.Log("Entering Rest state");
    }

    public override void exitState(Node _node)
    {
        _node.StopAllCoroutines();
        Debug.Log("Exiting Rest state");
    }

    public override void updateState(Node _node)
    {
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
