using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(0, 1);

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, Space.Self);
    }
}
