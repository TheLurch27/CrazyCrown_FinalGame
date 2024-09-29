using UnityEngine;

public class QueenWalkState : QueenState
{
    private enum WalkStage { ToPointA, ToPointB, ToDespawnPoint }
    private WalkStage currentStage;

    public override void EnterState()
    {
        queenFSM.queenAnimator.SetBool("isWalking", true);
        currentStage = WalkStage.ToPointA; // Starte mit der Bewegung zu Punkt A
    }

    public override void UpdateState()
    {
        switch (currentStage)
        {
            case WalkStage.ToPointA:
                MoveToPoint(queenFSM.pointA, WalkStage.ToPointB); // Gehe zu Punkt A, dann weiter zu Punkt B
                break;

            case WalkStage.ToPointB:
                MoveToPoint(queenFSM.pointB, WalkStage.ToDespawnPoint); // Gehe zu Punkt B, dann weiter zum Despawn-Punkt
                break;

            case WalkStage.ToDespawnPoint:
                MoveToPoint(queenFSM.despawnPoint, null); // Gehe zum Despawn-Punkt und beende den WalkState
                break;
        }
    }

    private void MoveToPoint(Transform targetPoint, WalkStage? nextStage)
    {
        // Bewegt die Queen zum Zielpunkt
        if (queenFSM.transform.position != targetPoint.position)
        {
            queenFSM.transform.position = Vector3.MoveTowards(
                queenFSM.transform.position,
                targetPoint.position,
                queenFSM.walkSpeed * Time.deltaTime
            );
        }
        else if (nextStage.HasValue)
        {
            currentStage = nextStage.Value; // Wechsel zum nächsten Wegpunkt
        }
        else
        {
            // Wenn der Despawn-Punkt erreicht wurde, wird die Queen entfernt
            queenFSM.ChangeState(null); // Beendet den aktuellen Zustand
            queenFSM.gameObject.SetActive(false); // Entfernt die Queen aus der Szene
        }
    }

    public override void ExitState()
    {
        queenFSM.queenAnimator.SetBool("isWalking", false); // Stoppt die Walk-Animation
    }
}
