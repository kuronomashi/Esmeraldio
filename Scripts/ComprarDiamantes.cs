using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComprarDiamantes : MonoBehaviour
{
    public backend BK;
    public int CantidadDeDiamantesNeCesariar;
    public int CantidadDeTokens;
    public ControladorInterfazCompra CompraInt;

    [Header("TextosDeLosProductos")]
    public TextMeshProUGUI TextoCantidadDiamantes, CantidadTokens;

    public void Start()
    {
        TextoCantidadDiamantes.text = CantidadDeDiamantesNeCesariar.ToString();
        CantidadTokens.text ="x" + CantidadDeTokens.ToString();
    }

    public void ComprarGemas()
    {
        if (GuardarDatosLocalMente.Instancia.DatosAtuales.NumeroCorazones >= CantidadDeDiamantesNeCesariar)
        {
            BK.MandarTokens(CantidadDeTokens);
            GuardarDatosLocalMente.Instancia.DatosAtuales.NumeroCorazones -= CantidadDeDiamantesNeCesariar;
            CompraInt.ActualizarCantidadesContadores();
        }
        else
        {
            print("Fondos Insuficientes");
        }
    }
}
