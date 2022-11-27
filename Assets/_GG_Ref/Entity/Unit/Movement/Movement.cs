using System;
using UnityEngine;

public class Movement 
{
    public float Speed { get; private set; }
    private GameObject _pursuingTarget;
    private MovementMode _mode;
    private Vector2 _movementDirection;
    private readonly GameObject _drivenGameObject;
    private Rigidbody2D DrivenRigidbody2D => _drivenGameObject.GetComponent<Rigidbody2D>();
    private Vector2 Velocity => _movementDirection.normalized * Speed;
    public Movement(GameObject drivenGameObject) : this(drivenGameObject, MovementMode.Idle, 0)
    {
    }
    public Movement(GameObject drivenGameObject, float speed) : this(drivenGameObject, MovementMode.Idle, speed)
    {
    }
    public Movement(GameObject drivenGameObject, MovementMode mode, float speed)
    {
        _mode = mode;
        Speed = speed;
        _movementDirection = Vector2.zero.normalized;
        _pursuingTarget = null;
        _drivenGameObject = drivenGameObject;
    }
    public void FixedUpdateMove()
    {
        switch (_mode)
        {
            case MovementMode.Idle:
                //Do nothing
                break;
            case MovementMode.Rectilinear:
                Move();
                break;
            case MovementMode.Pursue:
                Pursue();
                break;
            case MovementMode.Seek:
                Seek();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public void ChangeMode(MovementMode mode)
    {
        _mode = mode;
    }
    public void SetPursuingTarget(GameObject pursuingTarget)
    {
        _pursuingTarget = pursuingTarget;
    }
    public void AccelerateInDirection(float speed, Vector2 direction)
    {
        ChangeMovementDirection(direction);
        Accelerate(speed);
    }
    public void Accelerate(float speed)
    {
        if (speed >= 0)
        {
            Speed += speed;
        }
        else
        {
            return;
        }
    }
    public void SlowDown(float speed)
    {
        if (speed >= 0)
        {
            Speed -= speed;
        }
        else
        {
            return;
        }
    }
    public void ChangeMovementDirection(Vector2 direction)
    {
        _movementDirection += direction;
    }
    public void SetMovementDirection(Vector2 direction)
    {
        _movementDirection = direction;
    }
    private void Move()
    {
        DrivenRigidbody2D.MovePosition(DrivenRigidbody2D.position + Velocity * Time.fixedDeltaTime);
    }
    private void Pursue()
    {
        SetMovementDirection(_pursuingTarget.GetComponent<Rigidbody2D>().position - DrivenRigidbody2D.position);
        Move();
    }
    private void Seek()
    {
        LookToPursuingTarget();
        Pursue();
    }
    private void LookToPursuingTarget()
    {
        var angle = (Mathf.Atan2(DirectionToPursuingTarget().y, DirectionToPursuingTarget().x) - Mathf.PI / 2) * Mathf.Rad2Deg;

        DrivenRigidbody2D.rotation = angle;
    }
    private Vector2 DirectionToPursuingTarget()
    {
        var horizontal = _pursuingTarget.transform.position.x - DrivenRigidbody2D.transform.position.x;
        var vertical = _pursuingTarget.transform.position.y - DrivenRigidbody2D.transform.position.y;

        return new Vector2(horizontal, vertical);
    }
}