using BodyFatCalculators.Models;
using System.Collections.Generic;

namespace BodyFatCalculators.Services
{
    public class DataService
    {
        public bool Male { get; set; } = true;

        public USNavyModel USNavy { get; set; } = new USNavyModel { Male = true };

        public JacksonAndPollock3SiteSkinFoldModel JacksonAndPollock3SiteSkinFold { get; set; } = new JacksonAndPollock3SiteSkinFoldModel { Male = true };

        public Dictionary<string, (double maleMin, double maleMax, double femaleMin, double femaleMax)> FatPercentageTable { get; } =
           new Dictionary<string, (double maleMin, double maleMax, double femaleMin, double femaleMax)>
           {
               ["Essential Fat"] = (maleMin: 2, maleMax: 5, femaleMin: 10, femaleMax: 13),
               ["Athlete"] = (maleMin: 6, maleMax: 13, femaleMin: 14, femaleMax: 20),
               ["Fitness"] = (maleMin: 14, maleMax: 17, femaleMin: 21, femaleMax: 24),
               ["Acceptable"] = (maleMin: 18, maleMax: 24, femaleMin: 25, femaleMax: 31),
               ["Obese"] = (maleMin: 25, maleMax: 100, femaleMin: 32, femaleMax: 100),
           };
    }
}
