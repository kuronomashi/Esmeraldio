using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugadorMinijuegoDerrumbe : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveInput;

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
        if (moveInput < 0) 
        {
            transform.localScale = new Vector3(0.35f, 0.35f, 0.35f); 
        }
        else if (moveInput > 0) 
        {
            transform.localScale = new Vector3(-0.35f, 0.35f, 0.35f); 
        }
    }

    public void MoverAlADerecha()
    {
        moveInput = 1f;
    }

    public void MoverALaIzquierda()
    {
        moveInput = -1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Roquita"))
        {
            FindObjectOfType<DerrumbesMiniJuego>().PlayerHit();
            Destroy(collision.gameObject);
        }
    }
}
