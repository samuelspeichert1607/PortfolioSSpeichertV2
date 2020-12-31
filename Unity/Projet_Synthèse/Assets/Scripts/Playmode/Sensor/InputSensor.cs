namespace ProjetSynthese
{
    public abstract class InputSensor : GameScript
    {
        protected abstract class InputDevice : IInputDevice
        {
            public event UpEventHandler OnUp;
            public event DownEventHandler OnDown;
            public event ConfirmEventHandler OnConfirm;
            
            public event MoveXEventHandler OnMoveX;
            public event MoveYEventHandler OnMoveY;
            public event AimXEventHandler OnAimX;
            public event AimYEventHandler OnAimY;
            public event Skill1EventHandler OnSkill1;
            public event BuildEventHandler OnBuild;
            public event Skill2EventHandler OnSkill2;
            public event RepairEventHandler OnRepair;
            public event SwitchWeaponEventHandler OnSwitchWeapon;
            public event MeleeAttackEventHandler OnMeleeAttack;
            public event FireEventHandler OnFire;
            public event TogglePauseEventHandler OnTogglePause;

            public abstract IInputDevice this[int deviceIndex] { get; }


            // Menu
            protected virtual void NotifyUp()
            {
                if (OnUp != null) OnUp();
            }

            protected virtual void NotifyDown()
            {
                if (OnDown != null) OnDown();
            }

            protected virtual void NotifyConfirm()
            {
                if (OnConfirm != null) OnConfirm();
            }

            
            // Game
            protected virtual void NotifyMoveX(float intensity)
            {
                if (OnMoveX != null) OnMoveX(intensity);
            }

            protected virtual void NotifyMoveY(float intensity)
            {
                if (OnMoveY != null) OnMoveY(intensity);
            }

            protected virtual void NotifyAimX(float intensity)
            {
                if (OnAimX != null) OnAimX(intensity);
            }

            protected virtual void NotifyAimY(float intensity)
            {
                if (OnAimY != null) OnAimY(intensity);
            }

            protected virtual void NotifySkill1()
            {
                if (OnSkill1 != null) OnSkill1();
            }

            protected virtual void NotifyBuild()
            {
                if (OnBuild != null) OnBuild();
            }

            protected virtual void NotifySkill2()
            {
                if (OnSkill2 != null) OnSkill2();
            }

            protected virtual void NotifyRepair()
            {
                if (OnRepair != null) OnRepair();
            }

            protected virtual void NotifySwitchWeapon()
            {
                if (OnSwitchWeapon != null) OnSwitchWeapon();
            }

            protected virtual void NotifyMeleeAttack()
            {
                if (OnMeleeAttack != null) OnMeleeAttack();
            }

            protected virtual void NotifyFire()
            {
                if (OnFire != null) OnFire();
            }

            protected virtual void NotifyTogglePause()
            {
                if (OnTogglePause != null) OnTogglePause();
            }
        }

        protected abstract class TriggerOncePerFrameInputDevice : InputDevice
        {
            //Menu
            private bool upTriggered;
            private bool downTriggered;
            private bool confirmedTriggered;

            //Game
            private bool moveXTriggered;
            private bool moveYTriggered;
            private bool aimXTriggered;
            private bool aimYTriggered;

            private bool skill1Triggered;
            private bool buildTriggered;
            private bool skill2Triggered;
            private bool repairTriggered;

            private bool switchWeaponTriggered;
            private bool meleeAttackTriggered;
            private bool fireTriggered;
            private bool togglePauseTriggered;

            public abstract override IInputDevice this[int deviceIndex] { get; }

            public virtual void Reset()
            {
                //Menu
                upTriggered = false;
                downTriggered = false;
                confirmedTriggered = false;


                //Game
                moveXTriggered = false;
                moveYTriggered = false;
                aimXTriggered = false;
                aimYTriggered = false;

                skill1Triggered = false;
                buildTriggered = false;
                skill2Triggered = false;
                repairTriggered = false;

                switchWeaponTriggered = false;
                meleeAttackTriggered = false;   
                fireTriggered = false;
                togglePauseTriggered = false;
            }

            //Menu
            protected override void NotifyUp()
            {
                if (!upTriggered)
                {
                    base.NotifyUp();
                    upTriggered = true;
                }
            }

            protected override void NotifyDown()
            {
                if (!downTriggered)
                {
                    base.NotifyDown();
                    downTriggered = true;
                }
            }

            protected override void NotifyConfirm()
            {
                if (!confirmedTriggered)
                {
                    base.NotifyConfirm();
                    confirmedTriggered = true;
                }
            }

            //Game

            protected override void NotifyMoveX(float intensity)
            {
                if (!moveXTriggered)
                {
                    base.NotifyMoveX(intensity);
                    moveXTriggered = true;
                }
            }

            protected override void NotifyMoveY(float intensity)
            {
                if (!moveYTriggered)
                {
                    base.NotifyMoveY(intensity);
                    moveYTriggered = true;
                }
            }

            protected override void NotifyAimX(float intensity)
            {
                if (!aimXTriggered)
                {
                    base.NotifyAimX(intensity);
                    aimXTriggered = true;
                }
            }

            protected override void NotifyAimY(float intensity)
            {
                if (!aimYTriggered)
                {
                    base.NotifyAimY(intensity);
                    aimYTriggered = true;
                }
            }

            protected override void NotifySkill1()
            {
                if (!skill1Triggered)
                {
                    base.NotifySkill1();
                    skill1Triggered = true;
                }
            }

            protected override void NotifyBuild()
            {
                if (!buildTriggered)
                {
                    base.NotifyBuild();
                    buildTriggered = true;
                }
            }

            protected override void NotifySkill2()
            {
                if (!skill2Triggered)
                {
                    base.NotifySkill2();
                    skill2Triggered = true;
                }
            }

            protected override void NotifyRepair()
            {
                if (!repairTriggered)
                {
                    base.NotifyRepair();
                    repairTriggered = true;
                }
            }

            protected override void NotifySwitchWeapon()
            {
                if (!switchWeaponTriggered)
                {
                    base.NotifySwitchWeapon();
                    switchWeaponTriggered = true;
                }
            }

            protected override void NotifyMeleeAttack()
            {
                if (!meleeAttackTriggered)
                {
                    base.NotifyMeleeAttack();
                    meleeAttackTriggered = true;
                }
            }

            protected override void NotifyFire()
            {
                if (!fireTriggered)
                {
                    base.NotifyFire();
                    fireTriggered = true;
                }
            }

            protected override void NotifyTogglePause()
            {
                if (!togglePauseTriggered)
                {
                    base.NotifyTogglePause();
                    togglePauseTriggered = true;
                }
            }

        }
    }
}