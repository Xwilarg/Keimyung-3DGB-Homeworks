using UnityEngine;

public class SpaceScroller : MonoBehaviour
{
    [SerializeField]
    private bool isInverted;
    private const float speed = .1f;

    private void FixedUpdate()
    {
        transform.Translate((isInverted ? Vector2.up : Vector2.down) * speed);
        if (transform.position.y < -16.4f)
            transform.Translate(new Vector2(0f, 32.8f * (isInverted ? -1f : 1f)));
    }
}
