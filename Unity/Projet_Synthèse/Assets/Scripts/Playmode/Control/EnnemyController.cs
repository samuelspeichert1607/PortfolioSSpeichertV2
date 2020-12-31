using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/EnnemyController")]
    public class EnnemyController : GameScript
    {
        public enum StartingStates
        {
            Chase,
            Patrol,
            None
        }

        [SerializeField]
        [Tooltip("Seulement pour un ennemi qui patrol.\n" +
            "Tableau de game objects avec collider 2D trigger qui vont donner les points à atteindre en boucle pour le patrol de l'ennemi s'il le peut")]
        protected GameObject[] patrolPoints;

        [SerializeField]
        [Tooltip("Seulement pour un ennemi qui ne patrol pas.\n" +
            "Tableau de game objects avec collider 2D trigger qui vont donner les points à atteindre en boucle pour le patrol de l'ennemi s'il le peut")]
        protected GameObject mainTarget;

        [SerializeField]
        [Tooltip("L'état que l'ennemi doit commencer avec.\nCet état sera aussi l'état que l'ennemi va retourner par défaut lors de la fin d'un autre état.")]
        protected StartingStates startState;

        protected EnnemyState state;

        public EnnemyState State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }
        protected AiPath pathFinder;
        protected GameObject topParentGameObject;
        protected PlayerDetectSensor playerDetectSensor;
        protected PlayerInRangeSensor playerInRangeSensor;

        protected KillableObject health;
        protected EnnemyMover mover;
        protected CharacterStatistics stats;

        public GameObject MainTarget
        {
            get
            {
                return mainTarget;
            }

            private set
            {
                mainTarget = value;
            }
        }

        public GameObject[] PatrolPoints
        {
            get
            {
                return patrolPoints;
            }

            private set
            {
                patrolPoints = value;
            }
        }

        public StartingStates StartState
        {
            get
            {
                return startState;
            }

            private set
            {
                startState = value;
            }
        }



        protected void InjectEnnemiState([TopParentScope] GameObject topParentGameObject,
                                       [EntityScope] PlayerDetectSensor playerDetectSensor,
                                       [EntityScope] PlayerInRangeSensor playerInRangeSensor,
                                       [EntityScope] KillableObject health,
                                       [EntityScope] AiPath pathFinder,
                                       [EntityScope] EnnemyMover mover,
                                       [EntityScope] CharacterStatistics stats)
        {
            this.topParentGameObject = topParentGameObject;
            this.playerDetectSensor = playerDetectSensor;
            this.playerInRangeSensor = playerInRangeSensor;
            this.health = health;
            this.pathFinder = pathFinder;
            this.mover = mover;
            this.stats = stats;
        }

        protected virtual void Awake()
        {
            Inject();
        }

        protected void Inject()
        {
            InjectDependencies("InjectEnnemiState");
        }

        protected virtual void Start()
        {
            if (StartState == StartingStates.Patrol)
            {
                State = new Patrol(PatrolPoints, pathFinder, this, new List<GameObject>());
            }
            else if (StartState == StartingStates.Chase)
            {
                State = new Chase(mainTarget, pathFinder, this, new List<GameObject>());
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            State.Update();
        }

        private void OnEnable()
        {
            playerDetectSensor.OnDetectPlayerEnter += OnPlayerEnterDetected;
            playerDetectSensor.OnDetectPlayerExit += OnPlayerExitDetected;
            playerInRangeSensor.OnPlayerInRangeEnter += OnPlayerInRangeDetected;
            playerInRangeSensor.OnPlayerInRangeExit += OnPlayerOutOfRangeDetected;
            health.OnHealthChanged += OnHealthChanged;
            health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            playerDetectSensor.OnDetectPlayerEnter -= OnPlayerEnterDetected;
            playerDetectSensor.OnDetectPlayerExit -= OnPlayerExitDetected;
            playerInRangeSensor.OnPlayerInRangeEnter -= OnPlayerInRangeDetected;
            playerInRangeSensor.OnPlayerInRangeExit -= OnPlayerOutOfRangeDetected;
            health.OnHealthChanged -= OnHealthChanged;
            health.OnDeath -= OnDeath;
        }

        protected virtual void OnDeath()
        {
        }

        protected virtual void OnHealthChanged(int currentHealthPoints, int maxHealthPoints)
        {
        }

        protected virtual void OnPlayerEnterDetected(GameObject player)
        {
            State.OnPlayerEnterDetected(player);
        }

        protected virtual void OnPlayerExitDetected(GameObject player)
        {
            State.OnPlayerExitDetected(player);
        }

        protected virtual void OnPlayerInRangeDetected(GameObject player)
        {
            State.OnPlayerInRangeDetected(player);
        }

        protected virtual void OnPlayerOutOfRangeDetected(GameObject player)
        {
            State.OnPlayerOutOfRangeDetected(player);
        }

        public virtual void Move(Vector2 destination)
        {
            mover.Move(destination);
        }

        public virtual void Rotate(Vector2 destination)
        {
            mover.Turn(destination);
        }

        public void Attack()
        {
            if (stats.IsPacified())
            {
                ExecuteAttack();
            }
        }

        protected virtual void ExecuteAttack()
        {
            
        }
        
    }
}
