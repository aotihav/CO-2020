using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;

public class Spawning : State<Node>
{
    public int dir;
    Vector3 randomPos;

    Renderer renderer;
    Color nodeColor;

    bool waiting;
    bool isSpawning;

    public override void enterState(Node _node)
    {
        renderer = _node.GetComponent<Renderer>();
        nodeColor = renderer.material.GetColor("_Color") ;

        nodeColor.a = 0;

        renderer.material.SetColor("_Color", nodeColor);

        waiting = false;
        isSpawning = true;        

        dir = 1;
        Debug.Log("Spawning Node");
    }

    public override void exitState(Node _node)
    {
        _node.StopAllCoroutines();
        Debug.Log("Exiting Spawning state");
    }

    public override void updateState(Node _node)
    {
        if (!isSpawning)
        {
            _node.stateMachine.changeState(new Rest());
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

        renderer.material.color += new Color(0.0f, 0.0f, 0.0f, 0.01f);

        if(renderer.material.color.a >= 1.0f)
        {
            isSpawning = false;
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
