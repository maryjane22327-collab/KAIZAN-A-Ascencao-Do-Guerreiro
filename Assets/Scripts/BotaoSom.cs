using UnityEngine;
using UnityEngine.EventSystems;

public class SomBotao : MonoBehaviour, IPointerClickHandler
{
    public AudioClip somBotao;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Audios != null && somBotao != null)
        {
            GameManager.Audios.TocarSom(somBotao);
        }
    }
}