using BodyFatCalculators.Enums;
using System;

namespace BodyFatCalculators.Models
{
    public class JacksonAndPollock3SiteSkinFoldModel
    {
        public GenderType Gender { get; set; }

        public static (int min, int max) AgeMinMax => (min: 10, max: 70);

        public double Age { get; set; }

        public bool AgeValid => Age >= AgeMinMax.min && Age <= AgeMinMax.max;

        public static (int min, int max) ChestMmMinMax => (min: 1, max: 50);

        public double ChestMm { get; set; }

        public bool ChestMmValid => ChestMm >= ChestMmMinMax.min && ChestMm <= ChestMmMinMax.max;

        public static (int min, int max) AbdomenMmMinMax => (min: 1, max: 50);

        public double AbdomenMm { get; set; }

        public bool AbdomenMmValid => AbdomenMm >= AbdomenMmMinMax.min && AbdomenMm <= AbdomenMmMinMax.max;

        public static (int min, int max) HipMmMinMax => (min: 1, max: 50);

        public double HipMm { get; set; }

        public bool HipMmValid => HipMm >= HipMmMinMax.min && HipMm <= HipMmMinMax.max;

        public static (int min, int max) TricepMmMinMax => (min: 1, max: 50);

        public double TricepMm { get; set; }

        public bool TricepMmValid => TricepMm >= TricepMmMinMax.min && TricepMm <= TricepMmMinMax.max;

        public static (int min, int max) ThighMmMinMax => (min: 1, max: 50);

        public double ThighMm { get; set; }

        public bool ThighMmValid => ThighMm >= ThighMmMinMax.min && ChestMm <= ThighMmMinMax.max;

        public double FatPercentage
        {
            get
            {
                if (!AgeValid) return 0;

                var result =
                    Math.Round(
                        Gender switch
                        {
                            GenderType.Male when ChestMmValid && AbdomenMmValid && ThighMmValid =>
                            (
                                (
                                    495 /
                                    (
                                        1.10938
                                        - (0.0008267 * (ChestMm + AbdomenMm + ThighMm))
                                        + (0.0000016 * Math.Pow(ChestMm + AbdomenMm + ThighMm, 2))
                                        - (0.0002574 * Age)
                                    )
                                )
                                - 450
                            ),
                            GenderType.Female when HipMmValid && TricepMmValid && ThighMmValid =>
                            (
                                (
                                    495 /
                                    (
                                       1.089733
                                        - (0.0009245 * (HipMm + TricepMm + ThighMm))
                                        + (0.0000025 * Math.Pow(HipMm + TricepMm + ThighMm, 2))
                                        - (0.0000979 * Age)
                                    )
                                )
                                - 450
                            ),
                            _ => 0d
                        } / 100,
                        4
                    );

                return result switch
                {
                    _ when result > 0 && result < 1 => result,
                    _ when result >= 1 => 1,
                    _ when result <= 0 => 0,
                    _ => 0
                };
            }
        }
    }
}