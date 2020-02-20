﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineCode;
public class Separating : State<Node>
{
    private static Separating _instance;

    public float gametime;
    public int seconds = 0;

    private Separating()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static Separating Instance
    {
        get
        {
            if (_instance == null)
                new Separating();

            return _instance;
        }
    }

    public override void enterState(Node _node)
    {
        _node.rb.AddForce(new Vector3(Random.Range(500, 1000), Random.Range(500, 1000), Random.Range(500, 1000)));
        _node.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        Debug.Log("Entering Separating state");
    }

    public override void exitState(Node _node)
    {
        _node.StopAllCoroutines();
        Debug.Log("Exiting Separating state");
    }

    public override void updateState(Node _node)
    {
        if (_node.forming)
        {
            _node.stateMachine.changeState(Forming.Instance);
        }

        if(Time.time > gametime + 1)
        {
            gametime = Time.time;
            seconds++;
            Debug.Log(seconds);
        }

        if(seconds == 2)
        {
            _node.stateMachine.changeState(Rest.Instance);
        }
    }
}
