using Harmony;
using UnityEditor;

namespace ProjetSynthese
{
    [CustomEditor(typeof(Grid), true)]
    public class GridInspector : Inspector
    {
        private Grid grid;

        private void Awake()
        {
            grid = target as Grid;
        }

        private void OnDestroy()
        {
            grid = null;
        }

        protected override void OnDraw()
        {
            DrawDefaultInspector();

            if (!EditorApplication.isPlaying)
            {
                DrawButton("Generate", () =>
                {
                    Undo.RecordObject(grid, "Generate the Grid.");

                    grid.Create();
                });
                DrawButton("Clear", () =>
                {
                    Undo.RecordObject(grid, "Clear the Grid.");

                    grid.Clear();
                });
                DrawButton("Show Grid", () => {
                    grid.DrawDebug();
                });
                DrawButton("Show Neighbours", () => {
                    grid.DrawNeighbours();
                });
            }
            else
            {
                DrawInfoBox("To create a new Grid, please exit play mode.");
            }
        }
    }
}