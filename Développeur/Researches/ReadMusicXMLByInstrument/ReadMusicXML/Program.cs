using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml; 

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
            XmlDocument doc = new XmlDocument();
            string instrument = OpenXmlDocument(@"C:\Users\Lu'Juju\Documents\AudioPipe\XML\Dragonforce-My_Spirit_Will_Go_On.xml", doc);

            Console.WriteLine("\n\t====== Parsing selon l'instrument ======");
            TakeData("p" + instrument, doc);
            Console.WriteLine("\t             (ENTRER)              ");
            Console.ReadLine();

            Console.WriteLine("\t====== Done ======");
            Console.ReadLine();
        }

        /// <summary>
        /// Open a MusicXml document and return their instruments
        /// </summary>
        /// <param name="destination">URI file</param>
        /// <returns>Number of select instrument</returns>
        static string OpenXmlDocument( string destination, XmlDocument doc )
        {
            doc.Load(@destination);

            foreach (XmlNode node1 in doc.DocumentElement.ChildNodes)
            {
                // We select instruments
                int instrumentNumber = 0;
                if( node1.Name == "part-list" )
                {
                    foreach (XmlNode node2 in node1.ChildNodes)
                    {
                        foreach (XmlNode node3 in node2.ChildNodes)
                        {
                            if (node3.Name == "part-name")
                            {
                                foreach (XmlNode node4 in node3.ChildNodes)
                                {
                                    Console.WriteLine("\tInstrument numéro "+instrumentNumber+" : "+node4.Value);
                                }
                                instrumentNumber++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            Console.WriteLine("\n\tQuel instrument (numéro) ?");
            string choice = Console.ReadLine();
            return choice;
        }

        /// <summary>
        /// Take the data from instrument who was select                                                        // TO DO : return array of data
        /// </summary>
        /// <param name="instrument">Number of the instrument</param>
        /// <param name="doc">Xml document load</param>
        static void TakeData(string instrument, XmlDocument doc)
        {
            //SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            foreach (XmlNode node1 in doc.DocumentElement.ChildNodes)
            {
                int j = 0;
                int i = 0;
                int x = 0;

                if (node1.Name == "part" && node1.Attributes["id"].Value == instrument)
                {
                    Console.WriteLine("  <" + node1.Name + " id=" + node1.Attributes["id"].Value + ">");
                    foreach (XmlNode node2 in node1.ChildNodes)
                    {
                        Console.WriteLine("    <" + node2.Name + " number=" + node2.Attributes["number"].Value + ">");
                        Data n1 = new Data(node1.Name, node2.Name);
                        foreach (XmlNode node3 in node2.ChildNodes)
                        {
                            Console.WriteLine("      <" + node3.Name + ">");
                            foreach (XmlNode node4 in node3.ChildNodes)
                            {
                                Console.WriteLine("        <" + node4.Name + ">");
                                foreach (XmlNode node5 in node4.ChildNodes)
                                {
                                    if (node5.Value != null)
                                    {
                                        //dictionary.Add(node2.Name + node2.Attributes["number"].Value + ";" + node3.Name+x + ";" + node4.Name, node5.Value);
                                        Console.WriteLine("          " + node5.Value);
                                        x++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("          <" + node5.Name + ">");
                                        foreach (XmlNode node6 in node5.ChildNodes)
                                        {
                                            if (node6.Value != null)
                                            {
                                                //dictionary.Add(node2.Name + node2.Attributes["number"].Value + ";" + node3.Name+x + ";" + node4.Name + i + ";" + node5.Name, node6.Value);
                                                Console.WriteLine("            " + node6.Value);
                                                i++;
                                            }
                                            else
                                            {
                                                Console.WriteLine("            <" + node6.Name + ">");
                                                foreach (XmlNode node7 in node6.ChildNodes)
                                                {
                                                    //dictionary.Add(node2.Name + node2.Attributes["number"].Value + ";" + node3.Name+x + ";" + node4.Name+i + ";" + node5.Name+j + ";" + node6.Name + j, node7.Value);
                                                    Console.WriteLine("              " + node7.Value);
                                                    j++;
                                                }
                                                Console.WriteLine("            </" + node6.Name + ">");
                                            }
                                        }
                                        Console.WriteLine("          </" + node5.Name + ">");
                                    }
                                }
                                Console.WriteLine("        </" + node4.Name + ">");
                            }
                            Console.WriteLine("        </" + node3.Name + ">");
                        }
                        Console.WriteLine("    </" + node2.Name + " number=" + node2.Attributes["number"].Value + ">");
                        break;
                    }
                    Console.WriteLine("\n  </" + node1.Name + " id=" + node1.Attributes["id"].Value + ">");
                }
            }
        }
    }   
}