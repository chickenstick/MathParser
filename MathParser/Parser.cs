#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathParser.Tokens;

#endregion

namespace MathParser
{
    /// <summary>
    /// Static class used to parse and evaluate expressions.
    /// </summary>
    public static class Parser
    {

        #region - Public Methods -

        /// <summary>
        /// Cleans up the normal form expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string CleanUpNormalFormExpression(string expression)
        {
            NormalNotationQueue normalQueue = NormalNotationQueue.FromExpression(expression);
            return normalQueue.ToString();
        }

        /// <summary>
        /// Converts to Reverse Polish Notation.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string ConvertToReversePolishNotation(string expression)
        {
            NormalNotationQueue normalQueue = NormalNotationQueue.FromExpression(expression);
            RpnQueue rpn = normalQueue.ToReversePolishNotation();
            return rpn.ToString();
        }

        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static decimal EvaluateExpression(string expression)
        {
            NormalNotationQueue normalQueue = NormalNotationQueue.FromExpression(expression);
            RpnQueue rpn = normalQueue.ToReversePolishNotation();
            return rpn.Evaluate();
        }

        #endregion

    }
}
