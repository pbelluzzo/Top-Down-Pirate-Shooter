using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemySpawnAreas;
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private GameObject playerShip;
    private GameObject player;

    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] private int score;
    
    private void Start()
    {
        SpawnPlayerShip();
        StartCoroutine(EnemySpawn());
        StartCoroutine(SessionTimeControl());
    }

    private void SpawnPlayerShip()
    {
        player = Instantiate(playerShip);
        ShipController shipController = player.GetComponent<ShipController>();
        shipController.BeingDestroyed += ShipController_OnBeingDestroyed;
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            GameObject randomEnemy = enemies[Random.Range(0, (enemies.Length))];
            Transform randomSpawnArea = enemySpawnAreas[Random.Range(0, (enemySpawnAreas.Length - 1))].transform;
            GameObject enemy = Instantiate(randomEnemy, randomSpawnArea.position, randomSpawnArea.rotation);
            enemy.AddComponent<EnemyInitializer>();
            ShipController shipController = enemy.GetComponent<ShipController>();
            shipController.BeingDestroyed += ShipController_OnBeingDestroyed;
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("EnemyRespawnTime"));
        }
    }

    private IEnumerator SessionTimeControl()
    {
        float sessionTime = 60 * PlayerPrefs.GetFloat("GameSessionTime");
        yield return new WaitForSeconds(sessionTime);
        GameOver();
    }

    private void ShipController_OnBeingDestroyed(ShipController controller, bool isPlayer)
    {
        controller.BeingDestroyed -= ShipController_OnBeingDestroyed;
        if (!isPlayer)
        {
            score++;
            return;
        }
        GameOver();
    }
    
    private void GameOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) Destroy(enemy);
        StopCoroutine(SessionTimeControl());
        StopCoroutine(EnemySpawn());
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<GameOverMenu>().SetScore(score);
    }
}
