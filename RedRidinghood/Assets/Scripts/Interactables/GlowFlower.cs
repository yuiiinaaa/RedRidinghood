using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowFlower : MonoBehaviour
{
    private Transform visual;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        visual = GetComponent<Transform>();
        startPosition = visual.position; // Store the initial position
        
        StartCoroutine(PickupFloatingAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PickupFloatingAnimation()
    {
        while(true)
        {
            visual.Rotate(Vector3.up, 40 * Time.deltaTime, Space.World);
            visual.position = startPosition + new Vector3(0, Mathf.Sin(Time.time) * 0.2f, 0); // Add the sinusoidal movement based on the initial position
            yield return null;
        }
    }
}
