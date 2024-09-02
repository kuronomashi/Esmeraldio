using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InformacionObjetos", menuName = "ObjetosScriptables/InformacionObjetos", order = 1)]
public class InformacionDeLosObjetos : ScriptableObject
{
    public string Nombre;
    public float PorcentajeMultiplicadorDePuntos;
    public int ValorDelProducto;
}
