using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public GameObject stagesParent;
    public GameObject starCounter;
    public int totalStars;
    public int availableLevels;
    // Start is called before the first frame update
    void Start()
    {
        ShowStars();
    }

    private void ShowStars()
    {
        for (int i = 0; i < stagesParent.transform.childCount; i++)
        {
            if (stagesParent.transform.GetChild(i).gameObject.activeInHierarchy == true)
            {
                availableLevels++;
            }
        }
        for (int i = 0; i < availableLevels; i++)
        {
            var starsToFill = PointManage.GetInstance().customData.levels[i].score;
            var level = stagesParent.transform.GetChild(i);
            var levelStars = level.transform.GetChild(1);
            var counter = 0;
            foreach (Transform star in levelStars)
            {
                if (starsToFill > counter && level.gameObject.activeInHierarchy)
                {
                    counter++;
                    star.GetChild(0).gameObject.SetActive(true);
                    star.GetChild(1).gameObject.SetActive(false);
                }
            }
            totalStars += starsToFill;
        }
        starCounter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = totalStars.ToString();
    }
}
