using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculosDerrumbe : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float DistanciaDeRuptura = 20f;
    public float PosicionFinal;

    private void Start()
    {
        PosicionFinal = transform.position.y;
    }
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < PosicionFinal - DistanciaDeRuptura)
        {
            Destroy(gameObject);
        }
    }
}
