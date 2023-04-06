namespace AVE_automower.model
{
    /// <summary>
    /// table of correspondence between orientation/rotation pairs and final orientation.
    /// </summary>
    internal class OrientationMap
    {
        /// <summary>
        /// initial orientation (N,S,E or W)
        /// </summary>
        public string Orientationinitial = string.Empty;
        /// <summary>
        ///  direction of rotation ( R or L)
        /// </summary>
        public string Rotation = string.Empty;
        /// <summary>
        /// final orientation  (N,S,E or W) that depends on the two preceding attributes.
        /// </summary>
        public string OrientationFinal = string.Empty;

        /// <summary>
        /// Initialise an orientation map  white the three attribute 
        /// </summary>
        public OrientationMap(string orientationinitial, string rotation, string orientationFinal)
        {
            OrientationFinal = orientationFinal;
            Orientationinitial = orientationinitial;
            Rotation = rotation;
        }
    }
}
