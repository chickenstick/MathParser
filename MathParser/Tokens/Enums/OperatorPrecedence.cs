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
    /// An enumeration of C# operator precedences.  The values of 
    /// this enum can be compared against each other to determine 
    /// relative precedence.
    /// </summary>
    /// <remarks>
    /// For example "Multiplicative" > "Additive".  Not 
    /// all of the precedences will be used in this program.
    /// </remarks>
    internal enum OperatorPrecedence
    {
        /// <summary>
        /// An unknown precedence.
        /// </summary>
        [EnumDescription("", "")]
        [OperatorAssociativity(Associativity.Unknown)]
        Unknown = 0,
        /// <summary>
        /// =  *=  /=  %=  +=  -=  <<=  >>=  &=  ^=  |=
        /// </summary>
        [EnumDescription("Assignment", "Assignment")]
        [OperatorAssociativity(Associativity.RightAssociative)]
        Assignment = 1,
        /// <summary>
        /// ?:
        /// </summary>
        [EnumDescription("Conditional", "Conditional")]
        [OperatorAssociativity(Associativity.RightAssociative)]
        Conditional = 2,
        /// <summary>
        /// ||
        /// </summary>
        [EnumDescription("Conditional OR", "Conditional OR")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Conditional_OR = 3,
        /// <summary>
        /// &&
        /// </summary>
        [EnumDescription("Conditional AND", "Conditional AND")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Conditional_AND = 4,
        /// <summary>
        /// |
        /// </summary>
        [EnumDescription("Logical OR", "Logical OR")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Logical_OR = 5,
        /// <summary>
        /// ^ (I think I'll try using ^ as a "raise to" operator.")
        /// </summary>
        [EnumDescription("Logical XOR", "Logical XOR")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Logical_XOR = 6,
        /// <summary>
        /// &
        /// </summary>
        [EnumDescription("Logical AND", "Logical AND")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Logical_AND = 7,
        /// <summary>
        /// ==  !=
        /// </summary>
        [EnumDescription("Equality", "Equality")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Equality = 8,
        /// <summary>
        /// <  >  <=  >=  is  as
        /// </summary>
        [EnumDescription("Relational and Type Testing", "Relational and Type Testing")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        RelationalAndTypeTesting = 9,
        /// <summary>
        /// <<  >>
        /// </summary>
        [EnumDescription("Shift", "Shift")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Shift = 10,
        /// <summary>
        /// +  -
        /// </summary>
        [EnumDescription("Additive", "Additive")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Additive = 11,
        /// <summary>
        /// *  /  %
        /// </summary>
        [EnumDescription("Multiplicative", "Multiplicative")]
        [OperatorAssociativity(Associativity.LeftAssociative)]
        Multiplicative = 12,
        /// <summary>
        /// ^
        /// </summary>
        /// <remarks>Not included in the C# specification.</remarks>
        [EnumDescription("Exponentiation", "Exponentiation")]
        [OperatorAssociativity(Associativity.RightAssociative)]
        Exponentiation = 13,
        /// <summary>
        /// +  -  !  ~  ++x  --x  (T)x
        /// </summary>
        [EnumDescription("Unary", "Unary")]
        [OperatorAssociativity(Associativity.NotAssociative)]
        Unary = 14,
        /// <summary>
        /// x.y  f(x)  a[x]  x++  x--  new
        /// typeof  checked  unchecked
        /// </summary>
        [EnumDescription("Primary", "Primary")]
        [OperatorAssociativity(Associativity.NotAssociative)]
        Primary = 15
    }
}
