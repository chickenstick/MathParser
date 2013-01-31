#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathParser.Tokens.Enums;

#endregion

namespace MathParser.Tokens
{
    /// <summary>
    /// An interface for tokens.
    /// </summary>
    internal interface IToken
    {

        #region - Properties -

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        TokenType TokenType { get; }

        #endregion

    }
}
