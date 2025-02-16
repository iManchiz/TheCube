using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    void Start()
    {
        // Verificar si hay una posici�n guardada para el jugador
        if (PlayerPosition.hasPosition)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Teletransportar al jugador a la posici�n guardada
                player.transform.position = PlayerPosition.position;
            }
            // Reiniciar la bandera de posici�n guardada
            PlayerPosition.hasPosition = false;
        }
    }
}
