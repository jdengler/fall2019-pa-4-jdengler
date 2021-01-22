using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fall2019_PA4_Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            //PA4 Starter Code
            bool persistence = true;

            while(persistence)
            {
                int menuChc = Menu();

                switch(menuChc)
                {
                    case 1: Coder("en"); break;

                    case 2: Coder("de"); break;

                    case 3: WordCount(); break;

                    case 4: return;

                }

            }
            
        }

        static int Menu()
        {
            String input;
            int choice = 0;

            while(choice < 1 || choice > 4)
            {
                Console.WriteLine("\nMENU");
                Console.WriteLine("\t(1) Encode a file");
                Console.WriteLine("\t(2) Decode a file");
                Console.WriteLine("\t(3) Word count file");
                Console.WriteLine("\t(4) Exit program");
                Console.WriteLine("\nPlease enter number to select option: ");
                input = Console.ReadLine();

                if (input.All(char.IsDigit))
                    choice = int.Parse(input);

                if (choice < 1 || choice > 4)
                    Console.WriteLine("\n\tError: Invalid input. Please enter (1), (2), (3), or (4).");
            }

            return choice;
        }

        //id "en" == encode
        //id "de" == decode
        static void Coder(String id)
        {
            Console.WriteLine("\n" + id.ToUpper() + "CODER");

            //read file
            Console.WriteLine("Enter file name to " + id + "code: ");
            String inFileName = Console.ReadLine();
            ArrayList lines = new ArrayList();

            try
            {   //opens the text file using a stream reader
                using (StreamReader sr = new StreamReader(inFileName))
                {
                    //reads the stream to a string and adds string to arraylist
                    String line = sr.ReadToEnd();
                    lines.Add(line);
                }

            }
            //file not found exception
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Returning to menu.");
                return;
            }

            Console.WriteLine("Enter file name to store " + id + "coded file: ");
            String outFileName = Console.ReadLine();
            StreamWriter sw = new StreamWriter(File.Create(outFileName));

            //splits data to code from file into each line from arraylist
            for(int i = 0; i < lines.Count; i++)
            {
                String currLine = lines[i].ToString();

                //does coding for each char sequentially
                for(int j = 0; j < currLine.Length; j++)
                {
                    //A-M or a-m
                    if((currLine[j] >= 65 && currLine[j] <= 77) || (currLine[j] >= 97 && currLine[j] <= 109))
                        sw.Write((char)(currLine[j] + 13));

                    //N-Z or n-z
                    else if ((currLine[j] >= 78 && currLine[j] <= 90) || (currLine[j] >= 110 && currLine[j] <= 122))
                        sw.Write((char)(currLine[j] - 13));

                    //any non-alpha char
                    else
                        sw.Write(currLine[j]);
                }
                sw.WriteLine();
            }
            sw.Close();
            return;
        }

        static void WordCount()
        {
            Console.WriteLine("\nWORD COUNTER");
            Console.WriteLine("Enter file name to word count: ");
            String inFileName = Console.ReadLine();
            int wc = 0;

            try
            {   //opens the text file using a stream reader
                using (StreamReader sr = new StreamReader(inFileName))
                {
                    while (sr.Peek() >= 0) 
                    {
                        String line = sr.ReadLine();
                        line.Trim();
                        
                        if(line.Contains(" "))
                        {
                            String[] words = line.Split(' ');

                            for(int i = 0; i < words.Length; i++)
                                if(words[i].Length != 0)
                                    wc++;
                        }
                        else if(line.Length != 0)
                            wc++;
                    }
                    
                    Console.WriteLine("There are " + wc + " words in the file " + inFileName);
                }
            }
            //file not found exception
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Returning to menu.");
                return;
            }
        }

    }
}
