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
    [SerializeField] public Dictionary<GameObject, BoxCollider> estrelas = new Dictionary<GameObject, BoxCollider>();
    public static UnityEvent portalTeleport;
    public static UnityEvent puzzlesLoaded;
    public int i = 0;
    public int f = 0;
    public GameObject player;
    private PlayerMove playerMove;

    private void Awake()
    {
        if (portalTeleport == null)
            portalTeleport = new UnityEvent();
        if (puzzlesLoaded == null)
            puzzlesLoaded = new UnityEvent();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        portalTeleport.AddListener(NextStage);
        if (!stages.Any())FindToList();
        StartCoroutine(FindStageStartPoints());
        playerMove = player.GetComponent<PlayerMove>();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        StageHackTool();
        # endif
    }

    private void StageHackTool()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerMove.Teleport(startPoints[stages[0]].transform.position);
            i = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerMove.Teleport(startPoints[stages[1]].transform.position);
            i = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerMove.Teleport(startPoints[stages[2]].transform.position);
            i = 2;
        }
    }

    private IEnumerator FindStageStartPoints()
    {
        yield return new WaitForSeconds(1);
        while (startPoints.Count < stages.Count)
        {
            var start = Utils.FindComponentInChildWithTag<BoxCollider>(stages[f], "Start");
            startPoints.Add(stages[f], start);
            f++;
        }
        puzzlesLoaded.Invoke();
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
            playerMove.Teleport(startPoints[stages[i]].transform.position);
        }
    }
}
