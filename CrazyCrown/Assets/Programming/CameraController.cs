using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referenz auf den Spieler
    public float leftLimit; // Linke Begrenzung f�r die Kamera
    public float rightLimit; // Rechte Begrenzung f�r die Kamera

    private void Update()
    {
        // Kamera-Position aktualisieren basierend auf der Position des Spielers, aber nur auf der X-Achse
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(player.position.x, leftLimit, rightLimit); // Begrenzung der X-Achse

        // Y- und Z-Achse bleiben unver�ndert
        transform.position = newPosition;
    }
}
