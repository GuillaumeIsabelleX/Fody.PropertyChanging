﻿using System;

namespace PropertyChanging
{
    /// <summary>
    /// Exclude a <see cref="Type"/> or property from notification.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class DoNotNotifyAttribute : Attribute
    {
    }
}
namespace PropertyChanging
{
}