using Neptuo.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class UrlParser
    {
        public bool ParseUrl(string url, out IReadOnlyUrl parsedUrl)
        {
            parsedUrl = null;
            return false;
        }

        private class UrlStateMachine : StringStateMachine<UrlState>
        {
            public UrlStateMachine()
                : base(new UrlSchemaState())
            {

            }
        }

        private abstract class UrlState : StringState<object, UrlState>
        {
        }

        private class UrlErrorState : UrlState
        {
            public override UrlState Accept(char item, int position)
            {
                return this;
            }
        }

        private class UrlSchemaState : UrlState
        {
            private bool isSemicolon;
            private bool isSlash;

            public override UrlState Accept(char item, int position)
            {
                if (item == ':')
                {
                    if (isSemicolon || isSlash)
                        return Move<UrlErrorState>();

                    isSemicolon = true;
                }
                else if (item == '/')
                {
                    if (!isSemicolon)
                        return Move<UrlErrorState>();

                    if (isSlash)
                        return Move<UrlDomainState>();

                    isSlash = true;
                }
                else
                {
                    if (isSemicolon || isSlash)
                        return Move<UrlErrorState>();

                    Text.Append(item);
                }

                return this;
            }
        }

        private class UrlDomainState : UrlState
        {
            public override UrlState Accept(char item, int position)
            {
                throw new NotImplementedException();
            }
        }

    }
}
