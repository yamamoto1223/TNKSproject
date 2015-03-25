using UnityEngine;
using System.Collections;

namespace character
{

    public class KnightBase : CharacterPlayerUnit
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject base_object;  // 自身のキャラクターオブジェクト
        public GameObject anim_object;  // アニメーションオブジェクト

        // Use this for initialization
        void Start()
        {
            _character = GameObject.Find("character_knight");
            _animator = anim_object.GetComponent<Animator>();
            //_animator = GameObject.Find("body").GetComponent<Animator>();
            //_animator = GetComponent<Animator>();

            // 初期化
            InitPlayerUnit();
        }

        // Update is called once per frame
        void Update()
        {
            // キャラクタの基本動作
            CharacterBaseUpdate();

            // アニメーション
            BaseAnimation();
        }
    }
}
