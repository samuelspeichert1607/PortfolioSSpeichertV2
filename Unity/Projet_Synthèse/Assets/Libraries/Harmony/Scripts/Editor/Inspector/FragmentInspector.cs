﻿using UnityEditor;

namespace Harmony
{
    /// <summary>
    /// Inspecteur pour les Fragments dans l'éditeur Unity.
    /// </summary>
    [CustomEditor(typeof(Fragment), true)]
    public class FragmentInspector : Inspector
    {
        private EnumProperty scene;
        private EnumProperty controller;

        private void Awake()
        {
            scene = GetEnumProperty("scene", typeof(R.E.Scene));
            controller = GetEnumProperty("controller", typeof(R.E.GameObject));
        }

        private void OnDestroy()
        {
            scene = null;
            controller = null;
        }

        protected override void OnDraw()
        {
            DrawEnumPropertyGrid(scene, 2);
            DrawEnumPropertyGrid(controller, 2);
        }
    }
}