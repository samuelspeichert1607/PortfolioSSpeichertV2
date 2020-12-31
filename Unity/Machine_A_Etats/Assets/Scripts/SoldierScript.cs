using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    [SerializeField]
    private SoldierState soldierState;
    public SoldierState SoldierState
    {
        get { return soldierState; }
        set { soldierState = value; }
    }

    [SerializeField]
    private GameObject projectilePrefabBlue;

    [SerializeField]
    private GameObject projectilePrefabRed;

    /// <summary>
    /// L'objet que le soldat voudra attaquer. Ne sert pas pour le moment.
    /// </summary>
    private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }
    /// <summary>
    /// L'objet que le soldat voudra attaquer. Ne sert pas pour le moment.
    /// </summary>
    private GameObject initialTarget;
    public GameObject InitialTarget
    {
        get { return initialTarget; }
        set { initialTarget = value; }
    }

    /// <summary>
    /// Points de vie maximal du soldat.
    /// </summary>
    [SerializeField]
    public readonly int maxHealthPoints = 100;

    private int quartDeVie = 25;
    public int QuartDeVie
    {
        get { return quartDeVie; }
    }

    /// <summary>
    /// Points de vies du soldat.
    /// </summary>
    [SerializeField]
    private int healthPoints = 100;
    public int HealthPoints
    {
        get { return healthPoints; }
        set { healthPoints = value; }
    }

    /// <summary>
    /// L'endoit où se dirige actuellement le soldat
    /// </summary>
    [SerializeField]
    private Vector3 destination;
    public Vector3 Destination
    {
        get { return destination; }
        set { destination = value; }
    }

    /// <summary>
    /// L'objectif de base du soldat
    /// </summary>
    [SerializeField]
    private Vector3 destinationInitiale;
    public Vector3 InitialDestination
    {
        get { return destinationInitiale; }
        set { destinationInitiale = value; }
    }

    /// <summary>
    /// L'endroit ou le soldat a été crée
    /// </summary>
    [SerializeField]
    private Vector3 pointDeDepart;
    public Vector3 DeparturePoint
    {
        get { return pointDeDepart; }
        set { pointDeDepart = value; }
    }


    private bool isShooting = false;
    public bool IsShooting
    {
        get { return isShooting; }
        set { isShooting = value; }
    }

    /// <summary>
    /// Position du soldat dans une éventuelle collection conteneur.
    /// </summary>
    private int positionListe;
    public int PositionListe
    {
        set { positionListe = value; }
    }

    /// <summary>
    /// Les dommages que son arme doit causer.
    /// </summary>
    [SerializeField]
    private int bulletDamage = 10;
    public int BulletDamage
    {
        get { return bulletDamage; }
        set { bulletDamage = value; }
    }

    /// <summary>
    /// La cadence de tir de l'arme.
    /// </summary>
    [SerializeField]
    private float rateOfFire = 0.5f;
    public float RateOfFire
    {
        get { return rateOfFire; }
        set { rateOfFire = value; }
    }

    /// <summary>
    /// Le temps à attendre avant la prochaine séance de tir.
    /// </summary>
    private float nextFire;
    public float NextFire
    {
        get { return nextFire; }
        set { nextFire = value; }
    }

    /// <summary>
    /// La vitesse de déplacement du soldat.
    /// </summary>
    [SerializeField]
    private float speed = 10;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    /// <summary>
    /// Nombre de soldats adversaires tués.
    /// </summary>
    [SerializeField]
    private int kills = 0;
    public int Kills
    {
        get { return kills; }
        set { kills = value; }
    }

    /// <summary>
    /// Préfab de l'afficheur de texte
    /// </summary>
    [SerializeField]
    private GameObject afficheurPrefab;

    /// <summary>
    /// Un afficheur de texte concrétisé.
    /// </summary>
    private GameObject afficheur;

    /// <summary>
    /// Sprite du soldat mort (une flaque de sang).
    /// </summary>
    [SerializeField]
    private Sprite deathSprite;
    public Sprite DeathSprite
    {
        get { return deathSprite; }
        set { deathSprite = value; }
    }

    // Use this for initialization
    void Start()
    {
        //État soldat
        soldierState = new NormalState();

        //On instancie un afficheur à partir du préfab
        afficheur = Instantiate(afficheurPrefab);

        //On obtient la position du soldat actuel.
        Vector3 v = gameObject.transform.position;

        //Notre afficheur aura la position du soldat + 2 en y pour le mettre juste au dessus.
        afficheur.transform.position = new Vector3(v.x, v.y - 2, v.z);

        AffichageDeTexte("Normal");

        quartDeVie = maxHealthPoints / 4;
    }

    // Update is called once per frame
    void Update()
    {
        //On fait suivre l'afficheur...
        Vector3 v = this.gameObject.transform.position;
        afficheur.transform.position = new Vector3(v.x, v.y + 2, v.z);

        soldierState.Update(gameObject);

        if (healthPoints <= 0)
        {
            SoldierState = new DeathState();
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateOfFire;
            if (gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
            {
                GameObject bullet = Instantiate(projectilePrefabBlue, transform.position, transform.rotation);
                bullet.GetComponent<ProjectileScript>().Damage = BulletDamage;
            }
            if (gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                GameObject bullet = Instantiate(projectilePrefabRed, transform.position, transform.rotation);
                bullet.GetComponent<ProjectileScript>().Damage = BulletDamage;
            }
            soldierState.IsFiring(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(gameObject.name + positionListe.ToString() + " a touché à " + col.gameObject.name);

        if (gameObject.layer == LayerMask.NameToLayer("blueHitBox") && col.gameObject.layer == LayerMask.NameToLayer("redBallHitBox") ||
            gameObject.layer == LayerMask.NameToLayer("redHitBox") && col.gameObject.layer == LayerMask.NameToLayer("blueBallHitBox"))
        {
            Debug.Log(gameObject.name + positionListe.ToString() + " a été ATTAQUÉ par " + col.gameObject.name);
            healthPoints -= bulletDamage;
            Destroy(col.gameObject);
        }
    }

    public void AffichageDeTexte(string actualState)
    {
        if(afficheur != null)
        {
            TextMesh textmesh = afficheur.GetComponent<TextMesh>();
            textmesh.text = actualState + " " + healthPoints + "/" + maxHealthPoints;
        }
    }

    public void Die()
    {
        enabled = false;
        Destroy(afficheur);
        Destroy(this.gameObject, 2);
    }

   
}