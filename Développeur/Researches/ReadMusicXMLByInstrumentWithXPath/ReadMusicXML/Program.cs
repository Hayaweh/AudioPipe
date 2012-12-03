using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ReadMusicXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\t***********************************");
            Console.WriteLine("\t*  AudioPipe - ReadMusicXML v2.0  *");
            Console.WriteLine("\t*                                 *");
            Console.WriteLine("\t*  Le but de cet algo est de      *");
            Console.WriteLine("\t*  récupérer les données du XML   *");
            Console.WriteLine("\t*  selon l'instrument choisi.     *");
            Console.WriteLine("\t*                                 *");
            Console.WriteLine("\t***********************************");
            Console.WriteLine("\t             (ENTRER)              ");
            Console.ReadLine();

            Console.WriteLine("\t====== Ouverture du fichier ======");
                List<string> Instruments = new List<string>();
                Instruments = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part-list/score-part/part-name");

            Console.WriteLine("\n\t====== Parsing selon l'instrument ======");
            Console.WriteLine("\t             (ENTRER)              ");
                Console.WriteLine("Liste des instruments :");
                for (int i = 0; i < Instruments.Count(); i++ )
                {
                    Console.WriteLine(" {0}.{1}", i, Instruments[i]);
                }
                string choice = Console.ReadLine();

                Dictionary<string, List<string>> Measures = new Dictionary<string, List<string>>();
                Measures = InstrumentInformations(choice);

                Console.WriteLine("TEST : j'affiche les notes de la partition");

                /** SPECIAL VAR : count the note from measure node in XML file **/
                int countMeasure = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + choice + "\"]/measure", true);

                Console.WriteLine(countMeasure);

                for (int i = 0; i < --countMeasure; i++)
                {
                    if (Measures["step"].Count != 0) Console.WriteLine("step : {1}", i, Measures["step"][i]);
                    if (Measures["octave"].Count != 0) Console.WriteLine("octave : {1}", i, Measures["octave"][i]);
                    if (Measures["duration"].Count != 0) Console.WriteLine("duration : {1}", i, Measures["duration"][i]);
                    if (Measures["voice"].Count != 0) Console.WriteLine("voice : {1}", i, Measures["voice"][i]);
                    if (Measures["type"].Count != 0) Console.WriteLine("type : {1}", i, Measures["type"][i]);
                    if (Measures["fret"].Count != 0) Console.WriteLine("fret : {1}", i, Measures["fret"][i]);
                    if (Measures["string"].Count != 0) Console.WriteLine("string : {1}", i, Measures["string"][i]);
                    Console.WriteLine();
                }

            Console.WriteLine("\t====== Done ======");
            Console.ReadLine();
        }

        /// <summary>
        /// Return a list who include partition's informations from instrument selected.
        /// </summary>
        /// <param name="instrumentChoiceNumber">Instrument's number. Example: part-list id="p5" => instrument number = 5</param>
        /// <returns>Dictionary for all data included in note node (from measure node)</returns>
        static Dictionary<string, List<string>> InstrumentInformations(string instrumentChoiceNumber)
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
            noteStep = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p"+instrumentChoiceNumber+"\"]/measure/note/pitch/step");
            noteOctave = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/pitch/octave");
            noteDuration = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/duration");
            noteVoice = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/voice");
            noteType = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/type");
            noteFret = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/notations/technical/fret");
            noteString = TakeData(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", "/score-partwise/part[@id=\"p" + instrumentChoiceNumber + "\"]/measure/note/notations/technical/string");

            // And we sort this data in a dictionary for future analysis! (if list is not empty)
            if( noteStep != null ) dictionary.Add("step", noteStep);
            if (noteOctave != null) dictionary.Add("octave", noteOctave);
            if (noteDuration != null) dictionary.Add("duration", noteDuration);
            if (noteVoice != null) dictionary.Add("voice", noteVoice);
            if (noteType != null) dictionary.Add("type", noteType);
            if (noteFret != null) dictionary.Add("fret", noteFret);
            if (noteString != null) dictionary.Add("string", noteString);

            return dictionary;
        }

        /// <summary>
        /// Analyse XML file and take data with XPath class
        /// </summary>
        /// <param name="filename">(string) Filename of the XML file</param>
        /// <param name="xNodeName">(string) XPath syntaxe (example : http://msdn.microsoft.com/fr-fr/library/ms256086%28v=VS.80%29.aspx) </param>
        /// <returns>(list) Return a list of string from data selected by xNodeName</returns>
        static List<string> TakeData(string filename, string xNodeName)
        {
            List<string> myList = new List<string>();
            try
            {
                XPathDocument xPathDoc = new XPathDocument(filename);
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

        /// <summary>
        /// Analyse XML file and take data with XPath class
        /// </summary>
        /// <param name="filename">(string) Filename of the XML file</param>
        /// <param name="xNodeName">(string) XPath syntaxe (example : http://msdn.microsoft.com/fr-fr/library/ms256086%28v=VS.80%29.aspx) </param>
        /// <param name="count">(bool) TRUE if we want use this function like the other</param>
        /// <returns>(int) Return a int of string from data selected by xNodeName</returns>
        static int TakeData(string filename, string xNodeName, bool count)
        {
            int var = 0;
            try
            {
                XPathDocument xPathDoc = new XPathDocument(filename);
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
    }   
}