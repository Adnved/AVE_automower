using AVE_automower.model;

namespace AVE_automower
{
    public class AutoMower
    {
        /// <summary>
        /// the coordinate of the top right corner of the lawn 
        /// </summary>
        public Coordinate Lawn { get; set; }
        /// <summary>
        /// list of lawnmowers placed on the lawn."
        /// </summary>
        public List<Mower> Mowers { get; set; }

        private List<OrientationMap> OrientationMaps {get;}


        /// <summary>
        /// initialize table of correspondence that gives the final orientation based on an initial orientation and a direction of rotation.
        /// </summary>
        public void SetOrientationMaps()
        {
            OrientationMaps.Add(new OrientationMap("N", "L", "W"));
            OrientationMaps.Add(new OrientationMap("N", "R", "E"));
            OrientationMaps.Add(new OrientationMap("S", "L", "E"));
            OrientationMaps.Add(new OrientationMap("S", "R", "W"));
            OrientationMaps.Add(new OrientationMap("E", "L", "N"));
            OrientationMaps.Add(new OrientationMap("E", "R", "S"));
            OrientationMaps.Add(new OrientationMap("W", "L", "S"));
            OrientationMaps.Add(new OrientationMap("W", "R", "N"));
        }


        /// <summary>
        /// initialize an instruction whit coordinate of the top right corner and a list of mower 
        /// </summary>
        /// <param name="mowers"> list of lawnmowers placed on the lawn </param>
        /// <param name="x"> axis of the top right corner of the lawn </param>
        /// <param name="y"> ordinate of the top right corner of the lawn </param>
        public AutoMower(List<Mower> mowers, int x, int y)
        {
            Mowers = mowers;
            Lawn = new Coordinate(x, y);
            OrientationMaps = new List<OrientationMap>();
           SetOrientationMaps();
        }

        /// <summary>
        /// initialize an empty instruction
        /// </summary>
        public AutoMower()
        {
          Mowers = new List<Mower>();
          Lawn = new Coordinate(0, 0);
          OrientationMaps=new List<OrientationMap>();
          SetOrientationMaps();
        }

        /// <summary>
        /// Move each mower sequentially 
        /// </summary>
        /// <param name="instruction"> the instruction object containing all information for the run of the mower. </param>
        public void Run()
        {
            if (Mowers == null)
            {
                return;
            }

             foreach (Mower item in Mowers)
            {
                foreach (char lettre in item.Moveset.ToUpper().ToCharArray())
                {
                    if (lettre == 'F')
                    {
                        Position result = Changecoordinate(item);
                        if (CheckPostion(result))
                        {
                            item.PositionInfo = result;
                        }
                    }
                    else
                    {
                        ChangeOrientation(item, lettre);

                    }
                }
                Console.WriteLine(string.Format("{0} {1} {2}", item.PositionInfo.CoordinateInfo.X, item.PositionInfo.CoordinateInfo.Y, item.PositionInfo.Orientation));
            }
        }

        /// <summary>
        /// Check if the lawnmower stays within the field and if it doesn't collide with another lawnmower
        /// </summary>
        /// <param name="nextPosition"> the coordinate of the postion where the mower try to go  </param>
        /// <param name="instruction"> the instruction object containing all information for the run of the mower . </param>
        public  bool CheckPostion(Position nextPosition)
        {
            bool yarn = nextPosition.CoordinateInfo.X >= 0 && nextPosition.CoordinateInfo.Y >= 0 && nextPosition.CoordinateInfo.X <= (Lawn.X ) && nextPosition.CoordinateInfo.Y <= (Lawn.Y );

            bool colision = !Mowers.Any(p => p.PositionInfo.CoordinateInfo.X == nextPosition.CoordinateInfo.X && p.PositionInfo.CoordinateInfo.Y == nextPosition.CoordinateInfo.Y) ;

            return (yarn && colision);

        }


        /// <summary>
        /// calcul the coordinate of the mower after a movement
        /// </summary>
        /// <param name="Mower"> the mower , who try to move </param>
        /// <returns>The position the lawnmower would occupy after this movement.</returns>
        public  Position Changecoordinate(Mower item)
        {
            Position Result = new(item.PositionInfo.CoordinateInfo.X, item.PositionInfo.CoordinateInfo.Y, item.PositionInfo.Orientation);

            switch (item?.PositionInfo.Orientation)
            {
                case "N":
                    Result.CoordinateInfo.Y++;
                    break;
                case "S":
                    Result.CoordinateInfo.Y--;
                    break;
                case "W":
                    Result.CoordinateInfo.X--;
                    break;
                case "E":
                    Result.CoordinateInfo.X++;
                    break;
            }
            return Result;
        }

        /// <summary>
        /// return the orientation based on the current orientation and a direction of rotation
        /// </summary>
        /// <param name="mower"> the lawnmower whose orientation we want to change </param>
        /// <param name="rotation"> a direction of rotation </param>
        /// <param name="orientationMaps">table of correspondence that gives the final orientation based on an initial orientation and a direction of rotation. </param>
        public  void ChangeOrientation(Mower mower, char rotation)
        {
            mower.PositionInfo.Orientation = OrientationMaps.FirstOrDefault(p => p.Orientationinitial == mower.PositionInfo.Orientation
                                                     && p.Rotation == rotation.ToString())?.OrientationFinal ?? string.Empty;
        }
    }
}
