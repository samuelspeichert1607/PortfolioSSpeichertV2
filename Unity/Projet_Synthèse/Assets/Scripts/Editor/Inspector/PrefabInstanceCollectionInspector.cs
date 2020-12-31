using UnityEditor;

namespace ProjetSynthese
{
    [CustomEditor(typeof(PrefabInstanceCollection), true)]
    public class PrefabInstanceCollectionInspector : GameScriptInspector
    {
        private PrefabInstanceCollection prefabInstanceCollection;

        private void Awake()
        {
            prefabInstanceCollection = target as PrefabInstanceCollection;
        }

        private void OnDestroy()
        {
            prefabInstanceCollection = null;
        }

        protected override void OnDraw()
        {
            base.OnDraw();
            if (EditorApplication.isPlaying)
            {
                DrawInfoBox("Number of elements : " + prefabInstanceCollection.Count);
            }
        }
    }
}