using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;

public class Rest : State<Node> 
{
    private static Rest _instance;

    public int dir;
    Vector3 randomPos;

    bool waiting; 
    private Rest()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static Rest Instance
    {
        get
        {
            if (_instance == null)
                new Rest();

            return _instance;
        }
    }

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
        if (_node.forming)
        {
            _node.stateMachine.changeState(Forming.Instance);
        }

        if (waiting == false)
        {
            _node.StartCoroutine(GetNextPosition(_node));
            Debug.Log("New Targer (" + randomPos.x + ", " + randomPos.y + ")");
        }
        else
        {
            _node.rb.AddForce(_node.Seek(randomPos, 1.5f));
        }
    }

    IEnumerator GetNextPosition(Node _node)
    {
        _node.posIndex += 1 * dir;

        if (_node.posIndex >= _node.positions.Count)
        {
            _node.posIndex %= _node.positions.Count;
        }

        if (_node.posIndex < 0)
        {
            _node.posIndex = _node.positions.Count + _node.posIndex;
        }

        randomPos = new Vector3(_node.positions[_node.posIndex].x, _node.positions[_node.posIndex].y) + (new Vector3(Random.Range(0.2f, -0.2f), Random.Range(0.2f, -0.2f), Random.Range(1, -1)));

        waiting = true;
        yield return new WaitForSeconds(4f);
        waiting = false;
    }

}
