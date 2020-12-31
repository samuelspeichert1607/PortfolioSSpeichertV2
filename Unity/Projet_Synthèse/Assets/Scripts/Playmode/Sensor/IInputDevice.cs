namespace ProjetSynthese
{
    //Menu actions
    public delegate void UpEventHandler();

    public delegate void DownEventHandler();

    public delegate void ConfirmEventHandler();


    //Game actions
    public delegate void MoveXEventHandler(float intensity);

    public delegate void MoveYEventHandler(float intensity);

    public delegate void AimXEventHandler(float intensity);

    public delegate void AimYEventHandler(float intensity);

    public delegate void Skill1EventHandler();

    public delegate void BuildEventHandler();

    public delegate void Skill2EventHandler();

    public delegate void RepairEventHandler();

    public delegate void SwitchWeaponEventHandler();

    public delegate void MeleeAttackEventHandler();

    public delegate void FireEventHandler();

    public delegate void TogglePauseEventHandler();


    public interface IInputDevice
    {
        event UpEventHandler OnUp;
        event DownEventHandler OnDown;
        event ConfirmEventHandler OnConfirm;



        
       
        event MoveXEventHandler OnMoveX;

        event MoveYEventHandler OnMoveY;

        event AimXEventHandler OnAimX;

        event AimYEventHandler OnAimY;

        event Skill1EventHandler OnSkill1;

        event BuildEventHandler OnBuild;

        event Skill2EventHandler OnSkill2;

        event RepairEventHandler OnRepair;

        event SwitchWeaponEventHandler OnSwitchWeapon;

        event MeleeAttackEventHandler OnMeleeAttack;

        event FireEventHandler OnFire;

        event TogglePauseEventHandler OnTogglePause;

        IInputDevice this[int deviceIndex] { get; }
    }
}