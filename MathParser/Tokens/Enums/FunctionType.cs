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
    /// An enumeration of function types.
    /// </summary>
    internal enum FunctionType
    {
        /// <summary>
        /// Unknown function type.
        /// </summary>
        [EnumDescription("", "")]
        [ArgumentCount()]
        Unknown,
        /// <summary>
        /// Sine
        /// </summary>
        [EnumDescription("Sine", "sin")]
        [ArgumentCount(1)]
        Sine,
        /// <summary>
        /// Cosine
        /// </summary>
        [EnumDescription("Cosine", "cos")]
        [ArgumentCount(1)]
        Cosine,
        /// <summary>
        /// Tangent
        /// </summary>
        [EnumDescription("Tangent", "tan")]
        [ArgumentCount(1)]
        Tangent,
        /// <summary>
        /// Find the greater of two values
        /// </summary>
        [EnumDescription("Maximum", "max")]
        [ArgumentCount(2)]
        Maximum,
        /// <summary>
        /// Find the lesser of two values
        /// </summary>
        [EnumDescription("Minimum", "min")]
        [ArgumentCount(2)]
        Minimum
    }
}
