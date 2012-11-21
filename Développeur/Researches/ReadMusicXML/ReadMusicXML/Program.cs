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
                int countParentNode = 0;
                int countChildrenNode = 0;
                foreach (XmlNode node1 in doc.DocumentElement.ChildNodes)
                {
                    if (node1.Name != "work" && node1.Name != "movement-title" && node1.Name != "identification" && node1.Name != "part-list")
                    {
                        Console.WriteLine("  <" + node1.Name + " id=" + node1.Attributes["id"].Value + ">");
                        foreach (XmlNode node2 in node1.ChildNodes)
                        {
                            Console.WriteLine("    <" + node2.Name + " number=" + node2.Attributes["number"] .Value +  ">");
                            countChildrenNode++;
                            foreach (XmlNode node3 in node2.ChildNodes)
                            {
                                Console.WriteLine("      <" + node3.Name + ">");
                                foreach (XmlNode node4 in node3.ChildNodes)
                                {
                                    Console.WriteLine("        <" + node4.Name + ">");
                                    foreach (XmlNode node5 in node4.ChildNodes)
                                    {
                                        if (node5.Value != null)
                                            Console.WriteLine("          " + node5.Value);
                                        else
                                        {
                                            Console.WriteLine("          <" + node5.Name + ">");
                                            foreach (XmlNode node6 in node5.ChildNodes)
                                            {
                                                if (node6.Value != null)
                                                    Console.WriteLine("            " + node6.Value);
                                                else
                                                {
                                                    Console.WriteLine("            <" + node6.Name + ">");
                                                    foreach (XmlNode node7 in node6.ChildNodes)
                                                    {
                                                        Console.WriteLine("              " + node7.Value);
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
                            Console.WriteLine("    </" + node2.Name + " number=" + node2.Attributes["number"] .Value +  ">");
                        }
                        countParentNode++;
                        Console.WriteLine("\n  </" + node1.Name + " id=" + node1.Attributes["id"].Value + ">");
                    }
                }
                Console.WriteLine("\nNombre de noeud parents lu : " + countParentNode);
                Console.WriteLine("Nombre de noeud enfants lu : " + countChildrenNode);
                Console.WriteLine("Nombre de noeud enfants lu par noeud parents : " + countChildrenNode / countParentNode);
            Console.ReadLine();

          /*  Console.WriteLine("* Enregistrement des données dans des tableaux pour transfert de données (PRESS ENTER)\n");
                Dictionary<string, XmlNode> MusicXml = new Dictionary<string, XmlNode>();
                Dictionary<string, XmlNode> MusicXmlNode1 = new Dictionary<string, XmlNode>();

                foreach (XmlNode node1 in doc.DocumentElement.ChildNodes)
                {
                    if (node1.Name != "work" && node1.Name != "movement-title" && node1.Name != "identification" && node1.Name != "part-list")
                    {
                        foreach (XmlNode node2 in node1.ChildNodes)
                        {
                            foreach (XmlNode node3 in node2.ChildNodes)
                            {
                                foreach (XmlNode node4 in node3.ChildNodes)
                                {
                                    foreach (XmlNode node5 in node4.ChildNodes)
                                    {
                                        if (node5.Value == null)
                                        {
                                            foreach (XmlNode node6 in node5.ChildNodes)
                                            {
                                                if (node6.Value == null)
                                                {
                                                    foreach (XmlNode node7 in node6.ChildNodes)
                                                    {
                                                        Console.WriteLine("Node : " + node6.Name);
                                                        Console.WriteLine("Value : " + node7.Value);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    MusicXmlNode1.Add(node1.Name, null);
                }
                MusicXml.Add("part", MusicXmlNode1);
           * */

                Console.WriteLine(" -- Done");
            Console.ReadLine();

            Console.WriteLine("===== FIN DU SCRIPT DE RECUPERATION DE DONNEES XML =====    (PRESS ENTER)\n");
            Console.ReadLine();
        }
    }
}
