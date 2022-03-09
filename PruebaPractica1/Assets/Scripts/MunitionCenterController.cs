using UnityEngine;

public class MunitionCenterController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20;
    public Rigidbody2D rb;
    public GameObject bala;
    public int dano = 20;
    void Start()
    {
        Instantiate(bala, gameObject.transform.position, Quaternion.identity).GetComponent<MunitionController>().Shoot(gameObject, 0.5f);
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.scoreJugador += 1;
            collision.gameObject.GetComponent<PoliciaNPC>().hacerDano(dano);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
