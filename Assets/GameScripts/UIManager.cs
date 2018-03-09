using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    private GameObject pr_GO_Start;
    private GameObject pr_GO_Game;
    private GameObject pr_GO_PlayButton;
    private UILabel pr_UL_Gem;
    private UILabel pr_UL_Score;
    private UILabel pr_UL_GameGem;
    private UILabel pr_UL_GameScore;
    private PlayerControll pr_PC_GameConsole;
    private CameraFollow pr_CF_isfollow;
    //touch button
    public GameObject pb_GO_left;
    public GameObject pb_GO_right;
    public GameObject pb_GO_stop;
    //Player
    private GameObject Player1;
    private GameObject Player2;
    private GameObject Player3;
    private GameObject Player4;
    private GameObject Player5;
    private GameObject Player6;
    //player list
    List<GameObject> play = new List<GameObject>();
    // bool 
    public bool isCreat = false;
    //
    GameObject goo;
    //stop button
    private UISprite pr_GO_stop;
    //stop bool
    private bool pr_bl_stop = true;


    void Awake()
    {
        Player1 = Resources.Load("cube_battery") as GameObject;
        play.Add(Player1);
        Player2 = Resources.Load("cube_books") as GameObject;
        play.Add(Player2);
        Player3 = Resources.Load("cube_box") as GameObject;
        play.Add(Player3);
        Player4 = Resources.Load("cube_bread") as GameObject;
        play.Add(Player4);
        Player5 = Resources.Load("cube_cake") as GameObject;
        play.Add(Player5);
        Player6 = Resources.Load("cube_car") as GameObject;
        play.Add(Player6);
        CreatPlayer();
        isCreat = true;
    }
    void Start () {
        pr_GO_Game = GameObject.Find("GAME_UI");
        pr_GO_Start = GameObject.Find("Start_UI");
        pr_GO_PlayButton = GameObject.Find("Play_btn");
        pr_CF_isfollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        pr_GO_stop = GameObject.FindGameObjectWithTag("StopButton").GetComponent<UISprite>();
        pr_PC_GameConsole = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        UIEventListener.Get(pr_GO_PlayButton).onClick = PlayButtonClick;
        //Start UI
        pr_UL_Gem = GameObject.Find("Start_goldNumber").GetComponent<UILabel>();
        pr_UL_Score = GameObject.Find("Start_Score").GetComponent<UILabel>();        
        //Game UI
        pr_UL_GameGem = GameObject.Find("Game_goldNumber").GetComponent<UILabel>();
        pr_UL_GameScore = GameObject.Find("Game_Score").GetComponent<UILabel>();
        initScoreAndGem();
        pr_GO_Game.SetActive(false);

        //Game Touch button

        UIEventListener.Get(pb_GO_left).onClick = GameLeft;
        UIEventListener.Get(pb_GO_right).onClick = GameRight;
        UIEventListener.Get(pb_GO_stop).onClick = GamePause;

    }
	
	void Update () {
	
	}
    private void GamePause(GameObject go)
    {
        pauseGame();
    }
    private void GameLeft(GameObject go)
    {
        pr_PC_GameConsole.Left();

    }
    private void GameRight(GameObject go)
    {
        pr_PC_GameConsole.Right();
    }
    public void GameDate(int score,int gem)
    {
        pr_UL_GameGem.text = gem + "";
        pr_UL_GameScore.text = score + "";
        pr_UL_Gem.text = gem + "";
    }
    private void initScoreAndGem()
    {
        pr_UL_Score.text = PlayerPrefs.GetInt("score") + "";
        pr_UL_Gem.text = PlayerPrefs.GetInt("gem") + "";
        pr_UL_GameGem.text = PlayerPrefs.GetInt("gem")+"";
        pr_UL_GameScore.text = "0";

    }
    private void PlayButtonClick(GameObject go)
    {
        pr_GO_Game.SetActive(true);
        pr_GO_Start.SetActive(false);
        pr_PC_GameConsole.initialPlayerPos();        
        
    }

    public void ResetUI()
    {
        pr_GO_Start.SetActive(true);
        pr_GO_Game.SetActive(false);
        initScoreAndGem();

    }
    public void CreatPlayer()
    {
        int i = Random.Range(0, 6);
        Vector3 pos = new Vector3(0, 0, 0);       
        goo =GameObject.Instantiate(play[i], pos, Quaternion.identity)as GameObject;
    }
    public void pauseGame()
    {
        if (pr_bl_stop)
        {
            Time.timeScale = 0;
            pr_CF_isfollow.PauseBG();
            pr_GO_stop.spriteName = "btn_play";
            pr_bl_stop = false;

        }
        else
        {
            Time.timeScale = 1;
            pr_CF_isfollow.StartBG();
            pr_GO_stop.spriteName = "btn_pause";
            pr_bl_stop = true;

        }
    }

}
