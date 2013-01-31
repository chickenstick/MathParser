#region - Properties -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnumHelper;
using MathParser.Tokens.Enums;

#endregion

namespace MathParser.Tokens
{
    /// <summary>
    /// A token that represents a parenthesis.
    /// </summary>
    internal struct ParenthesisToken : IToken
    {

        #region - Fields -

        private ParenthesisType _parenthesisType;
        private TokenType _tokenType;

        #endregion

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthesisToken"/> struct.
        /// </summary>
        /// <param name="parenthesisType">Type of the parenthesis.</param>
        public ParenthesisToken(ParenthesisType parenthesisType)
        {
            if (parenthesisType != ParenthesisType.Left && parenthesisType != ParenthesisType.Right)
            {
                throw new ArgumentException("The parenthesis type must be either Left or Right.", "parenthesisType");
            }

            this._parenthesisType = parenthesisType;
            this._tokenType = (parenthesisType == ParenthesisType.Left) ? TokenType.LeftParenthesis : TokenType.RightParenthesis;
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
                return _tokenType;
            }
        }

        /// <summary>
        /// Gets the type of the parenthesis.
        /// </summary>
        /// <value>
        /// The type of the parenthesis.
        /// </value>
        public ParenthesisType ParenthesisType
        {
            get
            {
                return _parenthesisType;
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
            return EnumLookup.Parse(this._parenthesisType);
        }

        #endregion

        #region - Static Methods -

        /// <summary>
        /// Determines whether the specified segment is a parenthesis.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>
        ///   <c>true</c> if the specified segment is a parenthesis; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsParenthesis(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            ParenthesisType result = ParseValue(segment);
            return result != ParenthesisType.Unknown;
        }

        /// <summary>
        /// Parses the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        public static ParenthesisToken Parse(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            ParenthesisType parType = ParseValue(segment);

            if (parType == ParenthesisType.Unknown)
            {
                throw new ArgumentException(string.Format("Could not convert the segment '{0}' to a ParenthesisType.", segment), "segment");
            }

            return new ParenthesisToken(parType);
        }

        /// <summary>
        /// Parses the value.  A return value of <c>Unknown</c> means it was unable to parse the value.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        private static ParenthesisType ParseValue(string segment)
        {
            string cleanedUp = segment.Trim();
            ParenthesisType result = ParenthesisType.Unknown;

            ParenthesisType testVal = ParenthesisType.Unknown;
            if (EnumLookup.TryParse<ParenthesisType>(cleanedUp, out testVal))
            {
                result = testVal;
            }

            return result;
        }

        #endregion

    }
}
