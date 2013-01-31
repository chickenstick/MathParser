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
    /// An enumeration of parenthesis types.
    /// </summary>
    internal enum ParenthesisType
    {
        /// <summary>
        /// An unknown parenthesis type, usually indicating a parsing error.
        /// </summary>
        [EnumDescription("", "")]
        Unknown,
        /// <summary>
        /// The left parenthesis '('.
        /// </summary>
        [EnumDescription("Left Parenthesis", "(")]
        Left,
        /// <summary>
        /// The right parenthesis ')'.
        /// </summary>
        [EnumDescription("Right Parenthesis", ")")]
        Right
    }
}
