#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnumHelper;
using MathParser.Tokens.Attributes;
using MathParser.Tokens.Enums;

#endregion

namespace MathParser.Tokens.Extensions
{
    /// <summary>
    /// Utility class for handling operator precedence.
    /// </summary>
    internal static class UtilityExtensions
    {

        #region - Public Methods -

        /// <summary>
        /// Gets the operator's precedence.
        /// </summary>
        /// <param name="operatorType">Type of the operator.</param>
        /// <returns></returns>
        public static OperatorPrecedence GetOperatorPrecedence(this OperatorType operatorType)
        {
            OperatorPrecedence precedence = OperatorPrecedence.Unknown;
            bool hasAttribute = false;

            if (EnumLookup.HasAttribute<OperatorType, OperatorPrecedenceAttribute>(operatorType))
            {
                OperatorPrecedenceAttribute attr = EnumLookup.GetAttribute<OperatorType, OperatorPrecedenceAttribute>(operatorType);
                if (attr.Precedence != OperatorPrecedence.Unknown)
                {
                    hasAttribute = true;
                    precedence = attr.Precedence;
                }
            }

            if (!hasAttribute)
            {
                throw new ArgumentException(string.Format("The operator type '{0}' has no defined precedence.", operatorType), "operatorType");
            }

            return precedence;
        }

        /// <summary>
        /// Compares the values of the two precedences.
        /// Less than zero:  precedence1 &lt; precedence2.
        /// Zero:  have the same precedence.
        /// Greater than zero:  precedence1 &gt; precedence2.
        /// </summary>
        /// <param name="precedence1">The precedence1.</param>
        /// <param name="precedence2">The precedence2.</param>
        /// <returns></returns>
        public static int CompareTo(this OperatorPrecedence precedence1, OperatorPrecedence precedence2)
        {
            return precedence1.CompareTo(precedence2);
        }

        /// <summary>
        /// Compares the precedence of the two operator types.
        /// Less than zero:  operatorType1 &lt; operatorType2.
        /// Zero:  have the same precedence.
        /// Greater than zero:  operatorType1 &gt; operatorType2.
        /// </summary>
        /// <param name="operatorType1">The operator type1.</param>
        /// <param name="operatorType2">The operator type2.</param>
        /// <returns></returns>
        public static int CompareTo(this OperatorType operatorType1, OperatorType operatorType2)
        {
            OperatorPrecedence precedence1 = GetOperatorPrecedence(operatorType1);
            OperatorPrecedence precedence2 = GetOperatorPrecedence(operatorType2);

            return CompareTo(precedence1, precedence2);
        }

        /// <summary>
        /// Gets the precedence's associativity.
        /// </summary>
        /// <param name="precedence">The precedence.</param>
        /// <returns></returns>
        public static Associativity GetAssociativity(this OperatorPrecedence precedence)
        {
            Associativity associativity = Associativity.Unknown;
            bool hasAttribute = false;

            if (EnumLookup.HasAttribute<OperatorPrecedence, OperatorAssociativityAttribute>(precedence))
            {
                OperatorAssociativityAttribute attr = EnumLookup.GetAttribute<OperatorPrecedence, OperatorAssociativityAttribute>(precedence);
                if (attr.Associativity != Associativity.Unknown)
                {
                    hasAttribute = true;
                    associativity = attr.Associativity;
                }
            }

            if (!hasAttribute)
            {
                throw new ArgumentException(string.Format("The precedence '{0}' does not have a known associativity.", precedence), "precedence");
            }

            return associativity;
        }

        /// <summary>
        /// Gets the operator's associativity.
        /// </summary>
        /// <param name="operatorType">Type of the operator.</param>
        /// <returns></returns>
        public static Associativity GetAssociativity(this OperatorType operatorType)
        {
            OperatorPrecedence precedence = GetOperatorPrecedence(operatorType);
            return GetAssociativity(precedence);
        }

        /// <summary>
        /// Gets the argument count for the operator type.
        /// </summary>
        /// <param name="operatorType">Type of the operator.</param>
        /// <returns></returns>
        public static byte GetArgumentCount(this OperatorType operatorType)
        {
            return GetArgumentCount<OperatorType>(operatorType);
        }

        /// <summary>
        /// Gets the argument count for the function type.
        /// </summary>
        /// <param name="functionType">Type of the function.</param>
        /// <returns></returns>
        public static byte GetArgumentCount(this FunctionType functionType)
        {
            return GetArgumentCount<FunctionType>(functionType);
        }

        public static decimal[] ConvertToDecimalArray(this IList<IToken> args)
        {
            decimal[] numbers = new decimal[args.Count];
            for (int i = 0; i < numbers.Length; i++)
            {
                IToken token = args[i];

                if (token.TokenType == TokenType.Number)
                {
                    numbers[i] = ((NumberToken)token).Value;
                }
                else
                {
                    throw new ArgumentException("Each token in the args should be a number.", "args");
                }
            }

            return numbers;
        }

        /// <summary>
        /// Gets the constant value.
        /// </summary>
        /// <param name="constType">Type of the constant.</param>
        /// <returns></returns>
        public static decimal GetConstantValue(this ConstantType constType)
        {
            decimal value = 0.0M;
            bool hasAttribute = false;

            if (EnumLookup.HasAttribute<ConstantType, ConstantValueAttribute>(constType))
            {
                ConstantValueAttribute attr = EnumLookup.GetAttribute<ConstantType, ConstantValueAttribute>(constType);
                if (attr.Value.HasValue)
                {
                    hasAttribute = true;
                    value = attr.Value.Value;
                }
            }

            if (!hasAttribute)
            {
                throw new ArgumentException(string.Format("The constant type '{0}' is not supported.", constType), "constType");
            }

            return value;
        }

        #endregion

        #region - Private Methods -

        /// <summary>
        /// Gets the argument count.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static byte GetArgumentCount<TEnum>(TEnum value) where TEnum : struct
        {
            byte argumentCount = 0;
            bool hasAttribute = false;

            if (EnumLookup.HasAttribute<TEnum, ArgumentCountAttribute>(value))
            {
                ArgumentCountAttribute attr = EnumLookup.GetAttribute<TEnum, ArgumentCountAttribute>(value);
                if (attr.ArgumentCount.HasValue)
                {
                    hasAttribute = true;
                    argumentCount = attr.ArgumentCount.Value;
                }
            }

            if (!hasAttribute)
            {
                throw new ArgumentException(string.Format("The operator type '{0}' is not supported.", value), "operatorType");
            }

            return argumentCount;
        }

        #endregion

    }
}
