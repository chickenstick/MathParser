#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace MathParser.Tokens
{
    /// <summary>
    /// A factory class for building <see cref="IToken"/>.
    /// </summary>
    internal static class TokenFactory
    {

        #region - Public Methods -

        /// <summary>
        /// Gets the token from the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IToken GetToken(string value)
        {
            if (NumberToken.IsNumberToken(value))
            {
                return NumberToken.Parse(value);
            }
            else if (OperatorToken.IsOperatorToken(value))
            {
                return OperatorToken.Parse(value);
            }
            else if (ParenthesisToken.IsParenthesis(value))
            {
                return ParenthesisToken.Parse(value);
            }
            else if (FunctionToken.IsFunctionToken(value))
            {
                return FunctionToken.Parse(value);
            }
            else if (ArgumentSeparatorToken.IsArgumentSeparator(value))
            {
                return ArgumentSeparatorToken.Parse(value);
            }

            throw new ArgumentException(string.Format("The value '{0}' is not a valid token.", value), "value");
        }

        #endregion

    }
}
