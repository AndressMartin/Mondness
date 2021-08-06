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
                    new Level ( false, false, 0 ),
                    new Level ( false, false, 0 )
            };
        }
        
    }

    [System.Serializable]
    public class SysConfig
    {
        public bool musicMuted;
        public bool sfxMuted;
    }
    public CustomData customData;
    public SysConfig sysConfig;
    public bool loadOnStart = true;
    public string identifier = "playerData";
    public string sysIdentifier = "systemPrefs";
    public bool loaded = false;
    public int temporaryScore = 0;

    void Start()
    {
        if (loadOnStart)
        {
            LoadCustomData();
            LoadSys();
            loaded = true;
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
                    new Level ( false, false, 0 ),
                    new Level (false, false, 0)
            };
            SaveCustomData();
        }
#endif
    }
    public void SetLevels(int index, bool unlocked, bool completed, int score)
    {
        //Get true score
        var trueScore = score + temporaryScore;
        //Ignore the first two scenes ingame
        index -= 2;
        //Change score only if score is higher than whats saved.
        if (trueScore > customData.levels[index].score) customData.levels[index] = new Level(unlocked, completed, trueScore);
        else customData.levels[index] = new Level(unlocked, completed, customData.levels[index].score);
        resetTempScore();
    }
    public void SetTempScore(int score)
    {
        temporaryScore += score;
    }

    public void resetTempScore()
    {
        temporaryScore = 0;
    }

    public void SaveCustomData()
    {
        SaveGame.Save<CustomData>(identifier, customData/*, SerializerDropdown.Singleton.ActiveSerializer*/);
    }
    public void SaveSys()
    {
        SaveGame.Save<SysConfig>(sysIdentifier, sysConfig/*, SerializerDropdown.Singleton.ActiveSerializer*/);
    }
    public void LoadCustomData()
    {
        customData = SaveGame.Load<CustomData>(
            identifier,
            new CustomData()
            /*,SerializerDropdown.Singleton.ActiveSerializer*/);
    }
    public void LoadSys()
    {
        sysConfig = SaveGame.Load<SysConfig>(
            sysIdentifier,
            new SysConfig()
            /*,SerializerDropdown.Singleton.ActiveSerializer*/);
    }
}
