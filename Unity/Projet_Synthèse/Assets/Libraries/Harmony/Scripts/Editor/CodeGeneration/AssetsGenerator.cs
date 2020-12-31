using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Harmony
{
#if UNITY_EDITOR_WIN
    /// <summary>
    /// Script Editor Unity déclanchant la génération des BuildSettings et des classes de constantes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Android possède probablement le <i>Build Process</i> le mieux conçu à ce jour : très solide, facilement configurable
    /// et conçu pour éliminer le plus d'irritants possible lors du développement. Ce n'est pas le cas de Unity, malgré la quantité 
    /// importante d'outils fournis avec le moteur.
    /// </para>
    /// <para>
    /// L'une des forces du <i>Build Process</i> Android est la gestion très stricte des ressources et des identifiants de ressources. 
    /// Toute tentative d'accès à une ressource non existante est souvent détectée dès la compilation, pourvu que l'on passe par 
    /// les moyens traditionnels. Par exemple :
    /// </para>
    /// <code>
    /// public class MainActivity extends AppCompatActivity {
    ///     private EditText inputEditText;
    ///     private TextView outputTextView;
    ///     
    ///     @Override
    ///     protected void onCreate(Bundle savedInstanceState) {
    ///         super.onCreate(savedInstanceState);
    ///         setContentView(R.layout.activity_main);
    ///     
    ///         inputEditText = (EditText)findViewById(R.id.inputEditText);
    ///         outputTextView = (TextView)findViewById(R.id.outputTextView);
    ///     }
    ///     
    ///     public void onButtonClicked(View view) {
    ///         String format = getString(R.string.text_output);
    ///         String output = String.format(format, inputEditText.getText().toString());
    ///         outputTextView.setText(output);
    ///     }   
    /// }
    /// </code>
    /// <para>
    /// Notez la classe <c>R</c>. Sous Android, cette classe est générée automatiquement à partir des ressources du projet et contient
    /// les identifiants des ressources. Utiliser une ressource inexistante devient donc presque impossible, car cela en résulterait d'une erreur
    /// à la compilation.
    /// </para>
    /// <para>
    /// Cependant, sous Unity, il y a rien de tel : l'accès à une ressource non existante n'est détectée que lors de l'exécution. 
    /// Par exemple, obtenir un <i>GameObject</i> ainsi peut causer un <i>NullReferenceException</i> :
    /// </para>
    /// <code>
    /// public class GameScript : MonoBehaviour
    /// {
    /// 	private GameObject player;
    /// 
    ///     public void Awake()
    ///     {
    ///         player = GameObject.Find("Player");
    ///     }
    ///     
    ///     public void Update()
    ///     {
    ///         player.DoSomething();
    ///     }
    /// }
    /// </code>
    /// <para>
    /// Pour résoudre ce problème, Harmony génère des classes de constantes. Ces constantes sont extraites directement du projet Unity 
    /// et mises à jour à chaque fois qu'un paramètre change ou qu'une scène est modifiée.
    /// </para>
    /// <para>
    /// Plusieurs classes de constantes sont générées :
    /// <list type="bullet">
    /// <item>Tag : Les<i>Tags</i> du projet.</item>
    /// <item>Layer : Les <i>Layers</i> du projet.</item>
    /// <item>Scene : Les scènes dans le dossier <i>Scenes</i>.</item>
    /// <item>Prefab : Les <i>Prefabs</i> dans le dossier <i>Prefabs</i>..</item>
    /// <item>AnimatorParameter : Les <i>Animation Parameters</i> du projet.</item>
    /// <item>GameObject : Les <i>GameObjects</i> de toutes les scènes du dossier <i>Scenes</i> (incluant aussi les <i>Prefabs</i> du dossier <i>Prefabs</i>.).</item>
    /// </list>
    /// </para>
    /// <para>
    /// Chaque classe de constantes possède deux variantes : sous forme d'énumération et sous forme de string. Il est possible
    /// de passer de l'un à l'autre en utilisant les méthodes statiques <i>ToString</i> disponibles dans la variante sous forme
    /// de string.
    /// </para>
    /// <para>
    /// Tout comme pour Android, toutes ces constantes sont situées dans la classe <i>R</i>. Pour accéder aux enumérations, il faut
    /// ensuite naviguer dans la classe <i>E</i>. Pour les strings, il faut plutôt utiliser la classe <i>S</i>.
    /// </para>
    /// <para>
    /// Par exemple : <c>R.E.Tag.MainCamera</c> ou <c>R.S.Tag.MainCamera</c>.
    /// </para>
    /// <para>
    /// Il est donc possible d'utiliser ces constantes ainsi :
    /// </para>
    /// <code>
    /// public class GameScript : MonoBehaviour
    /// {
    /// 	private GameObject player;
    /// 
    ///     public void Awake()
    ///     {
    ///         player = GameObject.Find(R.S.GameObject.PLAYER);
    ///     }
    ///     
    ///     public void Update()
    ///     {
    ///         player.DoSomething();
    ///     }
    /// }
    /// </code>
    /// <para>
    /// En utilisant ces constantes, si le nom d'un <i>Tag</i>, d'un <i>Layer</i> ou d'un <i>GameObject</i> change, alors la valeur et le nom de sa 
    /// constante changera aussi, et ce, automatiquement. L'erreur sera alors détectée à la compilation et non pas à l'exécution.
    /// </para>
    /// <para>
    /// La génération de code se déclanche soit :
    /// </para>
    /// <list type="bullet">
    /// <item>Au lancement de Unity.</item>
    /// <item>Lors de la sauvegarde d'un fichier.</item>
    /// <item>Lors de la compilation.</item>
    /// </list>
    /// <para>
    /// Prenez note que la génération de code n'est pas effectuée par cette classe, mais par un programme externe. Ce programme doit être
    /// situé à la racine du projet, à <c>PROJECT_ROOT/BuildTools/Harmony/HarmonyCodeGenerator.exe</c>.
    /// </para>
    /// </remarks>
    public static class AssetsGenerator
    {

        [MenuItem("Assets/Generate Build Settings From Activities")]
        public static void GenerateBuildSettings()
        {
            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();

            //Add Main scene
            scenes.Add(new EditorBuildSettingsScene(AssetsExtensions.FindScenePath(R.S.Scene.Main), true));

            //Add Util scenes
            foreach (string scenePath in AssetsExtensions.FindScenesPathIn("Scenes/Util"))
            {
                scenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }

            //Add Activity scenes
            foreach (Activity activity in AssetsExtensions.FindAssets<Activity>())
            {
                if (activity.Scene != R.E.Scene.None)
                {
                    scenes.Add(new EditorBuildSettingsScene(AssetsExtensions.FindScenePath(R.S.Scene.ToString(activity.Scene)), true));
                }

                foreach (Fragment fragment in activity.Fragments)
                {
                    if (fragment.Scene != R.E.Scene.None)
                    {
                        scenes.Add(new EditorBuildSettingsScene(AssetsExtensions.FindScenePath(R.S.Scene.ToString(fragment.Scene)), true));
                    }
                }

                foreach (Menu menu in activity.Menus)
                {
                    if (menu.Scene != R.E.Scene.None)
                    {
                        scenes.Add(new EditorBuildSettingsScene(AssetsExtensions.FindScenePath(R.S.Scene.ToString(menu.Scene)), true));
                    }
                }
            }

            EditorBuildSettings.scenes = scenes.ToArray();
        }

        [MenuItem("Assets/Generate Const Classes")]
        public static void GenerateConstClasses()
        {
            //Prevent Unity from compiling code while generating more code.
            EditorApplication.LockReloadAssemblies();

            string pathToCodeGenerator = Path.GetFullPath(Path.Combine(Application.dataPath, "../BuildTools/Harmony/HarmonyCodeGenerator.exe"));
            string pathToProjectDirectory = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string pathToGeneratedDirectory = Path.GetFullPath(Path.Combine(Application.dataPath, "Generated"));

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = pathToCodeGenerator,
                Arguments = "\"" + pathToProjectDirectory + "\" \"" + pathToGeneratedDirectory + "\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process codeGenerationProcess = Process.Start(processStartInfo);
            if (codeGenerationProcess != null)
            {
                codeGenerationProcess.WaitForExit();
                if (codeGenerationProcess.ExitCode != 0)
                {
                    Debug.LogError("Code Generation Error \nClick to view\n" + 
                                    codeGenerationProcess.StandardOutput.ReadToEnd());
                }
            }
            else
            {
                Debug.Log("Code Generation is probably complete, but Unity doesn't know it yet. Please save your work to " +
                          "let Unity reload and compile the generated code.");
            }

            EditorApplication.UnlockReloadAssemblies();

            try
            {
                AssetDatabase.Refresh();
            }
            catch (Exception)
            {
                //Sometimes, this line causes a NullReferenceException. This is a known issue in Unity. Just ignore it.
            }
        }
    }

#endif
}