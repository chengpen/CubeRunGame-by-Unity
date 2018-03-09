using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {
    // tile/wall  objects
    private GameObject pr_GO_maptile;
    private GameObject pr_GO_mapwall;
    private GameObject pr_GO_spikes;
    private GameObject pr_GO_SmashingSpikes;
    private GameObject pr_GO_Gem;
    private GameObject pr_GO_null;
    //color
    private Color pr_Co_tileOne = new Color(124 / 255f, 155 / 255f, 230 / 255f);
    private Color pr_Co_tileTwo = new Color(125 / 255f, 169 / 255f, 233 / 255f);
    private Color pr_Co_tileWall = new Color(87 / 255f, 93 / 255f, 169 / 255f);
    //tileOne color list
    private Color colorOne = new Color(124 / 255f, 155 / 255f, 230 / 255f);
    private Color colorTwo = new Color(146/255f,120/255f,180/255f);
    private Color colorThree = new Color(120/255f,72/255f,209/255f);
    List<Color> TileOnecolorlist = new List<Color>();
    //tileTwo color list
    private Color colorOne2 = new Color(125 / 255f, 169 / 255f, 233 / 255f);
    private Color colorTwo2 = new Color(134/255f,113/255f,173/255f);
    private Color colorThree2 = new Color(204/255f,178/255f,251/255f);
    List<Color> TileTwocolorlist = new List<Color>();
    //wall color list
    private Color colorOneWall = new Color(87 / 255f, 93 / 255f, 169 / 255f);
    private Color colorTwoWall = new Color(115/255f,86/255f,139/255f);
    private Color colorThreeWall = new Color(109/255f,32/255f,251/255f);
    List<Color> Wallcolorlist = new List<Color>();

    //
    public float pr_float_bottomLength = Mathf.Sqrt(2) * 0.254f;
    //parent
    private Transform pr_Tf_tile;
    private Transform pr_Tf_wall;
    private Transform pr_Tf_pitfall;
    //map list
    public List<GameObject[]> maplist = new List<GameObject[]>();
    //Log Down Number
    private int pr_int_index=0;
    //PlayerControll
    private PlayerControll pr_PC_pos;
    //lists
    private List<int> tilelist = new List<int>() { 0, 1, 2, 3, 4, 5 };
    private List<int> tilelist2 = new List<int>();
    private List<GameObject> nullpts = new List<GameObject>();
    //pitfall probability
    private int pr_int_PbHole=0;
    private int pr_int_PbSpikes = 0;
    private int pr_int_PbSmashingSpikes = 0;
    private int pr_int_Gem = 2;
    /// <summary>
    /// Map Console 
    /// </summary>
    void Awake()
    {
        TileOnecolorlist.Add(colorOne);
        TileOnecolorlist.Add(colorTwo);
        TileOnecolorlist.Add(colorThree);
        //
        TileTwocolorlist.Add(colorOne2);
        TileTwocolorlist.Add(colorTwo2);
        TileTwocolorlist.Add(colorThree2);
        //
        Wallcolorlist.Add(colorOneWall);
        Wallcolorlist.Add(colorTwoWall);
        Wallcolorlist.Add(colorTwoWall);        
    }

    
    void Start () {
        pr_GO_maptile = Resources.Load("tile_white") as GameObject;
        pr_GO_mapwall = Resources.Load("wall2") as GameObject;
        pr_GO_spikes = Resources.Load("moving_spikes") as GameObject;
        pr_GO_SmashingSpikes = Resources.Load("smashing_spikes") as GameObject;
        pr_GO_Gem = Resources.Load("gem 2") as GameObject;
        pr_GO_null = Resources.Load("nullpt") as GameObject;
        //parent transform
        pr_Tf_wall = GameObject.Find("mapwall").GetComponent<Transform>();
        pr_Tf_tile = GameObject.Find("maptile").GetComponent<Transform>();
        pr_Tf_pitfall = GameObject.Find("mapPitfall").GetComponent<Transform>();
        //Player script
        pr_PC_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        CreatMaptile(0);
	}
	
    /// <summary>
    /// Creat Map
    /// </summary>
    public void CreatMaptile(float offsetZ)
    {
        int wall = Random.Range(0, 3);
        int tileOne = Random.Range(0, 3);
        int tileTwo = Random.Range(0, 3);
        for (int i = 0; i < 10; i++)
        {            
            GameObject[] item1 = new GameObject[6];
            for (int j =0;j<6;j++)
            {
                Vector3 tilepos = new Vector3(j * pr_float_bottomLength, 0, i * pr_float_bottomLength+offsetZ);
                Vector3 tilerotaion = new Vector3(-90, 45, 0);
                GameObject tile = null;
                if (j == 0 || j == 5)
                {
                    tile = GameObject.Instantiate(pr_GO_mapwall, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                    tile.GetComponent<MeshRenderer>().material.color = Wallcolorlist[wall];
                    tile.GetComponent<Transform>().SetParent(pr_Tf_wall);

                }
                else
                {
                    int pbPitFall = ClacPitfallPb();
                    if(pbPitFall==0)
                    {
                        tile = GameObject.Instantiate(pr_GO_maptile, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                        tile.GetComponent<Transform>().FindChild("normal_a2").GetComponent<MeshRenderer>().material.color = TileOnecolorlist[tileOne];
                        //Give tile color
                        tile.GetComponent<MeshRenderer>().material.color = TileOnecolorlist[tileOne];
                        //set parent
                        tile.GetComponent<Transform>().SetParent(pr_Tf_tile);
                        //Creat Gem
                        int Gempr = ClacGem();
                        if(Gempr==1)
                        {   Vector3 Gempos = tile.GetComponent<Transform>().position + new Vector3(0, 0.06f, 0);
                            GameObject Gem =GameObject.Instantiate(pr_GO_Gem, Gempos, Quaternion.identity) as GameObject;
                            Gem.GetComponent<Transform>().SetParent(tile.GetComponent<Transform>());
                        }
                    }else if(pbPitFall==1)
                    {
                        tile =GameObject.Instantiate(pr_GO_null, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;                                             
                        tile.GetComponent<Transform>().SetParent(pr_Tf_pitfall);
                    }else if(pbPitFall==2)
                    {

                            tile = GameObject.Instantiate(pr_GO_spikes, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                            tile.GetComponent<Transform>().SetParent(pr_Tf_pitfall);
                    }else if(pbPitFall==3)
                    {
                        tile = GameObject.Instantiate(pr_GO_SmashingSpikes, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                        tile.GetComponent<Transform>().SetParent(pr_Tf_pitfall);
                    }
                    
                }
                item1[j] = tile;              
            }
            nullpts.Clear();
            maplist.Add(item1);
            GameObject[] item2 = new GameObject[5];
            for (int j = 0; j < 5; j++)
            {
                Vector3 tilepos = new Vector3(j * pr_float_bottomLength + pr_float_bottomLength / 2, 0,offsetZ+ i * pr_float_bottomLength + pr_float_bottomLength / 2);
                Vector3 tilerotaion = new Vector3(-90, 45, 0);
                GameObject tile = null;
                int pbPitFall = ClacPitfallPb();       
                if (pbPitFall == 0)
                {
                    tile = GameObject.Instantiate(pr_GO_maptile, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                    tile.GetComponent<Transform>().FindChild("normal_a2").GetComponent<MeshRenderer>().material.color = TileTwocolorlist[tileTwo];
                    //Give tile color
                    tile.GetComponent<MeshRenderer>().material.color = TileTwocolorlist[tileTwo];
                    //set parent
                    tile.GetComponent<Transform>().SetParent(pr_Tf_tile);
                }
                else if (pbPitFall == 1)
                {
                    tile = GameObject.Instantiate(pr_GO_null, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                    tile.GetComponent<Transform>().SetParent(pr_Tf_pitfall);
                }
                else if (pbPitFall == 2)
                {                    
                    tile = GameObject.Instantiate(pr_GO_spikes, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                    nullpts.Add(tile);
                    tile.GetComponent<Transform>().SetParent(pr_Tf_pitfall);
                }
                else if (pbPitFall == 3)
                {
                    tile = GameObject.Instantiate(pr_GO_SmashingSpikes, tilepos, Quaternion.Euler(tilerotaion)) as GameObject;
                    tile.GetComponent<Transform>().SetParent(pr_Tf_pitfall);
                }
                tile.GetComponent<Transform>().SetParent(pr_Tf_tile);
                item2[j] = tile;
            }
            maplist.Add(item2);

        }
 
    }


	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0;i<maplist.Count;i++)
            {
                for(int j=0;j<maplist[i].Length;j++)
                {
                    maplist[i][j].name = i + "," + j;                   
                }
            }
        }
	}

    public void StartTileDown()
    {
        StartCoroutine("tileDown");
    }
    public void StopTileDown()
    {
        StopCoroutine("tileDown");
    }

    private IEnumerator tileDown()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.02f);
            //int Num3 = 0;
            tilelist2.Clear();
            for(int i =0;i<tilelist.Count;i++)
            {
                tilelist2.Add(tilelist[i]);
            }
            if (pr_int_index % 2 == 1)
            {
                tilelist2.RemoveAt(5);                
            }
            while (tilelist2.Count!=0)
            {
                //int Num2 = Random.Range(0, maplist[pr_int_index].Length-Num3);
                int Num2 = Random.Range(0, tilelist2.Count);
                int nm = tilelist2[Num2];
                //Num3++;
                Rigidbody tileRb = maplist[pr_int_index][nm].AddComponent<Rigidbody>();
                Vector3 tilerota = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                tileRb.angularVelocity = tilerota * Random.Range(2f, 10f);
                tilelist2.RemoveAt(Num2);
                GameObject.Destroy(maplist[pr_int_index][nm], 1.5f);
                yield return new WaitForSeconds(0.02f);
            }
            if(pr_int_index==pr_PC_pos.Z)
            {
                StopTileDown();
                pr_PC_pos.life = false;
                if (pr_PC_pos != null)
                {
                    pr_PC_pos.gameObject.AddComponent<Rigidbody>();
                    pr_PC_pos.StartCoroutine("GameOver");
                }
                
                
            }
            pr_int_index++;
        }
        /*for(int i=0;i<maplist[pr_int_index].Length;i++)
            {
                Rigidbody tileRb= maplist[pr_int_index][i].AddComponent<Rigidbody>();
                Vector3 tilerota = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                tileRb.angularVelocity = tilerota * Random.Range(2f, 10f);
                yield return new WaitForSeconds(0.1f);                
            }(Teacher method)*/
    }

    private int ClacPitfallPb()
    {
        int NumPr = Random.Range(1, 100);
        if(NumPr<=pr_int_PbHole)
        {
            return 1;
        }else if(31<NumPr&&NumPr<pr_int_PbSpikes+30&&nullpts.Count<3)
        {
            return 2;
        }
        else if(61<NumPr&&NumPr<pr_int_PbSmashingSpikes+60)
        {
            return 3;
        }
        return 0;
    }
    private int ClacGem()
    {
        int GemPr = Random.Range(1, 100);
        if(GemPr<=pr_int_Gem)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public void AddPb()
    {
        if(pr_int_PbHole<13)
        {
            pr_int_PbHole += 1;
        }
        if(pr_int_PbSpikes<21)
        {
            pr_int_PbSpikes += 2;
        }
        if(pr_int_PbSmashingSpikes<21)
        {
            pr_int_PbSmashingSpikes += 2;
        }
    }

    public void ResetMap()
    {
        Transform[] tileObjects = pr_Tf_tile.GetComponentsInChildren<Transform>();
        for(int i=1;i<tileObjects.Length;i++)
        {
            GameObject.Destroy(tileObjects[i].gameObject);
        }
        Transform[] wallObjects = pr_Tf_wall.GetComponentsInChildren<Transform>();
        for (int i = 1; i < wallObjects.Length; i++)
        {
            GameObject.Destroy(wallObjects[i].gameObject);
        }
        Transform[] PitfallObjects = pr_Tf_pitfall.GetComponentsInChildren<Transform>();
        for (int i = 1; i < PitfallObjects.Length; i++)
        {
            GameObject.Destroy(PitfallObjects[i].gameObject);
        }

        pr_int_PbSpikes = 0;
        pr_int_PbSmashingSpikes = 0;
        pr_int_PbHole = 0;

        pr_int_index = 0;

        maplist.Clear();

        CreatMaptile(0);
    }
    
}
