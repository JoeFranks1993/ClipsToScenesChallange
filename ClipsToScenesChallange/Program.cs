using System;

namespace ClipsToScenesChallange
{
    class Program
    {
        static void Main(string[] args)
        {
            var clipToSceneManager = new ClipToSceneManager();
            var scene1 = clipToSceneManager.Process(new char[] { 'a', 'b', 'a', 'b', 'c', 'b', 'a', 'c', 'a', 'd', 'e', 'f', 'e', 'g', 'd', 'e', 'h', 'i', 'j', 'h', 'k', 'l', 'i', 'j' });
            var scene2 = clipToSceneManager.Process(new char[] { 'a', 'b', 'c' });
            var scene3 = clipToSceneManager.Process(new char[] { 'a', 'b', 'c', 'a' });
        }
    }
}
