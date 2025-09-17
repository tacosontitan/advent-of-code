using System.Text.RegularExpressions;

namespace Aoc24;

public sealed partial class DayThree
{
    private sealed class AocLexer(string input)
    {
        private const string UnexpectedTokenMessage =
            "This should be an unexpected token exception, but oh well. ¯\\_(ツ)_/¯";
        
        private int _currentIndex;

        internal IToken GetNextToken()
        {
            SkipIgnoredCharacters();
            if (TryGetEnableMulToken(out IToken result))
                return result;

            if (TryGetDisableMulToken(out result))
                return result;

            if (TryGetMulToken(out result))
                return result;
        }

        private void SkipIgnoredCharacters()
        {
            const string nonIgnoredCharacters = "dm";
            while (_currentIndex < input.Length)
            {
                var currentCharacter = input[_currentIndex];
                if (!nonIgnoredCharacters.Contains(currentCharacter))
                    _currentIndex++;
                else
                    break;
            }
        }

        private bool TryGetEnableMulToken(out IToken? result)
        {
            if (input[_currentIndex] != 'd')
            {
                result = null;
                return false;
            }
            
            var startIndex = _currentIndex;
            var endIndex = input.IndexOf(')', _currentIndex + 1);
            if (endIndex == -1)
                throw new InvalidOperationException(UnexpectedTokenMessage);
            
            var value = input.Substring(startIndex, endIndex - startIndex + 1);
            if (value != "do()")
            {
                result = null;
                return false;
            }
            
            _currentIndex = endIndex + 1;
            result = new EnableMulToken();
            return true;
        }
        
        private bool TryGetDisableMulToken(out IToken? result)
        {
            if (input[_currentIndex] != 'd')
            {
                result = null;
                return false;
            }
            
            var startIndex = _currentIndex;
            var endIndex = input.IndexOf(')', _currentIndex + 1);
            if (endIndex == -1)
                throw new InvalidOperationException(UnexpectedTokenMessage);
            
            var value = input.Substring(startIndex, endIndex - startIndex + 1);
            if (value != "don't()")
            {
                result = null;
                return false;
            }
            
            _currentIndex = endIndex + 1;
            result = new DisableMulToken();
            return true;
        }
        
        private bool TryGetMulToken(out IToken? result)
        {
            const string regexPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            if (input[_currentIndex] != 'd')
            {
                result = null;
                return false;
            }
            
            var startIndex = _currentIndex;
            var endIndex = input.IndexOf(')', _currentIndex + 1);
            if (endIndex == -1)
                throw new InvalidOperationException(UnexpectedTokenMessage);
            
            var value = input.Substring(startIndex, endIndex - startIndex + 1);
            var matches = Regex.Matches(value, regexPattern);
            foreach (Match match in matches)
            {
                var leftOperand = int.Parse(match.Groups[1].Value);
                var rightOperand = int.Parse(match.Groups[2].Value);
                sum += leftOperand * rightOperand;
            }
        }
    }
}