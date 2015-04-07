using UnityEngine;
using System.Collections;

using define;

namespace character
{
    // 敵オブジェクトサンプル
    public class EnemyUnitAction : EnemyUnitBase
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject base_object;  // 自身のキャラクターオブジェクト
        public GameObject anim_object;  // アニメーションオブジェクト

        // 状態遷移
        public State_Walk walk = new State_Walk();
        public State_Stay stay = new State_Stay();
        public State_Attack attack = new State_Attack();
        public State_Battle battle = new State_Battle();
        public CharacterBase parent;

        // Use this for initialization
        void Start()
        {
            //_character = GameObject.Find("character_dark_knight");
            cnpAnimator = anim_object.GetComponent<Animator>();

            // 初期化
            EnemyUnitInit();

            // stateクラス初期化
            cStateManager.SetUnitInstance(this);
            cStateManager.SetAnimatorObj(anim_object);
            cStateManager.InitializeState(walk);
        }

        // Update is called once per frame
        void Update()
        {
            // 座標・速度
            vUnitPosition2D = rigidbody2D.position;   // 座標を取得
            vUnitVelocity2D = rigidbody2D.velocity;   // 速度を取得

            // キャラクタの基本動作
            CharacterBaseUpdate();
            PlayerUnitUpdate();

            // stateクラス実行
            cStateManager.StateExe();
        }
        // Collider2D
        void OnTriggerStay2D(Collider2D unit_collider)
        {
            // レイヤー名を取得
            string layer_name = LayerMask.LayerToName(unit_collider.gameObject.layer);

            // 反対勢力ユニットの場合
            if (layer_name == "PlayerUnit" && objTargetObject == null)
            {
                objTargetObject = unit_collider.gameObject;

            }  
        }

        //　待機
        public class State_Stay : State
        {
            public override void Init()
            {
                unit.fUnitMoveSpd = 0.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_STAY);
            }
            public override void Exe()
            {

            }
            public override void Exit()
            {

            }
        }

        //　動く
        public class State_Walk : State
        {
            public override void Init()
            {
                unit.fUnitMoveSpd = 1.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_WALK);
            }
            public override void Exe()
            {
                if (unit.objTargetObject != null)
                {
                    unit.cStateManager.ChangeState(new State_Attack());
                }
            }
            public override void Exit()
            {

            }
        }

        // 戦闘
        public class State_Battle : State
        {
            private int attack_cnt = 0;
            public override void Init()
            {
                unit.fUnitMoveSpd = 0.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_STAY);
            }
            public override void Exe()
            {
                //GameObject animator_obj = unit.cStateManager.GetAnimatorObj();
                // 敵がいない場合は移動
                if (unit.objTargetObject == null)
                {
                    unit.cStateManager.ChangeState(new State_Walk());
                }

                // 時間経過で攻撃へ
                if (unit.objTargetObject != null)
                {
                    attack_cnt++;
                    if (attack_cnt > 120)
                    {
                        unit.cStateManager.ChangeState(new State_Attack());
                    }
                }
            }
            public override void Exit()
            {
                //unit.objTargetObject = null;
            }
        }

        // 攻撃
        public class State_Attack : State
        {
            public override void Init()
            {
                unit.fUnitMoveSpd = 0.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTIOM_ATTACK);
            }
            public override void Exe()
            {
                // アニメーションデータ
                GameObject animator_obj = unit.cStateManager.GetAnimatorObj();
                Animator animator = animator_obj.GetComponent<Animator>();
                AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (animInfo.nameHash == Animator.StringToHash("Base Layer.Attack"))
                {
                    //if( !animation.isPlaying )
                    if (animInfo.normalizedTime > 1.0f)
                    {
                        // 敵消去
                        //Destroy(unit.objTargetObject);
                        if (unit.objTargetObject != null)
                        {
                            unit.cStateManager.ChangeState(new State_Battle());
                        }
                        else
                        {
                            unit.cStateManager.ChangeState(new State_Walk());
                        }
                    }
                }
            }
            public override void Exit()
            {
                unit.objTargetObject = null;
            }

        }
    }
}
