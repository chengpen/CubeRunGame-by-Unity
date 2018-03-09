using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerControll : MonoBehaviour {
    //Player transform
    public Transform pr_Tf_Player;
    //Map message
    private MapManager pr_Map_mapMes;
    //Camera message
    private CameraFollow pr_CF_isfollow;
    //UI message
    private UIManager pr_UI_message;
    //Player pos z x
    private int z = 3;
    private int x = 2;
    //trace color
    private Color pr_Co_traceOne = new Color(122/255f,85/255f,179/255f);
    private Color pr_Co_traceTwo = new Color(126/255f,93/255f,183/255f);
    // life bool
    private bool pr_bl_life = false;
    //Storage Gem/Score
    private int pr_int_GemNum=0;
    private int pr_int_ScoreNum=0;
    
    //audio
    public AudioClip pb_Ac_dead;
    public AudioClip pb_Ac_dead2;
    public AudioClip pb_Ac_Gem;
   
    void Start () {
        pr_int_GemNum = PlayerPrefs.GetInt("gem", 0);
        pr_Tf_Player = this.gameObject.GetComponent<Transform>();
        pr_Map_mapMes = GameObject.Find("MapManager").GetComponent<MapManager>();
        pr_CF_isfollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        pr_UI_message = GameObject.Find("UI Root").GetComponent<UIManager>();
        
    }
	
	void Update () {
        
    }
    /// <summary>
    /// attribute
    /// </summary>
    public int Z
    {
        get { return z; }
    }
    public int X
    {
        get { return x; }
    }
    public bool life
    {
        get { return pr_bl_life; }
        set { pr_bl_life = value; }
    }
    private void AddGem()
    {
        pr_int_GemNum++;
        pr_UI_message.GameDate(pr_int_ScoreNum, pr_int_GemNum);
        Debug.Log("宝石数：" + pr_int_GemNum);
    }
    private void AddScore()
    {
        pr_int_ScoreNum++;
        pr_UI_message.GameDate(pr_int_ScoreNum, pr_int_GemNum);

    }
    private void AddDate()
    {
        PlayerPrefs.SetInt("gem", pr_int_GemNum);
        if(pr_int_ScoreNum>PlayerPrefs.GetInt("score",0))
        {
            PlayerPrefs.SetInt("score", pr_int_ScoreNum);
        }
        else
        {
            pr_int_ScoreNum = 0;
        }
    }
    /// <summary>
    /// through Keycod M to initial Player Pos
    /// </summary>
    public void initialPlayerPos()
    {
           if(pr_UI_message.isCreat==false)
        {
            pr_UI_message.CreatPlayer();
        }
        SetPlayerPositon();
        pr_bl_life = true;
        pr_CF_isfollow.Isfollow = true;
        //pr_Map_mapMes.StartTileDown();
    }
    public void PlayerMove()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }
    }
    public void Left()
    {
        if (life == true)
        {
            if (x != 0)
            {
                z++;
                AddScore();
            }
            if (z % 2 == 1 && x != 0)
            {
                x--;
            }
            SetPlayerPositon();
            ClacPositon();
        }
        
    }
    public void Right()
    {
        if (life == true)
        {
            if (x != 4 || z % 2 == 0)
            {
                z++;
                AddScore();
            }
            if (z % 2 == 0 && x != 4)
            {
                
                x++;
                Debug.Log(x);
            }
            SetPlayerPositon();
            ClacPositon();
        }
       
    }



    public void SetPlayerPositon()
    {
       
        Transform Mappos = pr_Map_mapMes.maplist[z][x].GetComponent<Transform>();
        //change player initial postion 
        pr_Tf_Player.position = Mappos.position + new Vector3(0, 0.254f / 2, 0);
        pr_Tf_Player.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
        //Change trace color
        MeshRenderer TileObject = null;
        if(Mappos.tag=="pitfallSpkies")
        {
            TileObject= Mappos.FindChild("moving_spikes_a2").GetComponent<MeshRenderer>();
        }else if(Mappos.tag == "pitfallSKySpkies")
        {
            TileObject = Mappos.FindChild("smashing_spikes_a2").GetComponent<MeshRenderer>();
        }else if(Mappos.tag=="tile")
        {
            TileObject = Mappos.FindChild("normal_a2").GetComponent<MeshRenderer>();
        }  
        if(TileObject!=null)
        {
            if (z % 2 == 0)
            {
                TileObject.material.color = pr_Co_traceOne;
            }
            else
            {
                TileObject.material.color = pr_Co_traceTwo;
            }
        }else
        { 
            pr_bl_life = false;
            gameObject.AddComponent<Rigidbody>();
            pr_Map_mapMes.StopTileDown();
            StartCoroutine("GameOver");

        }                
    }
    private void ClacPositon()
    {
        if (pr_Map_mapMes.maplist.Count-z<=12)
        {
            pr_Map_mapMes.AddPb();
            float offset = pr_Map_mapMes.maplist[pr_Map_mapMes.maplist.Count - 1][0].GetComponent<Transform>().position.z + pr_Map_mapMes.pr_float_bottomLength / 2;
            pr_Map_mapMes.CreatMaptile(offset);
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag=="Spkies_Attack")
        {
            pr_bl_life = false;
            AudioSource.PlayClipAtPoint(pb_Ac_dead, gameObject.transform.position);
            pr_Map_mapMes.StopTileDown();
            StartCoroutine("GameOver");
        }
        if(coll.tag=="Gem")
        {
            AudioSource.PlayClipAtPoint(pb_Ac_Gem, gameObject.transform.position);
            Destroy(coll.gameObject.GetComponent<Transform>().parent.gameObject);
            AddGem();
            
        }
    }
    
    private void ResetPlayer()
    {
        z = 3;
        x = 2;
    }


    public IEnumerator GameOver()
    {
        pr_CF_isfollow.Isfollow = false;
        pr_CF_isfollow.StopBG();
        yield return new WaitForSeconds(0.2f);
            AudioSource.PlayClipAtPoint(pb_Ac_dead2, gameObject.transform.position);            
            life = false;
            AddDate();
        StartCoroutine("ResetGame");                    
    }
    
   public IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
        /* pr_UI_message.ResetUI();
        pr_CF_isfollow.ResetCamera();
        ResetPlayer();
        pr_Map_mapMes.ResetMap();
        pr_CF_isfollow.StartBG();
        pr_UI_message.isCreat = false;
        Destroy(this.gameObject);*/
    }

       
}
