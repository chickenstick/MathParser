#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnumHelper;
using MathParser.Tokens.Enums;
using MathParser.Tokens.Extensions;

#endregion

namespace MathParser.Tokens
{
    /// <summary>
    /// A token that represents an operator.
    /// </summary>
    internal struct OperatorToken : IToken
    {

        #region - Fields -

        private OperatorType _operatorType;
        private OperatorPrecedence _precedence;
        private Associativity _associativity;
        private byte _argumentCount;

        #endregion

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorToken"/> struct.
        /// </summary>
        /// <param name="operatorType">Type of the operator.</param>
        public OperatorToken(OperatorType operatorType)
        {
            if (operatorType != OperatorType.Add &&
                operatorType != OperatorType.Divide &&
                operatorType != OperatorType.Multiply &&
                operatorType != OperatorType.Subtract &&
                operatorType != OperatorType.RaiseTo &&
                operatorType != OperatorType.Modulo &&
                operatorType != OperatorType.UnaryMinus)
            {
                throw new ArgumentException("The operator type is not of a valid type.", "operatorType");
            }

            this._operatorType = operatorType;
            this._precedence = operatorType.GetOperatorPrecedence();
            this._associativity = operatorType.GetAssociativity();
            this._argumentCount = operatorType.GetArgumentCount();
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public TokenType TokenType
        {
            get 
            {
                return TokenType.Operator;
            }
        }

        /// <summary>
        /// Gets the type of the operator.
        /// </summary>
        /// <value>
        /// The type of the operator.
        /// </value>
        public OperatorType OperatorType
        {
            get
            {
                return this._operatorType;
            }
        }

        /// <summary>
        /// Gets the precedence.
        /// </summary>
        public OperatorPrecedence Precedence
        {
            get
            {
                return this._precedence;
            }
        }

        /// <summary>
        /// Gets the associativity.
        /// </summary>
        public Associativity Associativity
        {
            get
            {
                return this._associativity;
            }
        }

        /// <summary>
        /// Gets the number of arguments used by this operator.
        /// </summary>
        public byte ArgumentCount
        {
            get
            {
                return this._argumentCount;
            }
        }

        #endregion

        #region - Public Methods -

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return EnumLookup.Parse(this._operatorType);
        }

        /// <summary>
        /// Evaluates the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public decimal Evaluate(IToken[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            else if (args.Length != this._argumentCount)
            {
                throw new ArgumentException(string.Format("The number of arguments should equal {0}.", this._argumentCount), "args");
            }

            decimal[] numbers = args.ConvertToDecimalArray();

            decimal result = 0.0M;
            switch(this._operatorType)
            {
                case OperatorType.Add:
                    result = Add(numbers);
                    break;

                case OperatorType.Divide:
                    result = Divide(numbers);
                    break;

                case OperatorType.Modulo:
                    result = Modulo(numbers);
                    break;

                case OperatorType.Multiply:
                    result = Multiply(numbers);
                    break;

                case OperatorType.RaiseTo:
                    result = RaiseTo(numbers);
                    break;

                case OperatorType.Subtract:
                    result = Subtract(numbers);
                    break;

                case OperatorType.UnaryMinus:
                    result = UnaryMinus(numbers);
                    break;

                default:
                    throw new InvalidOperationException(string.Format("Unsupported operator type '{0}'.", this._operatorType));
            }

            return result;
        }

        #endregion

        #region - Evaluation Methods -

        /// <summary>
        /// Adds the specified numbers.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Add(decimal[] numbers)
        {
            return numbers[0] + numbers[1];
        }

        /// <summary>
        /// Divides the specified numbers.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Divide(decimal[] numbers)
        {
            return numbers[0] / numbers[1];
        }

        /// <summary>
        /// Moduloes the specified numbers.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Modulo(decimal[] numbers)
        {
            return numbers[0] % numbers[1];
        }

        /// <summary>
        /// Multiplies the specified numbers.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Multiply(decimal[] numbers)
        {
            return numbers[0] * numbers[1];
        }

        /// <summary>
        /// Raises to.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal RaiseTo(decimal[] numbers)
        {
            return (decimal)Math.Pow((double)numbers[0], (double)numbers[1]);
        }

        /// <summary>
        /// Subtracts the specified numbers.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Subtract(decimal[] numbers)
        {
            return numbers[0] - numbers[1];
        }

        /// <summary>
        /// Returns the opposite value.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal UnaryMinus(decimal[] numbers)
        {
            return -numbers[0];
        }

        #endregion

        #region - Static Methods -

        /// <summary>
        /// Determines whether the specified segment is an operator token.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>
        ///   <c>true</c> if the specified segment is an operator token; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOperatorToken(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            bool success = false;
            string cleanedUp = segment.Trim();

            OperatorType opType = OperatorType.Unknown;
            if (EnumLookup.TryParse<OperatorType>(cleanedUp, out opType))
            {
                if (opType != OperatorType.Unknown)
                {
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Parses the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        public static OperatorToken Parse(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            string cleanedUp = segment.Trim();

            OperatorType opType = OperatorType.Unknown;
            if (!EnumLookup.TryParse<OperatorType>(cleanedUp, out opType) || opType == OperatorType.Unknown)
            {
                throw new ArgumentException(string.Format("The segment \"{0}\" could not be parsed to an operator type.", segment), "segment");
            }

            return new OperatorToken(opType);
        }

        #endregion

    }
}
