using UnityEngine;
using System.Collections;

namespace character
{
    public class CharacterEnemyUnit : CharacterBase
    {

        // Use this for initialization
        void Start()
        {
            // この箇所に記述しても子クラスには影響がない
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected void EnemyUnitInit()
        {
            // 初期化
            _move_speed = 3.0f;
            _direction.Set(-1.0f, 0.0f);
        }

        // Collider2D
        void OnTriggerEnter2D(Collider2D unit_collider)
        {
            // レイヤー名を取得
            string layer_name = LayerMask.LayerToName(unit_collider.gameObject.layer);

            // 反対勢力ユニットの場合
            if (layer_name == "PlayerUnit" && _target_object == null)
            {
                _target_object = unit_collider.gameObject;
                _move_speed = 0.0f;

            }
        }


    }
}
