﻿using System.Collections.Generic;
using System.Linq;

namespace TSQLLint.Common
{
    public class FileLineActions
    {
        private readonly List<string> FileLines;

        private readonly List<IRuleViolation> RuleViolations;

        public FileLineActions(List<IRuleViolation> ruleViolations, List<string> fileLines)
        {
            RuleViolations = ruleViolations;
            FileLines = fileLines;
        }

        public void Insert(int index, string line)
        {
            FileLines.InsertRange(index, new[] { line });
        }

        public void InsertRange(int index, IList<string> lines)
        {
            FileLines.InsertRange(index, lines);

            foreach (var v in RuleViolations.Where(x => x.Line > index))
            {
                v.Line += lines.Count;
            }
        }

        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        public void RemoveRange(int index, int count)
        {
            FileLines.RemoveRange(index, count);

            foreach (var v in RuleViolations.Where(x => x.Line > index))
            {
                v.Line -= count;
            }
        }
    }
}