using UnityEngine;
using System.Collections;

using define;

namespace character
{

    public class PlayerUnitAction : PlayerUnitBase
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
            _animator = anim_object.GetComponent<Animator>();

            // 初期化
            CharacterBaseInit();
            PlayerUnitInit();

            // stateクラス初期化
            state_manager.SetUnitInstance(this);
            state_manager.SetAnimatorObj(anim_object);
            state_manager.InitializeState(walk);

        }

        // Update is called once per frame
        void Update()
        {
            // キャラクタの基本動作
            CharacterBaseUpdate();
            PlayerUnitUpdate();

            // stateクラス実行
            state_manager.StateExe();
        }

        // Collider2D
        void OnTriggerStay2D(Collider2D unit_collider)
        {
            // レイヤー名を取得
            string layer_name = LayerMask.LayerToName(unit_collider.gameObject.layer);

            // 反対勢力ユニットの場合
            if (layer_name == "EnemyUnit" && _target_object == null)
            {
                _target_object = unit_collider.gameObject;
                
            }
        }

        //　待機
        public class State_Stay : State
        {
            public override void Init()
            {
                unit._move_speed = 0.0f;
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
                unit._move_speed = 1.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_WALK);
            }
            public override void Exe()
            {
                if (unit._target_object != null)
                {
                    unit.state_manager.ChangeState(new State_Attack());
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
                unit._move_speed = 0.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_STAY);
            }
            public override void Exe()
            {
                //GameObject animator_obj = unit.state_manager.GetAnimatorObj();
                // 敵がいない場合は移動
                if (unit._target_object == null)
                {
                    unit.state_manager.ChangeState(new State_Walk());
                }

                // 時間経過で攻撃へ
                if (unit._target_object != null)
                {
                    attack_cnt++;
                    if (attack_cnt > 120)
                    {
                        unit.state_manager.ChangeState(new State_Attack());
                    }
                }
            }
            public override void Exit()
            {
                //unit._target_object = null;
            }
        }

        // 攻撃
        public class State_Attack : State
        { 
            public override void Init()
            {
                unit._move_speed = 0.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTIOM_ATTACK);
            }
            public override void Exe()
            {
                // アニメーションデータ
                GameObject animator_obj = unit.state_manager.GetAnimatorObj();
                Animator animator = animator_obj.GetComponent<Animator>();
                AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (animInfo.nameHash == Animator.StringToHash("Base Layer.Attack"))
                {
                    //if( !animation.isPlaying )
                    if (animInfo.normalizedTime > 1.0f)
                    {
                        // 敵消去
                        Destroy(unit._target_object);
                        if (unit._target_object != null)
                        {
                            unit.state_manager.ChangeState(new State_Battle());
                        }
                        else 
                        {
                            unit.state_manager.ChangeState(new State_Walk());
                        }
                    }
                }
            }
            public override void Exit()
            {
                unit._target_object = null;
            }
            
        }

    }
}
