namespace Project_CCSB.Models
{
    public class AppointmentVehicleUser
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Vehicle Vehicle { get; set; }
        public Appointment Appointment { get; set; }
    }
}
