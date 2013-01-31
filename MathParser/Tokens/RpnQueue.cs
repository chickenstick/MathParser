#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathParser.Tokens.Enums;

#endregion

namespace MathParser.Tokens
{
    /// <summary>
    /// Represents a queue of tokens in Reverse Polish Notation order.
    /// </summary>
    internal class RpnQueue : Queue<IToken>
    {

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="RpnQueue"/> class.
        /// </summary>
        public RpnQueue()
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
        /// Evaluates the Reverse Polish Notation expression.
        /// </summary>
        /// <returns></returns>
        public decimal Evaluate()
        {
            RpnQueue clone = Clone();
            Stack<IToken> stack = new Stack<IToken>();

            while (clone.Any())
            {
                IToken current = clone.Dequeue();

                switch(current.TokenType)
                {
                    case TokenType.Number:
                        EvaluateNumber(current, stack);
                        break;

                    case TokenType.Function:
                        EvaluateFunction(current, stack);
                        break;

                    case TokenType.Operator:
                        EvaluateOperator(current, stack);
                        break;

                    default:
                        throw new InvalidOperationException(string.Format("Should not have any tokens of type '{0}' in the queue while evaluating.", current.TokenType));
                }
            }

            if (stack.Count == 1 && stack.Peek().TokenType == TokenType.Number)
            {
                NumberToken token = (NumberToken)stack.Pop();
                return token.Value;
            }
            else
            {
                throw new InvalidOperationException("There should have only been one token at the stack at this point.");
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public RpnQueue Clone()
        {
            IToken[] tokenArray = new IToken[this.Count];

            RpnQueue clone = new RpnQueue();
            foreach (IToken token in this)
            {
                clone.Enqueue(token);
            }

            return clone;
        }

        #endregion

        #region - Evaluation Methods -

        /// <summary>
        /// Evaluates the number.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="stack">The stack.</param>
        private void EvaluateNumber(IToken current, Stack<IToken> stack)
        {
            stack.Push(current);
        }

        /// <summary>
        /// Evaluates the function.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="stack">The stack.</param>
        private void EvaluateFunction(IToken current, Stack<IToken> stack)
        {
            FunctionToken functionToken = (FunctionToken)current;

            IToken[] args = new IToken[functionToken.ArgumentCount];

            for (int i = args.Length - 1; i >= 0; i--)
            {
                args[i] = stack.Pop();
            }

            decimal result = functionToken.Evaluate(args);
            stack.Push(new NumberToken(result));
        }

        /// <summary>
        /// Evaluates the operator.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="stack">The stack.</param>
        private void EvaluateOperator(IToken current, Stack<IToken> stack)
        {
            OperatorToken operatorToken = (OperatorToken)current;

            IToken[] args = new IToken[operatorToken.ArgumentCount];

            // Make sure we get the args in the correct order.
            for (int i = args.Length - 1; i >= 0; i--)
            {
                args[i] = stack.Pop();
            }

            decimal result = operatorToken.Evaluate(args);
            stack.Push(new NumberToken(result));
        }

        #endregion

        #region - Static Methods -

        /// <summary>
        /// Builds a <see cref="RpnQueue"/> from a <see cref="NormalNotationQueue"/>.
        /// </summary>
        /// <param name="normalQueue">The <see cref="NormalNotationQueue"/>.</param>
        /// <returns>A <see cref="RpnQueue"/>.</returns>
        public static RpnQueue FromNormalNotationQueue(NormalNotationQueue normalQueue)
        {
            // Clone the argument queue so as to not destroy the reference.
            NormalNotationQueue tempNormalQueue = normalQueue.Clone();

            // DO NOT TOUCH normalQueue after this point.

            // Use the Shunting-yard algorithm
            RpnQueue outputQueue = new RpnQueue();
            Stack<IToken> tokenStack = new Stack<IToken>();

            while (tempNormalQueue.Any())
            {
                IToken token = tempNormalQueue.Dequeue();

                switch(token.TokenType)
                {
                    case TokenType.Number:
                        HandleNumberToken(token, outputQueue, tokenStack);
                        break;

                    case TokenType.Function:
                        HandleFunctionToken(token, outputQueue, tokenStack);
                        break;

                    case TokenType.ArgumentSeparator:
                        HandleArgumentSeparator(token, outputQueue, tokenStack);
                        break;

                    case TokenType.Operator:
                        HandleOperatorToken(token, outputQueue, tokenStack);
                        break;

                    case TokenType.LeftParenthesis:
                        HandleLeftParenthesisToken(token, outputQueue, tokenStack);
                        break;

                    case TokenType.RightParenthesis:
                        HandleRightParenthesisToken(token, outputQueue, tokenStack);
                        break;

                    default:
                        throw new ArgumentException(string.Format("Unsupported token type '{0}'.", token.TokenType), "normalQueue");
                }
            }

            while (tokenStack.Any())
            {
                IToken topOfStack = tokenStack.Pop();

                if (topOfStack.TokenType == TokenType.LeftParenthesis || topOfStack.TokenType == TokenType.RightParenthesis)
                {
                    throw new InvalidOperationException("There are mismatched parentheses.");
                }

                outputQueue.Enqueue(topOfStack);
            }

            return outputQueue;
        }

        /// <summary>
        /// Handles the number token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="outputQueue">The output queue.</param>
        /// <param name="tokenStack">The token stack.</param>
        private static void HandleNumberToken(IToken token, Queue<IToken> outputQueue, Stack<IToken> tokenStack)
        {
            outputQueue.Enqueue(token);
        }

        /// <summary>
        /// Handles the function token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="outputQueue">The output queue.</param>
        /// <param name="tokenStack">The token stack.</param>
        private static void HandleFunctionToken(IToken token, Queue<IToken> outputQueue, Stack<IToken> tokenStack)
        {
            tokenStack.Push(token);
        }

        /// <summary>
        /// Handles the argument separator.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="outputQueue">The output queue.</param>
        /// <param name="tokenStack">The token stack.</param>
        private static void HandleArgumentSeparator(IToken token, Queue<IToken> outputQueue, Stack<IToken> tokenStack)
        {
            IToken topOfStack = tokenStack.Peek();

            while (topOfStack.TokenType != TokenType.LeftParenthesis)
            {
                IToken item = tokenStack.Pop();
                outputQueue.Enqueue(item);
            }
        }

        /// <summary>
        /// Handles the operator token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="outputQueue">The output queue.</param>
        /// <param name="tokenStack">The token stack.</param>
        private static void HandleOperatorToken(IToken token, Queue<IToken> outputQueue, Stack<IToken> tokenStack)
        {
            OperatorToken currentOperator = (OperatorToken)token;

            bool topOfStackIsOperator = false;
            bool isLeftAssociativeAndLessPrecedent = false;
            bool hasLowerPrecedence = false;

            do
            {
                if (tokenStack.Any())
                {
                    IToken topOfStack = tokenStack.Peek();
                    topOfStackIsOperator = topOfStack.TokenType == TokenType.Operator;

                    if (topOfStackIsOperator)
                    {
                        OperatorToken stackOperator = (OperatorToken)topOfStack;

                        isLeftAssociativeAndLessPrecedent = (currentOperator.Associativity == Associativity.LeftAssociative) && (currentOperator.Precedence <= stackOperator.Precedence);
                        hasLowerPrecedence = currentOperator.Precedence < stackOperator.Precedence;

                        if (isLeftAssociativeAndLessPrecedent || hasLowerPrecedence)
                        {
                            stackOperator = (OperatorToken)tokenStack.Pop();
                            outputQueue.Enqueue(stackOperator);
                        }
                    }
                }
                else
                {
                    topOfStackIsOperator = false;
                }
            } while (topOfStackIsOperator && (isLeftAssociativeAndLessPrecedent || hasLowerPrecedence));

            tokenStack.Push(currentOperator);
        }

        /// <summary>
        /// Handles the left parenthesis token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="outputQueue">The output queue.</param>
        /// <param name="tokenStack">The token stack.</param>
        private static void HandleLeftParenthesisToken(IToken token, Queue<IToken> outputQueue, Stack<IToken> tokenStack)
        {
            tokenStack.Push(token);
        }

        /// <summary>
        /// Handles the right parenthesis token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="outputQueue">The output queue.</param>
        /// <param name="tokenStack">The token stack.</param>
        private static void HandleRightParenthesisToken(IToken token, Queue<IToken> outputQueue, Stack<IToken> tokenStack)
        {
            bool isLeftParenthesis = false;

            do
            {
                if (tokenStack.Any())
                {
                    IToken topOfStack = tokenStack.Peek();
                    isLeftParenthesis = topOfStack.TokenType == TokenType.LeftParenthesis;

                    if (!isLeftParenthesis)
                    {
                        topOfStack = tokenStack.Pop();
                        outputQueue.Enqueue(topOfStack);
                    }
                }
                else
                {
                    isLeftParenthesis = false;
                }
            } while (!isLeftParenthesis);

            ParenthesisToken leftParenthesis = (ParenthesisToken)tokenStack.Pop();
            if (leftParenthesis.ParenthesisType != ParenthesisType.Left)
            {
                throw new InvalidOperationException("The parenthesis should be a left parenthesis.");
            }

            bool topOfStackIsFunctionToken = tokenStack.Any() && tokenStack.Peek().TokenType == TokenType.Function;
            if (topOfStackIsFunctionToken)
            {
                FunctionToken functionToken = (FunctionToken)tokenStack.Pop();
                outputQueue.Enqueue(functionToken);
            }
        }

        #endregion

    }
}
