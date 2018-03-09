using UnityEngine;
using System.Collections;

public class GemRotate : MonoBehaviour {
    private Transform pr_Tf_Gem;
    private Transform pr_Tf_GemCube;
	void Start () {
        pr_Tf_Gem = gameObject.GetComponent<Transform>();
        pr_Tf_GemCube = pr_Tf_Gem.FindChild("gem 3").GetComponent<Transform>();
	}

	void Update () {
        pr_Tf_GemCube.Rotate(Vector3.up*2);
	}
}
