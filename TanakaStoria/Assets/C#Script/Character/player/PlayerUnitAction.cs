using UnityEngine;
using System.Collections;

using define;

namespace character
{

    public class PlayerUnitAction : PlayerUnitBase
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject objBase;          // 自身のキャラクターオブジェクト
        public GameObject objAnimation;     // アニメーションオブジェクト
        public GameObject objEffectCreater; // エフェクト作成

        // 状態遷移
        public State_Walk walk = new State_Walk();
        public State_Stay stay = new State_Stay();
        public State_Attack attack = new State_Attack();
        public State_Battle battle = new State_Battle();
        public CharacterBase parent;

        // Use this for initialization
        void Start()
        {
            cnpAnimator = objAnimation.GetComponent<Animator>();

            // 初期化
            CharacterBaseInit();
            PlayerUnitInit();

            // stateクラス初期化
            cStateManager.SetUnitInstance(this);
            cStateManager.SetAnimatorObj(objAnimation);
            cStateManager.SetEffectCreater(GetEffectCreater());
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

        // エフェクト取得
        public EffectCreater GetEffectCreater()
        {
            return objEffectCreater.GetComponent<EffectCreater>();
        }

        // Collider2D
        void OnTriggerStay2D(Collider2D unit_collider)
        {
            // レイヤー名を取得
            string layer_name = LayerMask.LayerToName(unit_collider.gameObject.layer);

            // 反対勢力ユニットの場合
            if (layer_name == "EnemyUnit" && objTargetObject == null)
            {
                objTargetObject = unit_collider.gameObject;
                //EnemyUnitBase enemy = objTargetObject.GetComponent<EnemyUnitBase>();
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
                if (unit.objTargetObject == null)
                {
                    unit.cStateManager.ChangeState(new State_Walk());
                    return;
                }
                // アニメーションデータ
                GameObject animator_obj = unit.cStateManager.GetAnimatorObj();
                Animator animator = animator_obj.GetComponent<Animator>();
                AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
                     
                if (animInfo.nameHash == Animator.StringToHash("Base Layer.Attack"))
                {
                    //if( !animation.isPlaying )
                    if (animInfo.normalizedTime > 1.0f)
                    {
                        // 敵情報
                        EnemyUnitBase enemy = unit.objTargetObject.GetComponent<EnemyUnitBase>();
                        
                        // ダメージ計算
                        int hp = enemy.GetUnitHp();
                        enemy.SetUnitHp(hp - unit.iUnitAtk);
                        if (enemy.GetUnitHp() <= 0) 
                        {
                            Destroy(unit.objTargetObject);
                        }
                        // エフェクト
                        effect.CreateEffect_DamageNum(enemy.vUnitPosition2D, unit.iUnitAtk);
         

                        if (unit.objTargetObject != null)
                        {
                            unit.cStateManager.ChangeState(new State_Battle());
                            return;
                        }
                        else 
                        {
                            unit.cStateManager.ChangeState(new State_Walk());
                            return;
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
