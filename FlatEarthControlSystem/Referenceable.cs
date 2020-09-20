using System;

namespace FlatEarthControlSystem
{
    public class Referenceable
    {
        public string UniqueName { get; }

        public string IndefiniteArticleForUniqueName { get; }
        
        public string RelaxedName { get; }

        public string IndefiniteArticleForRelaxedName { get; }

        public bool CanUseRelaxedName { get; set; }

        public Referenceable(
            string uniqueName,
            string indefiniteArticleForUniqueName,
            string relaxedName,
            string indefiniteArticleForRelaxedName)
        {
            UniqueName = uniqueName;
            IndefiniteArticleForUniqueName = indefiniteArticleForUniqueName;
            RelaxedName = relaxedName;
            IndefiniteArticleForRelaxedName = indefiniteArticleForRelaxedName;
        }

        public bool HasName =>
            !string.IsNullOrWhiteSpace(UniqueName)
            || !string.IsNullOrWhiteSpace(RelaxedName);

        public bool ItemHasMultipleNames =>
            !string.IsNullOrWhiteSpace(UniqueName)
            && !string.IsNullOrWhiteSpace(RelaxedName)
            && string.Compare(UniqueName, RelaxedName, StringComparison.CurrentCultureIgnoreCase) != 0;

        public string GetMostUniqueName() =>
            string.IsNullOrWhiteSpace(UniqueName)
                ? RelaxedName
                : UniqueName;

        public string GetMostRelaxedName() =>
            string.IsNullOrWhiteSpace(RelaxedName)
                ? UniqueName
                : RelaxedName;

        public string GetMostUniqueNameWithIndefiniteArticle() =>
            string.IsNullOrWhiteSpace(UniqueName)
                ? $"{IndefiniteArticleForRelaxedName} {RelaxedName}"
                : $"{IndefiniteArticleForUniqueName} {UniqueName}";

        public string GetMostRelaxedNameWithIndefiniteArticle() =>
            string.IsNullOrWhiteSpace(RelaxedName)
                ? $"{IndefiniteArticleForUniqueName} {UniqueName}"
                : $"{IndefiniteArticleForRelaxedName} {RelaxedName}";
    }
}