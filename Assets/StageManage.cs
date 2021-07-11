using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

public class StageManage : MonoBehaviour
{
    
    [SerializeField]public List<GameObject> stages = new List<GameObject>();
    [SerializeField] public Dictionary<GameObject, BoxCollider> startPoints = new Dictionary<GameObject, BoxCollider>();
    public static UnityEvent portalTeleport;
    public int i = 0;
    public int f = 0;
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (portalTeleport == null)
            portalTeleport = new UnityEvent();
        portalTeleport.AddListener(NextStage);
        if (!stages.Any())FindToList();
        StartCoroutine(FindStageStartPoints());
    }

    private IEnumerator FindStageStartPoints()
    {
        yield return new WaitForSeconds(1);
        while(startPoints.Count < 3)
        {
            var start = Utils.FindComponentInChildWithTag<BoxCollider>(stages[f], "Start");
            startPoints.Add(stages[f], start);
            f++;
        }
    }

    private void FindToList()
    {
        Debug.LogWarning("Finding");
        var objects = GameObject.FindGameObjectsWithTag("Stage");
        stages = objects.ToList();
    }

    private void NextStage()
    {
        i++;
        Debug.Log("Next Stage");
        foreach(var thing in startPoints)
        {
            Debug.Log(thing.Key + " " + thing.Value.transform.position);
        }
        for (int k = 0; k < stages.Count; k++)
        {
            if (k != i) stages[k].SetActive(false);
            else stages[k].SetActive(true);
        }
        if (startPoints[stages[i]])
        {
            player.GetComponent<PlayerMove>().Teleport(startPoints[stages[i]].transform.position);
        }
    }
}
