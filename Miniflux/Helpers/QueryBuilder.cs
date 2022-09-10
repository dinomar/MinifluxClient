using Miniflux.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Miniflux.Helpers
{
    public class QueryBuilder
    {
        private readonly StringBuilder _sb = new StringBuilder("?");

        public void AddItem(string name, object obj)
        {
            if (obj != null) { _sb.Append($"{name}={obj}&"); }
        }

        public void AddEnum(string name, Enum obj)
        {
            if (obj != null)
            {
                if (Attribute.IsDefined(obj.GetType(), typeof(FlagsAttribute)))
                {
                    List<string> selected = new List<string>();

                    foreach (Enum value in Enum.GetValues(obj.GetType()))
                    {
                        if (obj.HasFlag(value))
                        {
                            selected.Add(value.ToString());
                        }
                    }

                    if (selected.Count == 1)
                    {
                        _sb.Append($"{name}={Enum.GetName(obj.GetType(), obj)}&");
                    }
                    else
                    {
                        string temp = "[";
                        foreach (var item in selected) { temp += $"'{item}',"; }
                        temp = temp.Substring(0, temp.Length - 1);
                        temp += "]";

                        _sb.Append($"{name}={temp}&");
                    }
                }
                else
                {
                    _sb.Append($"{name}={Enum.GetName(obj.GetType(), obj)}&");
                }
            }
        }

        public void Clear()
        {
            _sb.Clear();
        }

        public override string ToString()
        {
            return (_sb.Length > 1) ? HttpUtility.UrlEncode(_sb.ToString(0, _sb.Length - 1).ToLower()) : string.Empty;
        }
    }
}
