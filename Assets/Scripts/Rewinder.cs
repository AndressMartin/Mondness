using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Rewinder : MonoBehaviour
{
    public bool isRewinding;
    public List<Vector3> positions;
    public Vector3 firstPosition;

    void Start()
    {
        firstPosition = transform.position;
        positions = new List<Vector3>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartRewind();
        if (isRewinding)
        {
            StartCoroutine(EsperaRewind());
        }
    }

    private void FixedUpdate()
    {
        
    }

    

    void Rewind()
    {
        Debug.Log("Voltando!");
        positions.Clear();
        transform.position = firstPosition;
        if (Vector2.Distance(transform.position, firstPosition) == 0)
        {
            StopRewind();
        }
    }

    //void Record()
    //{
    //    if (positions.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
    //    {
    //        //Debug.Log("Expirou!");
    //        tempoExpirado = true;
    //        positions.RemoveAt(positions.Count - 1);
    //    }
    //    positions.Insert(0, transform.position);
    //}

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

    //void VoltarAutomatico()
    //{
        
    //}

    IEnumerator EsperaRewind()
    {
        yield return new WaitForSeconds(1f);
        Rewind();
    }
}
