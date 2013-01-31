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
    /// An enumeration of operator types.
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// An unknown operator type, usually signifying a parsing error.
        /// </summary>
        [EnumDescription("", "")]
        [OperatorPrecedence(OperatorPrecedence.Unknown)]
        [ArgumentCount()]
        Unknown,
        /// <summary>
        /// The add operator '+'.
        /// </summary>
        [EnumDescription("Add", "+")]
        [OperatorPrecedence(OperatorPrecedence.Additive)]
        [ArgumentCount(2)]
        Add,
        /// <summary>
        /// The subtract operator '-'.
        /// </summary>
        [EnumDescription("Subtract", "-")]
        [OperatorPrecedence(OperatorPrecedence.Additive)]
        [ArgumentCount(2)]
        Subtract,
        /// <summary>
        /// The multiply operator '*'.
        /// </summary>
        [EnumDescription("Multiply", "*")]
        [OperatorPrecedence(OperatorPrecedence.Multiplicative)]
        [ArgumentCount(2)]
        Multiply,
        /// <summary>
        /// The divide operator '/'.
        /// </summary>
        [EnumDescription("Divide", "/")]
        [OperatorPrecedence(OperatorPrecedence.Multiplicative)]
        [ArgumentCount(2)]
        Divide,
        /// <summary>
        /// The exponent operator '^'.
        /// </summary>
        [EnumDescription("Raise To", "^")]
        [OperatorPrecedence(OperatorPrecedence.Exponentiation)]
        [ArgumentCount(2)]
        RaiseTo,
        /// <summary>
        /// The modulo operator '%'.
        /// </summary>
        [EnumDescription("Modulo", "%")]
        [OperatorPrecedence(OperatorPrecedence.Multiplicative)]
        [ArgumentCount(2)]
        Modulo,
        /// <summary>
        /// The unary minus operator, represented after sanitizing by '~'.  For example, -4.
        /// </summary>
        [EnumDescription("Unary Minus", "~")]
        [OperatorPrecedence(OperatorPrecedence.Unary)]
        [ArgumentCount(1)]
        UnaryMinus
    }
}
