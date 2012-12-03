using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectXmlAndMusicFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\t******************************************");
            Console.WriteLine("\t*  AudioPipe - SelectXmlAndMusixFile v1.0  *");
            Console.WriteLine("\t*                                          *");
            Console.WriteLine("\t*      Le but de cet algo est de           *");
            Console.WriteLine("\t*      récupérer les fichiers XML et       *");
            Console.WriteLine("\t*      la musique correspondante.          *");
            Console.WriteLine("\t*                                          *");
            Console.WriteLine("\t********************************************");
            Console.WriteLine("\t                 (ENTRER)                   ");

            Console.ReadLine();

            Console.WriteLine("\n\tListe des fichiers présent dans /Documents/AudioPipe");

            // AudioPipe's directory : Music and XML folder
            string rep = @"C:\Users\Lu'Juju\Documents\AudioPipe";

            Dictionary<string, List<string>> Files = new Dictionary<string, List<string>>();
            Files = GetFiles(rep);

            for (int i = 0; i < Files["Name"].Count(); i++)
            {
                Console.WriteLine("Name: {0} ({1})", Files["Name"][i], Files["Extension"][i]);
            }

            Console.WriteLine("\n\t                 (ENTRER)                   ");
            Console.ReadLine();
        }

        /// <summary>
        /// Make a list of the file who XML name is the same of Music name.
        /// </summary>
        /// <param name="rep">Filename of the directory</param>
        /// <returns>List of files name with no extension (Example: "myMusic", no "myMusic.xml" or "myMusic.mp3")</returns>
        static Dictionary<string, List<string>> GetFiles(string rep)
        {
            List<string> listName = new List<string>();
            List<string> listExtension = new List<string>();
            Dictionary<string, List<string>> myDictionary = new Dictionary<string, List<string>>();
            
            try
            {
                // We compare Music files with Xml files
                string[] MusicFiles = Directory.GetFiles(rep + "\\Music", "*");
                string[] XmlFiles = Directory.GetFiles(rep + "\\Xml", "*");

                foreach (string music in MusicFiles)
                {
                    foreach (string xml in XmlFiles)
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(music) == System.IO.Path.GetFileNameWithoutExtension(xml))
                        {
                            listName.Add(System.IO.Path.GetFileNameWithoutExtension(xml));
                            listExtension.Add(System.IO.Path.GetExtension(music));
                        }
                    }
                }
                myDictionary.Add("Name", listName);
                myDictionary.Add("Extension", listExtension);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : {0}", e.Message);
            }

            return myDictionary;
        }
    }
}
