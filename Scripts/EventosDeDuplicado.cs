using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventosDeDuplicado : MonoBehaviour
{
    public int multiplicadorDelEvento;
    public float TiempoDeDuracionDelEvento;
    public GameObject MinijuegoExplociones, MinijuegoDerrumbes;

    [Header("BotonMinijuegos")]
    public GameObject BotonDeAumento,NumeroDeMultiplicador2;
    public TextMeshProUGUI NumeroDeMultiplicador1, TiempoTranscurridoDelBufoTexto;

    [Header("Varaibles Internas")]
    public float ValorFinalMultiplicado;
    bool EstaEnBufo;
    float TiempoTranscurrido;
    [Header("Tiempo")]
    public int TiempoMinimo, TiempoMaximo;

    public ManejoAbrirYCerrarMenus Menus;

    [Header("Vinculacion Logica principal")]
    public LogicaPrincipal Logia;


    private void Start()
    {
        ComenzarAContarParSigueitneMinijuego();
    }
    public void Update()
    {
        if (EstaEnBufo)
        {
            TiempoTranscurrido -= Time.deltaTime;
            TiempoTranscurridoDelBufoTexto.text = TiempoTranscurrido.ToString("F2");
        }
    }
    public void ComenzarAContarParSigueitneMinijuego()
    {
        multiplicadorDelEvento = Random.Range(2, 4);
        int Time = Random.Range(TiempoMinimo,TiempoMaximo);
        Invoke("MostrarBotonDePortenciar", Time);
    }

    public void MostrarBotonDePortenciar()
    {
        BotonDeAumento.SetActive(true);
        NumeroDeMultiplicador1.text = multiplicadorDelEvento + "X";
    }

    public void ComenzarMinijuego()
    {
        BotonDeAumento.SetActive(false);
        if (Random.Range(0,2) != 0)
        {
            MinijuegoExplociones.SetActive(true);
            Menus.AparecerObjetoSeleccionMEnu(4);
            MinijuegoExplociones.GetComponent<Explocion>().CantidadRecompensa = multiplicadorDelEvento;
            MinijuegoExplociones.GetComponent<Explocion>().ComenzarJuego();
        }
        else
        {
            MinijuegoDerrumbes.SetActive(true);
            Menus.AparecerObjetoSeleccionMEnu(5);
            MinijuegoDerrumbes.GetComponent<DerrumbesMiniJuego>().CantidadRecompensa = multiplicadorDelEvento;
            MinijuegoDerrumbes.GetComponent<DerrumbesMiniJuego>().ComenzarJuego();
        }
    }

    public void GanoElminijuego()
    {
        TiempoTranscurrido = TiempoDeDuracionDelEvento;
        EstaEnBufo = true;
        NumeroDeMultiplicador2.SetActive(true);
        Menus.AparecerObjetosConFaade(0);
        NumeroDeMultiplicador2.GetComponent<TextMeshProUGUI>().text = multiplicadorDelEvento + "X"; 
        ValorFinalMultiplicado = Logia.multiplicadorputnosConstantesTotalActual * multiplicadorDelEvento;
        Logia.multiplicadorputnosConstantesTotalActual += ValorFinalMultiplicado;
        Logia.multiplicadorDePuntoTotalActual += ValorFinalMultiplicado;

        Logia.ActualizarValoresMultiplicadores();
        Invoke("TiempoDelBufo", TiempoDeDuracionDelEvento);
    }

    public void TiempoDelBufo()
    {
        EstaEnBufo = false;
        NumeroDeMultiplicador2.SetActive(false);
        Logia.multiplicadorputnosConstantesTotalActual -= ValorFinalMultiplicado;
        Logia.multiplicadorDePuntoTotalActual -= ValorFinalMultiplicado;
        Logia.ActualizarValoresMultiplicadores();
        ComenzarAContarParSigueitneMinijuego();
    }

    public void PerdioElminijuego()
    {
        Menus.AparecerObjetosConFaade(0);
        ComenzarAContarParSigueitneMinijuego();
    }
}
