﻿namespace Orca.Authorization.Abac
{
    /// <summary>
    /// Decorate action parameter to be allow be used on auhtorization policies.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class AbacParameterAttribute
        : Attribute
    {
        /// <summary>
        /// Modify the name of the parameter to be used on ABAC policies.
        /// </summary>
        public string Name { get; set; }
    }
}
