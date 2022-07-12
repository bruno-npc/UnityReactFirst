using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    private int highScore = 0;
    public static bool playGame = false;
    [SerializeField, Tooltip("Tempo de jogo")] public static float timeGame = 50;
    [SerializeField, Tooltip("Defini os itens do jogo.")] private List<GameObject> targets;
    private int qtdTargets = 0; //qtd itens to create;
    private float timeToSpawn = 0; //time to create itens;

    [SerializeField, Tooltip("Informa o score na HUD.")] private TMP_Text HUD_Score;
    [SerializeField, Tooltip("Informa o tempo na HUD.")] private TMP_Text HUD_Timer;

    [SerializeField, Tooltip("Informa GameOver na HUD.")] private GameObject HUD_GameOver;
    [SerializeField, Tooltip("Botão play na HUD.")] private GameObject HUD_MainMenu;
    [SerializeField, Tooltip("HUD de jogo em execução.")] private GameObject HUD_RunGame;

    //*****************Game Over HUD *****************
    [SerializeField, Tooltip("Informa o score da partida na HUD GameOver.")] private TMP_Text HUD_ScoreEnd;
    [SerializeField, Tooltip("Informa o maior score na HUD GameOver.")] private TMP_Text HUD_HighScore;


    void Start()
    {
        Application.targetFrameRate = 60;
        StartGame();
        
    }
    void Update()
    {  
        if(playGame == true)
        {
            UpdateHUD();
        }
    }

    private void UpdateHUD (){
        if(GameManager.timeGame > 0){
            GameManager.timeGame -= Time.deltaTime;
        }
        else{
            HUD_GameOver.SetActive(true);
            HUD_RunGame.SetActive(false);
            highScoreTxt();
        }

        if (HUD_Score != null){
            HUD_Score.text = "SCORE: " + GameManager.score;
        }
        if (HUD_Timer != null){
            int timeInt = (int) GameManager.timeGame;
            HUD_Timer.text = "Tempo de jogo: " + timeInt;
        }
    }

    void StartGame (){
        qtdTargets = 1;
        timeToSpawn = 1.5f;

        StartCoroutine(SpawnTargets());

        GameManager.score = 0;
    }

    void highScoreTxt(){
        if(score > highScore){
            highScore = score;
            HUD_HighScore.text = "Melhor score: " + highScore;
        }
        HUD_ScoreEnd.text = "Score: " + GameManager.score;
        Debug.Log("Score end text: " + GameManager.score);
    }

    private IEnumerator SpawnTargets(){
        while(true){
            yield return new WaitForSeconds(timeToSpawn);
            for (int i = 0; i <= qtdTargets; i++)
            {
                int index = Random.Range(0, targets.Count);
                Instantiate(targets[index]);   
            }
        }
    }

    private IEnumerator ChangeDificult(){
    while(timeToSpawn > 1){
        yield return new WaitForSeconds(10);
        timeToSpawn -= 0.1f; //Diminui tempo de spawn;
        qtdTargets += 1; //incrementa quantidade de itens na tela;
    }
    }

    public void RestartGame (){
        GameManager.timeGame = 50;
        GameManager.score = 0;
        timeToSpawn = 1.5f;
        HUD_RunGame.SetActive(true);
        HUD_GameOver.SetActive(false);
    }

    public void btn_Start(){
        GameManager.playGame = true;
        StartCoroutine(ChangeDificult());
        HUD_MainMenu.SetActive(false);
        HUD_RunGame.SetActive(true);
    }

    public void btn_Exit(){
        SceneManager.LoadScene("Login");
    }
}
