using UnityEngine;
using System.Collections;

public class EffectCreater : MonoBehaviour {

    public GameObject SampleEffect;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateEffect(Vector3 pos)
    {
        // エフェクト
        GameObject obj = (GameObject)Instantiate(SampleEffect, pos, Quaternion.identity);
        Effect_DamageNumGroup c = obj.GetComponent<Effect_DamageNumGroup>();
        c.SetBaseObject(obj);
        c.SetDamageNum("9999");
    }
}
