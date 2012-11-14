using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;               // We include System.Xml namespace to use XmlReader 

namespace ReadMusicXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== LECTURE D'UN FICHIER MUSIC XML =====\n\n");
            Console.ReadLine();

            Console.WriteLine("* Ouverture du fichier : musicXML.xml    (PRESS ENTER)\n");
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml");
                Console.WriteLine("\t---");
                Console.WriteLine("\t- Création de l'instance XmlDocument");
                Console.WriteLine("\t- Appel de la méthode Load() et chargement du fichier");
                Console.WriteLine("\t---");
            Console.ReadLine();

            Console.WriteLine("* Lecture du fichier : Noeuds principaux et enfants (PRESS ENTER)\n");
                foreach( XmlNode e in doc.ChildNodes )
                {
                    Console.WriteLine(" - "+e.Name);
                }
                foreach (XmlNode e in doc.DocumentElement.ChildNodes)
                {
                    Console.WriteLine(" -- "+e.Name);
                }
            Console.ReadLine();

            Console.WriteLine("* Récupération des informations du fichier : enfants \"part\" (PRESS ENTER)\n");
                foreach (XmlNode e in doc.DocumentElement.ChildNodes)
                {
                    Console.WriteLine(" -- " + e.Name+" id = "+e.Attributes);
                }
            Console.ReadLine();

            Console.WriteLine("===== FIN DU SCRIPT DE RECUPERATION DE DONNEES XML =====    (PRESS ENTER)\n");
            Console.ReadLine();
        }

        static void OpenFile()
        {

        }
    }
}
