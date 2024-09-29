using System.Collections;
using UnityEngine;

public class OpeningDialogController : MonoBehaviour
{
    public AudioSource playerAudioSource; // AudioSource des Spielers
    public AudioSource butlerAudioSource; // AudioSource des Butlers
    public AudioClip[] dialogClips; // Liste der Audio-Dateien
    public int[] speakerOrder; // 0 = Butler spricht, 1 = Spieler spricht
    public ButlerController butlerController; // Referenz auf den ButlerController
    public PlayerController playerController; // Referenz auf den PlayerController

    private int currentClipIndex = 0; // Der aktuelle Audio-Clip-Index

    public void StartDialog()
    {
        if (dialogClips.Length != speakerOrder.Length)
        {
            Debug.LogError("Die Länge der Dialog-Clips muss mit der Länge der Sprech-Reihenfolge übereinstimmen!");
            return;
        }

        // Starte den Dialog
        StartCoroutine(PlayDialogSequence());
    }

    IEnumerator PlayDialogSequence()
    {
        while (currentClipIndex < dialogClips.Length)
        {
            AudioSource currentSource = (speakerOrder[currentClipIndex] == 0) ? butlerAudioSource : playerAudioSource;
            currentSource.clip = dialogClips[currentClipIndex];
            currentSource.Play();

            // Warte, bis die aktuelle Audio-Datei abgespielt wurde
            yield return new WaitForSeconds(currentSource.clip.length);

            // Warte 1 Sekunde, bevor die nächste Datei abgespielt wird
            yield return new WaitForSeconds(1f);

            // Gehe zur nächsten Audio-Datei
            currentClipIndex++;
        }

        Debug.Log("Dialog abgeschlossen.");
        butlerController.SetDialogFinished(); // Sagt dem ButlerController, dass der Dialog beendet ist
        playerController.BlockPlayerInput(false); // Gibt den Spieler-Input nach dem Dialog wieder frei
    }
}
