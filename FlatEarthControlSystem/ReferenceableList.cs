using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatEarthControlSystem
{
    public class ReferenceableList<T> : List<Referenceable>
    {
        public string EnumerationText
        {
            get
            {
                if (Count <= 0)
                    return "";
                if (Count == 1)
                    return this.First().GetMostRelaxedNameWithIndefiniteArticle();
                if (Count == 2)
                {
                    var first = this.First().RelaxedName;
                    var last = this.Last().RelaxedName;
                    if (string.Compare(first, last, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        first = this.First().GetMostUniqueNameWithIndefiniteArticle();
                        last = this.Last().GetMostUniqueNameWithIndefiniteArticle();
                    }
                    else
                    {
                        first = this.First().GetMostRelaxedNameWithIndefiniteArticle();
                        last = this.Last().GetMostRelaxedNameWithIndefiniteArticle();
                    }
                    return $"{first}{Phrases.And}{last}";
                }

                foreach (var item in this)
                    item.CanUseRelaxedName = true;

                foreach (var item1 in this)
                {
                    foreach (
                        var item2 in this.Where(item2 => item1 != item2)
                            .Where(item2 => !string.IsNullOrWhiteSpace(item1.RelaxedName) && string.Compare(item1.GetMostRelaxedName(), item2.GetMostRelaxedName(), StringComparison.CurrentCultureIgnoreCase) == 0)
                    )
                    {
                        item1.CanUseRelaxedName = false;
                        item2.CanUseRelaxedName = false;
                    }
                }

                var strings = new List<string>();
                foreach (var item in this)
                {
                    if (item.CanUseRelaxedName)
                        strings.Add(item.GetMostRelaxedNameWithIndefiniteArticle());
                    else
                        strings.Add(item.GetMostUniqueNameWithIndefiniteArticle());
                }

                var result = new StringBuilder();
                for (var i = 0; i < strings.Count; i++)
                {
                    result.Append(this[i]);
                    if (i < strings.Count - 2)
                        result.Append(", ");
                    else if (i == strings.Count - 2)
                        result.Append(Phrases.And);
                }

                return result.ToString();
            }
        }
    }
}