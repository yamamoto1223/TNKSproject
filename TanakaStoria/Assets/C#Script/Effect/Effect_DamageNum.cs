using UnityEngine;
using System.Collections;

public class Effect_DamageNum : MonoBehaviour {

    //public GameObject object;  // 自身のキャラクターオブジェクト
    public bool is_finish = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnAnimationFinish()
    {
        this.is_finish = true;
    }
}
