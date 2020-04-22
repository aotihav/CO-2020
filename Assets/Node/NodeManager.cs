using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NodeManager : MonoBehaviour
{
    public static NodeManager current;

    public GameObject[] nodes;
    public GameObject[] activeNodes;
    
    public GameObject nodePF;
    public List<Vector2> positions;

    private void Awake()
    {
        List<GameObject> targets = GameObject.FindGameObjectsWithTag("movementTarget").ToList();
        positions = targets.Select(x => new Vector2(x.transform.position.x, x.transform.position.y)).ToList();
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

    public Vector2 getRandomPosition()
    {
        return positions[UnityEngine.Random.Range(0, positions.Count())];
    }
}
