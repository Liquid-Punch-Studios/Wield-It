using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private PlayerController pc;
    public void ToIdleState()
    {
        pc.MovementState = MovementActions.Idle;
    }
    void Start()
    {
        pc = transform.parent.GetComponent<PlayerController>();
    }
}
