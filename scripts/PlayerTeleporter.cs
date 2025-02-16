using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    void Start()
    {
        // Verificar si hay una posición guardada para el jugador
        if (PlayerPosition.hasPosition)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Teletransportar al jugador a la posición guardada
                player.transform.position = PlayerPosition.position;
            }
            // Reiniciar la bandera de posición guardada
            PlayerPosition.hasPosition = false;
        }
    }
}
