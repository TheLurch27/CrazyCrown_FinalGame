using UnityEngine;

public class ButlerDialogTrigger : MonoBehaviour
{
    public OpeningDialogController dialogController; // Referenz auf den Dialogcontroller
    public PlayerController playerController; // Referenz auf den PlayerController

    private bool dialogStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Butler") && !dialogStarted) // Trigger nur einmal ausl�sen
        {
            dialogStarted = true;

            // Blockiere den Input des Players
            playerController.BlockPlayerInput(true);

            // Starte den Dialog
            dialogController.StartDialog();
        }
    }
}
