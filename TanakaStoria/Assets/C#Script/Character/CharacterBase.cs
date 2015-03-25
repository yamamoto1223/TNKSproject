using UnityEngine;
using System.Collections;

using define;

namespace character
{
    public class CharacterBase : MonoBehaviour
    {
        //protected bool _isWalk = false;
        //protected bool _isStay = false;
        //protected bool _isAttack = false;
        //protected bool _isDead = false;
        //protected bool _isDamage = false;
        // private float _timer = 0.0f;

        protected float spd = 5.0f;
        protected Vector2 direction = new Vector2(1.0f, 0).normalized;  // 移動する向き
        protected Vector2 pos;  // 座標
        protected Vector2 vel;  // 速度

        protected GameObject    _charactor;
        protected Animator      _animator;
        private GameManager     _game_manager;     
      
        // アニメーションテーブル
        protected string[] AnimationTable = new string[(int)MotionIndex.MOTION_MAX]{
            "isStay",
            "isWalk",
            "isAttack",
            "isDamage",
            "isDead",
        };
        protected int animIndex;// = MotionIndex.MOTION_STAY;
        
        // Use this for initialization
        void Start()
        {
            

        }

        // Update is called once per frame
        void Update()
        {

        }

        // ユニットの基本動作
        protected void CharacterBaseUpdate()
        {
            // 座標・速度
            pos = rigidbody2D.position;   // 座標を取得
            vel = rigidbody2D.velocity;   // 速度を取得

            // 移動
            Move(direction);

            // アニメーション
            //BaseAnimation();

            // 自動スプライト反転
            SpriteReverse();
        }

        // animation
        protected void BaseAnimation()
        {

            // 移動
            if (vel.x < -0.1f || vel.x > 0.1f){
                animIndex = (int)MotionIndex.MOTION_WALK;
            // 待機
            }else{
                animIndex = (int)MotionIndex.MOTION_STAY;
            }

            // 攻撃
            if (pos.x >= 3.5f){
                animIndex = (int)MotionIndex.MOTIOM_ATTACK;
            }

            // アニメーション変更
            for (int i = 0; i < (int)MotionIndex.MOTION_MAX; i++)
            {
                bool animFlag = i == (int)animIndex;
                _animator.SetBool(AnimationTable[i], animFlag);

            }
        }

        protected void ChangeAnimation()
        { 

        }

        // reverse sprite
        protected void SpriteReverse()
        {
            Vector3 scale = transform.localScale; // サイズの取得
            if (vel.x >= 0)
            {
                scale.x = 1; // そのまま（右向き）
            }
            else
            {
                scale.x = -1; // 反転する（左向き）
            }
            transform.localScale = scale;
        }

        

        // move
        public void Move(Vector2 direction)
        {
            if (pos.x > 4.0f)
            {
                direction.Set(0.0f, 0.0f);
            }
            rigidbody2D.velocity = direction * spd;

        }


    }
}
