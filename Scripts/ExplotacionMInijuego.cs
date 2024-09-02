using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Explocion : MonoBehaviour
{

    public EventosDeDuplicado EventoControlador;
    public int TiempoDelMinijuego;
    public int CantidadRecompensa;

    [Header("Variables Internas")]
    public Transform PadreDeLosCubitos;
    public GameObject targetPrefab;  
    public RectTransform spawnArea;  
    public TextMeshProUGUI scoreText;           
    public TextMeshProUGUI timerText;           
    public float spawnInterval = 1.0f;       
    public int scoreToWin = 10;         
    private int currentScore = 0;       
    private float currentTime;

    public GameObject MinijuegoSi;

    public void ComenzarJuego()
    {
        currentTime = TiempoDelMinijuego;
        currentScore = 0;
        UpdateScoreText();
        UpdateTimerText();
        StartCoroutine(SpawnTargets());
    }
    void Update()
    {
        // Reducir el tiempo restante
        currentTime -= Time.deltaTime;
        UpdateTimerText();

        // Verificar si se ha acabado el tiempo
        if (currentTime <= 0)
        {
            EndGame();
        }
    }

    IEnumerator SpawnTargets()
    {
        while (currentTime > 0)
        {
            GenerarObjetivos();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void GenerarObjetivos()
    {
        
        float x = Random.Range(spawnArea.rect.xMin, spawnArea.rect.xMax);
        float y = Random.Range(spawnArea.rect.yMin, spawnArea.rect.yMax);
        Vector3 spawnPosition = new Vector3(x, y, 0) + spawnArea.position;
       GameObject Aux = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        Aux.transform.SetParent(PadreDeLosCubitos);
    }

    public void ObjetivoOprimido()
    {
        currentScore++;
        UpdateScoreText();
        if (currentScore >= scoreToWin)
        {
            EndGame(true);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = currentScore +"/" + scoreToWin;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame(bool win = false)
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
