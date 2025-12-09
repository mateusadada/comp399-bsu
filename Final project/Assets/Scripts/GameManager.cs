using UnityEngine;
using System.Collections;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public DetectorLinha sensorDaLinha; 
    public TextMeshProUGUI textoMensagem; 
    
    private int botoesApertados = 0;
    private int totalBotoes = 4;

    private string[] fakeLoadingMessages = {
        "Detecting user IP and data...",
        "Uploading data to third parties...",
        "Waiting for the us-east-1 AWS region...",
        "Establishing for satellite connection...",
        "Decrypting server response...",
    };

    void Start()
    {
        if (textoMensagem != null) textoMensagem.text = "";
    }

    public void RegistrarClique()
    {
        botoesApertados++;

        if (botoesApertados >= totalBotoes)
        {
            StartCoroutine(SequenciaEngracada());
        }
    }

    IEnumerator SequenciaEngracada()
    {
        foreach (string mensagem in fakeLoadingMessages)
        {
            textoMensagem.text = mensagem;
            
            yield return new WaitForSeconds(2f); 
        }

        if (sensorDaLinha.temCaixaTocando == true)
        {
            textoMensagem.text = "YOU WIN";
        }
        else
        {
            textoMensagem.text = "YOU LOST";
        }
    }
}
