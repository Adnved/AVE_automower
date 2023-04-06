namespace AVE_automower.model
{
    public class Coordinate
    {
        /// <summary>
        /// Axis
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// ordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// intialize coordinate with an axis and an ordinate value
        /// </summary>
        /// <param name="ParamX">postion on the axis </param>
        /// <param name="ParamY">postion on the ordinate</param>
        public Coordinate(int ParamX, int ParamY)
        {
            X = ParamX;
            Y = ParamY;
        }
    }
}
