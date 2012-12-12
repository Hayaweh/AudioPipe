using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APGameEngine;
using APGameGenerator;
using APSoundAnalyzer;

namespace AudioPipe
{
    static class AudioPipe
    {
        [STAThread]
        static void Main(string[] args)
        {
            GameEngine game = new GameEngine();
            GameGenerator generator = new GameGenerator();
            XMLAnalyzer analyzer = new XMLAnalyzer();
            game.setGameGenerator(generator);
            game.setSoundAnalyzer(analyzer);
            game.Run();
        }
    }
}
