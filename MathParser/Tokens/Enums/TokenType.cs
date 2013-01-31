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
    /// An enumeration of token types.
    /// </summary>
    internal enum TokenType
    {
        /// <summary>
        /// An unknown token type.
        /// </summary>
        [EnumDescription("", "")]
        Unknown,
        /// <summary>
        /// A token signifying a number.
        /// </summary>
        [EnumDescription("Number", "Number")]
        Number,
        /// <summary>
        /// A token signifying a function.
        /// </summary>
        [EnumDescription("Function", "Function")]
        Function,
        /// <summary>
        /// A token signifying an argument separator, such as a comma.
        /// </summary>
        [EnumDescription("Argument Separator", "ArgumentSeparator")]
        ArgumentSeparator,
        /// <summary>
        /// A token signifying a left parenthesis.
        /// </summary>
        [EnumDescription("Left Parenthesis", "(")]
        LeftParenthesis,
        /// <summary>
        /// A token signifying a right parenthesis.
        /// </summary>
        [EnumDescription("Right Parenthesis", ")")]
        RightParenthesis,
        /// <summary>
        /// A token signifying an operator.
        /// </summary>
        [EnumDescription("Operator", "Operator")]
        Operator
    }
}
