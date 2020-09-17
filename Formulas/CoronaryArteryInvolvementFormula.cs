using System;

namespace DICOMReporting.Formulas {
	public class CoronaryArteryInvolvementFormula : IFormula {
		private Constants constants;

		public double GetZScore(double observed_y) {
			double mean_y = (constants.Multiplier * Math.Pow(constants.BSA, constants.Power)) + constants.Intercept;
			return ((observed_y / 10) - mean_y) / Math.Sqrt(constants.SD);
		}
		private CoronaryArteryInvolvementFormula(Constants constants) {
			this.constants = constants;
		}

		public static CoronaryArteryInvolvementFormula LeftMainCoronary(double BSA) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.31747, 0.36008, -0.02887, 0.03040 + (0.01514 * BSA), BSA));
		}
		public static CoronaryArteryInvolvementFormula LeftAnteriorDescending(double BSA) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.26108, 0.37893, -0.02852, 0.01465 + (0.01996 * BSA), BSA));
		}
		public static CoronaryArteryInvolvementFormula RightCoronaryArtery(double BSA) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.26117, 0.3992, -0.02756, 0.02407 + (0.01597 * BSA), BSA));
		}
		private struct Constants {
			public double Multiplier {
				get; private set;
			}
			public double Power {
				get; private set;
			}
			public double Intercept {
				get; private set;
			}
			public double SD {
				get; private set;
			}
			public double BSA {
				get; private set;
			}
			public Constants(double multiplier, double power, double intercept, double sd, double bsa) {
				Multiplier = multiplier;
				Power = power;
				Intercept = intercept;
				SD = sd;
				BSA = bsa;
			}
		}
	}
}
