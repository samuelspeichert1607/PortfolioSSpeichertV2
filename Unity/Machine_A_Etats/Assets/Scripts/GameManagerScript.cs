using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Ceci est la classe qui gère les paramètres
/// de base du jeu, en plus de s'occuper de 
/// générer les tours et les soldats.
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    /// <summary>
    /// Nombre maximum de soldats à l'écran par camp.  Trouver un maximum pour votre jeu (pas obligé d'être 50)
    /// </summary>
    private const int maxNumberOfSoldiers = 50;

    /// <summary>
    /// Nombre de secondes entre les respawns, pas nécessairement utile pour le jeu final
    /// </summary>
    private const float cycleTime = 3.0f;

    /// <summary>
    /// Vitesse de déplacement des personnages, à ne pas nécessairement garder jusqu'à la fin
    /// </summary>
    //private const float vitesse = 5.0f;

    /// <summary>
    /// Les couleurs dans Unity sont de 0 à 1 dans le code, mais de 0 à 255 dans l'inspecteur.  Allez savoir pourquoi.
    /// </summary>
    private Color rouge = Color.red;
    private Color bleu = Color.blue;

    /// <summary>
    /// Lien dans l'inspecteur pour lier les prefabs de Soldat
    /// </summary>
    [SerializeField]
    private GameObject[] soldiersPrefabs;

    /// <summary>
    /// Lien dans l'inpecteur pour lier les tours bleus
    /// </summary>
    [SerializeField]
    private GameObject[] blueTowers;

    /// <summary>
    /// Lien dans l'inpecteur pour lier les tours rouges
    /// </summary>
    [SerializeField]
    private GameObject[] redTowers;

    /// <summary>
    /// Lien dans l'inpecteur pour lier les forêts
    /// </summary>
    [SerializeField]
    private GameObject[] Forets;

    /// <summary>
    /// tableaux fixes plutôt que Array. On préfère prendre de la mémoire que du temps machine
    /// </summary>
    private GameObject[] activeBlueSoldiers;

    /// <summary>
    /// tableaux fixes plutôt que Array. On préfère prendre de la mémoire que du temps machine
    /// </summary>
    private GameObject[] activeRedSoldiers;

    /// <summary>
    /// Compteur de temps pour le respwn de soldats probablement juste nécessaire pour le démo.
    /// </summary>
    private float tempsActuel = cycleTime;

    // Use this for initialization
    void Start ()
    {
        //On instatie nos tableaux et initialise le seed du randowm.
        activeBlueSoldiers = new GameObject[maxNumberOfSoldiers];
        activeRedSoldiers = new GameObject[maxNumberOfSoldiers];
        Random.InitState(System.DateTime.Now.Millisecond);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Time.deltaTime représente le temps depuis le dernier refresh d'écran.  Normalement c'est 1/60ème de seconde, mais ça peut varier légèrement.
        tempsActuel -= Time.deltaTime;

        //Si le temps tombe en dessous de zéro, on le ramène à son temps de départ et on spawn un nouveau soldat de chaque côté.
        if (tempsActuel <= 0.0f)
        {
            tempsActuel = cycleTime;
            SpawnSoldiers(activeBlueSoldiers, bleu, blueTowers, redTowers);
            SpawnSoldiers(activeRedSoldiers, rouge, redTowers, blueTowers);
        }

        //Chaque soldat instancié peut se déplacer.
        for (int i = 0; i < maxNumberOfSoldiers; i++)
        {
            if (activeBlueSoldiers[i] != null)
            {
                MoveSoldier(activeBlueSoldiers, i);
            }

            if (activeRedSoldiers[i] != null)
            {
                MoveSoldier(activeRedSoldiers, i);
            }
        }

        //Boucle for pour assigner une nouvelle tour à un soldat une fois que celle qui devais initialement détuire à été détruite.
        for (int i = 0; i < maxNumberOfSoldiers; i++)
        {
            if (activeBlueSoldiers[i] != null && activeBlueSoldiers[i].GetComponent<SoldierScript>().InitialTarget == null)
            {

                int j = Random.Range(0, redTowers.Length);

                while (redTowers[j] == null)
                {
                    j = Random.Range(0, redTowers.Length);
                }

                GameObject nouvelleTourACibler = redTowers[j];

                activeBlueSoldiers[i].GetComponent<SoldierScript>().Destination = nouvelleTourACibler.transform.position;

                activeBlueSoldiers[i].GetComponent<SoldierScript>().InitialTarget = nouvelleTourACibler;
            }

            if (activeRedSoldiers[i] != null && activeRedSoldiers[i].GetComponent<SoldierScript>().InitialTarget == null)
            {

                int j = Random.Range(0, blueTowers.Length);

                while (blueTowers[j] == null)
                {
                    j = Random.Range(0, blueTowers.Length);
                }

                GameObject nouvelleTourACibler = blueTowers[j];
                activeRedSoldiers[i].GetComponent<SoldierScript>().Destination = nouvelleTourACibler.transform.position;

                activeRedSoldiers[i].GetComponent<SoldierScript>().InitialTarget = nouvelleTourACibler;
            }
        }

        

    }

    

    /// <summary>
    /// Déplace un soldat donné vers son point de destination.  On lui fait faire aussi une rotation vers son point de destination
    /// </summary>
    /// <param name="soldiers">La collection de soldats dans laquelle on va chercher le dit soldat</param>
    /// <param name="positionDansLaListe">L'indice dans la collection</param>
    void MoveSoldier(GameObject[] soldiers, int positionDansLaListe)
    {
        //Question de racourcir les lignes de code.
        int i = positionDansLaListe;
        
        if (soldiers[i].GetComponent<SoldierScript>().SoldierState != null)
        {
            soldiers[i].GetComponent<SoldierScript>().SoldierState.MoveSoldier(soldiers[i]);
        }
    }

    /// <summary>
    /// Méthode pour spawner un soldat dans une collection de soldats
    /// </summary>
    /// <param name="soldiers">La collection dans laquelle on va spawner notre nouveau soldat</param>
    /// <param name="soldierColor">La couleur que l'on va lui donner</param>
    /// <param name="startTower">Collection des tours de départ de son camp.  une sera choisie au hasard</param>
    /// <param name="targetTower">Collection des tours cibles de son camp.  une sera choisie au hasard</param>
    void SpawnSoldiers(GameObject[] soldiers, Color soldierColor, GameObject[] startTower, GameObject[] targetTower)
    {
        for (int i = 0; i < maxNumberOfSoldiers; i++)
        {
            if (soldiers[i] == null)
            {
                //Instantiate permet d'utiliser un préfab et de l'instancier comme GameObject concret.  Le soldat choisi est au hasard
                GameObject soldier = Instantiate(soldiersPrefabs[Random.Range(0, soldiersPrefabs.Length)]);

                //On va chercher le script "SoldierScript" attaché à notre soldat et on exécute une de ses méthodes.  Ici c'est de lui donner sa positiond ans la collection
                soldier.GetComponent<SoldierScript>().PositionListe = i;

                //On va chercher le SpriteRenderer attaché à notre soldat et on change sa couleur
                soldier.GetComponent<SpriteRenderer>().color = soldierColor;

                //Assignation d'un soldat à une tour qui n'a pas été détruite encore!
                int j = Random.Range(0, startTower.Length);
                
                while(startTower[j] == null)
                {
                    j = Random.Range(0, startTower.Length);
                }
                
                soldier.transform.position = startTower[j].transform.position;

                soldier.GetComponent<SoldierScript>().DeparturePoint = soldier.transform.position;
                
                //Assignation d'une "tour-cible" qui n'a pas été détruite encore!
                int k = Random.Range(0, targetTower.Length);
              
                while (targetTower[k] == null)
                {
                    k = Random.Range(0, targetTower.Length);
                }

                soldier.GetComponent<SoldierScript>().InitialTarget = targetTower[k];

                soldier.GetComponent<SoldierScript>().Target = soldier.GetComponent<SoldierScript>().InitialTarget;

                //On détermine la position de sa destination, choisie au hasard parmis les tours de destination..
                soldier.GetComponent<SoldierScript>().Destination = soldier.GetComponent<SoldierScript>().InitialTarget.transform.position;

                //On détermine la position de sa destination, choisie au hasard parmis les tours de destination..
                soldier.GetComponent<SoldierScript>().InitialDestination = soldier.GetComponent<SoldierScript>().Destination;

                //On assigne le soldat à la collection appropriée
                soldiers[i] = soldier;

                if(soldierColor == rouge)
                {
                    soldier.layer = LayerMask.NameToLayer("redHitBox");
                }
                else if (soldierColor == bleu)
                {
                    soldier.layer = LayerMask.NameToLayer("blueHitBox");
                }

                //On assigne un seul soldat; sortie de la boucle dès que c'est fait.
                break;
            }
        }
    }
}



