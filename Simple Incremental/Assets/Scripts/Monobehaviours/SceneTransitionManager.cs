using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Script is used for changing scenes when the PlayerSpawnPoint is triggured
 * The spawnPointMap contains the mapping of scens to spawn points
 */

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    public List<SpawnPointMap> spawnPointMap;
    public string previousScene = null;
    public string previousSpawnPoint = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public String FindDestinationSpawn(string nextScene)
    {
        foreach (var spawnpoint in spawnPointMap)
        {
            //spawnpoint.currentSceneName == previousScene && 
            if (spawnpoint.sceneTwoName == nextScene && 
                spawnpoint.spawnPointOneName == previousSpawnPoint)
            {
                return spawnpoint.spawnPointTwoName;
            } else if (spawnpoint.sceneOneName == nextScene &&
                spawnpoint.spawnPointTwoName == previousSpawnPoint)
            {
                return spawnpoint.spawnPointOneName;
            }
        }
        return null;
    }

    public void PlayerSpawnPointTriggered(string triggeredSpawnPoint, GameObject playerObj)
    {
        foreach (var obj in spawnPointMap)
        {
            //Change the scene
            if (obj.spawnPointOneName == triggeredSpawnPoint)
            {
                previousSpawnPoint = triggeredSpawnPoint;
                previousScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(obj.sceneTwoName);
            } else if (obj.spawnPointTwoName == triggeredSpawnPoint)
            {
                previousSpawnPoint = triggeredSpawnPoint;
                previousScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(obj.sceneOneName);
            }
        }
    }
}

[Serializable]
public class SpawnPointMap
{
    public string spawnPointOneName = "Spawn1";
    public string sceneOneName = "Level0";
    public string sceneTwoName = "Level1";
    public string spawnPointTwoName = "Spawn2";
    bool loadNewScene = true;
}

