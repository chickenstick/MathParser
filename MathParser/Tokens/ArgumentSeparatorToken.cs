#region - Using Statements -

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
    /// A token that represents an argument separator.
    /// </summary>
    internal struct ArgumentSeparatorToken : IToken
    {

        #region - Fields -

        private ArgumentSeparatorType _separatorType;

        #endregion

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentSeparatorToken"/> struct.
        /// </summary>
        /// <param name="separatorType">Type of the separator.</param>
        public ArgumentSeparatorToken(ArgumentSeparatorType separatorType)
        {
            if (separatorType != ArgumentSeparatorType.Comma)
            {
                throw new ArgumentException("The separator type is not a valid type.", "separatorType");
            }

            this._separatorType = separatorType;
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
                return TokenType.ArgumentSeparator;
            }
        }

        /// <summary>
        /// Gets the type of the separator.
        /// </summary>
        /// <value>
        /// The type of the separator.
        /// </value>
        public ArgumentSeparatorType SeparatorType
        {
            get
            {
                return this._separatorType;
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
            return EnumLookup.Parse<ArgumentSeparatorType>(this._separatorType);
        }

        #endregion

        #region - Static Methods -

        /// <summary>
        /// Determines whether the specified segment is an argument separator.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>
        ///   <c>true</c> if the specified segment is an argument separator; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsArgumentSeparator(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            ArgumentSeparatorType separatorType = ParseValue(segment);
            return separatorType != ArgumentSeparatorType.Unknown;
        }

        /// <summary>
        /// Parses the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        public static ArgumentSeparatorToken Parse(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new ArgumentNullException("segment");
            }

            ArgumentSeparatorType separatorType = ParseValue(segment);

            if (separatorType == ArgumentSeparatorType.Unknown)
            {
                throw new ArgumentException(string.Format("The segment \"{0}\" could not be parsed to an argument separator type.", segment), "segment");
            }

            return new ArgumentSeparatorToken(separatorType);
        }

        /// <summary>
        /// Parses the value.  Returns <c>Unknown</c> if the value could not be parsed.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        private static ArgumentSeparatorType ParseValue(string segment)
        {
            ArgumentSeparatorType testVal = ArgumentSeparatorType.Unknown;
            if (EnumLookup.TryParse<ArgumentSeparatorType>(segment, out testVal) && testVal != ArgumentSeparatorType.Unknown)
            {
                return testVal;
            }

            return ArgumentSeparatorType.Unknown;
        }

        #endregion

    }
}
