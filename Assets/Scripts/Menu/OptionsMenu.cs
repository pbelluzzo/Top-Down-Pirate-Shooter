using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider gameSessionTimeSlider;
    [SerializeField] private Slider enemyRespawnTimeSlider;

    public void Awake()
    {
        LoadPlayerPrefsOnSlider();
    }
    public void SavePlayerPrefs()
    {
        float gameSessionTime = gameSessionTimeSlider.value;
        float enemyRespawnTime = enemyRespawnTimeSlider.value;
        PlayerPrefs.SetFloat("GameSessionTime", gameSessionTime);
        PlayerPrefs.SetFloat("EnemyRespawnTime", enemyRespawnTime);
    }
    public void LoadPlayerPrefsOnSlider()
    {
        gameSessionTimeSlider.value = PlayerPrefs.GetFloat("GameSessionTime", 1f);
        enemyRespawnTimeSlider.value = PlayerPrefs.GetFloat("EnemyRespawnTime", 1f);
    }
}
