using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloat : MonoBehaviour
{
    Vector3 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, z));
    static float z = 10.49f;
    private float _timeStartedLerping;
    public float timeTakenDuringLerp = .2f;
    Vector3 destinationPos;

    // Transforms to act as start and end markers for the journey.

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
       Transform transform = gameObject.GetComponent<Transform>();
       transform.position = randomPositionOnScreen;
       destinationPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, z));
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, destinationPos);
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(transform.position, destinationPos, fractionOfJourney);

    }
}
