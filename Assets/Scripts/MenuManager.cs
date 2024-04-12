using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Scene Transition")]
    [SerializeField] private int transitionTime;
    [SerializeField] private Animator squareAnim;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    private int maxScore;
    [Header("Dialogue")]
    [SerializeField] private string sentence;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private float dialogTime;
    [Header("Button Sound")]
    [SerializeField] private AudioSource buttonSound;
    private void Start()
    {
        buttonSound.Stop();
        maxScore = PlayerPrefs.GetInt("MaxScore");
        maxScoreText.text = "Max Score:" + maxScore.ToString();
        sentenceText.text = "";
        StartCoroutine(Dialog());   
    }
    private IEnumerator Dialog()
    {
        foreach(char newChar in sentence.ToCharArray())
        {
            sentenceText.text += newChar;
            yield return new WaitForSeconds(dialogTime);
        }     
    }
    private void SceneTransitionToGameScene()
    {
        if(Input.GetMouseButtonDown(0))
        {
            buttonSound.Play();
            SceneTransitionTime();
        }
    }
    private async void SceneTransitionTime()
    {
       
        squareAnim.SetTrigger("Pass");
        await Task.Delay(transitionTime);        
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        SceneTransitionToGameScene();
    }
}
