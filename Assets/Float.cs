using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    public bool animPos = true;
    public float posAmplitude;
    public float posSpeed;

    private Vector3 origPos;

    public float startAnimOffset = 0;


    /**
     * Awake
     */
    void Awake()
    {
        origPos = transform.position;
        startAnimOffset = Random.Range(0f, 540f);        // so that the xyz anims are already offset from each other since the start
    }

    /**
     * Update
     */
    void Update()
    {
        /* position */
        if (animPos)
        {
            Vector3 pos;
            pos.x = origPos.x + posAmplitude * Mathf.Sin(posSpeed * Time.time + startAnimOffset);
            pos.y = origPos.y + posAmplitude * Mathf.Sin(posSpeed * Time.time + startAnimOffset);
            pos.z = origPos.z + posAmplitude * Mathf.Sin(posSpeed * Time.time + startAnimOffset);
            transform.position = pos;
            posSpeed = Random.Range(1, 5);
            posAmplitude = Random.Range(1, 5);
        }

    }
}
