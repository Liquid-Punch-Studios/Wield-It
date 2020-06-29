using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Controls controls;

    public Vector2 shoulderOffset;  /// This is the point hand & weapon rotates against
    public float armLength = 1.5f;  /// How much further weapon can be held from shoulder

    public float wieldFactor = 0.1f; /// How much the hand moves in relation to the mouse/touch delta
    // TODO: Maybe seperate mouse & touch?
    // FIXME: Mouse simulation for touch input is not implemented in the new inout system yet

    public float moveSpeed = 4;
    public float moveForce = 100;
    public float jumpSpeed = 8;
    public float airMoveSpeed = 4;
    public float airMoveForce = 100;
    public float airJumpSpeed = 8;
    // TODO: Decide if control percent for air is better
    public int airJumpCount = 1;
    private int airJumpLeft;

    private int onGround = 0;
    public bool OnGround { get => onGround > 0; }

    // TODO: Not sure if we should restrict player to
    // only be able to jump on static ground, maybe
    // allow jumping from heads of enemies?
    public LayerMask groundLayerMask;

    private Rigidbody2D playerRb;

    private GameObject weapon;
    private Rigidbody2D weaponRb;
    private RelativeJoint2D weaponJoint;

    // This controls whether weapon is held in a fixed angle
    // Usually used when stabbing enemies and terrain, also 
    // makes some cool air slam possible
    private bool fixedWeaponAngle;

    // HACK: Need to keep track of angle because relative joint resets after [180, -180]
    // We then manually edit angular offset of the joint instead of the object it is bound to
    private float realAngularOffset;

    public void ChangeWepon(GameObject weapon)
    {
        this.weapon = weapon;
        if (weapon != null)
        {
            //weaponData = weapon.GetComponent<Weapon>();
            weaponRb = weapon.GetComponent<Rigidbody2D>();
            weaponJoint = weapon.GetComponent<RelativeJoint2D>();
            //weaponJoint.linearOffset = (weaponData.weaponHandle * weapon.transform.localScale);
        }
        else
        {
            //weaponData = null;
            weaponRb = null;
            weaponJoint = null;
        }
    }

    // Awake is called once during the lifetime of the script, on its initial awake state, prior to any other functions
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        //ChangeWepon(weapon);
    }

    private void OnEnable()
    {
        if (controls == null)
            controls = new Controls();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // FixedUpdate is called once per physics frame
    private void FixedUpdate()
    {
        float move = controls.Player.Move.ReadValue<float>();

        // Apply some force in the direction we want to
        // move if we're not already moving in that vector with enough speed
        // Bigger the difference bigger the force
        // FIXME: Needs fine tuning, experimental
        float speed = OnGround ? moveSpeed : airMoveSpeed;
        if (move > 0 && playerRb.velocity.x < speed * move)
        {
            float force = Mathf.Clamp(speed * move - playerRb.velocity.x, 0, speed) * moveForce;
            playerRb.AddForce(new Vector2(force, 0));
            Debug.Log(force);
        }
        else if (move < 0 && playerRb.velocity.x > speed * move)
        {
            float force = Mathf.Clamp(speed * move - playerRb.velocity.x, -speed, 0) * moveForce;
            playerRb.AddForce(new Vector2(force, 0));
            Debug.Log(force);
        }

        // Gamepads and some keyboards have pressure
        // sensors but we probably don't give a fuck
        bool jump = controls.Player.Up.triggered;
        bool down = controls.Player.Down.triggered;

        if (jump)
        {
            Vector2 vel = playerRb.velocity; // Don't touch X axis
            if (OnGround)
            {
                vel.y = jumpSpeed;
                playerRb.velocity = vel;
            }
            else if (airJumpLeft > 0)
            {
                vel.y = airJumpSpeed;
                playerRb.velocity = vel;
                airJumpLeft--;
            }
        }

        // Weapon movement delta
        Vector2 wield = controls.Player.Wield.ReadValue<Vector2>();

        // TODO: Implement weapon wielding mechanic
        // Need to figure out how to handle collision/slashing mechanics
    }

    // Update is called once per frame
    private void Update()
    {

    }

    #region Input Callbacks
    public void Move(InputAction.CallbackContext ctx)
    {

    }

    public void Jump(InputAction.CallbackContext ctx)
    {

    }

    public void Down(InputAction.CallbackContext ctx)
    {

    }

    public void Wield(InputAction.CallbackContext ctx)
    {

    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer | groundLayerMask.value) != 0)
            onGround++;
        if (onGround > 0)
            airJumpLeft = airJumpCount;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer | groundLayerMask.value) != 0)
            onGround--;
    }
}
