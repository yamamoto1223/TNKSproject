using UnityEngine;
using System.Collections;

namespace character
{
    // 敵オブジェクトサンプル
    public class DarkKnightBase : CharacterEnemyUnit
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject anim_object;

        // Use this for initialization
        void Start()
        {
            _charactor = GameObject.Find("character_knight");
            _animator = anim_object.GetComponent<Animator>();

            // 初期化
            InitEnemyUnit();
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
