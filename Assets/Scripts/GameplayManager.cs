using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemySpawnAreas;
    [SerializeField] private GameObject[] enemies;
    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            GameObject randomEnemy = enemies[Random.Range(0, (enemies.Length - 1))];
            Transform randomSpawnArea = enemySpawnAreas[Random.Range(0, (enemySpawnAreas.Length - 1))].transform;
            GameObject enemy = Instantiate(randomEnemy, randomSpawnArea.position, randomSpawnArea.rotation);
            enemy.AddComponent<EnemyInitializer>();
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("EnemyRespawnTime"));
        }
    }

}
