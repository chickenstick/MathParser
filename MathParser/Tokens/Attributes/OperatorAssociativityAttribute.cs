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
    /// Attribute that defines an operator's associativity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
    internal class OperatorAssociativityAttribute : Attribute
    {

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorAssociativityAttribute"/> class.
        /// </summary>
        /// <param name="associativity">The associativity.</param>
        public OperatorAssociativityAttribute(Associativity associativity)
        {
            this.Associativity = associativity;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets the associativity.
        /// </summary>
        public Associativity Associativity { get; private set; }

        #endregion

    }
}
