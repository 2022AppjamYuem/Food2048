using UnityEngine;

public class Moving : MonoBehaviour
{
    bool move, _combine;
    int _x2, _y2;

    float xOffset = 1.3631f;
    float yOffset = -3.019f;
    float interval = 0.8971f;

    void Update() { if (move) Move(_x2, _y2, _combine); }

    public void Move(int x2, int y2, bool combine)
    {
        move = true; _x2 = x2; _y2 = y2; _combine = combine;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(interval * x2 - xOffset, interval * y2 + yOffset, 0), 0.3f);
        if (transform.position == new Vector3(interval * x2 - xOffset, interval * y2 + yOffset, 0))
        {
            move = false;
            if (combine) { _combine = false; Destroy(gameObject); }
        }
    }
}
