using UnityEngine;

public class QueenRageAudioTrigger : MonoBehaviour
{
    public AudioClip rageAudio; // Der Audio-Clip, der abgespielt werden soll
    public AudioSource playerAudioSource; // Die AudioSource des Players, die die Audio-Datei abspielt
    private bool triggerDeactivated = false; // Pr�fen, ob der Trigger deaktiviert ist
    private bool audioPlayed = false; // Sicherstellen, dass die Audio nur einmal abgespielt wird

    // Methode, die den Trigger deaktiviert
    public void DeactivateTrigger()
    {
        triggerDeactivated = true;
    }

    // �berpr�fen, ob der Trigger deaktiviert wurde
    public bool IsTriggerDeactivated()
    {
        return triggerDeactivated;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �berpr�fen, ob der Spieler den Trigger betritt und ob der Trigger aktiviert wurde
        if (other.CompareTag("Player") && triggerDeactivated && !audioPlayed)
        {
            // Audio �ber die AudioSource des Players abspielen
            if (rageAudio != null && playerAudioSource != null)
            {
                playerAudioSource.PlayOneShot(rageAudio);
                audioPlayed = true; // Sicherstellen, dass das Audio nur einmal abgespielt wird
            }

            // Debugging-Informationen ausgeben
            Debug.Log("Trigger betreten. Audio abgespielt.");
        }
    }
}
