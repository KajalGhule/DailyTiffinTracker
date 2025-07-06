namespace TiffinTracker.Models
{
    public class ProviderDashboardViewModel
    {
        public int TotalStudentsAssigned { get; set; }
        public int MealsGivenToday { get; set; }
        public int PendingMealsToday { get; set; }
        public List<TodayMealDto> TodaysMeals { get; set; }
    }
}
