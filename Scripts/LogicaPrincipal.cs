using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogicaPrincipal : MonoBehaviour
{
    [Header("Recompensas")]
    public int CantidadME, CantidadminE;
    public int CantidadMO;
    public ControladorInterfazInicial ControladorPrincipal;

    [Header("Tierra")]
    public int TierraNecesaria;
    public float ProbabilidadDeEsmeraldas;

    [Header("Variables Internas")] 

    public float Tierra;
    public float PuntosBaseClick;
    public float PuntosBaseConstantes;

    public float multiplicadorputnosConstantesTotalActual;
    public float multiplicadorDePuntoTotalActual;
    public int MultiplicadoresComprados;

    [Header("Variables de Texto")]
    [SerializeField] private TextMeshProUGUI TextoTotalDineroAcumulado,
        TextoBaseClicks, TextoBAseConstantes, TextosMultiplicadorConstantes, TextoMultiplicadorCliker,TextoCantidadNEcesariaDeTierra;

    [Header("Multiplicadores Daño")]
    public Transform PadreMultiplicadoresContinuo, PadreMultiplicadoresclick;

    [Header("Variables Internas")]
    public bool SepuedenAgregarPuntosContinuos;
    public bool UboUnCambioEnLosMultiplicadores;
    void Start()
    {
        InicializacionProcesos();
        ActualizarValoresTexto();
    }

    void Update()
    {
        if (MultiplicadoresComprados > 0)
        {
            AgregarPuntosConstantemente();
        }
    }

    public void SeGeneroUnCambioEnLosMultiplicadores()
    {
        GenerarComprobacionDeMultiplicadoresdeClik();
    }

    public void AgregarPuntosConstantemente()
    {
        Tierra += CalcularPuntosConstantesFinales(PuntosBaseConstantes);
        ActualizarValoresTexto();
    }

    float CalcularPuntosConstantesFinales(float Puntos)
    {
        float Calculo = 0;
        Calculo = Puntos * multiplicadorputnosConstantesTotalActual;
        Calculo += Puntos;
        return Calculo;
    }

    float CalcularPuntosClickFinales(float Puntos)
    {
        float Calculo = 0;
        Calculo = Puntos * multiplicadorDePuntoTotalActual;
        Calculo += Puntos;
        return Calculo;
    }

    public void ClikDelJugador()
    {
        Tierra += CalcularPuntosClickFinales(PuntosBaseClick);
            ActualizarValoresTexto();
    }

    private void InicializacionProcesos()
    {
        UboUnCambioEnLosMultiplicadores = false;
        GenerarComprobacionDeMultiplicadoresdeClik();
        TextoCantidadNEcesariaDeTierra.text = TierraNecesaria.ToString();
    }

    public void GenerarComprobacionDeMultiplicadoresdeClik()
    {
        multiplicadorDePuntoTotalActual = 0;
        multiplicadorputnosConstantesTotalActual = 0;
        MultiplicadoresComprados = 0;
        if (PadreMultiplicadoresContinuo.childCount != 0)
        {
            for (int i = 0; i < PadreMultiplicadoresContinuo.childCount; i++)
            {
                if (PadreMultiplicadoresContinuo.GetChild(i).GetComponent<AdministradorObjetosEnElJUego>().FueComprado)
                {
                    multiplicadorputnosConstantesTotalActual += PadreMultiplicadoresContinuo.GetChild(i).GetComponent<AdministradorObjetosEnElJUego>().MultiplicadorFinal;
                    MultiplicadoresComprados++;
                }
            }
        }

        if (PadreMultiplicadoresclick.childCount != 0)
        {
            for (int i = 0; i < PadreMultiplicadoresclick.childCount; i++)
            {
                if (PadreMultiplicadoresContinuo.GetChild(i).GetComponent<AdministradorObjetosEnElJUego>().FueComprado)
                {
                    multiplicadorDePuntoTotalActual += PadreMultiplicadoresclick.GetChild(i).GetComponent<AdministradorObjetosEnElJUego>().MultiplicadorFinal;
                }
            }
        }
        ActualizarValoresMultiplicadores();
    }

    public void AgregarPuntosTotateles()
    {
        Tierra += CalcularPuntosClickFinales(PuntosBaseConstantes);
        ActualizarValoresTexto();
    }



    public void ActualizarValoresTexto()
    {
        TextoTotalDineroAcumulado.text = (int)Tierra + "";
    }

    public void ActualizarValoresMultiplicadores()
    {
        //Colocar Valores 
        TextosMultiplicadorConstantes.text = multiplicadorputnosConstantesTotalActual.ToString("F1") + "%";
        TextoMultiplicadorCliker.text = multiplicadorDePuntoTotalActual.ToString("F3") + "%";
        TextoBAseConstantes.text = PuntosBaseConstantes.ToString("F3");
        TextoBaseClicks.text = PuntosBaseClick.ToString("F3");
        TextoTotalDineroAcumulado.text = Tierra.ToString("F3");
    }

    public void RecalcularCantidadNecesariaTierrra()
    {
        TierraNecesaria = Mathf.CeilToInt(20 * Mathf.Pow(3.2f, MultiplicadoresComprados));
        TextoCantidadNEcesariaDeTierra.text = TierraNecesaria.ToString();
    }

    public void UsarTierraPacomprar()
    {
        if (Tierra >= TierraNecesaria)
        {
            Tierra -= TierraNecesaria;
            AbrirInterfazRecompenzas();
            ActualizarValoresTexto();
            RecalcularCantidadNecesariaTierrra();
        }

    }

    public void Salir()
    {
        PlayerPrefs.SetInt("SiguienteNivel", 2);
        Fade.Instacia.Aparecer(1, true);
    }



    public void AbrirInterfazRecompenzas()
    {
        float RecompensaOro = CalcucularRecompensasOro();
        float RecompensaEsmeraldas = CalcucularRecompensasEsmeralda();

        GuardarDatosLocalMente.Instancia.DatosAtuales.NumeroLikes += (int)RecompensaOro;
        GuardarDatosLocalMente.Instancia.DatosAtuales.NumeroCorazones += (int)RecompensaEsmeraldas;

        ControladorPrincipal.ActualizarInfromacionVisual();
    }


    //////////// ALEATORIO, PELIGRO ////////
    // Calculo Aleatorio COmplejo //

    private float CalcucularRecompensasOro()
    {
     
        float CantidadGanada = CantidadMO + (CantidadMO * MultiplicadoresComprados);
        return CantidadGanada;
    }

      private float CalcucularRecompensasEsmeralda()
    {
        float CantidadGanada = 0;
        if (Aleatorneitor())
        {
            CantidadGanada = CantidadDeRecompensa(CantidadminE , CantidadME); 
        }
        else
        {
            CantidadGanada = 0;
        }
        
        return CantidadGanada;
    }

    bool Aleatorneitor()
    {
        float RetornarValorAleatorio = Random.value;
        return RetornarValorAleatorio <= ProbabilidadDeEsmeraldas;
    }

    int CantidadDeRecompensa(int CantidadMinima,int CantidadMaxima)
    {
        return Random.Range(CantidadMinima, CantidadMaxima);
    }
}
