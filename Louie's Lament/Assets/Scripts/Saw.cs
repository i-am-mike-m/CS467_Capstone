using UnityEngine;

public class Saw : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * 6);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.PlayerDeath();
    }
}
