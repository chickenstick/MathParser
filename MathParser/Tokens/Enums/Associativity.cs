#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnumHelper;

#endregion

namespace MathParser.Tokens.Enums
{
    /// <summary>
    /// An enumeration of associativity values.
    /// </summary>
    internal enum Associativity
    {
        /// <summary>
        /// Unknown associativity.
        /// </summary>
        [EnumDescription("", "")]
        Unknown,
        /// <summary>
        /// Left-associative.
        /// </summary>
        [EnumDescription("Left-Associative", "Left-Associative")]
        LeftAssociative,
        /// <summary>
        /// Right-associative.
        /// </summary>
        [EnumDescription("Right-Associative", "Right-Associative")]
        RightAssociative,
        /// <summary>
        /// Not associative.
        /// </summary>
        [EnumDescription("Not Associative", "Not Associative")]
        NotAssociative
    }
}
