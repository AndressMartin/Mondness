using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour
{
    public float velocity = 4f;
    public enum Directions { up, right, forward}
    public Directions dir = Directions.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateAround();
    }

    void RotateAround()
    {
        if (dir == Directions.up)transform.Rotate(Vector3.up, velocity * Time.fixedDeltaTime);
        else if (dir == Directions.forward) transform.Rotate(Vector3.forward, velocity * Time.fixedDeltaTime);
        else if (dir == Directions.right) transform.Rotate(Vector3.right, velocity * Time.fixedDeltaTime);
    }
}
