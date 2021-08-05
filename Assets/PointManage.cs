using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManage : SingletonInstance<PointManage>
{

    [System.Serializable]
    public struct Level
    {
        public bool unlocked;
        public bool completed;
        public int score;

        public Level(bool unlocked, bool completed, int score)
        {
            this.unlocked = unlocked;
            this.completed = completed;
            this.score = score;
        }
    }

    [System.Serializable]
    public class CustomData
    {
        public List<Level> levels;
        public CustomData()
        {

            // Dummy data
            levels = new List<Level>() 
            {
                    new Level ( true, false, 0 ),
                    new Level ( false, false, 0 ),
                    new Level ( false, false, 0 ),
                    new Level ( false, false, 0 )
            };
        }

    }

    public CustomData customData;
    public bool loadOnStart = true;
    public string identifier = "playerData";


    // Start is called before the first frame update
    void Start()
    {
        if (loadOnStart)
        {
            Load();
        }
    }

    //public void SetScore(string score)
    //{
    //    customData.score = int.Parse(score);
    //}
    public void SetLevels(int index, bool unlocked, bool completed, int score)
    {
        //Change score only if score is higher than whats saved.
        if (score > customData.levels[index - 1].score) customData.levels[index-1] = new Level(unlocked, completed, score);
        else customData.levels[index - 1] = new Level(unlocked, completed, customData.levels[index - 1].score);
    }

    public void Save()
    {
        SaveGame.Save<CustomData>(identifier, customData/*, SerializerDropdown.Singleton.ActiveSerializer*/);
    }

    public void Load()
    {
        customData = SaveGame.Load<CustomData>(
            identifier,
            new CustomData()
            /*,SerializerDropdown.Singleton.ActiveSerializer*/);
        //scoreInputField.text = customData.score.ToString();
        //highScoreInputField.text = customData.highScore.ToString();
    }
}
