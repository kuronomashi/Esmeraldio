using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejoAbrirYCerrarMenus : MonoBehaviour
{
    public GameObject[] ObjetosParaAbirryCerrar;
    public int MenuActual = 0;

    public void AbrirMenu(int NuemeroRefrencial)
    {
        //if (NuemeroRefrencial == 0)
        //{
        //    Time.timeScale = 1;
        //}
        //else
        //{
        //    Time.timeScale = 0;
        //}
        if (MenuActual != NuemeroRefrencial)
        {
            ObjetosParaAbirryCerrar[MenuActual].SetActive(false);
            ObjetosParaAbirryCerrar[NuemeroRefrencial].SetActive(true);
            MenuActual = NuemeroRefrencial;
        }
    }

    public void AparecerObjetoSeleccionMEnu(int NuemeroRefrencial)
    {
        if (MenuActual != NuemeroRefrencial)
        {
            ObjetosParaAbirryCerrar[MenuActual].SetActive(false);
            ObjetosParaAbirryCerrar[NuemeroRefrencial].SetActive(true);
            Vector3 Aux = ObjetosParaAbirryCerrar[NuemeroRefrencial].GetComponent<RectTransform>().anchoredPosition;
            Aux.y -= 62;
            AnimacionesUI.DefaultReferenceUI.AparecerObjeto(ObjetosParaAbirryCerrar[NuemeroRefrencial],Aux,1);
            MenuActual = NuemeroRefrencial;
        }
    }

    public void AparecerObjetosConFaade(int NuemeroRefrencial)
    {
        StartCoroutine(Delay(NuemeroRefrencial));
    }

    IEnumerator Delay(int Aux)
    {
        Fade.Instacia.Aparecer(1, false);
        yield return new WaitForSeconds(0.2f);
        if (MenuActual != Aux)
        {
            ObjetosParaAbirryCerrar[MenuActual].SetActive(false);
            ObjetosParaAbirryCerrar[Aux].SetActive(true);
            MenuActual = Aux;
        }
    }
}
