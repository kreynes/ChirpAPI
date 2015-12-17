using System;
using System.Linq;

namespace ChirpLib
{
    public class IrcMessage
    {
        private IrcClient client;
        private string prefix;
        private string command;
        private string[] parameters;
        private string trail;

        public string Prefix
        {
            get { return prefix; }
        }

        public string Command
        {
            get { return command; }
        }

        public string[] Parameters
        {
            get { return parameters; }
        }

        public string Trail
        {
            get { return trail; }
        }

        public IrcMessage(string prefix, string command, string[] parameters, string trail)
        {
            this.prefix = prefix;
            this.command = command;
            this.parameters = parameters;
            this.trail = trail;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Prefix, Command, string.Join(" ", Parameters, Trail));
        }
    }
}


