using UnityEngine;
using UnityEngine.Events;

public class CollisionChecker : MonoBehaviour
{
    [Header("Extra config")]
    public string validTag;

    [Header("Events")]
    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionStay;
    public UnityEvent onCollisionExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == validTag)
        {
            if (onCollisionEnter != null)
                onCollisionEnter.Invoke();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == validTag)
        {
            if (onCollisionStay != null)
                onCollisionStay.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == validTag)
        {
            if (onCollisionExit != null)
                onCollisionExit.Invoke();
        }
    }
}
