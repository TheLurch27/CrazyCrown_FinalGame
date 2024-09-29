using UnityEngine;

public class FrameInteraction : MonoBehaviour
{
    public GameObject portraitNormal; // Referenz auf das normale Portrait
    public GameObject portraitSmear;  // Referenz auf das verschmierte Portrait
    public AudioClip frameAudio; // Der Audio-Clip für den Frame-Trigger
    public AudioSource playerAudioSource; // Referenz auf die AudioSource des Spielers
    public Collider2D newTrigger; // Der neue Trigger, der aktiviert wird, nachdem die Portraits getauscht wurden

    private bool isPlayerInside = false; // Prüfen, ob der Spieler den Trigger betreten hat
    private bool hasPlayedAudio = false; // Prüfen, ob die Audio bereits abgespielt wurde
    private bool triggerActive = true; // Der Trigger ist zu Beginn aktiv

    void Start()
    {
        newTrigger.enabled = false; // Neuer Trigger ist zu Beginn deaktiviert
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerActive)
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (triggerActive && isPlayerInside && Input.GetKeyDown(KeyCode.E) && !hasPlayedAudio)
        {
            SwapPortraits();
        }
    }

    void SwapPortraits()
    {
        portraitNormal.SetActive(false); // Versteckt das normale Portrait
        portraitSmear.SetActive(true);   // Zeigt das verschmierte Portrait

        // Audio über die AudioSource des Spielers abspielen
        if (frameAudio != null && playerAudioSource != null)
        {
            playerAudioSource.PlayOneShot(frameAudio);
        }

        hasPlayedAudio = true; // Sicherstellen, dass das Audio nur einmal abgespielt wird
        triggerActive = false; // Deaktiviert den Trigger, damit er nur einmal aktiviert werden kann

        // Aktiviert den neuen Trigger, nachdem die Portraits getauscht wurden
        newTrigger.enabled = true;
        Debug.Log("Portrait wurde geändert. Neuer Trigger aktiviert.");
    }
}
