using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatEarthControlSystem.WorldDefinition
{
    public class ExitList : List<Exit>
    {
        protected Room? Parent { get; set; }

        public ExitList()
        {
        }

        public ExitList(IEnumerable<Exit> exits)
        {
            AddRange(exits);
        }

        public Exit? GetDiscoveredExit(string? direction) =>
            direction == null
                ? null
                : this.FirstOrDefault(
                    x => x.Discovered && x.DirectionName == direction.ToString()
                );

        public Exit? GetAnyExit(string direction) =>
            this.FirstOrDefault(x => x.DirectionName == direction);

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append(StandardAnswers.ExitsAre);
            switch (Count)
            {
                case 0:
                    result.Append(StandardAnswers.None);
                    break;
                case 1:
                    result.Append(this.First());
                    result.Append(".");
                    break;
                case 2:
                    result.Append(this.First());
                    result.Append(StandardAnswers.And);
                    result.Append(this.Last());
                    result.Append(".");
                    break;
                default:

                    for (var i = 0; i < Count; i++)
                    {
                        if (i == Count - 2)
                            result.Append($"{this[i]}{StandardAnswers.And}");
                        else if (i == Count - 1)
                            result.Append($"{this[i]}.");
                        else
                            result.Append($"{this[i]}, ");
                    }
                    break;
            }
            return result
                .ToString()
                .Trim();
        }
    }
}