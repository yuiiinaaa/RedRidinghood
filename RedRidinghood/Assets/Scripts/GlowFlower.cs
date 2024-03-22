using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowFlower : MonoBehaviour
{
    private Transform visual;
    // Start is called before the first frame update
    void Start()
    {
        visual = transform;
        StartCoroutine(PickupFloatingAnimation());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PickupFloatingAnimation(){
        while(true){
            visual.Rotate(Vector3.up, 40 * Time.deltaTime, Space.World);
            visual.position = new Vector3(0, Mathf.Sin(Time.time) * 0.2f, 0);
            yield return null;
        }

    }
}
