using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 shoulderOffset;  /// This is the point hand & weapon rotates against
    public float armLength = 1.5f;  /// How much further weapon can be held from shoulder

    public float wieldFactor = 0.1f; /// How much the hand moves in relation to the mouse/touch delta
    // TODO: Maybe seperate mouse & touch?

    public float moveSpeed = 10;
    public float jumpSpeed = 10;
    public float airMoveSpeed = 4;
    public float airJumpSpeed = 8;
    // TODO: Decide if control percent for air is better
    public int airJumpCount = 1;
    private int airJumpLeft;
    private int onGround = 0;

    public LayerMask groundLayerMask; // Doesn't 

    private Rigidbody2D playerRb;
    private Rigidbody2D weaponRb;
    private RelativeJoint2D weaponJoint;

    // This controls whether weapon is held in a fixed angle
    // Usually used when stabbing enemies and terrain, also 
    // makes some cool air slam possible
    private bool fixedWeaponAngle;

    private float realAngularOffset; // Need to keep track of angle because relative joint resets after [180, -180]

    // Start is called before the first frame update
    private void Start()
    {

    }

    // FixedUpdate is called once per physics frame
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Wield(Vector2 delta)
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.layer | groundLayerMask.value) != 0)
            onGround++;
        if (onGround > 0)
            airJumpLeft = airJumpCount;
    }

    private void OnTriggerExit(Collider collision)
    {
        if ((collision.gameObject.layer | groundLayerMask.value) != 0)
            onGround--;
    }
}
