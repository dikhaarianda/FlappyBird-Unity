using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb; // emmasukan kelas Rigidbody2D menjadi objek
    public float jumpForce;
    public GameObject loseScreenUI, starScreenUI;
    public Button play;
    public int score, highScore;
    public Text scoreUI, highScoreUI;
    string HIGHSCORE = "HIGHSCORE";

    // reference objek Rigidbody2D
    private void Awake () {
        rb = GetComponent<Rigidbody2D>() ;
    }

    // Use this for instalation
    void Start()
    {
        Time.timeScale = 0;
        play.onClick.AddListener(StartGame);
        highScore = PlayerPrefs.GetInt(HIGHSCORE);
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
            AudioManager.singleton.PlaySound(0);
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayerLose() {
        AudioManager.singleton.PlaySound(1);
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE, highScore);
        }
        highScoreUI.text = "High Score: " + highScore.ToString();
        loseScreenUI.SetActive(true);
        Time.timeScale = 0;
    }

    void AddScore() {
        AudioManager.singleton.PlaySound(2);
        score++;
        scoreUI.text = "Score: " + score.ToString();
    }

    void StartGame() {
        Time.timeScale = 1;
        starScreenUI.SetActive(false);
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