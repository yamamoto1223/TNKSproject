using UnityEngine;
using System.Collections;

namespace character
{
    public class CharacterUnityChan : CharacterPlayerUnit
    {

        // Use this for initialization
        void Start()
        {
            _charactor = GameObject.Find("UnityChan2D");
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // キャラクタの基本動作
            base.CharacterBaseUpdate();

            // アニメーション
            BaseAnimation();

            // sample うろうろする
            if (pos.x > 4.0f)
            {
                direction.x = -1.0f;
            }
            if (pos.x < -4.0f)
            {
                direction.x = 1.0f;
            }
        }
    }
}
