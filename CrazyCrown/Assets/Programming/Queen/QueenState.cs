using UnityEngine;

public abstract class QueenState
{
    protected QueenFSM queenFSM;

    // Diese Methode wird in jedem Zustand aufgerufen, um die FSM zu referenzieren
    public void InitializeState(QueenFSM fsm)
    {
        queenFSM = fsm;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
