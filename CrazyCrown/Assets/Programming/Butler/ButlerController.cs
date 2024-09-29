using UnityEngine;

public class ButlerController : MonoBehaviour
{
    public Transform player; // Ziel: der Spieler
    public Transform startPoint; // Startpunkt des Butlers
    public float speed = 2f; // Geschwindigkeit des Butlers
    public float stopDistance = 2f; // Entfernung, in der der Butler vor dem Spieler stehen bleibt

    private bool movingToPlayer = true; // Bestimmt, ob der Butler zum Spieler läuft
    private bool dialogFinished = false; // Bestimmt, ob der Dialog beendet ist

    private void Update()
    {
        if (movingToPlayer)
        {
            // Nur bis auf 2 Meter vor den Spieler bewegen
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                MoveTo(player.position);
            }
            else
            {
                // Butler erreicht den Spieler, Dialog startet hier
                movingToPlayer = false; // Butler bleibt stehen
            }
        }
        else if (dialogFinished)
        {
            // Dialog ist beendet, Butler geht zurück zum Startpunkt
            MoveTo(startPoint.position);

            // Prüfen, ob der Butler wieder am Startpunkt ist
            if (Vector2.Distance(transform.position, startPoint.position) < 0.1f)
            {
                Destroy(gameObject); // Butler aus der Szene entfernen
            }
        }
    }

    public void SetDialogFinished()
    {
        dialogFinished = true; // Setzt das Flag, dass der Dialog beendet ist
    }

    private void MoveTo(Vector2 target)
    {
        // Richtung zum Ziel berechnen
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Flippen, abhängig von der Bewegungsrichtung
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip die X-Achse
        transform.localScale = scale;
    }
}
