﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Harmony
{
    /// <summary>
    /// Portée de niveau Tag.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cette portée permet d'obtenir :
    /// <list type="bullet">
    /// <item>
    /// Le GameObject ayant le tag donné.
    /// </item>
    /// <item>
    /// Un des Components dans le GameObject ayant le tag donné, incluant ses enfants et les enfants de ses enfants.
    /// </item>
    /// </list>
    /// Plusieurs GameObjects avec le tag donné sont autorisés.
    /// </para>
    /// </remarks>
    public class TagScope : Scope
    {
        private readonly string tagName;

        public TagScope(string tagName)
        {
            this.tagName = tagName;
        }

        protected override IList<GameObject> GetEligibleGameObjects(IScript target)
        {
            return GetDependencySources(target, typeof(GameObject));
        }

        protected override IList<object> GetEligibleDependencies(IScript target, Type dependencyType)
        {
            HashSet<object> dependencies = new HashSet<object>();
            IList<GameObject> dependencySources = GetDependencySources(target, dependencyType);
            foreach(GameObject gameObject in dependencySources)
            {
                foreach(object dependency in gameObject.GetComponentsInChildren(dependencyType))
                {
                    dependencies.Add(dependency);
                }
            }
            return new List<object>(dependencies);
        }

        private IList<GameObject> GetDependencySources(IScript target, Type dependencyType)
        {
            IList<GameObject> dependencySources = GameObject.FindGameObjectsWithTag(tagName);
            if (dependencySources.Count == 0)
            {
                throw new DependencySourceNotFoundException(target, dependencyType, this);
            }
            return dependencySources;
        }
    }
}
