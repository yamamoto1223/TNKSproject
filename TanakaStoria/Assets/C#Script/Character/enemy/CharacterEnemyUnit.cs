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

        protected void InitEnemyUnit()
        {
            // 初期化
            _move_speed = 1.0f;
            _direction.Set(-1.0f, 0.0f);
        }


    }
}
