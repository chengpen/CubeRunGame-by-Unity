using UnityEngine;
using System.Collections;

public class Smashing : MonoBehaviour {
    private Transform pr_Tf_Spikes;
    private Transform pr_Tf_SonSpikes;
    private Vector3 pr_V3_normalPos;
    private Vector3 pr_V3_TargetPos;
    void Start()
    {
        pr_Tf_Spikes = gameObject.GetComponent<Transform>();
        pr_Tf_SonSpikes = pr_Tf_Spikes.FindChild("smashing_spikes_b").GetComponent<Transform>();
        pr_V3_normalPos = pr_Tf_SonSpikes.position;
        pr_V3_TargetPos = pr_Tf_SonSpikes.position + new Vector3(0, 0.6f, 0);
        StartCoroutine("UpAndDown");
    }


    private IEnumerator UpAndDown()
    {
        while (true)
        {
            StopCoroutine("Down");
            StartCoroutine("Up");
            yield return new WaitForSeconds(2.0f);
            StopCoroutine("Up");
            StartCoroutine("Down");
            yield return new WaitForSeconds(2.0f);
        }
    }





    private IEnumerator Up()
    {
        while (true)
        {
            pr_Tf_SonSpikes.position = Vector3.Lerp(pr_Tf_SonSpikes.position, pr_V3_TargetPos, Time.deltaTime * 25);
            if (pr_Tf_SonSpikes.position.y - pr_Tf_Spikes.position.y > 0.6)
            {
                break;
            }
            yield return null;
        }
    }
    private IEnumerator Down()
    {
        while (true)
        {
            pr_Tf_SonSpikes.position = Vector3.Lerp(pr_Tf_SonSpikes.position, pr_V3_normalPos, Time.deltaTime * 25);
            if (pr_Tf_SonSpikes.position.y - pr_Tf_Spikes.position.y > 0.6)
            {
                break;
            }
            yield return null;
        }
    }

}
