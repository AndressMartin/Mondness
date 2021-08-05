using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static UnityEvent nextScene;

    [Header("Initialization")]
    public MusicManage musicManager;
    public PointManage pointManager;

    static bool instantiateMusic = false;
    public static bool paused = false;
    static Scene previousScene;
    private void Awake()
    {
        if (nextScene == null)
            nextScene = new UnityEvent();
        nextScene.AddListener(LoadNextScene);
    }

    private void Start()
    {
        previousScene = SceneManager.GetActiveScene();
        //If game starting or level chosen: 
        //if (FindObjectOfType<MusicManage>() == null) 
    }

    private void Update()
    {
        if (instantiateMusic) InstantiateMusic();
    }
    public void LoadNextScene()
    {
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        //if (nextScene > SceneManager.sceneCount)
        //{
        //    nextScene = 0;    
        //}
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
        // Add the delegate to be called when the scene is loaded, between Awake and Start.
        SceneManager.sceneLoaded += SceneLoaded;
    }
    static void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        paused = false;
        PointManage.GetInstance().resetTempScore();
        Debug.Log(System.String.Format("Scene{0} has been loaded ({1})", scene.name, loadSceneMode.ToString()));
        //If was on mainMenu or SelectMenu and isn't anymore
        if (previousScene.buildIndex <= 1 && scene.buildIndex > 1) instantiateMusic = true;
    }
    void InstantiateMusic()
    {
        Instantiate(musicManager);
        instantiateMusic = false;
    }

    public static void TogglePause()
    {
        paused = !paused;
    }
}
