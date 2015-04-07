using UnityEngine;
using System.Collections;

namespace character
{
    public class EnemyUnitBase : CharacterBase
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
            fUnitMoveSpd = 5.0f;
            vDirection2D.Set(-1.0f, 0.0f);
        }

        // 実行
        protected void PlayerUnitUpdate()
        {

        }


    }
}
