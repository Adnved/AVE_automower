namespace AVE_automower.model
{
    public class Mower
    {   /// <summary>
        /// coordinate and orientation of the mower
        /// </summary>
        public Position PositionInfo { get; set; }

        /// <summary>
        /// list of movement instructions
        /// </summary>
        public string Moveset { get; set; }


        /// <summary>
        /// intialise a mower with a position and no moveset
        /// </summary>
        /// <param name="x"> mower axis </param>
        /// <param name="y"> mower ordinate </param>
        /// <param name="orientation"> mower orientation </param>
        public Mower(int x, int y, string orientation)
        {
            PositionInfo = new Position(x, y, orientation);
            Moveset = String.Empty;
        }


    }
}
