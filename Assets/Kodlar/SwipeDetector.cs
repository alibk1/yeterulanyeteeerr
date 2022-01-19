using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    public bool leftb, rightb, downb, upb;
    public int sagsol, ilerigeri;
    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe;

    public static event Action<SwipeData> OnSwipe = delegate { };

    private void FixedUpdate()
    {
        if (upb)
        {
            if (ilerigeri >= 75)
            {
                upb = false;
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                transform.Translate(0, 0, 0.3f);
                ilerigeri++;
            }
           
        }
        if (rightb)
        {
            if (sagsol >= 20)
            {
                rightb = false;
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(0, 90, 0);
                transform.Translate(0, 0, 0.3f);
                sagsol++;
            }
           
        }
        if (downb)
        {
            if (ilerigeri <= -15)
            {
                downb = false;
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(0, 180, 0);
                transform.Translate(0, 0, 0.3f);
                ilerigeri--;
            }
        }
        if (leftb)
        {
            if (sagsol <= -20)
            {
                leftb = false;
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(0, 270, 0);
                transform.Translate(0, 0, 0.3f);
                sagsol--;
            }
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if(fingerUpPosition.x > fingerDownPosition.x)
        {
            leftb = true;
            rightb = false;
            downb = false;
            upb = false;
        }
        if(fingerUpPosition.x < fingerDownPosition.x)
        {
            rightb = true;
            leftb = false;
            downb = false;
            upb = false;
        }
        if(fingerUpPosition.y > fingerDownPosition.y)
        {
            downb = true;
            rightb = false;
            leftb = false;
            upb = false;
        }
        if(fingerUpPosition.y < fingerDownPosition.y)
        {
            upb = true;
            rightb = false;
            leftb = false;
            downb = false;
        }

        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        OnSwipe(swipeData);
    }
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}