using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Collections.Generic;
using static Globals;

//Acts as a singleton 

public class PersistentData : MonoBehaviour
{

    public static PersistentData data;

    #region Data That Persists

    //PlayerData
    public int level;
    public int experience;
    public int health;
    public int attackPower;
    public int defensePower;
    public int speed;
    public int acceleration;
    public int jumpPower;

    //BattleStart
    public List<Tuple<Enemy, Vector3Int>> battleEnemies; //enemies and their position
    public BattleArena battleArena;

    //BattleEnd
    public int battleExperienceAwarded;
    public bool battleWin;
    #endregion

    void Awake()
    {
        if(data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this;
        }
        else if(data != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        WritePlayerData();
    }

    public void Load()
    {
        ReadPlayerData();
    }

    #region Private Methods
    private void WritePlayerData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + PlayerFileName, FileMode.Open);
        PlayerData playerData = new PlayerData
        {
            level = this.level,
            experience = this.experience,
            health = this.health,
            attackPower = this.attackPower,
            defensePower = this.defensePower,
            speed = this.speed,
            acceleration = this.acceleration,
            jumpPower = this.jumpPower
        };
        bf.Serialize(file, playerData);
        file.Close();
    }

    private void ReadPlayerData()
    {
        //Read Player
        if (File.Exists(Application.persistentDataPath + PlayerFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + PlayerFileName);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            this.level = data.level;
            this.experience = data.experience;
            this.health = data.health;
            this.attackPower = data.attackPower;
            this.defensePower = data.defensePower;
            this.speed = data.speed;
            this.acceleration = data.acceleration;
            this.jumpPower = data.jumpPower;
        }
    }
    #endregion
}

#region Persistes Between Save/Load

[Serializable]
class PlayerData
{
    public int level;
    public int experience;

    public int health;
    public int attackPower;
    public int defensePower;

    public int speed;
    public int acceleration;
    public int jumpPower;
}

#endregion

#region Persists Between Scenes But Not Save/Load

class StartBattleInfo
{
    public List<Tuple<Enemy, Vector3Int>> battleEnemies; //enemies and their position
    public BattleArena battleArena;
}

class EndBattleInfo
{
    public int battleExperienceAwarded;
    public bool battleWin;
}

#endregion