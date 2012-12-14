using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

#region Autor. How to use?
/*
 * APSoundAnalyser, class by Julien Bernard (jbernard@intechinfo.fr) for IN'TECH INFO's AudioPipe project - 2012
 * 
 * How to use (example):
 *  List<string> Instruments = new List<string>();                                                                                      - Create your list
 *  Instruments = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\mySong.xml", "/score-partwise/part-list/score-part/part-name");   - Take and put informations into this list
 *  
 */
#endregion

namespace APSoundAnalyzer
{
    public class XMLAnalyzer
    {
        #region InstrumentsInformations

        /// <summary>
        /// Return a list who include partition's informations from instrument selected.
        /// </summary>
        /// <param name="instrumentChoiceNumber">Instrument's number. Example: part-list id="p5" => instrument number = 5</param>
        /// <returns>Dictionary for all data included in note node (from measure node)</returns>
        public static Dictionary<string, List<string>> InstrumentInformations(string instrumentChoiceNumber)
        {
            // We creating the list to make data
            List<string> noteStep = new List<string>();
            List<string> noteOctave = new List<string>();
            List<string> noteDuration = new List<string>();
            List<string> noteVoice = new List<string>();
            List<string> noteType = new List<string>();
            List<string> noteFret = new List<string>();
            List<string> noteString = new List<string>();
            // .. and the dictionary sort all data
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

            // Now we put data in their respective list
            noteStep = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/pitch/step");
            noteOctave = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/pitch/octave");
            noteDuration = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/duration");
            noteVoice = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/voice");
            noteType = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/type");
            noteFret = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/notations/technical/fret");
            noteString = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/notations/technical/string");

            // And we sort this data in a dictionary for future analysis! (if list is not empty)
            if (noteStep != null) dictionary.Add("step", noteStep);
            if (noteOctave != null) dictionary.Add("octave", noteOctave);
            if (noteDuration != null) dictionary.Add("duration", noteDuration);
            if (noteVoice != null) dictionary.Add("voice", noteVoice);
            if (noteType != null) dictionary.Add("type", noteType);
            if (noteFret != null) dictionary.Add("fret", noteFret);
            if (noteString != null) dictionary.Add("string", noteString);

            return dictionary;
        }

        #endregion

        #region TakeData

        /// <summary>
        /// Analyse XML file and take data with XPath class
        /// </summary>
        /// <param name="path">(string) Filename of the XML file</param>
        /// <param name="xNodeName">(string) XPath syntaxe (example : http://msdn.microsoft.com/fr-fr/library/ms256086%28v=VS.80%29.aspx) </param>
        /// <returns>(list) Return a list of string from data selected by xNodeName</returns>
        static List<string> TakeData(string path, string xNodeName)
        {
            List<string> myList = new List<string>();
            try
            {
                XPathDocument xPathDoc = new XPathDocument(path);
                XPathNavigator xPathNav = xPathDoc.CreateNavigator();
                XPathExpression xExp;
                xExp = xPathNav.Compile(xNodeName);
                XPathNodeIterator iterator = xPathNav.Select(xExp);

                try
                {
                    while (iterator.MoveNext())
                    {
                        XPathNavigator nav = iterator.Current.Clone();
                        myList.Add(nav.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : {0}.", ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : {0}.", ex.Message);
            }
            return myList;
        }

        #endregion

        #region TakeData (for count data)

        /// <summary>
        /// Analyse XML file and take data with XPath class
        /// </summary>
        /// <param name="path">(string) Filename of the XML file</param>
        /// <param name="xNodeName">(string) XPath syntaxe (example : http://msdn.microsoft.com/fr-fr/library/ms256086%28v=VS.80%29.aspx) </param>
        /// <param name="count">(bool) TRUE if we want use this function like the other</param>
        /// <returns>(int) Return a int of string from data selected by xNodeName</returns>
        static int TakeData(string path, string xNodeName, bool count)
        {
            int var = 0;
            try
            {
                XPathDocument xPathDoc = new XPathDocument(path);
                XPathNavigator xPathNav = xPathDoc.CreateNavigator();
                XPathExpression xExp;
                xExp = xPathNav.Compile(xNodeName);
                XPathNodeIterator iterator = xPathNav.Select(xExp);

                try
                {
                    while (iterator.MoveNext())
                    {
                        XPathNavigator nav = iterator.Current.Clone();
                        var++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : {0}.", ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : {0}.", ex.Message);
            }
            return var;
        }

        #endregion

        #region GetMusicDuration

        /// <summary>
        /// Return the duration from the music file was selected
        /// </summary>
        /// <param name="song">Song object from song's filename</param>
        /// <returns>Timespan of music's duration</returns>
        public static TimeSpan GetMusicDuration(Song song)
        {
            return song.Duration;
        }

        #endregion
    }
}
