using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourScript : MonoBehaviour
{
    [SerializeField]
    private int healthPoints = 1000;

    public const int maxHealthPoints = 1000;

    /// <summary>
    /// Préfab de l'afficheur de texte
    /// </summary>
    [SerializeField]
    private GameObject afficheurPrefab;

    /// <summary>
    /// Un afficheur de texte concrétisé.
    /// </summary>
    private GameObject afficheur;

    // Use this for initialization
    void Start ()
    {
        //On instancie un afficheur à partir du préfab
        afficheur = Instantiate(afficheurPrefab);

        //On obtient la position de la tour actuelle.
        Vector3 v = gameObject.transform.position;

        //Notre afficheur aura la position de la tour + 2 en y pour le mettre juste au dessus.
        afficheur.transform.position = new Vector3(v.x, v.y + 4, v.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        AffichageDeTexte();
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.layer == LayerMask.NameToLayer("blueTower") && col.gameObject.layer == LayerMask.NameToLayer("redBallHitBox") ||
            gameObject.layer == LayerMask.NameToLayer("redTower") && col.gameObject.layer == LayerMask.NameToLayer("blueBallHitBox"))
        {

            Debug.Log(gameObject.name + " a été attaqué par " + col.gameObject.name);
            healthPoints -= col.gameObject.GetComponent<ProjectileScript>().Damage;

            //On détruit la balle une fois qu'elle heurte une tour.
            Destroy(col.gameObject);
        }
    }

    public void AffichageDeTexte()
    {
        if (afficheur != null)
        {
            TextMesh textmesh = afficheur.GetComponent<TextMesh>();
            textmesh.text = healthPoints + "/" + maxHealthPoints;
        }
    }
}
