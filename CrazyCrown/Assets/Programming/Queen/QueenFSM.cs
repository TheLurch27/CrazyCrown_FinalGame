using UnityEngine;
using System.Collections;

public class QueenFSM : MonoBehaviour
{
    public Transform spawnPoint; // Punkt, an dem die Queen in die Szene geladen wird
    public Transform despawnPoint; // Punkt, an dem die Queen die Szene verlässt
    public Transform pointA; // Wegpunkt A
    public Transform pointB; // Wegpunkt B
    public float walkSpeed = 2f; // Geschwindigkeit der Queen
    public Animator queenAnimator; // Referenz auf den Animator der Queen
    public QueenRageAudioTrigger queenRageTrigger; // Referenz zum QueenRageAudioTrigger

    private float spawnTimer = 20f; // Zeitintervall für das Spawnen der Queen
    private bool canSpawn = false; // Steuert, ob die Queen spawnen darf

    private QueenState currentState; // Der aktuelle Zustand der Queen

    // Zustände werden hier als Felder deklariert
    private QueenWalkState walkState;
    private QueenIdleState idleState;
    private QueenRageModeState rageModeState;

    private void Start()
    {
        // Initialisierung der Zustände
        walkState = new QueenWalkState();
        idleState = new QueenIdleState();
        rageModeState = new QueenRageModeState();

        // Initialisieren aller Zustände mit einer Referenz auf die FSM
        walkState.InitializeState(this);
        idleState.InitializeState(this);
        rageModeState.InitializeState(this);

        // Startet ohne aktiven Zustand
        currentState = null;

        StartCoroutine(CheckForRageTrigger()); // Prüft, wann der RageTrigger deaktiviert wird
    }

    private void Update()
    {
        if (canSpawn)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnQueen(); // Queen wird gespawnt
                spawnTimer = 20f; // Timer wird zurückgesetzt
            }
        }

        if (currentState != null)
        {
            currentState.UpdateState(); // Der aktuelle Zustand wird aktualisiert
        }
    }

    private IEnumerator CheckForRageTrigger()
    {
        // Überprüft, ob der QueenRageAudioTrigger deaktiviert wurde
        while (!queenRageTrigger.IsTriggerDeactivated())
        {
            yield return null; // Wartet, bis der Trigger deaktiviert wurde
        }

        // Sobald der Trigger deaktiviert ist, darf die Queen spawnen
        canSpawn = true;
    }

    private void SpawnQueen()
    {
        // Setzt die Queen an den Spawnpunkt und startet den WalkState
        transform.position = spawnPoint.position;
        gameObject.SetActive(true);

        ChangeState(walkState);
    }

    public void ChangeState(QueenState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState();
    }
}
