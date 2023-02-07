using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb; // emmasukan kelas Rigidbody2D menjadi objek
    public float jumpForce;
    public GameObject loseScreenUi;
    public int score;
    public Text scoreUI;

    // reference objek Rigidbody2D
    private void Awake () {
        rb = GetComponent<Rigidbody2D>() ;
    }

    // Use this for instalation
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump() ;
    }

    void PlayerJump() {
        // melompat melalui input mouse
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // melompat melalui input keyboard Space
        if(Input.GetKeyUp("space"))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayerLose() {
        loseScreenUi.SetActive(true);
        Time.timeScale = 0;
    }

    void AddScore() {
        score++;
        scoreUI.text = score.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("obstacel")) {
            PlayerLose();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("score")) {
            AddScore();
        }
    }
}