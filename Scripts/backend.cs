using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.Networking;
using Thirdweb;
using System.Text;

public class backend : MonoBehaviour
{
    private string baseUrl = "https://5kr3qw3x-3000.use2.devtunnels.ms/";

    string address;
    public void MandarTokens(int Cantidad)
    {
       string Aux = Cantidad + "000000000000000000";
        StringBuilder jsonData = new StringBuilder();
        jsonData.Append("{");
        jsonData.Append("\"receiver\": \"0x5496FDc429F1c62dC4973bd31AA94154e866973D\", ");
        jsonData.Append("\"amount\": \"" + Aux + "\"");
        jsonData.Append("}");
        StartCoroutine(PostRequest("transfer-native-token", jsonData.ToString()));
    }
    IEnumerator PostRequest(string endpoint, string jsonData)
    {
        using (UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }
}
