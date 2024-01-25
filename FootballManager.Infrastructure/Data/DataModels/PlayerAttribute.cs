namespace FootballManager.Infrastructure.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    public class PlayerAttribute 
    {
        public int Id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        [Range(1, 20)]
        public int Finishing { get; set; }
        [Range(1, 20)]
        public double FinishingWeight { get; set; }
        [Range(1, 20)]
        public int Passing { get; set; }
        [Range(1, 20)]
        public double PassingWeight { get; set; }
        [Range(1, 20)]
        public int Heading { get; set; }
        [Range(1, 20)]
        public double HeadingWeight { get; set; }
        [Range(1, 20)]
        public int Tackling { get; set; }
        [Range(1, 20)]
        public double TacklingWeight { get; set; }
        [Range(1, 20)]
        public int Positioning { get; set; }
        [Range(1, 20)]
        public double PositioningWeight { get; set; }
        [Range(1, 20)]
        public int Pace { get; set; }
        [Range(1, 20)]
        public double PaceWeight { get; set; }
        [Range(1, 20)]
        public int Stamina { get; set; }
        [Range(1, 20)]
        public double StaminaWeight { get; set; }
        [Range(1, 20)]
        public int Strength { get; set; }
        [Range(1, 20)]
        public double StrengthWeight { get; set; }
        [Range(1, 20)]
        public int Dribbling { get; set; }
        [Range(1, 20)]
        public double DribblingWeight { get; set; }
        [Range(1, 20)]
        public int BallControll { get; set; }
        [Range(1, 20)]
        public double BallControllWeight { get; set; }
        [Range(1, 20)]
        public int OneOnOne { get; set; }
        [Range(1, 20)]
        public double OneOnOneWeight { get; set; }
        [Range(1, 20)]
        public int Reflexes { get; set; }
        [Range(1, 20)]
        public double ReflexesWeight { get; set; }
    }
}
