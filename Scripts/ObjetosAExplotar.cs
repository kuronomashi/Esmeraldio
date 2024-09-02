using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosAExplotar : MonoBehaviour
{
    public GameObject ParticulasExplocion;
    public int Delay;
    public void ExplotarObjeto()
    {
        FindObjectOfType<Explocion>().ObjetivoOprimido();
        gameObject.SetActive(false);
        ParticulasExplocion.SetActive(true);
        Destroy(transform.parent.gameObject,Delay);
    }
}
