using UnityEngine;

public class DetectorLinha : MonoBehaviour
{
    public bool temCaixaTocando = false;
    
    private int quantidadeCaixas = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            quantidadeCaixas++;
            temCaixaTocando = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            quantidadeCaixas--;
            
            if (quantidadeCaixas <= 0)
            {
                temCaixaTocando = false;
                quantidadeCaixas = 0;
            }
        }
    }
}
