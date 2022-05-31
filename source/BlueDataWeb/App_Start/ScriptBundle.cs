using System;
using System.Web.Optimization;

namespace BlueDataWeb
{
    internal class ScriptBundle
    {
        private string v;

        public ScriptBundle(string v)
        {
            this.v = v;
        }

        internal Bundle Include(string v1, string v2, string v3, string v4, string v5, string v6)
        {
            throw new NotImplementedException();
        }

        internal Bundle Include(string v)
        {
            throw new NotImplementedException();
        }
    }
}