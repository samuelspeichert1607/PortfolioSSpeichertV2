using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///     Class with every constant used.
/// </summary>
public class Constants
{
    /// <summary>
    ///     Contains all the layers' names
    /// </summary>
    internal class Layers
    {
        public const string Wood = "Wood";
        public const string Block = "Block";
    }

    /// <summary>
    ///     Contains all the parameters needed to do the binding from the animator.
    /// </summary>
    internal class AnimatorParameters
    {
        public const string Speed = "speed";
        public const string VelocityX = "velocityX";
        public const string VelocityY = "velocityY";
        public const string Shooting = "shooting";
        public const string OnGround = "onGround";
        public const string IsCrouched = "isCrouched";
    }

}
