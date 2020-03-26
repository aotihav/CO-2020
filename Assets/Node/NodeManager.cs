using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class NodeManager : MonoBehaviour
{
    public static NodeManager current;

    public GameObject[] nodes;
    public GameObject[] activeNodes;
    
    public GameObject nodePF;


    private void Awake()
    {
        current = this;
    }

    public event Action OnActivateNode;
    public void ActivateNode()
    {
        if(OnActivateNode != null)
        {
            OnActivateNode.Invoke();
        }
    }
    public void DectivateNode()
    {

    }
}
