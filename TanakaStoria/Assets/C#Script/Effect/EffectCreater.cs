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

    public void CreateEffect_DamageNum(Vector3 pos, int damage)
    {
        // 桁数取得
        int digit = damage.ToString().Length;

        // エフェクト
        GameObject obj = (GameObject)Instantiate(SampleEffect, pos, Quaternion.identity);
        Effect_DamageNumGroup effect = obj.GetComponent<Effect_DamageNumGroup>();
        effect.SetBaseObject(obj);
        effect.SetDamageNum(damage);
        effect.SetDamageStr("9999");
    }
}
