using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public GameObject[] nodeTargets;
    public GameObject[] nodes;

    public GameObject nodePF;


    // Start is called before the first frame update
    void Start()
    {
        nodeTargets = GameObject.FindGameObjectsWithTag("movementTarget");
        StartCoroutine(createNode());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator createNode()
    {
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(3f);
            Instantiate(nodePF, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
