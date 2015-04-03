using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Effect_DamageNumGroup : MonoBehaviour {


    public GameObject base_object; 
    public GameObject child_object;
    public Effect_DamageNum damage_text;
    public Text text;

    public string damage_num;

    // Use this for initialization
    void Start () 
    {       
        text = child_object.GetComponent<Text>();
        damage_text = child_object.GetComponent<Effect_DamageNum>();

        // ダメージ値
        text.text = damage_num;
    }

    // Update is called once per frame
    void Update()
    {
        // アニメーション終了
        if (damage_text.is_finish)
        {
            Destroy(base_object);
            Destroy(child_object);
        }
        
    }
    // ダメージ値の代入
    public void SetDamageNum(string str) { damage_num = str; }
    public void SetBaseObject(GameObject obj) { base_object = obj; }
}
