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
    /// A token that represents a function.
    /// </summary>
    internal struct FunctionToken : IToken
    {

        #region - Fields -

        private FunctionType _functionType;
        private byte _argumentCount;

        #endregion

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionToken"/> struct.
        /// </summary>
        /// <param name="functionType">Type of the function.</param>
        public FunctionToken(FunctionType functionType)
        {
            if (functionType != FunctionType.Cosine &&
                functionType != FunctionType.Sine &&
                functionType != FunctionType.Tangent &&
                functionType != FunctionType.Maximum &&
                functionType != FunctionType.Minimum)
            {
                throw new ArgumentException("The function type is not a valid type.", "functionType");
            }

            this._functionType = functionType;
            this._argumentCount = functionType.GetArgumentCount();
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public TokenType TokenType
        {
            get
            {
                return TokenType.Function;
            }
        }

        /// <summary>
        /// Gets the type of the function.
        /// </summary>
        /// <value>
        /// The type of the function.
        /// </value>
        public FunctionType FunctionType
        {
            get
            {
                return this._functionType;
            }
        }

        /// <summary>
        /// Gets the number of arguments used by this function.
        /// </summary>
        public byte ArgumentCount
        {
            get
            {
                return _argumentCount;
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
            return EnumLookup.Parse<FunctionType>(this._functionType);
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
            switch(this._functionType)
            {
                case FunctionType.Cosine:
                    result = Cosine(numbers);
                    break;

                case FunctionType.Sine:
                    result = Sine(numbers);
                    break;

                case FunctionType.Tangent:
                    result = Tangent(numbers);
                    break;

                case FunctionType.Maximum:
                    result = Maximum(numbers);
                    break;

                case FunctionType.Minimum:
                    result = Minimum(numbers);
                    break;

                default:
                    throw new InvalidOperationException(string.Format("Unsupported function type '{0}'.", this._functionType));
            }

            return result;
        }

        #endregion

        #region - Evaluation Methods -

        /// <summary>
        /// Calculates the cosine.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Cosine(decimal[] numbers)
        {
            return (decimal)Math.Cos((double)numbers[0]);
        }

        /// <summary>
        /// Calculates the sine.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Sine(decimal[] numbers)
        {
            return (decimal)Math.Sin((double)numbers[0]);
        }

        /// <summary>
        /// Calculates the tangent.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Tangent(decimal[] numbers)
        {
            return (decimal)Math.Tan((double)numbers[0]);
        }

        /// <summary>
        /// Returns the larger of two numbers.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Maximum(decimal[] numbers)
        {
            return Math.Max(numbers[0], numbers[1]);
        }

        /// <summary>
        /// Returns the smaller of two numbers.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns></returns>
        private decimal Minimum(decimal[] numbers)
        {
            return Math.Min(numbers[0], numbers[1]);
        }

        #endregion

        #region - Static Methods -

        /// <summary>
        /// Determines whether the specified segment is a function token.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>
        ///   <c>true</c> if the specified segment is a function token; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFunctionToken(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            FunctionType testVal = ParseValue(segment);
            return (testVal != FunctionType.Unknown);
        }

        /// <summary>
        /// Parses the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        public static FunctionToken Parse(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            FunctionType testVal = ParseValue(segment);
            if (testVal == FunctionType.Unknown)
            {
                throw new ArgumentException(string.Format("The segment \"{0}\" could not be parsed to an operator type.", segment), "segment");
            }

            return new FunctionToken(testVal);
        }

        /// <summary>
        /// Parses the value.  Returns <c>Unknown</c> if the value cannot be parsed, or if the parsed value actually is <c>Unknown</c>.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        private static FunctionType ParseValue(string segment)
        {
            FunctionType testVal = FunctionType.Unknown;
            if (EnumLookup.TryParse<FunctionType>(segment, out testVal))
            {
                return testVal;
            }

            return FunctionType.Unknown;
        }

        #endregion

    }
}
