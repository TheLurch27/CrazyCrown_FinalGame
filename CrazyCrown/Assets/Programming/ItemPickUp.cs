using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject itemToPickup; // Referenz auf das Item (Edding)
    public Collider2D frameTrigger; // Referenz auf den Trigger des Bilderrahmens (wird später aktiviert)
    public AudioClip pickupAudio; // Der Audio-Clip, der abgespielt werden soll
    public AudioSource playerAudioSource; // Referenz auf die AudioSource des Spielers
    public Transform playerTransform; // Referenz auf die Position des Players
    public float pickupRange = 1f; // Reichweite für das Einsammeln
    public DoorSaluteTrigger saluteTrigger; // Referenz auf das Salute Trigger Skript

    private bool hasItem = false; // Prüfen, ob der Spieler das Item eingesammelt hat

    void Start()
    {
        frameTrigger.enabled = false; // Trigger des Bilderrahmens ist zu Beginn deaktiviert
    }

    void Update()
    {
        // Überprüfen, ob der Spieler das Item einsammeln kann (nachdem der Salute-Trigger ausgelöst wurde)
        if (Input.GetKeyDown(KeyCode.C) && !hasItem && IsPlayerInRange() && saluteTrigger.IsTriggerDeactivated())
        {
            PickUpItem();
        }
    }

    bool IsPlayerInRange()
    {
        // Berechnet die Entfernung zwischen dem Spieler und dem Edding
        float distance = Vector2.Distance(playerTransform.position, itemToPickup.transform.position);
        return distance <= pickupRange; // Überprüfen, ob der Spieler innerhalb der Reichweite ist
    }

    void PickUpItem()
    {
        hasItem = true; // Item wurde eingesammelt
        Destroy(itemToPickup); // Edding wird aus der Szene entfernt
        frameTrigger.enabled = true; // Aktiviert den Trigger des Bilderrahmens

        // Audio über die AudioSource des Players abspielen
        if (pickupAudio != null && playerAudioSource != null)
        {
            playerAudioSource.PlayOneShot(pickupAudio);
        }

        Debug.Log("Item eingesammelt! Trigger auf dem Bilderrahmen aktiviert.");
    }
}
