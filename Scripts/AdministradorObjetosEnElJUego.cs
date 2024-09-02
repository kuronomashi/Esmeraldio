using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdministradorObjetosEnElJUego : MonoBehaviour
{
    public InformacionDeLosObjetos Info;
    public bool FueComprado = false;
    public int nivelDelObjeto;
    float precioTotal;
    public float MultiplicadorFinal;
    [Header("ObjetosTextos")]
    public GameObject PrecioT, NombreT, PorcentajeT;
    [Header("InformacionDelObjeto")]
    [SerializeField] private TextMeshProUGUI PrecioText, NombreText,PorcentajeTexto;
    public GameObject ImagenDeBloqueo,BotonComprar;
    public LogicaPrincipal LogicaP;
    public ControladorInterfazInicial CI;

    public Animator animator;
    private void Start()
    {
        ActualizacionDeNombreYCosto();
        LogicaP = GameObject.Find("LogicaDelJuego").GetComponent<LogicaPrincipal>();
    }

    private void OnEnable()
    {
        if (FueComprado)
        {
            animator.SetBool("Ejecutar", true);
        }
    }

    public void ActualizacionDeNombreYCosto()
    {
        precioTotal = Info.ValorDelProducto;
        precioTotal += Info.ValorDelProducto * nivelDelObjeto;

        MultiplicadorFinal = Info.PorcentajeMultiplicadorDePuntos;
        MultiplicadorFinal += Info.PorcentajeMultiplicadorDePuntos * nivelDelObjeto;

        PrecioText.text = precioTotal.ToString();
        PorcentajeTexto.text = MultiplicadorFinal.ToString();
        NombreText.text = Info.Nombre;
    }

    public void ActualizarTextos()
    {
        PrecioT.SetActive(false);
        NombreT.SetActive(false);
        BotonComprar.SetActive(false);
        ImagenDeBloqueo.SetActive(false);
    }

    public void ComprarObjeto()
    {
        if (GuardarDatosLocalMente.Instancia.DatosAtuales.NumeroLikes >= precioTotal)
        {
            GuardarDatosLocalMente.Instancia.DatosAtuales.NumeroLikes -= (int)precioTotal;
            animator.SetBool("Ejecutar", true);
            CI.ActualizarInfromacionVisual();
            FueComprado = true;
            LogicaP.SeGeneroUnCambioEnLosMultiplicadores();
            ActualizarTextos();
        }
    }
}
