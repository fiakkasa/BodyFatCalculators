using BodyFatCalculators.Enums;
using System;

namespace BodyFatCalculators.Models
{
    public class USNavyModel
    {
        public GenderType Gender { get; set; }

        public (int min, int max) HeightCmMinMax => (min: 50, max: 300);

        public double HeightCm { get; set; }

        public bool HeightCmValid => HeightCm >= HeightCmMinMax.min && HeightCm <= HeightCmMinMax.max;

        public (int min, int max) NavalCmMinMax => (min: 50, max: 300);

        public double NavalCm { get; set; }

        public bool NavalCmValid => NavalCm >= NavalCmMinMax.min && NavalCm <= NavalCmMinMax.max;

        public (int min, int max) NeckMinMax => (min: 10, max: 100);

        public double NeckCm { get; set; }

        public bool NeckCmValid => NeckCm >= NeckMinMax.min && NeckCm <= NeckMinMax.max;

        public (int min, int max) HipsCmMinMax => (min: 50, max: 300);

        public double HipsCm { get; set; }

        public bool HipsCmValid => HipsCm >= HipsCmMinMax.min && NavalCm <= HipsCmMinMax.max;

        public double FatPercentage
        {
            get
            {
                var result =
                    Math.Round(
                        Gender switch
                        {
                            GenderType.Male when HeightCmValid && NavalCmValid && NeckCmValid =>
                            (
                                495 /
                                (
                                    1.0324
                                    - 0.19077 * Math.Log10(NavalCm - NeckCm)
                                    + 0.15456 * Math.Log10(HeightCm)
                                )
                                - 450
                            ),
                            GenderType.Female when HeightCmValid && NavalCmValid && NeckCmValid && HipsCmValid =>
                            (
                                495 /
                                (
                                    1.29579
                                    - 0.35004 * Math.Log10(NavalCm + HipsCm - NeckCm)
                                    + 0.221 * Math.Log10(HeightCm)
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
