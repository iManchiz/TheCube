using UnityEngine;

public class RestartOnCollision : MonoBehaviour
{
    // Este método se llama cuando ocurre una colisión
    private void OnCollisionEnter(Collision collision)
    {
        // Obtener la instancia del GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
            return;
        }

        // Verificar colisiones con objetos con tags "True" o "False"
        if (collision.gameObject.CompareTag("True"))
        {
            gameManager.HandleCollisionTrue();
            gameManager.TeleportPlayerToInitialPosition();
            gameManager.LoadRandomScene();
        }
        else if (collision.gameObject.CompareTag("False"))
        {
            gameManager.HandleCollisionFalse();
            gameManager.TeleportPlayerToInitialPosition();
            gameManager.LoadRandomScene();
        }
    }
}
