using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class PlayerSpawnPoint : MonoBehaviour
{
    public SceneTransitionManager sceneTransitionManager;

    public void Start()
    {
        sceneTransitionManager = SceneTransitionManager.instance;
        string currentScene = SceneManager.GetActiveScene().name;
        GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
        BoxCollider2D spawnPointCollider = GetComponent<BoxCollider2D>();
        spawnPointCollider.enabled = false;

        foreach (var player in players)
        {
            string destSpawnName = sceneTransitionManager.FindDestinationSpawn(currentScene);
            if (destSpawnName != null && destSpawnName == gameObject.name)
            {
                player.transform.position = transform.position;
            }
            
        }
        spawnPointCollider.enabled = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sceneTransitionManager.PlayerSpawnPointTriggered(gameObject.name, collision.gameObject);
        }
    }

}
