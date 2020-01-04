using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;

namespace Horizon.UI
{
    public class LuaFoldingStrategy
    {
        /// <summary>
        /// Creates a new BraceFoldingStrategy.
        /// </summary>
        public LuaFoldingStrategy()
        {
        }

        /// <summary>
        /// Create <see cref="NewFolding"/> s for the specified document.
        /// </summary>
        public IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            return this.CreateNewFoldings(document);
        }

        /// <summary>
        /// Create <see cref="NewFolding"/> s for the specified document.
        /// </summary>
        public IEnumerable<NewFolding> CreateNewFoldings(ITextSource document)
        {
            List<NewFolding> newFoldings = new List<NewFolding>();
            Regex reg = new Regex("^[\\s]*(?:local)?[\\s]+(function)[\\s]+(.*)$", RegexOptions.Multiline, Regex.InfiniteMatchTimeout);
            Regex reg3 = new Regex("^[\\s]*(for)[\\s]+.*$", RegexOptions.Multiline, Regex.InfiniteMatchTimeout);
            Regex reg4 = new Regex("^[\\s]*(if)[\\s]+.*$", RegexOptions.Multiline, Regex.InfiniteMatchTimeout);
            Regex reg2 = new Regex("^[\\s]*(end)[\\s]*$", RegexOptions.Multiline, Regex.InfiniteMatchTimeout);
            List<Match> startMatches = reg.Matches(document.Text).ToList();
            startMatches.AddRange(reg3.Matches(document.Text).ToList());
            startMatches.AddRange(reg4.Matches(document.Text).ToList());
            startMatches = startMatches.OrderBy(x => x.Groups[1].Index).ToList();
            List<Match> endMatches = reg2.Matches(document.Text).ToList().OrderBy(x => x.Index).ToList();

            List<Tuple<int, int>> org = new List<Tuple<int, int>>();
            int endOn = 0;
            while (true)
            {
                if (startMatches.Count == 0 && endMatches.Count == 0)
                {
                    break;
                }
                if (startMatches.FirstOrDefault()?.Groups[1].Index < endMatches?.FirstOrDefault()?.Groups[1].Index)
                {
                    org.Add(new Tuple<int, int>(startMatches.First().Groups[1].Index, 0));
                    startMatches.Remove(startMatches.First());
                }
                else
                {
                    if (endOn > org.Count - 1)
                    {
                        endMatches.Remove(endMatches.First());
                        continue;
                    }
                    org[org.IndexOf(org.Last(x => x.Item2 == 0))] = new Tuple<int, int>(org.Last(x => x.Item2 == 0).Item1, endMatches.First().Groups[1].Index);
                    endMatches.Remove(endMatches.First());
                    endOn++;
                }
            }

            foreach (Tuple<int, int> folding in org)
            {
                if (folding.Item2 == 0)
                {
                    continue;
                }
                newFoldings.Add(new NewFolding(folding.Item1, folding.Item2));
            }

            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return newFoldings;
        }

        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            IEnumerable<NewFolding> newFoldings = this.CreateNewFoldings(document, out int firstErrorOffset);
            manager.UpdateFoldings(newFoldings, firstErrorOffset);
        }
    }
}