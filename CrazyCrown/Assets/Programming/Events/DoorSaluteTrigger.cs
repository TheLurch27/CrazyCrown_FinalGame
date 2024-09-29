using UnityEngine;

public class DoorSaluteTrigger : MonoBehaviour
{
    public AudioClip saluteAudio; // Der Audio-Clip, der abgespielt werden soll
    private AudioSource audioSource; // AudioSource-Komponente für das Abspielen des Sounds
    public Animator playerAnimator; // Referenz auf den Animator des Players

    private bool isPlayerInside = false; // Prüfen, ob der Spieler im Collider ist
    private bool audioPlayed = false; // Sicherstellen, dass der Sound nur einmal abgespielt wird
    private bool isSaluting = false; // Prüfen, ob der Spieler aktuell salutiert
    private bool triggerActive = true; // Der Trigger ist anfangs aktiv
    private bool triggerDeactivated = false; // Status, ob der Trigger deaktiviert ist

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Trigger betreten
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerActive) // Prüfen, ob es der Spieler ist und ob der Trigger aktiv ist
        {
            isPlayerInside = true;
        }
    }

    // Trigger verlassen
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (triggerActive) // Nur wenn der Trigger aktiv ist
        {
            // Wenn der Spieler im Collider ist, die S-Taste drückt und die Audio noch nicht abgespielt wurde
            if (isPlayerInside && Input.GetKeyDown(KeyCode.S) && !audioPlayed)
            {
                PlaySaluteAudio();
            }

            // Animation beenden, wenn das Audio nicht mehr abgespielt wird
            if (isSaluting && !audioSource.isPlaying)
            {
                StopSaluteAnimation();
                triggerActive = false; // Deaktiviert den Trigger dauerhaft
                triggerDeactivated = true; // Setzt den Triggerstatus auf deaktiviert
            }
        }
    }

    void PlaySaluteAudio()
    {
        audioSource.PlayOneShot(saluteAudio); // Audio einmal abspielen
        playerAnimator.SetBool("isSaluting", true); // Salute-Animation starten
        isSaluting = true; // Setze den Salut-Status auf true
        audioPlayed = true; // Sicherstellen, dass es nur einmal abgespielt wird
    }

    void StopSaluteAnimation()
    {
        playerAnimator.SetBool("isSaluting", false); // Salute-Animation stoppen
        isSaluting = false; // Setze den Salut-Status auf false
    }

    // Methode zur Überprüfung, ob der Trigger deaktiviert wurde
    public bool IsTriggerDeactivated()
    {
        return triggerDeactivated;
    }
}
