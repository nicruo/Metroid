using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public TextMeshProUGUI textField;

    public GameObject bulletPrefab;

    public int jumpCounter;

    public List<GameObject> bullets;

    public int currentBulletId;

    private bool jumping = false;

    public float jumpForce = 1;
    public float movementForce = 2;

    public int repeatSeconds = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("JumpCount"))
        {
            jumpCounter = PlayerPrefs.GetInt("JumpCount");
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bullets = new List<GameObject>();
        for(int i = 0; i <= 20; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, bulletPrefab.transform.rotation) as GameObject;
            bullet.SetActive(false);
            bullets.Add(bullet);
        }

        StartCoroutine(Loopy());
    }

    IEnumerator Loopy()
    {
        while(true)
        {
            Debug.Log(repeatSeconds);
            yield return new WaitForSeconds(repeatSeconds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (!jumping && Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter++;
            PlayerPrefs.SetInt("JumpCount", jumpCounter);
            PlayerPrefs.Save();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(currentBulletId >= bullets.Count)
            {
                currentBulletId = 0;
            }
            bullets[currentBulletId].transform.position = gameObject.transform.position;
            bullets[currentBulletId].SetActive(true);
            currentBulletId++;
        }

            spriteRenderer.flipX = rb.velocity.x < 0;

        anim.SetBool("bWalking", rb.velocity.x != 0);

        rb.AddForce(Vector2.right * horizontalInput * movementForce, ForceMode2D.Impulse);

        textField.text = jumpCounter + "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        jumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
