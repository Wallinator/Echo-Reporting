using DICOMReporting.Data;
using System;

namespace DICOMReporting.Formulas {
	public class CoronaryArteryInvolvementFormula : IFormula {
		private Constants constants;

		public double GetZScore(double observed_y) {
			double mean_y = (constants.Multiplier * Math.Pow(constants.BSA, constants.Power)) + constants.Intercept;
			return ((observed_y / 10) - mean_y) / constants.SD;
		}
		public bool ZScoreable() => true;
		private CoronaryArteryInvolvementFormula(Constants constants) {
			this.constants = constants;
		}

		public static CoronaryArteryInvolvementFormula LeftMainCoronary(PatientData pd) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.31747, 0.36008, -0.02887, 0.03040 + (0.01514 * pd.BSA), pd));
		}
		public static CoronaryArteryInvolvementFormula LeftAnteriorDescending(PatientData pd) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.26108, 0.37893, -0.02852, 0.01465 + (0.01996 * pd.BSA), pd));
		}
		public static CoronaryArteryInvolvementFormula RightCoronaryArtery(PatientData pd) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.26117, 0.3992, -0.02756, 0.02407 + (0.01597 * pd.BSA), pd));
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
			public double BSA => Pd.BSA;
			private PatientData Pd;
			public Constants(double multiplier, double power, double intercept, double sd, PatientData pd) {
				Multiplier = multiplier;
				Power = power;
				Intercept = intercept;
				SD = sd;
				Pd = pd;
			}
		}
	}
}
