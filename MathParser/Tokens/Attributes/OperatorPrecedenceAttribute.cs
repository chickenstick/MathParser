#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathParser.Tokens.Enums;

#endregion

namespace MathParser.Tokens.Attributes
{
    /// <summary>
    /// Attribute that defines an operator's precedence.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
    internal class OperatorPrecedenceAttribute : Attribute
    {

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorPrecedenceAttribute"/> class.
        /// </summary>
        /// <param name="precedence">The precedence.</param>
        public OperatorPrecedenceAttribute(OperatorPrecedence precedence)
            : base()
        {
            this.Precedence = precedence;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets the precedence.
        /// </summary>
        public OperatorPrecedence Precedence { get; private set; }

        #endregion

    }
}
