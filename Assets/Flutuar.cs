using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flutuar : MonoBehaviour
{
    Vector3 velF;
    private void Start()
    {
        velF = new Vector3(transform.position.x, UnityEngine.Random.Range(-10f, 10f), transform.position.z);

    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(velF * Time.deltaTime);
    }
}
