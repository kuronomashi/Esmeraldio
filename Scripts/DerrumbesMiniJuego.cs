using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DerrumbesMiniJuego : MonoBehaviour
{
    public EventosDeDuplicado EventoControlador;
    public int TiempoDelMinijuego;
    public int CantidadRecompensa;

    public Transform PadreObstaculos;

    public GameObject player;
    public GameObject obstaclePrefab;
    public RectTransform spawnPoint;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI timerText;
    public float spawnInterval = 1.0f;
    public int playerLives = 3;

    private float currentTime;
    private int currentLives;

    public GameObject MinijuegoSi;
    public void ComenzarJuego()
    {
        currentTime = TiempoDelMinijuego;
        currentLives = playerLives;
        UpdateLifeText();
        UpdateTimerText();
        StartCoroutine(SpawnObstacles());
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        UpdateTimerText();

        if (currentTime <= 0)
        {
            EndGame(true);
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (currentTime > 0 && currentLives > 0)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObstacle()
    {
        float x = Random.Range(spawnPoint.rect.xMin, spawnPoint.rect.xMax);
        float y = Random.Range(spawnPoint.rect.yMin, spawnPoint.rect.yMax);
        Vector3 spawnPosition = new Vector3(x, y, 0) + spawnPoint.position;
        GameObject Aux = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        Aux.transform.SetParent(PadreObstaculos);
        Aux.GetComponent<ObstaculosDerrumbe>().fallSpeed = Random.Range(300,450);
    }

    public void PlayerHit()
    {
        currentLives--;
        UpdateLifeText();
        if (currentLives <= 0)
        {
            EndGame(false);
        }
    }

    void UpdateLifeText()
    {
        lifeText.text = currentLives.ToString();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame(bool win)
    {
        StopAllCoroutines();

        if (win)
        {
            EventoControlador.GanoElminijuego();
        }
        else
        {
            EventoControlador.PerdioElminijuego();
        }


        MinijuegoSi.SetActive(false);
    }
}
