using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCync : MonoBehaviour
{
    public static int posID = Shader.PropertyToID("_Position");
    public static int sizeID = Shader.PropertyToID("_Size");

    public Material wallMat;
    public Camera camera;
    public LayerMask mask;

    void Update()
    {
        var dir = camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        if (Physics.Raycast(ray, 3000, mask)) wallMat.SetFloat(sizeID, 1);
        else wallMat.SetFloat(sizeID, 0);

        var view = camera.WorldToViewportPoint(transform.position);
        wallMat.SetVector(posID, view);

    }
}
