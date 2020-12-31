using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/EpongeGeanteController")]
    public class EpongeGeanteController : MonsterController
    {
        [SerializeField]
        [Tooltip("Songe part that will be spawned around the ennemi")]
        private GameObject spongePart;

        [SerializeField]
        [Tooltip("Lenght and height of the square songe parts that will be spawned")]
        private double spongeDimention;

        [SerializeField]
        [Tooltip("radious of the sponge parts that will be spawned around ennemi at first")]
        private double spongeRadiousBeginning;

        private float currentSpongeRadious;

        private int lvlOfAnger = 0;

        private bool playerSpotted;

        private GameObject target;

        private List<Vector2> SpongesPlaced;

        protected void InjectEpongeGeanteState([TopParentScope] GameObject topParentGameObject,
                                       [EntityScope] PlayerDetectSensor playerSensor,
                                       [EntityScope] KillableObject health,
                                       [EntityScope] PlayerInRangeSensor playerInRangeSensor)
        {
            this.topParentGameObject = topParentGameObject;
            this.playerDetectSensor = playerSensor;
            this.playerInRangeSensor = playerInRangeSensor;
            this.health = health;
        }

        protected override void Awake()
        {
            spongePart.transform.localScale = new Vector2((float)spongeDimention, (float)spongeDimention);
            currentSpongeRadious = (float)spongeRadiousBeginning;
            InjectDependencies("InjectEpongeGeanteState");
        }

        //implémenté pour que le start de la super classe ne soit pas appelé
        protected override void Start()
        {
            SpawnSponges();
            state = null;
        }

        protected override void Update()
        {

        }
        /// <summary>
        /// fait apparître des éponges autour de l'énnemi dans un rayon déterminé par currentSpongeRadious.
        /// Lors de la création des éponges, tous les joueurs seront poussés à l'extérieur de la zone d'éponges,
        /// tout ce qui n'est pas un joueur et qui a de la vie sera détruit
        /// </summary>
        private void SpawnSponges()
        {
            //on s'occupe de tous les objets qui sont où seront les éponges
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(gameObject.transform.position,
                                                                  (int)currentSpongeRadious,
                                                                  Vector2.zero))
            {
                GameObject collidedObject = hit.collider.gameObject;
                if (collidedObject != topParentGameObject)
                {
                    //on déplace les joueurs
                    if (collidedObject.layer == LayerMask.NameToLayer(R.S.Layer.Player))
                    {
                        collidedObject.transform.position = getTargetNewPosition(collidedObject.transform.position);
                    }
                    //on tue tout ce qui n'est pas un joueur ou et qui a de la vie
                    else if (collidedObject.GetComponentInChildren<KillableObject>() != null)
                    {
                        collidedObject.GetComponentInChildren<KillableObject>().GetComponentInChildren<KillableObject>().ReceiveDamage(int.MaxValue);
                    }
                }
            }
            SpongesPlaced = new List<Vector2>();
            SpawnSpongesAround(gameObject.transform.position);
        }

        private void SpawnSpongesAround(Vector2 currentPosition)
        {
            if (currentPosition.x != gameObject.transform.position.x || currentPosition.y != gameObject.transform.position.y)
            {
                Instantiate(spongePart, currentPosition, gameObject.transform.rotation);
            }
            SpongesPlaced.Add(currentPosition);
            float dimention = (float)spongeDimention;
            Vector2 nextSponge = new Vector2(currentPosition.x, currentPosition.y + dimention);
            if (!IsSpongeThere(nextSponge) && !IsWallThere(nextSponge) && GetDisatanceToCenter(nextSponge) <= currentSpongeRadious)
            {
                SpawnSpongesAround(nextSponge);
            }
            nextSponge = new Vector2(currentPosition.x, currentPosition.y - dimention);
            if (!IsSpongeThere(nextSponge) && !IsWallThere(nextSponge) && GetDisatanceToCenter(nextSponge) <= currentSpongeRadious)
            {
                SpawnSpongesAround(nextSponge);
            }
            nextSponge = new Vector2(currentPosition.x + dimention, currentPosition.y);
            if (!IsSpongeThere(nextSponge) && !IsWallThere(nextSponge) && GetDisatanceToCenter(nextSponge) <= currentSpongeRadious)
            {
                SpawnSpongesAround(nextSponge);
            }
            nextSponge = new Vector2(currentPosition.x - dimention, currentPosition.y);
            if (!IsSpongeThere(nextSponge) && !IsWallThere(nextSponge) && GetDisatanceToCenter(nextSponge) <= currentSpongeRadious)
            {
                SpawnSpongesAround(nextSponge);
            }

        }

        /// <summary>
        /// détermine si un mur toucherais une éponge placée à la position donnée
        /// </summary>
        /// <param name="position">position où serait placé l'éponge</param>
        /// <returns>vrai s'il y a un mur, faux s'il n'y a pas de mur</returns>
        private bool IsWallThere(Vector2 position)
        {
            foreach (RaycastHit2D hit in Physics2D.BoxCastAll(position,
                                                              Vector2.one * (float)spongeDimention,
                                                              0,
                                                              Vector2.zero))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer(R.S.Layer.WallDetection))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// détermine si'il y a déjà une éponge à la position donnée
        /// </summary>
        /// <param name="position">position à regarder</param>
        /// <returns>vrai s'il y a une éponge, faux s'il n'y en a pas</returns>
        private bool IsSpongeThere(Vector2 position)
        {
            for (int i = 0; i < SpongesPlaced.Count; i++)
            {
                if (SpongesPlaced[i] == position)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// détermine la distance entre le centre de l'ennemi et un point donné avec pythagore
        /// </summary>
        /// <param name="position">position dont nous devons trouver la distance du centre</param>
        /// <returns>la distance entre le centre de l'ennemi et le point donné</returns>
        private float GetDisatanceToCenter(Vector2 position)
        {
            return Mathf.Sqrt(Mathf.Pow(position.x - gameObject.transform.position.x, 2) + Mathf.Pow(position.y - gameObject.transform.position.y, 2));
        }

        /// <summary>
        /// Trouve la nouvelle position d'un joueur qui serais pris dans les éponges
        /// </summary>
        /// <param name="targetPostion">position du joueur pris dans les éponges</param>
        /// <returns>la nouvelle position du joueur</returns>
        private Vector2 getTargetNewPosition(Vector2 targetPostion)
        {
            Vector2 myPosition = topParentGameObject.transform.position;
            float hypotenuse = Mathf.Sqrt(Mathf.Pow(targetPostion.x - myPosition.x, 2) + Mathf.Pow(targetPostion.y - myPosition.y, 2));
            float multiplicateur = currentSpongeRadious / hypotenuse;
            float newX;
            float newY;
            if (targetPostion.x > myPosition.x)
            {
                newX = myPosition.x + ((targetPostion.x - myPosition.x) * multiplicateur);
            }
            else
            {
                newX = myPosition.x - ((myPosition.x - targetPostion.x) * multiplicateur);
            }
            if (targetPostion.y > myPosition.y)
            {
                newY = myPosition.y + ((targetPostion.y - myPosition.y) * multiplicateur);
            }
            else
            {
                newY = myPosition.y - ((myPosition.y - targetPostion.y) * multiplicateur);
            }
            return new Vector2(newX, newY);
        }

        protected override void OnHealthChanged(int currentHealthPoints, int maxHealthPoints)
        {
            if (lvlOfAnger == 0)
            {
                lvlOfAnger = 1;
            }
            else if (lvlOfAnger == 1 && currentHealthPoints <= maxHealthPoints * 3 / 4)
            {
                currentSpongeRadious = (float)(spongeRadiousBeginning * 2);
                lvlOfAnger = 2;
                SpawnSponges();
            }
            else if (lvlOfAnger == 2 && currentHealthPoints <= maxHealthPoints * 2 / 4)
            {
                currentSpongeRadious = (float)(spongeRadiousBeginning * 3);
                lvlOfAnger = 3;
                SpawnSponges();
            }
            else if (lvlOfAnger == 3 && currentHealthPoints <= maxHealthPoints * 1 / 4)
            {
                currentSpongeRadious = (float)(spongeRadiousBeginning * 4);
                lvlOfAnger = 4;
                SpawnSponges();
            }
        }

        protected override void OnPlayerEnterDetected(GameObject player)
        {
            if (player != target)
            {
                playerSpotted = true;
            }
        }

        protected override void OnPlayerExitDetected(GameObject player)
        {
            if (player == target)
            {
                playerSpotted = false;
            }
        }

        protected override void OnPlayerInRangeDetected(GameObject player)
        {
            if (player == target)
            {
                playerSpotted = false;
            }
        }

        protected override void OnPlayerOutOfRangeDetected(GameObject player)
        {
            if (player == target)
            {
                playerSpotted = false;
            }
        }

        //implemented so that it won't move like the super class does
        public override void Move(Vector2 destination)
        {

        }

    }
}
