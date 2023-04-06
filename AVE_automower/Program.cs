using AVE_automower.model;
using System.Reflection;

namespace AVE_automower
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Please enter the file path or leave it empty to run the example:");
            string filePath = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrEmpty(filePath))
            {
                filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data\example.txt");
            }

            AutoMower? autoMower = FileCheck(filePath);

            if (autoMower == null)
            {
                Console.WriteLine("The processing has been canceled because the incoming file does not comply with the contract.");
                Console.ReadLine();

            }
            else
            {

                autoMower.Run();
                Console.ReadLine();
            }

        }

        /// <summary>
        /// check that the incoming file follows the defined structure.
        /// </summary>
        /// <param name="path"> path of the incoming instruction file.</param>
        /// <returns> a intsruction object use to execute AutoMower </returns> 
        public static AutoMower? FileCheck(string path)
        {
            try
            {
                StreamReader sr = new(path);
                int lineIndex = 1;
                List<Mower> mowers = new();
                string? line = sr.ReadLine();

                if (line == null)
                {
                    sr.Close();
                    Console.WriteLine("The instruction file is empty.");
                    return null;
                }

                if (!LawnCheck(line, out int MaxX, out int MaxY))
                {
                    sr.Close();
                    return null;
                }

                line = sr.ReadLine();
                while (line != null)
                {

                    if (lineIndex % 2 == 0)
                    {

                        if (!CheckInstruction(line))
                        {
                            sr.Close();
                            Console.WriteLine("the movement instructions must be one of the following three values: R, L, F");
                            Console.WriteLine(string.Format("Error in mower instructions, line {0}.", lineIndex));
                            return null;
                        }
                        else
                        {
                            mowers.Last().Moveset = line;
                        }
                    }
                    else
                    {
                        Mower? mowerTemp = CheckPositionStart(line, MaxX, MaxY);

                        if (mowerTemp == null)
                        {
                            sr.Close();
                            Console.WriteLine(string.Format("Error in initial positioning of mower, line {0}.", lineIndex));
                            return null;
                        }
                        else
                        {
                            mowers.Add(mowerTemp);
                        }
                    }

                    lineIndex++;
                    line = sr.ReadLine();
                }

                if (lineIndex % 2 == 0)
                {
                    sr.Close();
                    Console.WriteLine("inconsistent line count in the file.");
                    return null;
                }

                return new AutoMower(mowers, MaxX, MaxY);
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }
        }
        /// <summary>
        /// check that the instruction line conforms to the structure defined for the field."
        /// </summary>
        /// <param name="line"> the instruction line to check.</param>
        /// <param name="maxX"> return the coordinate X of upper-right corner of the field  </param> 
        /// <param name="maxY"> return the coordinate Y of upper-right corner of the field  </param> 
        /// <returns>true if the data is valid, otherwise false.</returns> 
        public static bool LawnCheck(string line, out int maxX, out int maxY)
        {

            string[] coordinate = line.Split(' ');
            maxX = 0;
            maxY = 0;

            if (coordinate.Length != 2)
            {
                Console.WriteLine("the upper - right corner of the field must have 2 coordinates , line 1");
                return false;
            }

            if (!int.TryParse(coordinate[0], out int X) || !int.TryParse(coordinate[1], out int Y))
            {
                Console.WriteLine("the coordinates must be numbers, , line 1");
                return false;
            }

            if (X < 0 || Y < 0)
            {
                Console.WriteLine("the coordinates must be numbers greater than or equal to 0 , line 1");
                return false;
            }

            maxX = X;
            maxY = Y;
            return true;
        }
        /// <summary>
        /// check that the instruction line conforms to the structure defined for the mower position start."
        /// </summary>
        /// <param name="line"> the instruction line to check.</param>
        /// <param name="maxX"> the coordinate X of upper-right corner of the field  </param> 
        /// <param name="maxY"> the coordinate Y of upper-right corner of the field  </param> 
        /// <returns>if the data is valid return a new mower object, otherwise return null.</returns> 
        public static Mower? CheckPositionStart(string line, int maxX, int maxY)
        {
            string[] coordinate = line.Split(' ');

            if (coordinate.Length != 3)
            {
                Console.WriteLine("the starting position of the mowers must have 3 arguments.");
                return null;
            }

            if (!int.TryParse(coordinate[0], out int X) || !int.TryParse(coordinate[1], out int Y))
            {
                Console.WriteLine("the coordinates must be numbers.");
                return null;
            }

            if (X < 0 || Y < 0 || X > maxX || Y > maxY)
            {
                Console.WriteLine("the position indicated by the coordinates places the mower outside of the field.");
                return null;
            }

            if (!"NWSE".Contains(coordinate[2]) || coordinate[2].ToString().Length != 1)
            {
                Console.WriteLine("the orientation must be one of the following four values: N, S, W, E.");
                return null;
            }

            return new Mower(X, Y, coordinate[2]);

        }

        /// <summary>
        /// check that the instruction line conforms to the structure defined for the moveset of a mower"
        /// </summary>
        /// <param name="line"> the instruction line to check.</param>
        /// <returns>true if the data is valid, otherwise false.</returns> 

        public static bool CheckInstruction(string line)
        {

            if (string.IsNullOrWhiteSpace(line)) return false;

            System.Text.RegularExpressions.Regex myRegex = new("^[FRL]*$");
            return myRegex.IsMatch(line.ToUpper());

        }


    }
}