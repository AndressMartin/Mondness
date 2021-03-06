using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManage : MonoBehaviour
{
    //TODO: Implement a system to recognize which scenes are unlocked
    // Start is called before the first frame update
    [SerializeField]List<int> tempList = new List<int>();
    Dictionary<Button, int> levelsAndIndexes = new Dictionary<Button, int>();
    void Start()
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy == true) ConfigureLevelButton(i);
        }
    }

    private void ConfigureLevelButton(int i)
    {
        int x = i; //Store in a temporary value to pass to the onClick event
        var button = transform.GetChild(i).GetComponent<Button>();
        button.onClick.AddListener(() => { LoadCorrectLevel(x); });
        levelsAndIndexes.Add(button, i);
        tempList.Add(i);
        button.interactable = PointManage.GetInstance().customData.levels[i].unlocked;
    }

    public void LoadCorrectLevel(int index)
    {
        SceneManager.LoadScene(index+2); //Jump two indexes because of the two menus in the beginning
    }
}
