namespace CarDiary.Web.ViewModels.Administration.Dashboard
{
    public class IndexModel
    {
		public IndexModel(int usersCount, int carsCount, int repairsCount, int refuelsCount, int tripsCount)
		{
			UsersCount = usersCount;
			CarsCount = carsCount;
			RepairsCount = repairsCount;
			RefuelsCount = refuelsCount;
			TripsCount = tripsCount;
		}

		public int UsersCount { get; set; }
		public int CarsCount { get; set; }
		public int RepairsCount { get; set; }
		public int RefuelsCount { get; set; }
		public int TripsCount { get; set; }
	}
}
