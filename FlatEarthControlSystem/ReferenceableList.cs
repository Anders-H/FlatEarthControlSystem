using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatEarthControlSystem.Extensions;

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
                    if (first.Is(last))
                    {
                        first = this.First().GetMostUniqueNameWithIndefiniteArticle();
                        last = this.Last().GetMostUniqueNameWithIndefiniteArticle();
                    }
                    else
                    {
                        first = this.First().GetMostRelaxedNameWithIndefiniteArticle();
                        last = this.Last().GetMostRelaxedNameWithIndefiniteArticle();
                    }
                    return $"{first} {Phrases.And} {last}";
                }

                foreach (var item in this)
                    item.CanUseRelaxedName = true;

                foreach (var item1 in this)
                {
                    foreach (
                        var item2 in this
                            .Where(item2 => item1 != item2)
                            .Where(item2 => !item1.RelaxedName.IsEmpty() && item1.GetMostRelaxedName().Is(item2.GetMostRelaxedName()))
                    )
                    {
                        item1.CanUseRelaxedName = false;
                        item2.CanUseRelaxedName = false;
                    }
                }

                var strings = this.Select(
                    item => item.CanUseRelaxedName
                        ? item.GetMostRelaxedNameWithIndefiniteArticle()
                        : item.GetMostUniqueNameWithIndefiniteArticle()
                ).ToList();

                var result = new StringBuilder();
                for (var i = 0; i < strings.Count; i++)
                {
                    result.Append(strings[i]);
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