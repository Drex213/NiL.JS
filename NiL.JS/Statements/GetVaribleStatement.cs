using System;
using NiL.JS.Core.BaseTypes;
using NiL.JS.Core;

namespace NiL.JS.Statements
{
    public sealed class GetVaribleStatement : Statement
    {
        private Context cacheContext;
        private JSObject cacheRes;
        private string varibleName;

        public string VaribleName { get { return varibleName; } }

        /// <summary>
        /// ������ ��� ����, ����� ��������� ����� ���������� � ������ ������ �������� ���� ��������� �������.
        /// ����� ������� �������� ������ � ��������� ����������� ���������� ���������.
        /// </summary>
        private static System.Collections.Generic.Dictionary<string, GetVaribleStatement> cache = new System.Collections.Generic.Dictionary<string, GetVaribleStatement>();

        internal static void ResetCache(string name)
        {
            GetVaribleStatement gvs = null;
            if (cache.TryGetValue(name, out gvs))
                gvs.cacheRes = null;
        }

        internal GetVaribleStatement(string name)
        {
            int i = 0;
            if ((name != "this") && !Parser.ValidateName(name, ref i, false, true, true, false))
                throw new ArgumentException("Invalid varible name");
            this.varibleName = name;
            if (!cache.ContainsKey(name))
                cache[name] = this;
        }

        internal override JSObject InvokeForAssing(Context context)
        {
            context.objectSource = null;
            if (context == cacheContext)
                if (cacheRes == null)
                    return (cacheRes = context.GetField(varibleName));
                else
                    return cacheRes;
            cacheContext = context;
            return cacheRes = context.GetField(varibleName);
        }

        internal override JSObject Invoke(Context context)
        {
            context.objectSource = null;
            if (context == cacheContext)
                if (cacheRes == null)
                    return (cacheRes = context.GetField(varibleName));
                else
                    return cacheRes;
            if (context.GetType() == typeof(WithContext))
            {
                cacheRes = context.GetField(varibleName);
                if (cacheRes.ValueType == JSObjectType.Property)
                    cacheRes = (cacheRes.oValue as NiL.JS.Core.BaseTypes.Function[])[1].Invoke(context.objectSource, null);
                return cacheRes;
            }
            else
            {
                cacheContext = context;
                return cacheRes = context.GetField(varibleName);
            }
        }

        public override string ToString()
        {
            return varibleName;
        }

        internal override bool Optimize(ref Statement _this, int depth, System.Collections.Generic.Dictionary<string, Statement> varibles)
        {
            _this = cache[varibleName];
            return false;
        }
    }
}