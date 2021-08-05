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


    void Start()
    {
        if (loadOnStart)
        {
            Load();
        }
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (InputExt.GetKeyDown(KeyCode.P))
        {
            customData.levels = new List<Level>()
            {
                    new Level ( true, false, 0 ),
                    new Level ( false, false, 0 ),
                    new Level ( false, false, 0 ),
                    new Level ( false, false, 0 )
            };
            Save();
        }
#endif
    }
    public void SetLevels(int index, bool unlocked, bool completed, int score)
    {
        //Ignore the first two scenes ingame
        index -= 2;
        //Change score only if score is higher than whats saved.
        if (score > customData.levels[index].score) customData.levels[index] = new Level(unlocked, completed, score);
        else customData.levels[index] = new Level(unlocked, completed, customData.levels[index].score);
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
    }
}
