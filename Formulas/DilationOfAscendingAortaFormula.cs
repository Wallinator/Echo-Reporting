using System;

namespace DICOMReporting.Formulas {
	public class DilationOfAscendingAortaFormula : IFormula {
		private Constants constants;


		public double GetZScore(double observed_y) {
			double mean_y = (constants.Multiplier * Math.Log(constants.BSA)) + constants.Intercept;
			return (Math.Log(observed_y) - mean_y) / constants.MSE;
		}
		public bool ZScoreable() => true;
		public static DilationOfAscendingAortaFormula AscendingAorta(double BSA) {
			return new DilationOfAscendingAortaFormula(new Constants(0.421, 2.898, 0.09111, BSA));
		}/*
			public static Constants AorticAnnulus(double BSA) {
				return new Constants(0.426, 2.732, 0.10392, BSA);
			}
			public static Constants AorticSinuses(double BSA) {
				return new Constants(0.443, 3.021, 0.10173, BSA);
			}
			public static Constants STJunction(double BSA) {
				return new Constants(0.434, 2.819, 0.10961, BSA);
			}*/
		private DilationOfAscendingAortaFormula(Constants constants) {
			this.constants = constants;
		}
		private struct Constants {
			public double Multiplier {
				get; private set;
			}
			public double Intercept {
				get; private set;
			}
			public double MSE {
				get; private set;
			}
			public double BSA {
				get; private set;
			}
			public Constants(double multiplier, double intercept, double mse, double bsa) {
				Multiplier = multiplier;
				Intercept = intercept;
				MSE = mse;
				BSA = bsa;
			}
		}
	}
}
