using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneradorDeNumeroAleatorios : MonoBehaviour
{
    public GameObject numberPrefab;
    public RectTransform spawnArea;
    public int minValue = 1;
    public int maxValue = 100;
    public float spawnInterval = 0.5f;
    public float particleLifetime = 2f;

    public void GenerarNumeroDeClick()
    {
        int NumeroAleatorio = Random.Range(1, 3);
        GameObject numberObj = Instantiate(numberPrefab, spawnArea);
        TextMeshProUGUI numberText = numberObj.GetComponent<TextMeshProUGUI>();
        numberText.text = "+" + NumeroAleatorio.ToString();

        numberObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            Random.Range(-spawnArea.rect.width / 2, spawnArea.rect.width / 2),
            spawnArea.rect.height / 2
        );

        StartCoroutine(AnimateAndDestroy(numberObj));
    }


    IEnumerator AnimateAndDestroy(GameObject numberObj)
    {
        RectTransform rectTransform = numberObj.GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + new Vector2(0, 100);

        float elapsedTime = 0f;

        while (elapsedTime < particleLifetime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / particleLifetime;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, progress);
            yield return null;
        }

        Destroy(numberObj);
    }
}

