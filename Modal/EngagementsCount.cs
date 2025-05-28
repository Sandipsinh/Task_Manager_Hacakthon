namespace Task_Manager_Hacakthon.Modal
{
    public class EngagementsCount
    {
        public int tax_year { get; set; } 
        public int roll_forward_engagements { get; set; } 
        public double roll_forward_difference_percentage { get; set; } 
        public int activated_engagements { get; set; } 
        public double activated_engagements_difference_percentage { get; set; } 
        public int inactivated_engagements { get; set; }
        public double inactivated_engagements_difference_percentage { get; set; } 
    }

}