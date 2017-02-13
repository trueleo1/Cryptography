using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CryptographyCracking_From_Scratch_
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please Press any key and Select a Valid Text File");
                Console.ReadKey();
                Console.WriteLine();
                string filepath = "";
                string[] file;
                OpenFileDialog FileExplorer = new OpenFileDialog();
                FileExplorer.Filter = "Text files (*.txt)|*.txt|All files (*.*) | *.*";
                FileExplorer.FilterIndex = 2;

                try
                {
                    if (FileExplorer.ShowDialog() == DialogResult.OK)
                    {
                        filepath = FileExplorer.FileName;

                        file = File.ReadAllLines(filepath); //Puts all lines of the text file into an array, change if necessary for testing purposes;
                        string cryptedstring = ParseTestFile(file);
                        //change the method used here
                        DCOffset(cryptedstring);
                    }
                }
                catch (Exception e)
                {
                    //Console.Write("Error has occured: {0}", e);
                    throw new Exception(e.Message);
                }
            }
        }//main method
        public static string ParseTestFile(string[] s)
        {
            string tempstring = "";

            for (int i = 0; i < s.Length; i++)
            {
                tempstring += s[i];
            }
            tempstring = tempstring.Replace(" ", "");


            return tempstring;
        }
        public static string DCOffset(string s)
        {
            string tempstring = "";
            s = s.ToLower();

            for (int i = 0; i < 25; i++)
            {
                tempstring = "";
                for (int j = 0; j < s.Length; j++)
                {
                    int ascii = 0;
                    ascii = s[j];
                    ascii = ascii + i;

                    if (ascii > 122)
                        ascii = ascii - 26;

                    tempstring += (char)ascii;

                }
                Console.WriteLine("is your text a valid text? If so, press y, otherwise press any key to try again" + "Key is {0}", i);
                Console.WriteLine(tempstring);
                char readkey = Char.ToLower(Console.ReadKey().KeyChar);
                if (readkey == 'y')
                {
                    System.IO.File.WriteAllText(@"C:\Users\Leo\Documents\Visual Studio 2015\Projects\ConsoleApplication1\Results\Decrypted.txt", tempstring);
                    break;
                }

            }


            return tempstring;

        }
        public static string DCTranspose(string s) //with no padding 
        {
            int length = s.Length;


            for (int j = 0; j < s.Length; j++)
            {
                {
                    int counter = 0;
                    int rows = length / s.Length; // determining rows, the factor is the column
                    char[,] grid = new char[rows, s.Length];

                    for (int x = 0; x < rows; x++)
                    {
                        for (int y = 0; y < s.Length; y++)
                        {
                            grid[x, y] = s[counter];
                            counter++;
                        }
                    }
                    string tempstring = "";
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        for (int k = 0; k < grid.GetLength(0); k++)
                        {
                            tempstring += grid[j, i];
                        }

                    }
                    Console.WriteLine("is this the correct text configuration?");
                    Console.WriteLine(tempstring);
                    char readkey = Char.ToLower(Console.ReadKey().KeyChar);
                    if (readkey == 'y')
                    {
                        System.IO.File.WriteAllText(@"C:\Users\Leo\Documents\Visual Studio 2015\Projects\ConsoleApplication1\Results\Decrypted.txt", tempstring);
                    }
                }
            }


            return s;
        }
    }
}
