#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace MathParser.Tokens
{
    /// <summary>
    /// Represents a queue of tokens in normal notation order.
    /// </summary>
    internal class NormalNotationQueue : Queue<IToken>
    {

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalNotationQueue"/> class.
        /// </summary>
        public NormalNotationQueue()
            : base()
        {
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
            return string.Join(" ", this.ToList().ConvertAll<string>(i => i.ToString()));
        }

        /// <summary>
        /// Converts this instance to an <see cref="RpnQueue"/> instance.
        /// </summary>
        /// <returns></returns>
        public RpnQueue ToReversePolishNotation()
        {
            return RpnQueue.FromNormalNotationQueue(this);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public NormalNotationQueue Clone()
        {
            IToken[] tokenArray = new IToken[this.Count];

            NormalNotationQueue clone = new NormalNotationQueue();
            foreach (IToken token in this)
            {
                clone.Enqueue(token);
            }

            return clone;
        }

        #endregion

        #region - Static Methods -

        /// <summary>
        /// Builds a <see cref="NormalNotationQueue"/> from the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static NormalNotationQueue FromExpression(string expression)
        {
            NormalNotationQueue queue = new NormalNotationQueue();

            string sanitized = SanitizeExpression(expression);
            string[] tokenStrings = sanitized.Split(' ');

            foreach (string strToken in tokenStrings)
            {
                IToken token = TokenFactory.GetToken(strToken);
                queue.Enqueue(token);
            }

            return queue;
        }

        /// <summary>
        /// Sanitizes the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        private static string SanitizeExpression(string expression)
        {
            string buffer = expression.Trim().ToLower();

            // Capture numbers
            buffer = Regex.Replace(buffer, @"(?<number>\d+(\.\d+)?)", @" ${number} ");

            // Capture operators and parentheses
            buffer = Regex.Replace(buffer, @"(?<ops>[+\-*/^%()])", @" ${ops} ");

            // Captures alphabets. Currently captures the two math constants PI and E,
            // and the 3 basic trigonometry functions, sine, cosine and tangent.
            buffer = Regex.Replace(buffer, "(?<alpha>(pi|e|sin|cos|tan|max|min))", " ${alpha} ");

            // Trim extra spaces
            buffer = Regex.Replace(buffer, @"\s+", " ").Trim();



            // The following chunk captures unary minus operations:

            // 1) We replace every minus sign with the string "MINUS".
            buffer = Regex.Replace(buffer, "-", "MINUS");

            // 2) Then if we find a "MINUS" with a number or constant in front,
            //    then it's a normal minus operation.  Looking for pi or e or generic number \d+(\.\d+)?
            buffer = Regex.Replace(buffer, @"(?<number>(pi|e|(\d+(\.\d+)?)))\s+MINUS", "${number} -");

            // 3) Otherwise, it's a unary minus operation.  Use the tilde ~ as the unary minus operator.
            buffer = Regex.Replace(buffer, "MINUS", "~");

            return buffer;
        }

        #endregion

    }
}
