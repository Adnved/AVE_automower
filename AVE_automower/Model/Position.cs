namespace AVE_automower.model
{
    public class Position
    {
        /// <summary>
        /// coordinate information 
        /// </summary>
        public Coordinate CoordinateInfo { get; set; }

        /// <summary>
        /// orientation (N,S,E or W)
        /// </summary>
        public string Orientation { get; set; }

        /// <summary>
        /// initialise a position 
        /// </summary>
        /// <param name="x"> CoordinateInfo axis </param>
        /// <param name="y"> CoordinateInfo ordinate </param>
        /// <param name="orientation"> actual orientation</param>
        public Position(int x, int y, string orientation)
        {
            CoordinateInfo = new Coordinate(x, y);
            Orientation = orientation;
        }

    }
}
