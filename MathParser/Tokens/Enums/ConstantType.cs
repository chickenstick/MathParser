#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnumHelper;
using MathParser.Tokens.Attributes;

#endregion

namespace MathParser.Tokens.Enums
{
    /// <summary>
    /// An enumeration of constant types.
    /// </summary>
    internal enum ConstantType
    {
        /// <summary>
        /// An unknown constant.
        /// </summary>
        [EnumDescription("", "")]
        [ConstantValue()]
        Unknown,
        /// <summary>
        /// Pi
        /// </summary>
        [EnumDescription("Pi", "pi")]
        [ConstantValue(Math.PI)]
        Pi,
        /// <summary>
        /// e
        /// </summary>
        [EnumDescription("e", "e")]
        [ConstantValue(Math.E)]
        E
    }
}
