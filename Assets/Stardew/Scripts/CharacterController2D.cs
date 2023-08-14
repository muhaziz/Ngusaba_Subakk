using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator anima;

    [SerializeField] float speed = 2f;
    [SerializeField] float runSpeed = 5f;
    private float originalSpeed;
    private bool isSpeedReduced = false;
    private bool running = false;

    Vector2 motionVector;
    public Vector2 lastmotionVector;
    public bool moving;

    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashDistance = 5f;
    private bool canDash = true;

    private string XInput = "Horizontal";
    private string YInput = "Vertical";
    private string X = "X";
    private string Y = "Y";
    private string IsMove = "moving";
    private string LastPosition = "LastHori";
    private string LastPositionY = "LastVerti";

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        originalSpeed = speed;
    }

    private void Update()
    {
        float Horizontal = Input.GetAxisRaw(XInput);
        float Vertical = Input.GetAxisRaw(YInput);
        motionVector = new Vector2(Horizontal, Vertical);
        anima.SetFloat(X, Horizontal);
        anima.SetFloat(Y, Vertical);

        moving = Horizontal != 0 || Vertical != 0;
        anima.SetBool(IsMove, moving);

        if (Horizontal != 0 || Vertical != 0)
        {
            lastmotionVector = new Vector2(Horizontal, Vertical).normalized;
            anima.SetFloat(LastPosition, Horizontal);
            anima.SetFloat(LastPositionY, Vertical);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isSpeedReduced)
            {
                speed /= 4;
                isSpeedReduced = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (isSpeedReduced)
            {
                speed = originalSpeed;
                isSpeedReduced = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb2D.velocity = motionVector * (running == true ? runSpeed : speed);
    }

    private void Dash()
    {
        rb2D.velocity = Vector2.zero;
        Vector2 dashDirection = motionVector.normalized;
        Vector2 dashPosition = rb2D.position + dashDirection * dashDistance;
        rb2D.MovePosition(dashPosition);
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
