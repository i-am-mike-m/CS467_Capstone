using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    Player player;
    private bool headToRightSide = true;
    [SerializeField] GameObject leftEdge;
    [SerializeField] GameObject rightEdge;

    // CITATION: Getting rotation and translation to work in conjunction
    // https://answers.unity.com/questions/765699/simultaneously-translating-and-rotating-2d-sprite.html

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (headToRightSide)
        {
            gameObject.transform.Translate(Vector3.right * 9 * Time.deltaTime, Space.World);
            gameObject.transform.Rotate(Vector3.back * 6);

            if (hitRightEdge())
            {
                headToRightSide = false;
            }
        }
        else
        {
           gameObject.transform.Translate(Vector3.left * 6 * Time.deltaTime, Space.World);
           gameObject.transform.Rotate(Vector3.forward * 6);

            if (hitLeftEdge())
            {
                headToRightSide = true;
            }
        }
    }

    private bool hitLeftEdge()
    {
        return gameObject.transform.position.x < leftEdge.transform.position.x;
    }

    private bool hitRightEdge()
    {
        return gameObject.transform.position.x > rightEdge.transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.PlayerDeath();
    }
}
