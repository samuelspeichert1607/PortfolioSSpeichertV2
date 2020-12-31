using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Config/ApplicationConfiguration")]
    public class ApplicationConfiguration : GameScript
    {
        private static ApplicationConfiguration applicationConfiguration;

        private Injector injector;

        /// <summary>
        /// Injecte les dépendances du GameScript reçu en paramètre.
        /// </summary>
        /// <param name="target">
        /// Le Script où effectuer l'injection des dépendances.
        /// </param>
        /// <param name="injectMethodName">
        /// Nom de la méthode où l'injection doit être effectuée.
        /// </param>
        /// <param name="valueDependencies">
        /// Les dépendences de <i>valeur</i> à envoyer à la méthode <i>Inject</i>. L'ordre est important.
        /// </param>
        public static void InjectDependencies(IScript target, string injectMethodName, params object[] valueDependencies)
        {
            //If no "ApplicationConfiguration" exists, but this method is called, this means that the "Application" scene is not loaded.
            if (applicationConfiguration != null)
            {
                applicationConfiguration.injector.InjectDependencies(target, injectMethodName, valueDependencies);
            }
            else
            {
                Debug.LogError("The \"Main\" scene doesn't seem to be loaded. Before starting anything, make sure that the \"Main\" scene is loaded.");
            }
        }

        protected void Awake()
        {
            applicationConfiguration = this;

            injector = new Injector();
        }

        protected void OnDestroy()
        {
            applicationConfiguration = null;
        }
    }
}
