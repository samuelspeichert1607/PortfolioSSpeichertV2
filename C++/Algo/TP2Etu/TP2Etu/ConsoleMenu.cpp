#include "ConsoleMenu.h"

/// <summary>
/// Cette classe va gérer le menu de l'application.
/// </summary>


//Constructeur de ConsoleMenu
ConsoleMenu::ConsoleMenu()
{
	
}

//Destructeur de ConsoleMenu.
ConsoleMenu::~ConsoleMenu()
{

}

/// <summary>
/// La fonction Run va gérer le déroulement du programme.
/// </summary>
/// <returns>Aucune valeur de retour.</returns>
void ConsoleMenu::Run()
{
	
   
    char input;
	//Les entrées supportées
    char tabValidInputs[] = { '1', '2', 'q' };
    const int NB_ELEMENTS = 3;
	
    //Tant qu'il ne quitte pas, on demande à l'utilisateur ce qu'il veut faire.
    do
    { 
		DisplayCredits();
		input = ReadValidInput(tabValidInputs, NB_ELEMENTS);
    } while (ManageSelection(input));
	
}

/// <summary>
/// La fonction ManageSelection va faire la gestion du menu et des entrants de l'utilisateur.
/// </summary>
/// <param name="tabValidInputs">Ce tableau de caractères va contenir les caractères acceptés.</param>
/// <param name="nbElements">Le nombre d'éléments (sans blague?).</param>
/// <returns>La fonction retourne un caractère lu. Sinon, elle retourne NULL.</returns>
char ConsoleMenu::ReadValidInput(char tabValidInputs[], int nbElements)
{
	
    DisplayMenu();
    string userEntry;
    cin >> userEntry;
	
    if (userEntry.size() == 1)
    {
        //Pour tous les caractères valides,
        for (int i = 0; i < nbElements; i++)
        {
            //Si notre entrée y est présente,
            if (tabValidInputs[i] == userEntry[0])
                //on retourne ce caractère.
                return userEntry[0];
        }
    }

	 cout << "Votre entree doit etre une seule lettre correspondante au menu ci-dessus." << endl;
     system("pause");
	 system("cls");
  
	 return NULL;
}

/// <summary>
/// Cette fonction fait afficher à la console le menu.
/// </summary>
/// <returns>Aucune valeur de retour.</returns>
void ConsoleMenu::DisplayMenu()
{
    cout << "Que voulez-vous faire ? " << endl;
    cout << "Appuyez sur 1 pour solutionner l'algorithme du chemin de sortie" << endl;
    cout << "Appuyez sur 2 pour solutionner l'algorithme de tous les points" << endl;
    cout << "Appuyez sur q pour quitter le programme." << endl;
}

/// <summary>
/// Cette fonction fait afficher à la console le menu.
/// </summary>
/// <returns>Aucune valeur de retour.</returns>
void ConsoleMenu::DisplayCredits()
{
	cout << "-------------------------------------------------------------------" << endl;
    cout << "|                       TRAVAIL PRATIQUE 2                        |" << endl;
    cout << "|                              R.O.B                              |" << endl;
    cout << "|                                                                 |" << endl;
    cout << "|                       AUTEUR ET CACHOTIER :                     |" << endl;
    cout << "|                         Samuel Speichert                        |" << endl;
    cout << "------------------------------------------------------------------|" << endl;
    cout << endl;
	cout << endl;
}

/// <summary>
/// La fonction ManageSelection va faire la gestion du menu et des entrants de l'utilisateur.
/// </summary>
/// <param name="entry">Le caractère entré en paramètre.</param>
/// <returns>La fonction retourne vrai le programme est en cours d'exécution.</returns>
bool ConsoleMenu::ManageSelection(char entry)
{
	ROB rob; // Création du robot.
    bool toContinue = true; //Dois-je continuer?
    
    switch (entry)
    {
		//Si l'utilisateur veut résoudre l'Algo #1
		case '1':
		{  
			rob.SolvePathToExit();
			rob.GetSolutionPathToExit();
       		system("pause");
			system("cls");
			break;
		}

		//Si l'utilisateur veut résoudre l'Algo #1
		case '2':
		{
			rob.SolveAllPoints(NULL);
			rob.GetSolutionAllPoints();
			system("pause");
			system("cls");
			break;
		}
		
		//Si l'utilisateur veut quitter le programme,
		case 'q':
			//on quitte le programme.
			toContinue = false;
			break;

    }
	
	return toContinue;
}


