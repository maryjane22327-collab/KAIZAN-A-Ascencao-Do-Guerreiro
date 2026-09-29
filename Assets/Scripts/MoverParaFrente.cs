using UnityEngine;

public class MoverParaFrente : MonoBehaviour
{
    public float velocidade;
    public float tempoDeExistencia; 
    void Start()
    {
        Destroy(gameObject, tempoDeExistencia);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }
}
