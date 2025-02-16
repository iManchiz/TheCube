using UnityEngine;

public class OptimizeLights : MonoBehaviour
{
    public Light[] lights; // Lista de luces a optimizar
    public Transform player; // Transform del jugador
    public float shadowDisableDistance = 50f; // Distancia para desactivar sombras

    void Update()
    {
        foreach (Light light in lights)
        {
            float distance = Vector3.Distance(light.transform.position, player.position);
            if (distance > shadowDisableDistance)
            {
                light.shadows = LightShadows.None;
            }
            else
            {
                light.shadows = LightShadows.Soft;
            }
        }
    }
}
