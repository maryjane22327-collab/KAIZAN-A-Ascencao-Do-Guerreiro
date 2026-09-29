using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint[] proximoDestino;

    public Waypoint ObterProximoDestino()
    {
        if( proximoDestino == null)
        {
            return null;
        }
        int destino = new System.Random().Next(0, proximoDestino.Length);
        return proximoDestino[destino];
    }
}
