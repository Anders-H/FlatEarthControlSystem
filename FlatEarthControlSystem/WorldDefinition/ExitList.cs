using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatEarthControlSystem.WorldDefinition
{
    public class ExitList : List<Exit>
    {
        public ExitList()
        {
        }

        public ExitList(IEnumerable<Exit> exits)
        {
            AddRange(exits);
        }

        public Exit GetDiscoveredExit(string direction) =>
            this.FirstOrDefault(x => x.Discovered && x.DirectionName == direction);

        public Exit GetAnyExit(string direction) =>
            this.FirstOrDefault(x => x.DirectionName == direction);

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("EXITS ARE: ");
            switch (Count)
            {
                case 0:
                    result.Append("NONE.");
                    break;
                case 1:
                    result.Append(this.First());
                    result.Append(".");
                    break;
                case 2:
                    result.Append(this.First());
                    result.Append(" AND ");
                    result.Append(this.Last());
                    result.Append(".");
                    break;
                default:

                    for (var i = 0; i < Count; i++)
                    {
                        if (i == Count - 2)
                            result.Append($"{this[i]} AND ");
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