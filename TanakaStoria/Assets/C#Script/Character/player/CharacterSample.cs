using UnityEngine;
using System.Collections;

namespace character
{
    public class CharacterSample : CharacterPlayerUnit
    {
        // Use this for initialization
        void Start()
        {
            _charactor = GameObject.Find("body");
            _animator = GetComponent<Animator>();
        }

       

        // Update is called once per frame
        void Update()
        {
            //// キャラクタの基本動作
            //base.CharacterBaseUpdate();



            //// sample うろうろする
            //if (pos.x > 4.0f)
            //{
            //    direction.x = -1.0f;
            //}
            //if (pos.x < -4.0f)
            //{
            //    direction.x = 1.0f;
            //}
        }
    }
}
