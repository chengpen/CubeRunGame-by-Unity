using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private Transform pr_Tf_camera;
    private Transform pr_Tf_Player;
    private Vector3 pr_V3_normal;
    private AudioSource pr_AS_bg;
    private bool pr_bl_IsFollow = false;

	void Start () {
        pr_AS_bg = gameObject.GetComponent<AudioSource>();
        pr_Tf_camera = gameObject.GetComponent<Transform>();
        pr_V3_normal = pr_Tf_camera.position;
        pr_Tf_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	void Update () {
        CameraMove();
	}
    
    public bool Isfollow
    {
        get { return pr_bl_IsFollow; }
        set { pr_bl_IsFollow = value; }
    }

    private void CameraMove()
    {
        if(pr_bl_IsFollow)
        {
            Vector3 nextPostion = new Vector3(pr_Tf_camera.position.x, pr_Tf_Player.position.y + 1.6f, pr_Tf_Player.position.z);
            pr_Tf_camera.position = Vector3.Lerp(pr_Tf_camera.position, nextPostion, Time.deltaTime);
        }
    }
    public void ResetCamera()
    {
        pr_Tf_camera.position = pr_V3_normal;
    }

    public void StartBG()
    {
        pr_AS_bg.Play();
    }
    public void StopBG()
    {
        pr_AS_bg.Stop();
    }
    public void PauseBG()
    {
        pr_AS_bg.Pause();
    }
}
