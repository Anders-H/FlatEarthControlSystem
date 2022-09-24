using FlatEarthControlSystem.Extensions;

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
            !UniqueName.IsEmpty() || !RelaxedName.IsEmpty();

        public bool ItemHasMultipleNames =>
            !UniqueName.IsEmpty()
            && !RelaxedName.IsEmpty()
            && !UniqueName.Is(RelaxedName);

        public string GetMostUniqueName() =>
            string.IsNullOrWhiteSpace(UniqueName)
                ? RelaxedName
                : UniqueName;

        public string GetMostRelaxedName() =>
            RelaxedName.IsEmpty()
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