using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class GameManager : MonoBehaviour
{
    public bool EditorMode;
    [Header("Scene Object Controller")]
    [SerializeField] private Transform losingLine;
    [SerializeField] private Camera cam;
    private int score;
    [HideInInspector] public int MaxScore;
    [Header("UI")]
    [SerializeField] private SquareController squareManager;
    [SerializeField] private TextMeshProUGUI scoreText, loseUIScore;
    [SerializeField] private GameObject loseUI, playerUI;
    [SerializeField] private AudioSource buttonSound;
    private void OnEnable()
    {
        SquareController.ScoreUpdateControl += Score;
        SquareController.PlayerLoseControl += LoseControl;
    }
    private void OnDisable()
    {
        SquareController.ScoreUpdateControl -= Score;
        SquareController.PlayerLoseControl -= LoseControl;
    }
    private void Start()
    {
        buttonSound.Stop();
        score = 0;
    }
    private void LoseControl()
    {
        loseUI.SetActive(true);
        playerUI.SetActive(false);
        loseUIScore.text = "Score:" + score.ToString();
    }
    private void LosingLineControl()
    {
        if(squareManager.IsGrounded)
          losingLine.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y - 20f);
    }
    private void Score()
    {
        score++;
        if(MaxScore<score)
        {
            MaxScore = score;
            print("New Record");
        }
        scoreText.text = "Score:" + score.ToString();
        PlayerPrefs.SetInt("MaxScore", MaxScore);
    }
    private void Update()
    {
        LosingLineControl();
    }
    //Buttons
    public void RestartButton()
    {
        buttonSound.Play();
        SceneManager.LoadScene(1);
    }
    public void MenuButton()
    {
        buttonSound.Play();
        SceneManager.LoadScene(0);
    }
    //Data
    [UnityEditor.MenuItem("Tools/Delete All Data")]
    public static void DeletData()
    {
        PlayerPrefs.DeleteAll();
    }
    [UnityEditor.MenuItem("Tools/LinkedIn")]
    public static void Instagram()
    {
        print("Follow");
        Process.Start("https://www.linkedin.com/in/muhammed-demir-b557b028b/");
    }
    [UnityEditor.MenuItem("Tools/Youtube")]
    public static void Youtube()
    {
        print("Follow");
        Process.Start("https://www.youtube.com/@muhammed04797/videos");
    }
}
