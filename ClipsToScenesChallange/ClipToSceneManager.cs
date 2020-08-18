using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClipsToScenesChallange
{
    public class ClipToSceneManager
    {
        public int[] Process(char[] clips)
        {
            var clipMapping = new Dictionary<char, ScenePositionState>();

            BuildClipMapping(clips, clipMapping);
            return GroupClips(clips, clipMapping);
        }

        private int[] GroupClips(char[] clips, Dictionary<char, ScenePositionState> clipMapping)
        {
            var result = new List<int>();
            var processedClips = new List<char>();
            
            for (var i = 0; i < clips.Length; i++)
            {
                if (!processedClips.Contains(clips[i]))
                {
                    var charsInRange = new List<char>() { clips[i] };

                    foreach (var kvp in clipMapping)
                    {
                        if ((ValueIsBetween(kvp.Value.EarliestPosition, clipMapping[clips[i]].EarliestPosition, GetCharHighestPosition(charsInRange, clipMapping)) ||
                            ValueIsBetween(kvp.Value.HighestPositition, clipMapping[clips[i]].EarliestPosition, GetCharHighestPosition(charsInRange, clipMapping))) &&
                            clips[i] != kvp.Key)
                        {
                            charsInRange.Add(kvp.Key);
                        }
                    }

                    var highestIndexOfRelevantChars = GetCharHighestPosition(charsInRange, clipMapping);
                    // reuslt.Add(i == 0 ? 1 + highestIndexOfRelevantChars : i + highestIndexOfRelevantChars);
                    result.Add((highestIndexOfRelevantChars - i) + 1);
                    processedClips.AddRange(charsInRange);
                }
            }

            return result.ToArray();
        }

        private bool ValueIsBetween(int value, int x, int y)
        {
            return value >= x && value <= y;
        }

        private int GetCharHighestPosition(List<char> source, Dictionary<char, ScenePositionState> clipMapping)
        {
            var highestIndex = 0;
            
            foreach (var c in source)
            {
                if (clipMapping[c].HighestPositition > highestIndex) highestIndex = clipMapping[c].HighestPositition;
            }

            return highestIndex;
        }

        private void BuildClipMapping(char[] clips, Dictionary<char, ScenePositionState> clipMapping)
        {
            for (var i = 0; i < clips.Length; i++)
            {
                if (!clipMapping.ContainsKey(clips[i])) // if we do not have an entry for this char yet, create one with the current iteration as the lowest appearance.
                {
                    clipMapping.Add(clips[i], new ScenePositionState() { EarliestPosition = i, HighestPositition = i });
                }
                else if (clipMapping[clips[i]].HighestPositition < i) // else, we do have an entry, set the current iteration as the highest apperance. 
                {
                    clipMapping[clips[i]].HighestPositition = i;
                }
            }
        }
    }
}