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
    /// A token that represents a numeric value.
    /// </summary>
    internal sealed class NumberToken : IToken
    {

        #region - Fields -

        private decimal _value;

        #endregion

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberToken"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public NumberToken(decimal value)
        {
            this._value = value;
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
                return TokenType.Number;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public decimal Value
        {
            get
            {
                return _value;
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
            return _value.ToString();
        }

        #endregion

        #region - Static Methods -

        /// <summary>
        /// Determines whether the specified segment is a number token.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>
        ///   <c>true</c> if the specified segment is a number token; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumberToken(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            decimal? value = ParseSegment(segment);
            return value.HasValue;
        }

        /// <summary>
        /// Parses the specified segment to a <see cref="NumberToken"/>.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        public static NumberToken Parse(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            decimal? value = ParseSegment(segment);

            if (!value.HasValue)
            {
                throw new ArgumentException("The segment could not be parsed to a number value.", "segment");
            }

            return new NumberToken(value.Value);
        }

        /// <summary>
        /// Parses the segment to an <see cref="int?"/>.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        private static decimal? ParseSegment(string segment)
        {
            decimal? value = null;

            decimal testVal = 0;
            if (decimal.TryParse(segment, out testVal))
            {
                value = testVal;
            }
            else
            {
                // Check if the segment is a constant value.
                ConstantType constType = ConstantType.Unknown;
                if (EnumLookup.TryParse<ConstantType>(segment, out constType) && constType != ConstantType.Unknown)
                {
                    value = constType.GetConstantValue();
                }
            }

            return value;
        }

        #endregion

    }
}
