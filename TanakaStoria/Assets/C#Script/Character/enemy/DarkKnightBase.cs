using UnityEngine;
using System.Collections;

namespace character
{
    // 敵オブジェクトサンプル
    public class DarkKnightBase : CharacterEnemyUnit
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject base_object;  // 自身のキャラクターオブジェクト
        public GameObject anim_object;  // アニメーションオブジェクト

        // Use this for initialization
        void Start()
        {
            //_character = GameObject.Find("character_dark_knight");
            _animator = anim_object.GetComponent<Animator>();

            // 初期化
            EnemyUnitInit();
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
