using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private PlayerController pc;
    public void ToIdleState()
    {
		if (pc.lastMovementState == MovementActions.Idle)
			pc.MovementState = MovementActions.Idle;
    }
    void Start()
    {
        pc = transform.parent.GetComponent<PlayerController>();
    }
}
