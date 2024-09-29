using UnityEngine;

public class QueenRageModeState : QueenState
{
    public override void EnterState()
    {
        queenFSM.queenAnimator.SetBool("isAngry", true);
    }

    public override void UpdateState()
    {
        // Bewegt die Queen zum Despawn-Punkt
        if (queenFSM.transform.position != queenFSM.despawnPoint.position)
        {
            queenFSM.transform.position = Vector3.MoveTowards(
                queenFSM.transform.position,
                queenFSM.despawnPoint.position,
                queenFSM.walkSpeed * Time.deltaTime
            );
        }
        else
        {
            queenFSM.gameObject.SetActive(false); // Entfernt die Queen aus der Szene
        }
    }

    public override void ExitState()
    {
        queenFSM.queenAnimator.SetBool("isAngry", false);
    }
}
