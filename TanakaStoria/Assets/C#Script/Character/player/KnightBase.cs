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
            //_character = GameObject.Find("character_knight");
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

            // 殺害
            if (_target_object != null)
            {
                Destroy(_target_object);
                _move_speed = 1.0f;
            }
        }

        // Collider2D
        void OnTriggerEnter2D(Collider2D unit_collider)
        {
            // レイヤー名を取得
            string layer_name = LayerMask.LayerToName(unit_collider.gameObject.layer);

            // 反対勢力ユニットの場合
            if (layer_name == "EnemyUnit" && _target_object == null)
            {
                _target_object = unit_collider.gameObject;
                _move_speed = 0.0f;
                
            }
        }
    }
}
