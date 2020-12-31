using System.Collections;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/PlayerController")]
    public class PlayerController : GameScript
    {
        [SerializeField]
        [Tooltip("Le numéro du joueur selon sa manette.")]
        private int playerControlerIndex;

        [SerializeField]
        [Tooltip("The player's main weapon")]
        private GameObject firstWeapon;

        [SerializeField]
        [Tooltip("The player's secondary weapon")]
        private GameObject secondWeapon;

        private RangedWeapon mainWeaponControl;
        private RangedWeapon subWeaponControl;
        private bool isUsingMainWeapon = true;

        private KillableObject health;
        private PlayerInputSensor playerInputSensor;
        private PlayerMovement playerMovement;
        private RangedWeapon rangedWeapon;
        private Metal metalSubmarineQuantity;
        private TurretSpawner turretSpawner;
        private MeleeWeaponController meleeWeaponController;
        private DepositDetectSensor depositDetectSensor;
        private MetalDepositController depositController;
        private AbilityController abilityCaster;
        private PlayerDeathEventChannel eventChannel;
        private CharacterStatistics stats;

        public RangedWeapon GetCurretWeapon { get { return isUsingMainWeapon ? mainWeaponControl : subWeaponControl; } }
        public RangedWeapon GetMainWeapon { get { return mainWeaponControl; } }
        public RangedWeapon GetSubWeapon { get { return subWeaponControl; } }

        private void InjectPlayerController(
                                           [ApplicationScope] PlayerInputSensor playerInputSensor,
                                           [TagScope(R.S.Tag.Submarine)] Metal submarineMetal,
                                           [GameObjectScope] KillableObject health,
                                           [GameObjectScope] PlayerMovement playerMovement,
                                           [EntityScope] MeleeWeaponController meleeWeaponController,
                                           [EntityScope] TurretSpawner turretSpawner,
                                           [EntityScope] AbilityController abilityCaster,
                                           [EntityScope] CharacterStatistics stats,
                                           [EventChannelScope] PlayerDeathEventChannel eventChannel)
        {
            metalSubmarineQuantity = submarineMetal;
            this.eventChannel = eventChannel;
            this.health = health;
            this.playerInputSensor = playerInputSensor;
            this.playerMovement = playerMovement;
            this.turretSpawner = turretSpawner;
            this.meleeWeaponController = meleeWeaponController;
            this.abilityCaster = abilityCaster;
            this.stats = stats;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerController");

            depositController = null;

            depositDetectSensor = transform.parent.gameObject.GetComponentInChildren<DepositDetectSensor>();


            GameObject createdWeapon = Instantiate(firstWeapon, transform.position + Vector3.up, transform.rotation);
            createdWeapon.transform.parent = transform;
            mainWeaponControl = createdWeapon.GetComponent<RangedWeapon>();
            createdWeapon = Instantiate(secondWeapon, transform.position + Vector3.up, transform.rotation);
            createdWeapon.transform.parent = transform;
            subWeaponControl = createdWeapon.GetComponent<RangedWeapon>();

        }

        private void OnEnable()
        {
            depositDetectSensor.OnDetectDepositEnter += DepositInRange;
            depositDetectSensor.OnDetectDepositExit += DepositNotInRange;

            health.OnDeath += PlayerDead;

            playerInputSensor.Players[playerControlerIndex].OnMoveX += OnMoveX;
            playerInputSensor.Players[playerControlerIndex].OnMoveY += OnMoveY;
            playerInputSensor.Players[playerControlerIndex].OnAimX += OnAimX;
            playerInputSensor.Players[playerControlerIndex].OnAimY += OnAimY;

            playerInputSensor.Players[playerControlerIndex].OnSkill1 += OnSkill1;
            playerInputSensor.Players[playerControlerIndex].OnBuild += OnBuild;
            playerInputSensor.Players[playerControlerIndex].OnSkill2 += OnSkill2;
            playerInputSensor.Players[playerControlerIndex].OnRepair += OnRepair;

            playerInputSensor.Players[playerControlerIndex].OnSwitchWeapon += OnSwitchWeapon;
            playerInputSensor.Players[playerControlerIndex].OnMeleeAttack += OnMeleeAttack;
            playerInputSensor.Players[playerControlerIndex].OnFire += OnFire;
            playerInputSensor.Players[playerControlerIndex].OnTogglePause += OnTogglePause;
        }

        private void OnDisable()
        {
            depositDetectSensor.OnDetectDepositEnter -= DepositInRange;
            depositDetectSensor.OnDetectDepositExit -= DepositNotInRange;

            health.OnDeath -= PlayerDead;

            playerInputSensor.Players[playerControlerIndex].OnMoveX -= OnMoveX;
            playerInputSensor.Players[playerControlerIndex].OnMoveY -= OnMoveY;
            playerInputSensor.Players[playerControlerIndex].OnAimX -= OnAimX;
            playerInputSensor.Players[playerControlerIndex].OnAimY -= OnAimY;

            playerInputSensor.Players[playerControlerIndex].OnSkill1 -= OnSkill1;
            playerInputSensor.Players[playerControlerIndex].OnBuild -= OnBuild;
            playerInputSensor.Players[playerControlerIndex].OnSkill2 -= OnSkill2;
            playerInputSensor.Players[playerControlerIndex].OnRepair -= OnRepair;

            playerInputSensor.Players[playerControlerIndex].OnSwitchWeapon -= OnSwitchWeapon;
            playerInputSensor.Players[playerControlerIndex].OnMeleeAttack -= OnMeleeAttack;
            playerInputSensor.Players[playerControlerIndex].OnFire -= OnFire;
            playerInputSensor.Players[playerControlerIndex].OnTogglePause -= OnTogglePause;
        }

        public void Configure()
        {
            health.ResetLife();
            health.ResetArmor();
        }

        private void PlayerDead()
        {
            eventChannel.Publish(new PlayerDeathEvent());
        }

        private void OnMoveX(float impulse)
        {
            playerMovement.MoveX(impulse);

        }

        private void OnMoveY(float impulse)
        {
            playerMovement.MoveY(impulse);

        }

        private void OnAimX(float impulse)
        {
            playerMovement.AimX(impulse);
        }

        private void OnAimY(float impulse)
        {
            playerMovement.AimY(impulse);
        }

        private void OnSkill1()
        {
            if (!stats.IsPacified())
            {
                abilityCaster.CastFirstAbility();
            }
            
        }

        private void OnBuild()
        {
            if (!stats.IsPacified())
            {
                if (depositController != null)
                {
                    if (depositController.GetExtractorPrice() < metalSubmarineQuantity.MetalQuantity)
                    {
                        metalSubmarineQuantity.ReduceMetalQuantity(depositController.GetExtractorPrice());
                    }
                    else
                    {
                        metalSubmarineQuantity.ReduceMetalQuantity((metalSubmarineQuantity.MetalQuantity - 1));
                    }
                    depositController.CreateExtractor();
                }
                else if (turretSpawner.GetTurretPrice() < metalSubmarineQuantity.MetalQuantity)
                {
                    turretSpawner.Spawn();
                    metalSubmarineQuantity.ReduceMetalQuantity(turretSpawner.GetTurretPrice());
                }
            }

        }

        private void OnSkill2()
        {
            if (!stats.IsPacified())
            {
                abilityCaster.CastSecondAbility();
            }

        }

        private void OnRepair()
        {
            if (!stats.IsPacified())
            {
                //Does nothing for now  
            }

        }

        private void OnSwitchWeapon()
        {
            if (!stats.IsPacified())
            {
                isUsingMainWeapon = !isUsingMainWeapon;
            }

        }

        private void OnMeleeAttack()
        {
            if (!stats.IsPacified())
            {
                meleeWeaponController.Attack();
            }

        }

        private void OnFire()
        {
            if (!stats.IsPacified())
            {
                if (isUsingMainWeapon)
                {
                    mainWeaponControl.Fire();
                }
                else
                {
                    subWeaponControl.Fire();
                }
            }


        }

        private void OnTogglePause()
        {
            //Does nothing for now 
        }

        private void DepositInRange(GameObject deposit)
        {
            depositController = deposit.transform.parent.gameObject.GetComponentInChildren<MetalDepositController>();
        }

        private void DepositNotInRange(GameObject deposit)
        {
            depositController = null;
        }


    }
}